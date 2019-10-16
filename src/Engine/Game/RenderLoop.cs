using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Calculators;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;
using User32 = TerraFX.Interop.User32;
using Windows = BouncyBox.VorpalEngine.Engine.Interop.Windows;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     An engine thread worker that renders render states.
    /// </summary>
    internal sealed class RenderLoop<TRenderState> : IEngineThreadWorker
        where TRenderState : class
    {
        private const int RenderWindowDeactivatedFramesPerSecond = 30;
        private readonly EngineStats _engineStats;
        private readonly TimeSpan _engineStatsFrequency = TimeSpan.FromSeconds(1);
        private readonly Stopwatch _engineStatsStopwatch = Stopwatch.StartNew();
        private readonly EventFrequencyCalculator _fpsCalculator = new EventFrequencyCalculator(true);
        private readonly TimeSpanAccumulator _frametimeAccumulator = new TimeSpanAccumulator(10000);
        private readonly IInterfaces _interfaces;
        private readonly TimeSpan _minimizedRenderWindowSleepDuration = TimeSpan.FromMilliseconds(100);
        private readonly Action<TRenderState> _renderDelegate;
        private readonly IRenderStateManager<TRenderState> _renderStateManager;
        private readonly TimeSpan _renderWindowDeactivatedRenderPeriod = TimeSpan.FromSeconds(1) / RenderWindowDeactivatedFramesPerSecond;
        private DirectXResourceManager? _directXResourceManager;
        private ulong _frameCount;
        private bool _isRenderWindowActivated;
        private bool _isRenderWindowMinimized;
        private IntPtr _windowHandle;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RenderLoop{TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        /// <param name="renderDelegate">A delegate that is invoked to render a render state.</param>
        /// <param name="context">A nested context.</param>
        public RenderLoop(
            IInterfaces interfaces,
            IRenderStateManager<TRenderState> renderStateManager,
            EngineStats engineStats,
            Action<TRenderState> renderDelegate,
            NestedContext context)
        {
            _interfaces = interfaces;
            _renderStateManager = renderStateManager;
            _engineStats = engineStats;
            _renderDelegate = renderDelegate;
            Context = context.CopyAndPush(nameof(RenderLoop<TRenderState>));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RenderLoop{TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="renderDelegate">A delegate that is invoked to render a render state.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        public RenderLoop(
            IInterfaces interfaces,
            IRenderStateManager<TRenderState> renderStateManager,
            EngineStats engineStats,
            Action<TRenderState> renderDelegate)
            : this(interfaces, renderStateManager, engineStats, renderDelegate, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public NestedContext Context { get; }

        /// <summary>
        ///     <para>Subscribes to global messages.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowHandleCreatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </summary>
        /// <param name="globalMessagePublisherSubscriber">A global message publisher/subscriber.</param>
        public void SubscribeToMessages(ConcurrentMessagePublisherSubscriber<IGlobalMessage> globalMessagePublisherSubscriber)
        {
            globalMessagePublisherSubscriber
                .Subscribe<RenderWindowHandleCreatedMessage>(HandleRenderWindowHandleCreatedMessage)
                .Subscribe<RenderWindowActivatedMessage>(HandleRenderWindowActivatedMessage)
                .Subscribe<RenderWindowDeactivatedMessage>(HandleRenderWindowDeactivatedMessage)
                .Subscribe<RenderWindowMinimizedMessage>(HandleRenderWindowMinimizedMessage)
                .Subscribe<RenderWindowRestoredMessage>(HandleRenderWindowRestoredMessage);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Executes the loop that renders render states. The loop is exited via cancellation of the provided cancellation token.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public unsafe void DoWork(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            // If the window handle has not yet been created or the render window is minimized, then don't render
            if (_directXResourceManager == null || _isRenderWindowMinimized)
            {
                // Reduce CPU utilization
                Thread.Sleep(_minimizedRenderWindowSleepDuration);

                return;
            }

            // Render
            (RenderResult result, TimeSpan? frametime) =
                _directXResourceManager.Render(
                    resources =>
                    {
                        // Ensure there is a state to render
                        TRenderState? renderState = _renderStateManager.GetRenderStateForRendering(cancellationToken);

                        if (renderState == null)
                        {
                            return (Windows.S_OK, null);
                        }

                        // Frametime measurements should start only after a render state is retrieved
                        long startTimestamp = Stopwatch.GetTimestamp();

                        // Get the render window's client rectangle

                        RECT clientRect;

                        if (User32.GetClientRect(_windowHandle, &clientRect) == TerraFX.Interop.Windows.FALSE)
                        {
                            throw Win32ExceptionHelper.GetException();
                        }

                        // Do not render if the client rectangle has no area; this is usually due to the render window being minimized
                        if (clientRect.right == 0 || clientRect.bottom == 0)
                        {
                            return (Windows.S_OK, null);
                        }

                        // Begin rendering

                        resources.D2D1DeviceContext.BeginDraw();
                        resources.D2D1DeviceContext.Clear();

                        _renderDelegate(renderState);

                        return (resources.D2D1DeviceContext.EndDraw(), startTimestamp);
                    });

            // Calculate engine stats

            if (result == RenderResult.FrameRendered)
            {
                _frameCount++;

                Debug.Assert(frametime != null);

                _fpsCalculator.IncrementEvents();
                _frametimeAccumulator.Accumulate(frametime.Value);
            }

            // Throttle rendering only if the render window is deactivated
            if (!_isRenderWindowActivated)
            {
                long timestamp = Stopwatch.GetTimestamp();
                TimeSpan sleepDuration = _renderWindowDeactivatedRenderPeriod - TimeSpan.FromTicks(Stopwatch.GetTimestamp() - timestamp);

                if (sleepDuration > TimeSpan.Zero)
                {
                    Thread.Sleep(sleepDuration);
                }
            }

            if (_engineStatsStopwatch.Elapsed < _engineStatsFrequency)
            {
                return;
            }

            // Update engine stats

            _engineStats.FrameCount = _frameCount;
            _engineStats.FramesPerSecond = _fpsCalculator.GetFrequency();
            _engineStats.MeanFrametime = _frametimeAccumulator.Mean;
            _engineStats.MinimumFrametime = _frametimeAccumulator.Minimum;
            _engineStats.MaximumFrametime = _frametimeAccumulator.Maximum;

            // Reset engine stats

            _fpsCalculator.Restart();
            _frametimeAccumulator.Reset();
            _engineStatsStopwatch.Restart();
        }

        /// <summary>
        ///     <para>Performs post-work clean-up.</para>
        ///     <para>Publishes the <see cref="EngineThreadsTerminatedMessage" /> global message.</para>
        /// </summary>
        public void CleanUp()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            _directXResourceManager?.Dispose();

            _interfaces.GlobalConcurrentMessageQueue.Publish<EngineThreadsTerminatedMessage>();
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowHandleCreatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowHandleCreatedMessage(RenderWindowHandleCreatedMessage message)
        {
            _windowHandle = message.WindowHandle;
            _directXResourceManager = new DirectXResourceManager(_interfaces, message.WindowHandle, Context);
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowActivatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowActivatedMessage(RenderWindowActivatedMessage message)
        {
            _isRenderWindowActivated = true;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowActivatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowDeactivatedMessage(RenderWindowDeactivatedMessage message)
        {
            _isRenderWindowActivated = false;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowMinimizedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowMinimizedMessage(RenderWindowMinimizedMessage message)
        {
            _isRenderWindowMinimized = true;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowRestoredMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowRestoredMessage(RenderWindowRestoredMessage message)
        {
            _isRenderWindowMinimized = false;
        }
    }
}