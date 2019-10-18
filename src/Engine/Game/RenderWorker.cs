using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Calculators;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using TerraFX.Interop;
using User32 = TerraFX.Interop.User32;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>An engine thread worker that renders render states.</summary>
    internal sealed class RenderWorker<TGameState, TRenderState> : EngineThreadWorker
        where TGameState : class
        where TRenderState : class, new()
    {
        private const int RenderWindowDeactivatedFramesPerSecond = 30;
        private readonly TimeSpan _engineStatsFrequency = TimeSpan.FromSeconds(1);
        private readonly Stopwatch _engineStatsStopwatch = Stopwatch.StartNew();
        private readonly IEntityManager<TGameState, TRenderState> _entityManager;
        private readonly EventFrequencyCalculator _fpsCalculator = new EventFrequencyCalculator(true);
        private readonly TimeSpanAccumulator _frametimeAccumulator = new TimeSpanAccumulator(10000);
        private readonly TimeSpan _minimizedRenderWindowSleepDuration = TimeSpan.FromMilliseconds(100);
        private readonly TimeSpan _renderWindowDeactivatedRenderPeriod = TimeSpan.FromSeconds(1) / RenderWindowDeactivatedFramesPerSecond;
        private ulong _frameCount;
        private bool _isRenderWindowActivated;
        private bool _isRenderWindowMinimized;
        private IntPtr _windowHandle;

        /// <summary>Initializes a new instance of the <see cref="RenderWorker{TGameState,TRenderState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState,TRenderState}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public RenderWorker(IInterfaces interfaces, IEntityManager<TGameState, TRenderState> entityManager, NestedContext context)
            : base(interfaces, EngineThread.Render, context.CopyAndPush(nameof(RenderWorker<TGameState, TRenderState>)))
        {
            _entityManager = entityManager;
        }

        /// <summary>Initializes a new instance of the <see cref="RenderWorker{TGameState,TRenderState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState,TRenderState}" /> implementation.</param>
        public RenderWorker(IInterfaces interfaces, IEntityManager<TGameState, TRenderState> entityManager)
            : this(interfaces, entityManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <remarks>
        ///     <para>Subscribes to the <see cref="RenderWindowHandleCreatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </remarks>
        protected override void OnPrepare()
        {
            GlobalMessagePublisherSubscriber
                .Subscribe<RenderWindowHandleCreatedMessage>(HandleRenderWindowHandleCreatedMessage)
                .Subscribe<RenderWindowActivatedMessage>(HandleRenderWindowActivatedMessage)
                .Subscribe<RenderWindowDeactivatedMessage>(HandleRenderWindowDeactivatedMessage)
                .Subscribe<RenderWindowMinimizedMessage>(HandleRenderWindowMinimizedMessage)
                .Subscribe<RenderWindowRestoredMessage>(HandleRenderWindowRestoredMessage);
        }

        /// <inheritdoc />
        /// <summary>Performs the work.</summary>
        /// <remarks>Publishes the <see cref="RecreateRenderTargetMessage" /> global message.</remarks>
        /// <exception>Thrown when <see cref="TerraFX.Interop.User32.GetClientRect" /> failed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the result of rendering is an unexpected value.</exception>
        protected override unsafe void OnDoWork(CancellationToken cancellationToken)
        {
            var clientRect = new RECT();

            if (_windowHandle != IntPtr.Zero && User32.GetClientRect(_windowHandle, &clientRect) == TerraFX.Interop.Windows.FALSE)
            {
                throw Win32ExceptionHelper.GetException();
            }

            // Don't render if the render window doesn't yet exist, is minimized, or has an invalid client rect
            if (_windowHandle == IntPtr.Zero || _isRenderWindowMinimized || clientRect.right == 0 || clientRect.bottom == 0)
            {
                // Reduce CPU utilization
                cancellationToken.WaitHandle.WaitOne(_minimizedRenderWindowSleepDuration);
                return;
            }

            // Render
            (RenderResult result, TimeSpan frametime) = _entityManager.Render(cancellationToken);

            switch (result)
            {
                case RenderResult.FrameRendered:
                    break;
                case RenderResult.FrameSkipped:
                    return;
                case RenderResult.RecreateTarget:
                    GlobalMessagePublisherSubscriber.Publish<RecreateRenderTargetMessage>();
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Calculate engine stats

            _frameCount++;
            _fpsCalculator.IncrementEvents();
            _frametimeAccumulator.Accumulate(frametime);

            // Throttle rendering only if the render window is deactivated
            if (!_isRenderWindowActivated)
            {
                long timestamp = Stopwatch.GetTimestamp();
                TimeSpan sleepDuration = _renderWindowDeactivatedRenderPeriod - TimeSpan.FromTicks(Stopwatch.GetTimestamp() - timestamp);

                if (sleepDuration > TimeSpan.Zero)
                {
                    cancellationToken.WaitHandle.WaitOne(sleepDuration);
                }
            }

            // Only update engine stats at the appropriate frequency
            if (_engineStatsStopwatch.Elapsed < _engineStatsFrequency)
            {
                return;
            }

            // Publish engine stats
            GlobalMessagePublisherSubscriber.Publish(
                new EngineRenderStatsMessage(
                    _fpsCalculator.GetFrequency(),
                    _frameCount,
                    _frametimeAccumulator.Mean,
                    _frametimeAccumulator.Minimum,
                    _frametimeAccumulator.Maximum));

            // Reset engine stats calculations

            _fpsCalculator.Restart();
            _frametimeAccumulator.Reset();
            _engineStatsStopwatch.Restart();
        }

        /// <summary>Handles the <see cref="RenderWindowHandleCreatedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowHandleCreatedMessage(RenderWindowHandleCreatedMessage message)
        {
            _windowHandle = message.WindowHandle;
        }

        /// <summary>Handles the <see cref="RenderWindowActivatedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowActivatedMessage(RenderWindowActivatedMessage message)
        {
            _isRenderWindowActivated = true;
        }

        /// <summary>Handles the <see cref="RenderWindowActivatedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowDeactivatedMessage(RenderWindowDeactivatedMessage message)
        {
            _isRenderWindowActivated = false;
        }

        /// <summary>Handles the <see cref="RenderWindowMinimizedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowMinimizedMessage(RenderWindowMinimizedMessage message)
        {
            _isRenderWindowMinimized = true;
        }

        /// <summary>Handles the <see cref="RenderWindowRestoredMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowRestoredMessage(RenderWindowRestoredMessage message)
        {
            _isRenderWindowMinimized = false;
        }
    }
}