﻿using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Interop;
using BouncyBox.VorpalEngine.Interop.D2D1;
using BouncyBox.VorpalEngine.Interop.D2D1_1;
using BouncyBox.VorpalEngine.Interop.D3D11;
using BouncyBox.VorpalEngine.Interop.DWrite;
using BouncyBox.VorpalEngine.Interop.DXGI;
using BouncyBox.VorpalEngine.Interop.DXGI1_2;
using Serilog.Events;
using TerraFX.Interop;
using DXGIDebug = BouncyBox.VorpalEngine.Interop.DXGIDebug.DXGIDebug;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;
using User32 = TerraFX.Interop.User32;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>Manages all core DirectX resources.</summary>
    public class DirectXResourceManager<TGameState> : IDirectXResourceManager<TGameState>
        where TGameState : class
    {
        private const DXGI_FORMAT DxgiFormat = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
        private const int MaximumRenderResourcesInitializationAttempts = 3;
        private readonly IEntityManager<TGameState> _entityManager;
        private readonly GlobalMessageQueueHelper _globalMessageQueueHelper;
        private readonly IInterfaces _interfaces;
        private readonly object _renderLockObject = new object();
        private readonly ContextSerilogLogger _serilogLogger;
        private D2D_SIZE_U? _clientSize;
        private D2D1Device? _d2d1Device;
        private D2D1DeviceContext? _d2d1DeviceContext;
        private D3D11Device? _d3d11Device;
        private D3D11DeviceContext? _d3d11DeviceContext;
        private DWriteFactory? _dWriteFactory;
        private DXGIAdapter? _dxgiAdapter;
#if DEBUG
        private DXGIDebug? _dxgiDebug;
#endif
        private DXGISwapChain1? _dxgiSwapChain1;
        private bool _isDisposed;
        private TimeSpan? _refreshPeriod;
        private int _renderResourcesInitializationAttempts;
        private bool _renderResourcesInitialized;
        private IntPtr _windowHandle = IntPtr.Zero;

        /// <summary>Initializes a new instance of the <see cref="DirectXResourceManager{TGameState}" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="RenderWindowHandleCreatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="DisplayChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResolutionChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RecreateRenderTargetMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public DirectXResourceManager(IInterfaces interfaces, IEntityManager<TGameState> entityManager, NestedContext context)
        {
            context = context.Push(nameof(DirectXResourceManager<TGameState>));

            _interfaces = interfaces;
            _entityManager = entityManager;
            _globalMessageQueueHelper =
                new GlobalMessageQueueHelper(interfaces.GlobalMessageQueue, context)
                    .WithThread(ProcessThread.Render)
                    .Subscribe<RenderWindowHandleCreatedMessage>(HandleRenderWindowHandleCreatedMessage)
                    .Subscribe<DisplayChangedMessage>(HandleDisplayChangedMessage)
                    .Subscribe<ResolutionChangedMessage>(HandleResolutionChangedMessage)
                    .Subscribe<RecreateRenderTargetMessage>(HandleRecreateRenderTargetMessage);
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
        }

        /// <summary>Initializes a new instance of the <see cref="DirectXResourceManager{TGameState}" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="RenderWindowHandleCreatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="DisplayChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResolutionChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RecreateRenderTargetMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        public DirectXResourceManager(IInterfaces interfaces, IEntityManager<TGameState> entityManager)
            : this(interfaces, entityManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the <see cref="ProcessThread.Render" /> thread.
        /// </exception>
        public void ReleaseRenderResources(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            ReleaseRenderResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the <see cref="ProcessThread.Render" /> thread.
        /// </exception>
        public unsafe (RenderResult result, TimeSpan frametime) Render(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            // Wait for render resources to initialize
            lock (_renderLockObject)
            {
                if (!_renderResourcesInitialized)
                {
                    // Render resources are not initialized
                    return (RenderResult.FrameSkipped, TimeSpan.Zero);
                }

                RECT clientRect;

                if (User32.GetClientRect(_windowHandle, &clientRect) == TerraFX.Interop.Windows.FALSE)
                {
                    throw Win32ExceptionHelper.GetException();
                }

                if (clientRect.right == 0 || clientRect.bottom == 0)
                {
                    // No need to render unless the client area is non-zero
                    return (RenderResult.FrameSkipped, TimeSpan.Zero);
                }

                Debug.Assert(_clientSize is object);

                // Frametime measurements should start only after a render state is retrieved
                long startTimestamp = Stopwatch.GetTimestamp();

                // Begin drawing

                _d2d1DeviceContext!.BeginDraw();
                _d2d1DeviceContext.Clear();

                var resources = new DirectXResources(
                    _dxgiAdapter!,
                    _dxgiSwapChain1!,
                    _d2d1Device!,
                    _d2d1DeviceContext,
                    _dWriteFactory!,
                    _clientSize!.Value);
                bool atLeastOneEntityRendered = _entityManager.Render(resources, cancellationToken) > 0;

                // End drawing
                HResult endDrawResult = _d2d1DeviceContext.EndDraw();

                // Skip presenting the frame if no entities were rendered
                if (!atLeastOneEntityRendered)
                {
                    return (RenderResult.FrameSkipped, TimeSpan.Zero);
                }

                // Calculate frametime
                TimeSpan frametime = TimeSpan.FromTicks(Stopwatch.GetTimestamp() - startTimestamp);

                // Check if the render target needs to be recreated
                if (endDrawResult == TerraFX.Interop.Windows.D2DERR_RECREATE_TARGET)
                {
                    _serilogLogger.LogWarning("Direct2D reports that the render target must be recreated");

                    _globalMessageQueueHelper.Publish<RecreateRenderTargetMessage>();

                    return (RenderResult.FrameSkipped, TimeSpan.Zero);
                }

                endDrawResult.ThrowIfFailed("Failed to end drawing.");

                // Present

                using D2D1Factory d2d1Factory = _d2d1Device!.GetFactory();
                using var d2d1Multithread = new D2D1Multithread(d2d1Factory.QueryInterface<ID2D1Multithread>());

                // Lock underlying DXGI and Direct3D resources during presentation
                d2d1Multithread.Enter();

                try
                {
                    _dxgiSwapChain1!.Present(_interfaces.CommonGameSettings.EnableVSync ? 1u : 0u, 0);
                }
                finally
                {
                    // Release the lock on DXGI and Direct3D resources
                    d2d1Multithread.Leave();
                }

                // Consider the frame to be skipped if no entities actually rendered themselves
                return (RenderResult.FrameRendered, frametime);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _interfaces.ThreadManager.DisposeHelper(
                () =>
                {
                    _globalMessageQueueHelper.Dispose();
                    DisposeDirectXObjects();
                },
                ref _isDisposed,
                ProcessThread.Main);
        }

        /// <summary>Initialized render resources.</summary>
        /// <remarks>Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.</remarks>
        private unsafe void InitializeRenderResources()
        {
            for (var i = 0; i < MaximumRenderResourcesInitializationAttempts; i++)
            {
                // Wait for rendering to complete
                lock (_renderLockObject)
                {
                    // Ensure that render resources need to be initialized
                    if (_renderResourcesInitialized)
                    {
                        return;
                    }

                    _serilogLogger.LogDebug(
                        "Initializing render resources (attempt {InitializationAttempt} of {MaximumInitializationAttempts})",
                        ++_renderResourcesInitializationAttempts,
                        MaximumRenderResourcesInitializationAttempts);

                    try
                    {
                        // Optionally create a DXGI debug reference

#if DEBUG
                        const bool debug = true;

                        _serilogLogger.LogDebug($"Creating {nameof(TerraFX.Interop.DXGIDebug)}");

                        DXGIDebug.Create(out _dxgiDebug).ThrowIfFailed($"Failed to create {nameof(TerraFX.Interop.DXGIDebug)}.");
#else
            const bool debug = false;
#endif

                        // Initialize Direct3D

                        var featureLevels = new[] { D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_1, D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0 };

                        _serilogLogger.LogDebug($"Creating hardware {nameof(D3D11Device)}");

                        try
                        {
                            D3D11Device
                                .Create(
                                    driverType: D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE,
                                    featureLevels: featureLevels,
                                    debug: debug,
                                    device: out _d3d11Device,
                                    immediateContext: out _d3d11DeviceContext)
                                .ThrowIfFailed($"Failed to create hardware {nameof(D3D11Device)}.");
                        }
                        catch (Exception exception)
                        {
                            _serilogLogger.LogWarning(exception, $"Failed to create hardware {nameof(D3D11Device)}");
                            _serilogLogger.LogDebug($"Creating WARP {nameof(D3D11Device)}");

                            D3D11Device
                                .Create(
                                    driverType: D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_WARP,
                                    featureLevels: featureLevels,
                                    debug: debug,
                                    device: out _d3d11Device,
                                    immediateContext: out _d3d11DeviceContext)
                                .ThrowIfFailed($"Failed to create WARP {nameof(D3D11Device)}.");
                        }

                        // Initialize DXGI

                        _serilogLogger.LogDebug($"Querying {nameof(DXGIDevice)}");

                        using var dxgiDevice = new DXGIDevice(_d3d11Device!.QueryInterface<IDXGIDevice>());

                        _serilogLogger.LogDebug($"Retrieving {nameof(DXGIAdapter)}");

                        dxgiDevice.GetAdapter(out _dxgiAdapter).ThrowIfFailed($"Failed to retrieve {nameof(DXGIAdapter)}.");

                        _serilogLogger.LogDebug($"Retrieving {nameof(DXGIFactory2)}");

                        _dxgiAdapter!.GetParent(out IDXGIFactory2* parent).ThrowIfFailed($"Failed to get parent {nameof(DXGIFactory2)}.");

                        using var dxgiFactory2 = new DXGIFactory2(parent);

                        // Initialize swap chain

                        _serilogLogger.LogDebug($"Creating {nameof(DXGISwapChain1)}");

                        var dxgiSwapChainDesc1 =
                            new DXGI_SWAP_CHAIN_DESC1
                            {
                                Width = 0,
                                Height = 0,
                                Format = DxgiFormat,
                                Stereo = TerraFX.Interop.Windows.FALSE,
                                SampleDesc =
                                    new DXGI_SAMPLE_DESC
                                    {
                                        Count = 1,
                                        Quality = 0
                                    },
                                BufferUsage = DXGI.DXGI_USAGE_RENDER_TARGET_OUTPUT,
                                // Jesse Natalie on DirectX Discord: "Using 3 buffers prevents quantizing to 30hz if you drop below 60"
                                BufferCount = 2, // Triple-buffering
                                Scaling = DXGI_SCALING.DXGI_SCALING_NONE,
                                SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_DISCARD,
                                AlphaMode = DXGI_ALPHA_MODE.DXGI_ALPHA_MODE_IGNORE,
                                Flags = 0
                            };

                        dxgiFactory2
                            .CreateSwapChainForHwnd(_d3d11Device, _windowHandle, &dxgiSwapChainDesc1, swapChain: out _dxgiSwapChain1)
                            .ThrowIfFailed($"Failed to create {nameof(DXGISwapChain1)}.");

                        // Initialize Direct2D

                        _serilogLogger.LogDebug($"Creating {nameof(D2D1Device)}");

                        D2D1Device.Create(dxgiDevice, debug, out _d2d1Device).ThrowIfFailed($"Failed to create {nameof(D2D1Device)}.");

                        _serilogLogger.LogDebug($"Creating {nameof(D2D1DeviceContext)}");

                        _d2d1Device!
                            .CreateDeviceContext(
                                D2D1_DEVICE_CONTEXT_OPTIONS.D2D1_DEVICE_CONTEXT_OPTIONS_ENABLE_MULTITHREADED_OPTIMIZATIONS,
                                out _d2d1DeviceContext)
                            .ThrowIfFailed($"Failed to create {nameof(D2D1DeviceContext)}.");

                        InitializeRenderTarget();

                        _serilogLogger.LogDebug("Making window association");

                        dxgiFactory2.MakeWindowAssociation(_windowHandle, DXGI.DXGI_MWA_NO_ALT_ENTER).ThrowIfFailed("Failed to make window association.");

                        // Initialize DirectWrite

                        _serilogLogger.LogDebug($"Creating {nameof(DWriteFactory)}");

                        DWriteFactory.Create(out _dWriteFactory).ThrowIfFailed($"Failed to create {nameof(DWriteFactory)}.");

                        // Initialization complete

                        RECT clientRect;

                        if (User32.GetClientRect(_windowHandle, &clientRect) == TerraFX.Interop.Windows.FALSE)
                        {
                            throw Win32ExceptionHelper.GetException();
                        }

                        _clientSize = new D2D_SIZE_U((uint)clientRect.right, (uint)clientRect.bottom);

                        // Initialize all entities' render resources

                        var resources = new DirectXResources(
                            _dxgiAdapter!,
                            _dxgiSwapChain1!,
                            _d2d1Device!,
                            _d2d1DeviceContext!,
                            _dWriteFactory!,
                            _clientSize.Value);

                        _entityManager.InitializeRenderResources(resources);

                        PublishRefreshPeriodChangedMessage();

                        _renderResourcesInitializationAttempts = 0;
                        _renderResourcesInitialized = true;

                        // Initialization succeeded
                        return;
                    }
                    catch (Exception exception)
                    {
                        // Release partially-created resources
                        ReleaseRenderResources(true);

                        _serilogLogger.Log(
                            _renderResourcesInitializationAttempts == MaximumRenderResourcesInitializationAttempts
                                ? LogEventLevel.Error
                                : LogEventLevel.Warning,
                            exception,
                            "Initialization of DirectX failed (attempt {InitializationAttempt} of {MaximumInitializationAttempts})",
                            _renderResourcesInitializationAttempts,
                            MaximumRenderResourcesInitializationAttempts);
                    }
                }
            }

            // Initialization failed
            throw new InvalidOperationException(
                $"DirectX resource initialization failed after {_renderResourcesInitializationAttempts} attemp{(_renderResourcesInitializationAttempts == 1 ? "" : "s")}.");
        }

        /// <inheritdoc cref="IEntityManager{TGameState}.ReleaseRenderResources" />
        /// <param name="force">A value indicating if partially-initialized resources should be released.</param>
        private void ReleaseRenderResources(bool force = false)
        {
            // Wait for rendering to complete
            lock (_renderLockObject)
            {
                // Ensure that render resources are initialized
                if (!force && !_renderResourcesInitialized)
                {
                    return;
                }

                _serilogLogger.LogDebug("Releasing render resources");

                // Release all entities' render resources
                _entityManager.ReleaseRenderResources();

                DisposeDirectXObjects();

                _renderResourcesInitialized = false;
            }
        }

        /// <summary>Initializes the render target. Render targets must be reinitialized when initializing or resizing render resources.</summary>
        private unsafe void InitializeRenderTarget()
        {
            _serilogLogger.LogDebug("Retrieving back buffer IDXGISurface");

            _dxgiSwapChain1!.GetBuffer(0, out IDXGISurface* surface).ThrowIfFailed($"Failed to get {nameof(IDXGISurface)}.");

            using var dxgiSurface = new DXGISurface(surface);

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
            _d2d1DeviceContext!
                .CreateBitmapFromDxgiSurface(dxgiSurface, &bitmapProperties, out D2D1Bitmap1? d2d1Bitmap1)
                .ThrowIfFailed("Failed to create {nameof(D2D1Bitmap)}.");

            try
            {
                _serilogLogger.LogDebug("Setting target");

                _d2d1DeviceContext.SetTarget(d2d1Bitmap1!);
                _d2d1DeviceContext.SetTextAntialiasMode(D2D1_TEXT_ANTIALIAS_MODE.D2D1_TEXT_ANTIALIAS_MODE_CLEARTYPE);
            }
            finally
            {
                d2d1Bitmap1!.Dispose();
            }
        }

        /// <summary>Disposes DirectX objects.</summary>
        private void DisposeDirectXObjects()
        {
            _d3d11Device?.Dispose();
            _d3d11Device = null;
            _d3d11DeviceContext?.Dispose();
            _d3d11DeviceContext = null;
            _dxgiAdapter?.Dispose();
            _dxgiAdapter = null;
            _dxgiSwapChain1?.Dispose();
            _dxgiSwapChain1 = null;
            _d2d1Device?.Dispose();
            _d2d1Device = null;
            _d2d1DeviceContext?.Dispose();
            _d2d1DeviceContext = null;
            _dWriteFactory?.Dispose();
            _dWriteFactory = null;
#if DEBUG
            _dxgiDebug?.ReportLiveObjects(TerraFX.Interop.DXGIDebug.DXGI_DEBUG_ALL, DXGI_DEBUG_RLO_FLAGS.DXGI_DEBUG_RLO_SUMMARY);
            _dxgiDebug?.Dispose();
            _dxgiDebug = null;
#endif
        }

        /// <summary>Handles the <see cref="RenderWindowHandleCreatedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowHandleCreatedMessage(RenderWindowHandleCreatedMessage message)
        {
            _windowHandle = message.WindowHandle;

            InitializeRenderResources();
        }

        /// <summary>Handles the <see cref="DisplayChangedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleDisplayChangedMessage(DisplayChangedMessage message)
        {
            // Wait for rendering to complete
            lock (_renderLockObject)
            {
                if (!_renderResourcesInitialized)
                {
                    return;
                }

                PublishRefreshPeriodChangedMessage();
            }
        }

        /// <summary>Handles the <see cref="ResolutionChangedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private unsafe void HandleResolutionChangedMessage(ResolutionChangedMessage message)
        {
            // Wait for rendering to complete
            lock (_renderLockObject)
            {
                if (!_renderResourcesInitialized)
                {
                    return;
                }

                _serilogLogger.LogDebug("Clearing state of and flushing immediate context");

                // Required due to deferred destruction:
                // https://docs.microsoft.com/en-us/windows/win32/api/d3d11/nf-d3d11-id3d11devicecontext-flush#Defer_Issues_with_Flip

                _d3d11DeviceContext!.ClearState();
                _d3d11DeviceContext.Flush();

                // Required to avoid leaking memory
                _d2d1DeviceContext!.SetTarget();

                _dxgiSwapChain1!.ResizeBuffers(0, 0, 0, DXGI_FORMAT.DXGI_FORMAT_UNKNOWN, 0).ThrowIfFailed("Failed to resize buffers.");

                InitializeRenderTarget();

                _clientSize = new D2D_SIZE_U((uint)message.Resolution.Width, (uint)message.Resolution.Height);

                // Resize all entities' render resources

                var resources = new DirectXResources(_dxgiAdapter!, _dxgiSwapChain1, _d2d1Device!, _d2d1DeviceContext, _dWriteFactory!, _clientSize.Value);

                _entityManager.ResizeRenderResources(resources, _clientSize.Value);
            }
        }

        /// <summary>Handles the <see cref="RecreateRenderTargetMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRecreateRenderTargetMessage(RecreateRenderTargetMessage message)
        {
            ReleaseRenderResources(true);
            InitializeRenderResources();
        }

        /// <summary>Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.</summary>
        private unsafe void PublishRefreshPeriodChangedMessage()
        {
            // Retrieve the current refresh rate

            DXGIOutput? dxgiOutput = null;
            DXGIOutput1? dxgiOutput1 = null;

            try
            {
                _dxgiSwapChain1!.GetContainingOutput(out dxgiOutput).ThrowIfFailed($"Failed to get {nameof(DXGIOutput)}.");
                dxgiOutput1 = new DXGIOutput1(dxgiOutput!.QueryInterface<IDXGIOutput1>());

                var modeToMatch =
                    new DXGI_MODE_DESC1
                    {
                        Format = DxgiFormat
                    };

                dxgiOutput1.FindClosestMatchingMode1(&modeToMatch, out DXGI_MODE_DESC1 closestMatch).ThrowIfFailed("Failed to find closest matching mode.");

                // Convert the refresh rate to a refresh period

                double hz = closestMatch.RefreshRate.Numerator / (closestMatch.RefreshRate.Denominator == 0 ? 1.0 : closestMatch.RefreshRate.Denominator);
                TimeSpan refreshPeriod = TimeSpan.FromSeconds(1) / hz;

                if (refreshPeriod == _refreshPeriod)
                {
                    return;
                }

                _refreshPeriod = refreshPeriod;

                _globalMessageQueueHelper.Publish(new RefreshPeriodChangedMessage(refreshPeriod, hz));
            }
            finally
            {
                dxgiOutput?.Dispose();
                dxgiOutput1?.Dispose();
            }
        }
    }
}