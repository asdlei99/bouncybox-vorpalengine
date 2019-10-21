using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using Serilog.Events;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;
using User32 = TerraFX.Interop.User32;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Manages all entities and their interactions with the engine.</summary>
    public class EntityManager<TGameState> : IEntityManager<TGameState>
        where TGameState : class
    {
        private const DXGI_FORMAT DxgiFormat = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
        private const int MaximumRenderResourcesInitializationAttempts = 3;
        private readonly EntityCollection _entities = new EntityCollection();
        private readonly object _entitiesLockObject = new object();
        private readonly IGameExecutionStateManager _gameExecutionStateManager;
        private readonly IInterfaces _interfaces;
        private readonly object _renderLockObject = new object();
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _renderResourcesGlobalMessagePublisherSubscriber;
        private readonly ContextSerilogLogger _serilogLogger;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _updateGlobalMessagePublisherSubscriber;
        private D2D_SIZE_U? _clientSize;
        private D2D1Device? _d2d1Device;
        private D2D1DeviceContext? _d2d1DeviceContext;
        private D3D11Device? _d3d11Device;
        private DWriteFactory1? _dWriteFactory1;
        private DXGIAdapter? _dxgiAdapter;
#if DEBUG
        private DXGIDebug1? _dxgiDebug1;
#endif
        private DXGISwapChain1? _dxgiSwapChain1;
        private TimeSpan? _refreshPeriod;
        private int _renderResourcesInitializationAttempts;
        private bool _renderResourcesInitialized;
        private bool _shouldPause;
        private bool _shouldResume;
        private bool _shouldSuspend;
        private bool _shouldUnpause;
        private IntPtr _windowHandle = IntPtr.Zero;

        /// <summary>Initializes a new instance of the <see cref="EntityManager{TGameState}" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="GamePausedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameUnpausedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameSuspendedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameResumedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowHandleCreatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="DisplayChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResolutionChangedMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public EntityManager(IInterfaces interfaces, IGameExecutionStateManager gameExecutionStateManager, NestedContext context)
        {
            context = context.CopyAndPush(nameof(EntityManager<TGameState>));

            _updateGlobalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces)
                    .Subscribe<GamePausedMessage>(HandleGamePausedMessage)
                    .Subscribe<GameUnpausedMessage>(HandleGameUnpausedMessage)
                    .Subscribe<GameSuspendedMessage>(HandleGameSuspendedMessage)
                    .Subscribe<GameResumedMessage>(HandleGameResumedMessage);
            _renderResourcesGlobalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces)
                    .Subscribe<RenderWindowHandleCreatedMessage>(HandleRenderWindowHandleCreatedMessage)
                    .Subscribe<DisplayChangedMessage>(HandleDisplayChangedMessage)
                    .Subscribe<ResolutionChangedMessage>(HandleResolutionChangedMessage)
                    .Subscribe<RecreateRenderTargetMessage>(HandleRecreateRenderTargetMessage);
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _interfaces = interfaces;
            _gameExecutionStateManager = gameExecutionStateManager;
        }

        /// <summary>Initializes a new instance of the <see cref="EntityManager{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        public EntityManager(IInterfaces interfaces, IGameExecutionStateManager gameExecutionStateManager)
            : this(interfaces, gameExecutionStateManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Add(IEnumerable<IEntity> entities)
        {
            entities = entities.ToImmutableArray();

            PrepareForAdd(entities);

            lock (_entitiesLockObject)
            {
                _entities.Add(entities);
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Add(params IEntity[] entities)
        {
            return Add((IEnumerable<IEntity>)entities);
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Remove(IEnumerable<IEntity> entities)
        {
            entities = entities.ToImmutableArray();

            lock (_entitiesLockObject)
            {
                _entities.Remove(entities);
            }

            foreach (IEntity entity in entities)
            {
                entity.Dispose();
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Remove(params IEntity[] entities)
        {
            return Remove((IEnumerable<IEntity>)entities);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.Update" /> thread.
        /// </exception>
        public void Update(in CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            ImmutableArray<IUpdatingEntity> entities;

            lock (_entitiesLockObject)
            {
                entities = _entities.OrderedByUpdateOrder.ToImmutableArray();
            }

            foreach (IUpdatingEntity entity in entities)
            {
                if (_shouldPause)
                {
                    entity.Pause();
                    _shouldPause = false;
                }
                else if (_shouldUnpause)
                {
                    entity.Unpause();
                    _shouldUnpause = false;
                }
                if (_shouldSuspend)
                {
                    entity.Suspend();
                    _shouldSuspend = false;
                }
                else if (_shouldResume)
                {
                    entity.Resume();
                    _shouldResume = false;
                }

                entity.UpdateGameState(cancellationToken);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void ReleaseRenderResources(in CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            ReleaseRenderResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Render" /> thread.
        /// </exception>
        public unsafe (RenderResult result, TimeSpan frametime) Render(in CancellationToken cancellationToken)
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

                ImmutableArray<IRenderingEntity> entities;

                lock (_entitiesLockObject)
                {
                    entities = _entities.OrderedByRenderOrder.ToImmutableArray();
                }

                Debug.Assert(_clientSize != null);

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
                    _dWriteFactory1!,
                    _clientSize.Value);
                var atLeastOneEntityRendered = false;

                // Render entities
                // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
                foreach (IRenderingEntity entity in entities)
                {
                    if (entity.Render(resources, cancellationToken) == EntityRenderResult.FrameRendered)
                    {
                        atLeastOneEntityRendered = true;
                    }
                }

                // End drawing
                int endDrawResult = _d2d1DeviceContext.EndDraw();

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

                    _renderResourcesGlobalMessagePublisherSubscriber.Publish<RecreateRenderTargetMessage>();

                    return (RenderResult.FrameSkipped, TimeSpan.Zero);
                }

                ComObject.CheckResultHandle(endDrawResult, "Failed to end drawing.");

                // Present

                using D2D1Factory d2d1Factory = _d2d1Device!.GetFactory();
                using D2D1Multithread d2d1Multithread = d2d1Factory.QueryD2D1Multithread()!;

                // Lock underlying DXGI and Direct3D resources during presentation
                d2d1Multithread.Enter();

                try
                {
                    _dxgiSwapChain1!.Present(_interfaces.CommonGameSettings.EnableVSync ? 1u : 0u);
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

        /// <summary>Handles dispatched update messages.</summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void HandleDispatchedUpdateMessages()
        {
            _updateGlobalMessagePublisherSubscriber.HandleDispatched();
        }

        /// <summary>Handles dispatched render resources messages.</summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void HandleDispatchedRenderResourcesMessages()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            _renderResourcesGlobalMessagePublisherSubscriber.HandleDispatched();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        public void Dispose()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Main);

            _updateGlobalMessagePublisherSubscriber.Dispose();
            _renderResourcesGlobalMessagePublisherSubscriber.Dispose();
            DisposeDirectXObjects();
        }

        /// <summary>Initialized render resources.</summary>
        /// <remarks>
        ///     Publishes the <see cref="RefreshPeriodChangedMessage" /> global message.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        private unsafe void InitializeRenderResources()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

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

                        if (User32.GetClientRect(_windowHandle, &clientRect) == TerraFX.Interop.Windows.FALSE)
                        {
                            throw Win32ExceptionHelper.GetException();
                        }

                        _clientSize = D2DFactory.CreateSizeU((uint)clientRect.right, (uint)clientRect.bottom);

                        // Initialize all entities' render resources

                        var resources = new DirectXResources(
                            _dxgiAdapter!,
                            _dxgiSwapChain1!,
                            _d2d1Device!,
                            _d2d1DeviceContext!,
                            _dWriteFactory1!,
                            _clientSize.Value);
                        ImmutableArray<IRenderingEntity> entities;

                        lock (_entitiesLockObject)
                        {
                            entities = _entities.OrderedByRenderOrder.ToImmutableArray();
                        }

                        foreach (IRenderingEntity entity in entities)
                        {
                            entity.InitializeRenderResources(resources);
                        }

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
            throw new DirectXException(
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

                ImmutableArray<IRenderingEntity> entities;

                lock (_entitiesLockObject)
                {
                    entities = _entities.OrderedByRenderOrder.ToImmutableArray();
                }

                foreach (IRenderingEntity entity in entities)
                {
                    entity.ReleaseRenderResources();
                }

                DisposeDirectXObjects();

                _renderResourcesInitialized = false;
            }
        }

        /// <summary>Initializes the render target. Render targets must be reinitialized when initializing or resizing render resources.</summary>
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

        /// <summary>Prepares entities for adding by informing them of game execution state changes.</summary>
        /// <param name="entities">The entities to prepare.</param>
        private void PrepareForAdd(IEnumerable<IEntity> entities)
        {
            GameExecutionState gameExecutionState = _gameExecutionStateManager.GameExecutionState;

            foreach (IEntity entity in entities)
            {
                if (gameExecutionState.IsPaused)
                {
                    entity.Pause();
                }
                if (gameExecutionState.IsSuspended)
                {
                    entity.Suspend();
                }
            }
        }

        /// <summary>Disposes DirectX objects.</summary>
        private void DisposeDirectXObjects()
        {
            _d3d11Device?.Dispose();
            _d3d11Device = null;
            _dxgiAdapter?.Dispose();
            _dxgiAdapter = null;
            _dxgiSwapChain1?.Dispose();
            _dxgiSwapChain1 = null;
            _d2d1Device?.Dispose();
            _d2d1Device = null;
            _d2d1DeviceContext?.Dispose();
            _d2d1DeviceContext = null;
            _dWriteFactory1?.Dispose();
            _dWriteFactory1 = null;
#if DEBUG
            _dxgiDebug1?.ReportLiveObjects(DXGIDebug.DXGI_DEBUG_ALL, DXGI_DEBUG_RLO_FLAGS.DXGI_DEBUG_RLO_SUMMARY);
            _dxgiDebug1?.Dispose();
            _dxgiDebug1 = null;
#endif
        }

        /// <summary>Handles the <see cref="GamePausedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGamePausedMessage(GamePausedMessage message)
        {
            _shouldPause = true;
        }

        /// <summary>Handles the <see cref="GameUnpausedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGameUnpausedMessage(GameUnpausedMessage message)
        {
            _shouldUnpause = true;
        }

        /// <summary>Handles the <see cref="GameSuspendedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGameSuspendedMessage(GameSuspendedMessage message)
        {
            _shouldSuspend = true;
        }

        /// <summary>Handles the <see cref="GameResumedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGameResumedMessage(GameResumedMessage message)
        {
            _shouldResume = true;
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
        private void HandleResolutionChangedMessage(ResolutionChangedMessage message)
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

                _d3d11Device!.ImmediateContext.ClearState();
                _d3d11Device.ImmediateContext.Flush();

                // Required to avoid leaking memory
                _d2d1DeviceContext!.SetTarget(null);

                _dxgiSwapChain1!.ResizeBuffers();

                InitializeRenderTarget();

                _clientSize = D2DFactory.CreateSizeU((uint)message.Resolution.Width, (uint)message.Resolution.Height);

                // Resize all entities' render resources

                ImmutableArray<IRenderingEntity> entities;

                lock (_entitiesLockObject)
                {
                    entities = _entities.OrderedByRenderOrder.ToImmutableArray();
                }

                var resources = new DirectXResources(_dxgiAdapter!, _dxgiSwapChain1, _d2d1Device!, _d2d1DeviceContext, _dWriteFactory1!, _clientSize.Value);

                foreach (IRenderingEntity entity in entities)
                {
                    entity.ResizeRenderResources(resources, _clientSize.Value);
                }
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
        private void PublishRefreshPeriodChangedMessage()
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

            _updateGlobalMessagePublisherSubscriber.Publish(new RefreshPeriodChangedMessage(refreshPeriod, hz));
        }
    }
}