using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Calculators;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Runs the update loop.
    /// </summary>
    internal sealed class UpdateLoop : IDisposable
    {
        private const double UpdatesPerSecondMultiplier = 1.15;
        private readonly EngineStats _engineStats;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly IInterfaces _interfaces;
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;
        private bool _isMinimized;
        private bool _isRenderFormActivated;
        private TimeSpan _updatePeriod = TimeSpan.FromSeconds(1) / (60 * UpdatesPerSecondMultiplier); // Default to 60 Hz

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
        /// <param name="context">A nested context.</param>
        public UpdateLoop(IInterfaces interfaces, EngineStats engineStats, NestedContext context)
        {
            context = context.CopyAndPush(nameof(UpdateLoop));

            _interfaces = interfaces;
            _engineStats = engineStats;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<RefreshPeriodChangedMessage>(HandleRefreshRateChangedMessage)
                    .Subscribe<RenderWindowActivatedMessage>(HandleRenderWindowActivatedMessage)
                    .Subscribe<RenderWindowDeactivatedMessage>(HandleRenderWindowDeactivatedMessage)
                    .Subscribe<RenderWindowMinimizedMessage>(HandleRenderWindowMinimizedMessage)
                    .Subscribe<RenderWindowRestoredMessage>(HandleRenderWindowRestoredMessage);
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
        public UpdateLoop(IInterfaces interfaces, EngineStats engineStats)
            : this(interfaces, engineStats, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(() => { _globalMessagePublisherSubscriber?.Dispose(); }, ref _isDisposed, _interfaces.ThreadManager, ProcessThread.Main);
        }

        /// <summary>
        ///     Executes the loop that updates the game state. The loop is exited via cancellation of the provided cancellation token.
        /// </summary>
        /// <param name="updateDelegate">A delegate that is invoked to update the game state.</param>
        /// <param name="cancellationToken">A cancellation token that, upon cancellation, will cause the loop to exit.</param>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Update(Action updateDelegate, CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            TimeSpan renderFormDeactivatedUpdatePeriod = TimeSpan.FromSeconds(1) / (30 * UpdatesPerSecondMultiplier); // 30 Hz
            var upsCalculator = new EventFrequencyCalculator(true);
            Stopwatch engineStatsStopwatch = Stopwatch.StartNew();
            TimeSpan engineStatsFrequency = TimeSpan.FromSeconds(1);
            TimeSpan minimizedRenderFormSleepDuration = TimeSpan.FromMilliseconds(100);

            while (!cancellationToken.IsCancellationRequested)
            {
                // If the render window is minimized then don't update the game state
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

                long timestamp = Stopwatch.GetTimestamp();

                // Update
                updateDelegate();

                // Throttle updating if the render window is deactivated
                TimeSpan sleepDuration =
                    (_isRenderFormActivated ? _updatePeriod : renderFormDeactivatedUpdatePeriod) - TimeSpan.FromTicks(Stopwatch.GetTimestamp() - timestamp);

                if (sleepDuration > TimeSpan.Zero)
                {
                    // Sleep until the next update period
                    Thread.Sleep(sleepDuration);
                }

                // Calculate engine stats
                upsCalculator.IncrementEvents();

                if (engineStatsStopwatch.Elapsed < engineStatsFrequency)
                {
                    continue;
                }

                // Update engine stats
                _engineStats.UpdatesPerSecond = upsCalculator.GetFrequency();

                // Reset engine stats

                upsCalculator.Restart();
                engineStatsStopwatch.Restart();
            }
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

            _isRenderFormActivated = true;
        }

        /// <summary>
        ///     Handles the <see cref="RenderWindowDeactivatedMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowDeactivatedMessage(RenderWindowDeactivatedMessage message)
        {
            // The underlying XInputEnable API is technically obsolete
            Gamepad.Disable();

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