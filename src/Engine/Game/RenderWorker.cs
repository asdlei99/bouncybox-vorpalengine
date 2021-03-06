﻿using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Calculators;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>An engine thread worker that renders render states.</summary>
    internal sealed class RenderWorker<TGameState> : EngineThreadWorker
        where TGameState : class
    {
        private readonly IDirectXResourceManager<TGameState> _directXResourceManager;
        private readonly TimeSpan _engineStatsFrequency = TimeSpan.FromSeconds(1);
        private readonly Stopwatch _engineStatsStopwatch = Stopwatch.StartNew();
        private readonly EventFrequencyCalculator _fpsCalculator = new EventFrequencyCalculator(true);
        private readonly TimeSpanAccumulator _frametimeAccumulator = new TimeSpanAccumulator(10000);
        private readonly TimeSpan _throttledRenderPeriod = TimeSpan.FromSeconds(1) / 30; // 30 Hz
        private ulong _frameCount;
        private bool _isRenderWindowActivated;
        private bool _isRenderWindowMinimized;

        /// <summary>Initializes a new instance of the <see cref="RenderWorker{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="directXResourceManager">An <see cref="IDirectXResourceManager{TGameState}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public RenderWorker(IInterfaces interfaces, IDirectXResourceManager<TGameState> directXResourceManager, NestedContext context)
            : base(interfaces, EngineThread.Render, context.Push(nameof(RenderWorker<TGameState>)))
        {
            _directXResourceManager = directXResourceManager;
        }

        /// <summary>Initializes a new instance of the <see cref="RenderWorker{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="directXResourceManager">An <see cref="IDirectXResourceManager{TGameState}" /> implementation.</param>
        public RenderWorker(IInterfaces interfaces, IDirectXResourceManager<TGameState> directXResourceManager)
            : this(interfaces, directXResourceManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        protected override void OnPrepare()
        {
            GlobalMessageQueue
                .WithThread(ProcessThread.Render)
                .Subscribe<RenderWindowActivatedMessage>(HandleRenderWindowActivatedMessage)
                .Subscribe<RenderWindowDeactivatedMessage>(HandleRenderWindowDeactivatedMessage)
                .Subscribe<RenderWindowMinimizedMessage>(HandleRenderWindowMinimizedMessage)
                .Subscribe<RenderWindowRestoredMessage>(HandleRenderWindowRestoredMessage);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the result of rendering is an unexpected value.</exception>
        protected override void OnDoWork(CancellationToken cancellationToken)
        {
            long timestamp = Stopwatch.GetTimestamp();

            (RenderResult result, TimeSpan frametime) = _directXResourceManager.Render(cancellationToken);

            switch (result)
            {
                case RenderResult.FrameRendered:
                    break;
                case RenderResult.FrameSkipped:
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Calculate engine stats

            _frameCount++;
            _fpsCalculator.IncrementEvents();
            _frametimeAccumulator.Accumulate(frametime);

            // Throttle rendering as necessary
            if (!_isRenderWindowActivated || _isRenderWindowMinimized)
            {
                TimeSpan sleepDuration = _throttledRenderPeriod - TimeSpan.FromTicks(Stopwatch.GetTimestamp() - timestamp);

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
            GlobalMessageQueue.Publish(
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

        /// <inheritdoc />
        protected override void OnCleanUp()
        {
            _directXResourceManager.ReleaseRenderResources(CancellationToken.None);
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

        /// <summary>Handles the <see cref="RenderWindowMinimizedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowRestoredMessage(RenderWindowRestoredMessage message)
        {
            _isRenderWindowMinimized = false;
        }
    }
}