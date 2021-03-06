﻿using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Calculators;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>An engine thread worker that updates the game state.</summary>
    internal sealed class UpdateWorker<TGameState> : EngineThreadWorker
        where TGameState : class
    {
        private const double UpdatesPerSecondMultiplier = 1.15;
        private readonly TimeSpan _engineStatsFrequency = TimeSpan.FromSeconds(1);
        private readonly Stopwatch _engineStatsStopwatch = Stopwatch.StartNew();
        private readonly IEntityManager<TGameState> _entityManager;
        private readonly TimeSpan _minimizedRenderWindowSleepDuration = TimeSpan.FromMilliseconds(100);
        private readonly TimeSpan _renderWindowDeactivatedUpdatePeriod = TimeSpan.FromSeconds(1) / (30 * UpdatesPerSecondMultiplier);
        private readonly ContextSerilogLogger _serilogLogger;
        private readonly EventFrequencyCalculator _upsCalculator = new EventFrequencyCalculator(true);
        private bool _isRenderWindowActivated;
        private bool _isRenderWindowMinimized;
        private TimeSpan _updatePeriod = TimeSpan.FromSeconds(1) / (60 * UpdatesPerSecondMultiplier); // Default to 60 Hz

        /// <summary>Initializes a new instance of the <see cref="UpdateWorker{TGameState}" />.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public UpdateWorker(IInterfaces interfaces, IEntityManager<TGameState> entityManager, NestedContext context)
            : base(interfaces, EngineThread.Update, context.Push(nameof(UpdateWorker<TGameState>)))
        {
            _entityManager = entityManager;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, Context);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateWorker{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        public UpdateWorker(IInterfaces interfaces, IEntityManager<TGameState> entityManager)
            : this(interfaces, entityManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <remarks>
        ///     <para>Subscribes to the <see cref="RefreshPeriodChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </remarks>
        protected override void OnPrepare()
        {
            GlobalMessageQueue
                .WithThread(ProcessThread.Update)
                .Subscribe<RefreshPeriodChangedMessage>(HandleRefreshRateChangedMessage)
                .Subscribe<RenderWindowActivatedMessage>(HandleRenderWindowActivatedMessage)
                .Subscribe<RenderWindowDeactivatedMessage>(HandleRenderWindowDeactivatedMessage)
                .Subscribe<RenderWindowMinimizedMessage>(HandleRenderWindowMinimizedMessage)
                .Subscribe<RenderWindowRestoredMessage>(HandleRenderWindowRestoredMessage);
        }

        /// <inheritdoc />
        protected override void OnDoWork(CancellationToken cancellationToken)
        {
            // If the render window is minimized then don't update the game state
            if (_isRenderWindowMinimized)
            {
                // Reduce CPU utilization
                cancellationToken.WaitHandle.WaitOne(_minimizedRenderWindowSleepDuration);
                return;
            }

            long timestamp = Stopwatch.GetTimestamp();

            // Update the game state
            _entityManager.Update(cancellationToken);

            // Throttle updating if the render window is deactivated
            TimeSpan sleepDuration =
                (_isRenderWindowActivated ? _updatePeriod : _renderWindowDeactivatedUpdatePeriod) - TimeSpan.FromTicks(Stopwatch.GetTimestamp() - timestamp);

            if (sleepDuration > TimeSpan.Zero)
            {
                // Sleep until the next update period
                cancellationToken.WaitHandle.WaitOne(sleepDuration);
            }

            // Calculate engine stats
            _upsCalculator.IncrementEvents();

            if (_engineStatsStopwatch.Elapsed < _engineStatsFrequency)
            {
                return;
            }

            // Publish engine stats
            GlobalMessageQueue.Publish(new EngineUpdateStatsMessage(_upsCalculator.GetFrequency()));

            // Reset engine stats

            _upsCalculator.Restart();
            _engineStatsStopwatch.Restart();
        }

        /// <summary>Handles the <see cref="RefreshPeriodChangedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRefreshRateChangedMessage(RefreshPeriodChangedMessage message)
        {
            _serilogLogger.LogInformation("Refresh period changed to ~{RefreshPeriod} ms (~{Hz} Hz)", message.RefreshPeriod.TotalMilliseconds, message.Hz);

            _updatePeriod = message.RefreshPeriod / UpdatesPerSecondMultiplier;
        }

        /// <summary>Handles the <see cref="RenderWindowActivatedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowActivatedMessage(RenderWindowActivatedMessage message)
        {
            // The underlying XInputEnable API is technically obsolete
            Gamepad.Enable();

            _isRenderWindowActivated = true;
        }

        /// <summary>Handles the <see cref="RenderWindowDeactivatedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowDeactivatedMessage(RenderWindowDeactivatedMessage message)
        {
            // The underlying XInputEnable API is technically obsolete
            Gamepad.Disable();

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