using System;
using System.ComponentModel;
using System.Diagnostics;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Messaging.RenderMessages;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;
using User32 = TerraFX.Interop.User32;
using Windows = BouncyBox.VorpalEngine.Engine.Interop.Windows;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>
    ///     Manages core DirectX resource lifetime, including initialization, release, and resizing.
    /// </summary>
    internal class DirectXResourceManager : IDisposable
    {
        private const DXGI_FORMAT DxgiFormat = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
        private readonly NestedContext _context;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly IInterfaces _interfaces;
        private readonly MessagePublisherSubscriber<IRenderMessage> _renderMessagePublisherSubscriber;
        private readonly ContextSerilogLogger _serilogLogger;
        private readonly IntPtr _windowHandle;
        private D2D_SIZE_U? _clientSize;
        private D2D1Device? _d2d1Device;
        private D2D1DeviceContext? _d2d1DeviceContext;
        private D3D11Device? _d3d11Device;
        private DWriteFactory1? _dWriteFactory1;
        private DXGIAdapter? _dxgiAdapter;
        private DXGIDebug1? _dxgiDebug1;
        private DXGISwapChain1? _dxgiSwapChain1;
        private bool _isDisposed;
        private bool _isInitialized;
        private TimeSpan? _refreshPeriod;

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="DirectXResourceManager" /> type.</para>
        ///     <para>Subscribes to the <see cref="DisplayChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResolutionChangedMessage" /> global message.</para>
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="windowHandle">The window handle of the render window.</param>
        /// <param name="context">A nested context.</param>
        public DirectXResourceManager(IInterfaces interfaces, IntPtr windowHandle, NestedContext context)
        {
            _context = context.CopyAndPush(nameof(DirectXResourceManager));
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, _context);
            _interfaces = interfaces;
            _windowHandle = windowHandle;
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, _context)
                    .Subscribe<DisplayChangedMessage>(HandleDisplayChangedMessage)
                    .Subscribe<ResolutionChangedMessage>(HandleResolutionChangedMessage);
            _renderMessagePublisherSubscriber = MessagePublisherSubscriber<IRenderMessage>.Create(interfaces.RenderMessageQueue, _context);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectXResourceManager" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="windowHandle">The window handle of the render window.</param>
        public DirectXResourceManager(IInterfaces interfaces, IntPtr windowHandle)
            : this(interfaces, windowHandle, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _globalMessagePublisherSubscriber.Dispose();
                    _renderMessagePublisherSubscriber.Dispose();

                    ReleaseResources();
                },
                ref _isDisposed,
                _interfaces.ThreadManager,
                ProcessThread.Render);
        }

        /// <summary>
        ///     Request a render. The request may result in resources being initialized and released.
        /// </summary>
        /// <param name="drawDelegate">A delegate to invoke that will perform the actual rendering once all core resources are initialized.</param>
        /// <returns>Returns a tuple containing a <see cref="RenderResult" /> and a frametime.</returns>
        /// <exception cref="DirectXException">Thrown when the query for <see cref="ID2D1Multithread" /> failed.</exception>
        public (RenderResult result, TimeSpan? frametime) Render(Func<DirectXResources, (int endDrawResult, long? startTimestamp)> drawDelegate)
        {
            if (!_isInitialized)
            {
                InitializeResources();
            }

            Debug.Assert(_clientSize != null);

            (int endDrawResult, long? startTimestamp) =
                drawDelegate.Invoke(new DirectXResources(_dxgiAdapter!, _d2d1DeviceContext!, _dWriteFactory1!, _clientSize.Value));

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (endDrawResult)
            {
                case Windows.S_OK when startTimestamp == null:
                    return (RenderResult.FrameSkipped, null);
                case TerraFX.Interop.Windows.D2DERR_RECREATE_TARGET:
                    ReleaseResources();

                    return (RenderResult.Reinitialized, null);
            }

            if (TerraFX.Interop.Windows.FAILED(endDrawResult))
            {
                throw new DirectXException("Failed to end drawing.", new Win32Exception(endDrawResult));
            }

            using D2D1Factory d2d1Factory = _d2d1Device!.GetFactory();
            using D2D1Multithread d2d1Multithread = d2d1Factory.QueryD2D1Multithread()!;

            try
            {
                // Ensure underlying DXGI and Direct3D resources are safe during presentation
                d2d1Multithread.Enter();

                try
                {
                    _dxgiSwapChain1!.Present(_interfaces.CommonGameSettings.EnableVSync ? 1u : 0u);
                }
                finally
                {
                    // Release the lock
                    d2d1Multithread.Leave();
                }
            }
            finally
            {
                d2d1Multithread.Dispose();
            }

            Debug.Assert(startTimestamp != null);

            TimeSpan frametime = TimeSpan.FromTicks(Stopwatch.GetTimestamp() - startTimestamp.Value);

            // Handle dispatched messages
            _globalMessagePublisherSubscriber.HandleDispatched();

            return (RenderResult.FrameRendered, frametime);
        }

        /// <summary>
        ///     <para>
        ///         Initializes core DirectX resources such as factories, devices, device contexts, and swap chains. Also associates those
        ///         resources with the render window's handle.
        ///     </para>
        ///     <para>Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.</para>
        ///     <para>Publishes the <see cref="DirectXResourcesInitializedMessage" /> render message.</para>
        ///     <para>
        ///         See <a href="https://katyscode.files.wordpress.com/2013/01/direct2d-initialization-comparison.png">this image</a>.
        ///     </para>
        /// </summary>
        /// <exception cref="DirectXException">Thrown when neither a hardware nor WARP <see cref="ID3D11Device" /> could be created.</exception>
        /// <!--<exception cref="DirectXException">Thrown when the query for <see cref="ID3D11Device1" /> failed.</exception>-->
        /// <exception cref="DirectXException">Thrown when the query for <see cref="IDXGIDevice" /> failed.</exception>
        /// <exception cref="DirectXException">Thrown when the <see cref="IDXGIAdapter" /> could not be retrieved.</exception>
        /// <exception cref="DirectXException">Thrown when the <see cref="IDXGIFactory2" /> could not be retrieved.</exception>
        /// <exception cref="DirectXException">Thrown when a <see cref="IDXGISwapChain1" /> could not be created.</exception>
        /// <exception cref="DirectXException">Thrown when a <see cref="ID2D1Device" /> could not be created.</exception>
        /// <exception cref="DirectXException">Thrown when a <see cref="ID2D1DeviceContext" /> could not be created.</exception>
        /// <exception cref="DirectXException">Thrown when a window association could not be made.</exception>
        /// <exception cref="DirectXException">Thrown when a <see cref="IDWriteFactory2" /> could not be created.</exception>
        /// <exception>Thrown when <see cref="TerraFX.Interop.User32.GetClientRect" /> failed.</exception>
        private unsafe void InitializeResources()
        {
#if DEBUG
            const bool debug = true;

            _serilogLogger.LogDebug($"Creating {nameof(IDXGIDebug1)}");

            _dxgiDebug1 = new DXGIDebug1();
#else
            const bool debug = false;
#endif

            // Initialize Direct3D

            try
            {
                _serilogLogger.LogDebug($"Creating hardware {nameof(ID3D11Device)}");

                _d3d11Device = new D3D11Device(D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE, new[] { D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0 }, debug);
            }
            catch (Exception exception)
            {
                _serilogLogger.LogWarning(exception, $"Failed to create hardware {nameof(ID3D11Device)}");
                _serilogLogger.LogDebug($"Creating WARP {nameof(ID3D11Device)}");

                _d3d11Device = new D3D11Device(D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_WARP, new[] { D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0 }, debug);
            }

            // Initialize DXGI

            _serilogLogger.LogDebug($"Querying {nameof(IDXGIDevice)}");

            using DXGIDevice dxgiDevice = _d3d11Device.QueryDXGIDevice()!;

            _serilogLogger.LogDebug($"Retrieving {nameof(IDXGIAdapter)}");

            _dxgiAdapter = dxgiDevice.GetAdapter();

            _serilogLogger.LogDebug($"Retrieving {nameof(IDXGIFactory2)}");

            using DXGIFactory2 dxgiFactory2 = _dxgiAdapter.GetParentDXGIFactory2()!;

            // Initialize swap chain

            _serilogLogger.LogDebug($"Creating {nameof(IDXGISwapChain1)}");

            _dxgiSwapChain1 = dxgiFactory2.CreateSwapChainForHwnd(_d3d11Device, _windowHandle, DxgiFormat);

            // Initialize Direct2D

            _serilogLogger.LogDebug($"Creating {nameof(ID2D1Device)}");

            _d2d1Device = new D2D1Device(dxgiDevice, debug);

            _serilogLogger.LogDebug($"Creating {nameof(ID2D1DeviceContext)}");

            _d2d1DeviceContext = _d2d1Device.CreateDeviceContext();

            InitializeRenderTarget();

            _serilogLogger.LogDebug("Making window association");

            dxgiFactory2.MakeWindowAssociation(_windowHandle, DXGI.DXGI_MWA_NO_ALT_ENTER);

            // Initialize DirectWrite

            _serilogLogger.LogDebug($"Creating {nameof(IDWriteFactory1)}");

            _dWriteFactory1 = new DWriteFactory1();

            // Notify subscribers that resources have been initialized

            RECT clientRect;

            if (User32.GetClientRect(_windowHandle, &clientRect) == TerraFX.Interop.Windows.FALSE)
            {
                throw Win32ExceptionHelper.GetException();
            }

            _clientSize = D2DFactory.CreateSizeU((uint)clientRect.right, (uint)clientRect.bottom);

            var directXResources = new DirectXResources(_dxgiAdapter, _d2d1DeviceContext, _dWriteFactory1, _clientSize.Value);

            _interfaces.RenderMessageQueue.Publish(new DirectXResourcesInitializedMessage(directXResources), _context);

            _isInitialized = true;

            PublishRefreshRateChangedMessage();
        }

        /// <summary>
        ///     <para>Releases core DirectX resources.</para>
        ///     <para>Publishes the <see cref="DirectXResourcesReleasedMessage" /> render message.</para>
        /// </summary>
        private void ReleaseResources()
        {
            _interfaces.RenderMessageQueue.Publish<DirectXResourcesReleasedMessage>(_context);

            _d3d11Device?.Dispose();
            _dxgiAdapter?.Dispose();
            _dxgiSwapChain1?.Dispose();
            _d2d1Device?.Dispose();
            _d2d1DeviceContext?.Dispose();
            _dWriteFactory1?.Dispose();
            _isInitialized = false;

#if DEBUG
            _dxgiDebug1?.ReportLiveObjects(DXGIDebug.DXGI_DEBUG_ALL, DXGI_DEBUG_RLO_FLAGS.DXGI_DEBUG_RLO_SUMMARY);
            _dxgiDebug1?.Dispose();
#endif
        }

        /// <summary>
        ///     Initializes the render target. This method is called when initializing resources and also in response to resolution changes.
        /// </summary>
        /// <exception cref="DirectXException">Thrown when the <see cref="IDXGISurface" /> could not be retrieved.</exception>
        /// <exception cref="DirectXException">Thrown when a <see cref="ID2D1Bitmap1" /> could not be created.</exception>
        private void InitializeRenderTarget()
        {
            _serilogLogger.LogDebug("Retrieving back buffer IDXGISurface");

            using DXGISurface dxgiSurface = _dxgiSwapChain1!.GetBuffer(0);

            _serilogLogger.LogDebug($"Creating {nameof(ID2D1Bitmap1)} target");

            var bitmapProperties =
                new D2D1_BITMAP_PROPERTIES1
                {
                    pixelFormat =
                        new D2D1_PIXEL_FORMAT
                        {
                            format = DxgiFormat,
                            alphaMode = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_IGNORE
                        },
                    dpiX = 0,
                    dpiY = 0,
                    bitmapOptions = D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_CANNOT_DRAW,
                    colorContext = null
                };
            using D2D1Bitmap1 d2d1Bitmap1 = _d2d1DeviceContext!.CreateBitmapFromDxgiSurface(dxgiSurface, ref bitmapProperties);

            _serilogLogger.LogDebug("Setting target");

            _d2d1DeviceContext.SetTarget(d2d1Bitmap1);
            _d2d1DeviceContext.SetTextAntialiasMode(D2D1_TEXT_ANTIALIAS_MODE.D2D1_TEXT_ANTIALIAS_MODE_CLEARTYPE);
        }

        /// <summary>
        ///     <para>Handles the <see cref="DisplayChangedMessage" /> global message.</para>
        ///     <para>Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.</para>
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleDisplayChangedMessage(DisplayChangedMessage message)
        {
            if (!_isInitialized)
            {
                return;
            }

            PublishRefreshRateChangedMessage();
        }

        /// <summary>
        ///     <para>Handles the <see cref="ResolutionChangedMessage" /> global message.</para>
        ///     <para>Publishes the <see cref="DirectXResourcesResizedMessage" /> render message.</para>
        /// </summary>
        /// <param name="message">The message being handled.</param>
        /// <exception cref="DirectXException">Thrown when resizing buffers failed.</exception>
        private void HandleResolutionChangedMessage(ResolutionChangedMessage message)
        {
            if (!_isInitialized)
            {
                return;
            }

            _serilogLogger.LogDebug("Clearing state of and flushing immediate context");

            // Required due to deferred destruction:
            // https://docs.microsoft.com/en-us/windows/win32/api/d3d11/nf-d3d11-id3d11devicecontext-flush#Defer_Issues_with_Flip

            _d3d11Device!.ImmediateContext.ClearState();
            _d3d11Device.ImmediateContext.Flush();

            // Required to avoid leaking memory
            _d2d1DeviceContext!.SetTarget(null);

            _dxgiSwapChain1!.ResizeBuffers();

            InitializeRenderTarget();

            _clientSize = D2DFactory.CreateSizeU((uint)message.Resolution.Width, (uint)message.Resolution.Height);

            _renderMessagePublisherSubscriber.Publish(new DirectXResourcesResizedMessage(_clientSize.Value));
        }

        /// <summary>
        ///     Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.
        /// </summary>
        /// <exception cref="DirectXException">Thrown when the <see cref="IDXGIOutput" /> could not be retrieved.</exception>
        /// <exception cref="DirectXException">Thrown when the query for <see cref="IDXGIOutput1" /> failed.</exception>
        /// <exception cref="DirectXException">Thrown when the retrieval of the closest matching output failed.</exception>
        private void PublishRefreshRateChangedMessage()
        {
            // Retrieve the current refresh rate

            using DXGIOutput dxgiOutput = _dxgiSwapChain1!.GetContainingOutput();
            using DXGIOutput1 dxgiOutput1 = dxgiOutput.QueryDXGIOutput1()!;
            DXGI_MODE_DESC1 closestMatch = dxgiOutput1.FindClosestMatchingMode1();

            // Convert the refresh rate to a refresh period

            double hz = closestMatch.RefreshRate.Numerator / (closestMatch.RefreshRate.Denominator == 0 ? 1.0 : closestMatch.RefreshRate.Denominator);
            TimeSpan refreshPeriod = TimeSpan.FromSeconds(1) / hz;

            if (refreshPeriod == _refreshPeriod)
            {
                return;
            }

            _refreshPeriod = refreshPeriod;

            _globalMessagePublisherSubscriber.Publish(new RefreshPeriodChangedMessage(refreshPeriod, hz));
        }
    }
}