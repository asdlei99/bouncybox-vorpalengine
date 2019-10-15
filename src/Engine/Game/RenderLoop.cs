using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Calculators;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;
using User32 = TerraFX.Interop.User32;
using Windows = BouncyBox.VorpalEngine.Engine.Interop.Windows;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Runs the render loop.
    /// </summary>
    internal sealed class RenderLoop<TRenderState> : IDisposable
        where TRenderState : class
    {
        private readonly EngineStats _engineStats;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly IInterfaces _interfaces;
        private readonly IRenderStateManager<TRenderState> _renderStateManager;
        private bool _isDisposed;
        private bool _isMinimized;
        private bool _isRenderFormActivated;

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="RenderLoop{TRenderState}" /> type.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public RenderLoop(IInterfaces interfaces, IRenderStateManager<TRenderState> renderStateManager, EngineStats engineStats, NestedContext context)
        {
            context = context.CopyAndPush(nameof(RenderLoop<TRenderState>));

            _interfaces = interfaces;
            _renderStateManager = renderStateManager;
            _engineStats = engineStats;
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<RenderWindowActivatedMessage>(HandleRenderWindowActivatedMessage)
                    .Subscribe<RenderWindowDeactivatedMessage>(HandleRenderWindowDeactivatedMessage)
                    .Subscribe<RenderWindowMinimizedMessage>(HandleRenderWindowMinimizedMessage)
                    .Subscribe<RenderWindowRestoredMessage>(HandleRenderWindowRestoredMessage);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RenderLoop{TRenderState}" /> type.
        ///     <para>Subscribes to the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        public RenderLoop(IInterfaces interfaces, IRenderStateManager<TRenderState> renderStateManager, EngineStats engineStats)
            : this(interfaces, renderStateManager, engineStats, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(() => { _globalMessagePublisherSubscriber?.Dispose(); }, ref _isDisposed, _interfaces.ThreadManager, ProcessThread.Main);
        }

        /// <summary>
        ///     Executes the loop that renders render states. The loop is exited via cancellation of the provided cancellation token.
        /// </summary>
        /// <param name="windowHandle">The window handle of the render window.</param>
        /// <param name="renderDelegate">A delegate that is invoked to render a render state.</param>
        /// <param name="cancellationToken">A cancellation token that, upon cancellation, will cause the loop to exit.</param>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        /// <exception>Thrown when <see cref="TerraFX.Interop.User32.GetClientRect" /> failed.</exception>
        public unsafe void Render(IntPtr windowHandle, Action<TRenderState> renderDelegate, CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            const int renderFormDeactivatedFramesPerSecond = 30;
            TimeSpan renderFormDeactivatedRenderPeriod = TimeSpan.FromSeconds(1) / renderFormDeactivatedFramesPerSecond; // 30 Hz
            using var resourceManager = new DirectXResourceManager(_interfaces, windowHandle);
            var fpsCalculator = new EventFrequencyCalculator(true);
            var frametimeAccumulator = new TimeSpanAccumulator(10000);
            Stopwatch engineStatsStopwatch = Stopwatch.StartNew();
            TimeSpan engineStatsFrequency = TimeSpan.FromSeconds(1);
            var frameCount = 0ul;
            TimeSpan minimizedRenderFormSleepDuration = TimeSpan.FromMilliseconds(100);

            while (!cancellationToken.IsCancellationRequested)
            {
                // If the render window is minimized then don't render
                if (_isMinimized)
                {
                    do
                    {
                        // Handle dispatched messages
                        _globalMessagePublisherSubscriber.HandleDispatched();

                        // Reduce CPU utilization
                        Thread.Sleep(minimizedRenderFormSleepDuration);
                    } while (_isMinimized);

                    continue;
                }

                // Handle dispatched messages
                _globalMessagePublisherSubscriber.HandleDispatched();

                // Render
                (RenderResult result, TimeSpan? frametime) =
                    resourceManager.Render(
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

                            if (User32.GetClientRect(windowHandle, &clientRect) == TerraFX.Interop.Windows.FALSE)
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

                            renderDelegate(renderState);

                            return (resources.D2D1DeviceContext.EndDraw(), startTimestamp);
                        });

                // Calculate engine stats

                if (result == RenderResult.FrameRendered)
                {
                    frameCount++;

                    Debug.Assert(frametime != null);

                    fpsCalculator.IncrementEvents();
                    frametimeAccumulator.Accumulate(frametime.Value);
                }

                // Throttle rendering only if the render window is deactivated
                if (!_isRenderFormActivated)
                {
                    long timestamp = Stopwatch.GetTimestamp();
                    TimeSpan sleepDuration = renderFormDeactivatedRenderPeriod - TimeSpan.FromTicks(Stopwatch.GetTimestamp() - timestamp);

                    if (sleepDuration > TimeSpan.Zero)
                    {
                        Thread.Sleep(sleepDuration);
                    }
                }

                if (engineStatsStopwatch.Elapsed < engineStatsFrequency)
                {
                    continue;
                }

                // Update engine stats

                _engineStats.FrameCount = frameCount;
                _engineStats.FramesPerSecond = fpsCalculator.GetFrequency();
                _engineStats.MeanFrametime = frametimeAccumulator.Mean;
                _engineStats.MinimumFrametime = frametimeAccumulator.Minimum;
                _engineStats.MaximumFrametime = frametimeAccumulator.Maximum;

                // Reset engine stats

                fpsCalculator.Restart();
                frametimeAccumulator.Reset();
                engineStatsStopwatch.Restart();
            }
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowActivatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowActivatedMessage(RenderWindowActivatedMessage message)
        {
            _isRenderFormActivated = true;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowActivatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowDeactivatedMessage(RenderWindowDeactivatedMessage message)
        {
            _isRenderFormActivated = false;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowMinimizedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowMinimizedMessage(RenderWindowMinimizedMessage message)
        {
            _isMinimized = true;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowRestoredMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowRestoredMessage(RenderWindowRestoredMessage message)
        {
            _isMinimized = false;
        }
    }
}