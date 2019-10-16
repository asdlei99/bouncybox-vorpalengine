using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Calculators;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     An engine thread worker that updates the game state.
    /// </summary>
    internal sealed class UpdateLoop : IEngineThreadWorker
    {
        private const double UpdatesPerSecondMultiplier = 1.15;
        private readonly EngineStats _engineStats;
        private readonly TimeSpan _engineStatsFrequency = TimeSpan.FromSeconds(1);
        private readonly Stopwatch _engineStatsStopwatch = Stopwatch.StartNew();
        private readonly IInterfaces _interfaces;
        private readonly TimeSpan _minimizedRenderWindowSleepDuration = TimeSpan.FromMilliseconds(100);
        private readonly TimeSpan _renderWindowDeactivatedUpdatePeriod = TimeSpan.FromSeconds(1) / (30 * UpdatesPerSecondMultiplier);
        private readonly ContextSerilogLogger _serilogLogger;
        private readonly Action _updateDelegate;
        private readonly EventFrequencyCalculator _upsCalculator = new EventFrequencyCalculator(true);
        private bool _isMinimized;
        private bool _isRenderWindowActivated;
        private TimeSpan _updatePeriod = TimeSpan.FromSeconds(1) / (60 * UpdatesPerSecondMultiplier); // Default to 60 Hz

        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateLoop" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        /// <param name="updateDelegate">A delegate that is invoked to update the game state.</param>
        /// <param name="context">A nested context.</param>
        public UpdateLoop(IInterfaces interfaces, EngineStats engineStats, Action updateDelegate, NestedContext context)
        {
            _interfaces = interfaces;
            _engineStats = engineStats;
            _updateDelegate = updateDelegate;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            Context = context.CopyAndPush(nameof(UpdateLoop));
        }

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="UpdateLoop" /> type.</para>
        ///     <para>Subscribes to the <see cref="RefreshPeriodChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        /// <param name="updateDelegate">A delegate that is invoked to update the game state.</param>
        public UpdateLoop(IInterfaces interfaces, EngineStats engineStats, Action updateDelegate)
            : this(interfaces, engineStats, updateDelegate, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public NestedContext Context { get; }

        /// <summary>
        ///     <para>Subscribes to global messages.</para>
        ///     <para>Subscribes to the <see cref="RefreshPeriodChangedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowActivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowDeactivatedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowMinimizedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="RenderWindowRestoredMessage" /> global message.</para>
        /// </summary>
        /// <param name="globalMessagePublisherSubscriber">A global message publisher/subscriber.</param>
        public void SubscribeToMessages(ConcurrentMessagePublisherSubscriber<IGlobalMessage> globalMessagePublisherSubscriber)
        {
            globalMessagePublisherSubscriber
                .Subscribe<RefreshPeriodChangedMessage>(HandleRefreshRateChangedMessage)
                .Subscribe<RenderWindowActivatedMessage>(HandleRenderWindowActivatedMessage)
                .Subscribe<RenderWindowDeactivatedMessage>(HandleRenderWindowDeactivatedMessage)
                .Subscribe<RenderWindowMinimizedMessage>(HandleRenderWindowMinimizedMessage)
                .Subscribe<RenderWindowRestoredMessage>(HandleRenderWindowRestoredMessage);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Executes the loop that updates the game state. The loop is exited via cancellation of the provided cancellation token.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void DoWork(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            // If the render window is minimized then don't update the game state
            if (_isMinimized)
            {
                // Reduce CPU utilization
                Thread.Sleep(_minimizedRenderWindowSleepDuration);

                return;
            }

            long timestamp = Stopwatch.GetTimestamp();

            // Update
            _updateDelegate();

            // Throttle updating if the render window is deactivated
            TimeSpan sleepDuration =
                (_isRenderWindowActivated ? _updatePeriod : _renderWindowDeactivatedUpdatePeriod) - TimeSpan.FromTicks(Stopwatch.GetTimestamp() - timestamp);

            if (sleepDuration > TimeSpan.Zero)
            {
                // Sleep until the next update period
                Thread.Sleep(sleepDuration);
            }

            // Calculate engine stats
            _upsCalculator.IncrementEvents();

            if (_engineStatsStopwatch.Elapsed < _engineStatsFrequency)
            {
                return;
            }

            // Update engine stats
            _engineStats.UpdatesPerSecond = _upsCalculator.GetFrequency();

            // Reset engine stats

            _upsCalculator.Restart();
            _engineStatsStopwatch.Restart();
        }

        /// <inheritdoc />
        public void CleanUp()
        {
        }

        /// <summary>
        ///     Handles the <see cref="RefreshPeriodChangedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRefreshRateChangedMessage(RefreshPeriodChangedMessage message)
        {
            _serilogLogger.LogInformation("Refresh period changed to ~{RefreshPeriod} ms (~{Hz} Hz)", message.RefreshPeriod.TotalMilliseconds, message.Hz);

            _updatePeriod = message.RefreshPeriod / UpdatesPerSecondMultiplier;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowActivatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowActivatedMessage(RenderWindowActivatedMessage message)
        {
            // The underlying XInputEnable API is technically obsolete
            Gamepad.Enable();

            _isRenderWindowActivated = true;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowDeactivatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowDeactivatedMessage(RenderWindowDeactivatedMessage message)
        {
            // The underlying XInputEnable API is technically obsolete
            Gamepad.Disable();

            _isRenderWindowActivated = false;
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