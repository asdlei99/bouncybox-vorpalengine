using System;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using Serilog.Events;
using TerraFX.Interop;
using User32 = TerraFX.Interop.User32;
using Windows = TerraFX.Interop.Windows;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     An engine thread worker that manages requests to initialize, resize, and release DirectX resources.
    /// </summary>
    internal sealed class RendererResourcesWorker<TGameState, TRenderState> : EngineThreadWorker
        where TGameState : class
        where TRenderState : class
    {
        private const DXGI_FORMAT DxgiFormat = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
        private const int MaximumInitializationAttempts = 3;
        private readonly IEntityManager<TGameState, TRenderState> _entityManager;
        private readonly IInterfaces _interfaces;
        private readonly ContextSerilogLogger _serilogLogger;
        private D2D_SIZE_U? _clientSize;
        private D2D1Device? _d2d1Device;
        private D2D1DeviceContext? _d2d1DeviceContext;
        private D3D11Device? _d3d11Device;
        private DirectXResources? _directXResources;
        private DWriteFactory1? _dWriteFactory1;
        private DXGIAdapter? _dxgiAdapter;
#if DEBUG
        private DXGIDebug1? _dxgiDebug1;
#endif
        private DXGISwapChain1? _dxgiSwapChain1;
        private int _initializationAttempts;
        private bool _initializationFailed;
        private TimeSpan? _refreshPeriod;
        private IntPtr _windowHandle = IntPtr.Zero;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RendererResourcesWorker{TGameState,TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState,TRenderState}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public RendererResourcesWorker(IInterfaces interfaces, IEntityManager<TGameState, TRenderState> entityManager, NestedContext context)
            : base(interfaces, EngineThread.RendererResources, context.CopyAndPush(nameof(RendererResourcesWorker<TGameState, TRenderState>)))
        {
            _interfaces = interfaces;
            _entityManager = entityManager;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, Context);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RendererResourcesWorker{TGameState,TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState,TRenderState}" /> implementation.</param>
        public RendererResourcesWorker(IInterfaces interfaces, IEntityManager<TGameState, TRenderState> entityManager)
            : this(interfaces, entityManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <remarks>
        ///     <para>Subscribes to the <see cref="RenderWindowHandleCreatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="DisplayChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResolutionChangedMessage" /> global message.</para>
        /// </remarks>
        protected override void OnPrepare()
        {
            GlobalMessagePublisherSubscriber
                .Subscribe<RenderWindowHandleCreatedMessage>(HandleRenderWindowHandleCreatedMessage)
                .Subscribe<DisplayChangedMessage>(HandleDisplayChangedMessage)
                .Subscribe<ResolutionChangedMessage>(HandleResolutionChangedMessage);
        }

        /// <inheritdoc />
        /// <summary>
        ///     <para>Performs the work.</para>
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the renderer resources thread.</exception>
        /// <exception cref="DirectXException">Thrown if the maximum initialization attempts were reached without success.</exception>
        protected override void OnDoWork(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RendererResources);

            // Do not perform any work if the maximum number of initialization attempts was reached,
            // if the window handle was not received, or if DirectX resources already exist
            if (_initializationFailed || _windowHandle == IntPtr.Zero || _directXResources != null)
            {
                return;
            }

            _initializationAttempts++;

            _serilogLogger.LogDebug(
                "Initializing DirectX (attempt {InitializationAttempt} of {MaximumInitializationAttempts})",
                _initializationAttempts,
                MaximumInitializationAttempts);

            try
            {
                InitializeResources();
            }
            catch (Exception exception)
            {
                ReleaseResources();

                _initializationFailed = _initializationAttempts == MaximumInitializationAttempts;

                _serilogLogger.Log(
                    _initializationFailed ? LogEventLevel.Error : LogEventLevel.Warning,
                    exception,
                    "Initialization of DirectX failed (attempt {InitializationAttempt} of {MaximumInitializationAttempts})",
                    _initializationAttempts,
                    MaximumInitializationAttempts);

                if (_initializationFailed)
                {
                    throw new DirectXException(
                        $"DirectX resource initialization failed after {_initializationAttempts} attemp{(_initializationAttempts == 1 ? "" : "s")}.",
                        exception);
                }
                return;
            }

            // Initialization succeeded so reset the attempts

            _initializationAttempts = 0;
            _initializationFailed = false;
        }

        /// <summary>
        ///     Initializes DirectX and renderer resources.
        /// </summary>
        /// <remarks>
        ///     Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.
        /// </remarks>
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

            // Initialization complete

            RECT clientRect;

            if (User32.GetClientRect(_windowHandle, &clientRect) == Windows.FALSE)
            {
                throw Win32ExceptionHelper.GetException();
            }

            _clientSize = D2DFactory.CreateSizeU((uint)clientRect.right, (uint)clientRect.bottom);
            _directXResources = new DirectXResources(_dxgiAdapter, _dxgiSwapChain1, _d2d1Device, _d2d1DeviceContext, _dWriteFactory1, _clientSize.Value);

            _entityManager.InitializeRendererResources(_directXResources.Value);

            PublishRefreshRateChangedMessage();
        }

        /// <summary>
        ///     Releases DirectX and renderer resources.
        /// </summary>
        private void ReleaseResources()
        {
            _entityManager.ReleaseRendererResources();

            _d3d11Device?.Dispose();
            _dxgiAdapter?.Dispose();
            _dxgiSwapChain1?.Dispose();
            _d2d1Device?.Dispose();
            _d2d1DeviceContext?.Dispose();
            _dWriteFactory1?.Dispose();

#if DEBUG
            _dxgiDebug1?.ReportLiveObjects(DXGIDebug.DXGI_DEBUG_ALL, DXGI_DEBUG_RLO_FLAGS.DXGI_DEBUG_RLO_SUMMARY);
            _dxgiDebug1?.Dispose();
#endif

            _directXResources = null;
        }

        /// <summary>
        ///     Initializes the render target. This method is called when initializing resources and also in response to resolution changes.
        /// </summary>
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
        ///     Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.
        /// </summary>
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

            GlobalMessagePublisherSubscriber.Publish(new RefreshPeriodChangedMessage(refreshPeriod, hz));
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowHandleCreatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowHandleCreatedMessage(RenderWindowHandleCreatedMessage message)
        {
            _windowHandle = message.WindowHandle;

            InitializeResources();
        }

        /// <summary>
        ///     Handles the <see cref="DisplayChangedMessage" /> global message.
        /// </summary>
        /// <remarks>
        ///     Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.
        /// </remarks>
        /// <param name="message">The message being handled.</param>
        private void HandleDisplayChangedMessage(DisplayChangedMessage message)
        {
            if (_directXResources == null)
            {
                return;
            }

            PublishRefreshRateChangedMessage();
        }

        /// <summary>
        ///     Handles the <see cref="ResolutionChangedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleResolutionChangedMessage(ResolutionChangedMessage message)
        {
            if (_directXResources == null)
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

            _entityManager.ResizeRendererResources(_clientSize.Value);
        }
    }
}