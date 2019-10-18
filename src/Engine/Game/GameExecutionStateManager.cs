using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>Manages game execution state changes.</summary>
    internal class GameExecutionStateManager : IGameExecutionStateManager
    {
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;
        private bool _isGamePaused;
        private bool _isGameSuspended;

        /// <summary>Initializes a new instance of the <see cref="GameExecutionStateManager" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="PauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnpauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="SuspendGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResumeGameMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public GameExecutionStateManager(IInterfaces interfaces, NestedContext context)
        {
            context = context.CopyAndPush(nameof(GameExecutionStateManager));

            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<PauseGameMessage>(HandlePauseGameMessage)
                    .Subscribe<UnpauseGameMessage>(HandleUnpauseGameMessage)
                    .Subscribe<SuspendGameMessage>(HandleSuspendGameMessage)
                    .Subscribe<ResumeGameMessage>(HandleResumeGameMessage);
        }

        /// <summary>Initializes a new instance of the <see cref="GameExecutionStateManager" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="PauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnpauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="SuspendGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResumeGameMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        public GameExecutionStateManager(IInterfaces interfaces)
            : this(interfaces, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public GameExecutionState GameExecutionState => new GameExecutionState(_isGamePaused, _isGameSuspended);

        /// <inheritdoc />
        public void HandleDispatchedMessages()
        {
            _globalMessagePublisherSubscriber.HandleDispatched();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(() => { _globalMessagePublisherSubscriber?.Dispose(); }, ref _isDisposed);
        }

        /// <summary>Handles the <see cref="PauseGameMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandlePauseGameMessage(PauseGameMessage message)
        {
            if (_isGamePaused)
            {
                return;
            }

            _serilogLogger.LogInformation("Game paused");
            _isGamePaused = true;
        }

        /// <summary>Handles the <see cref="UnpauseGameMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleUnpauseGameMessage(UnpauseGameMessage message)
        {
            if (!_isGamePaused)
            {
                return;
            }

            _serilogLogger.LogInformation("Game unpaused");
            _isGamePaused = false;
        }

        /// <summary>Handles the <see cref="SuspendGameMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleSuspendGameMessage(SuspendGameMessage message)
        {
            if (_isGameSuspended)
            {
                return;
            }

            _serilogLogger.LogInformation("Game suspended");
            _isGameSuspended = true;
        }

        /// <summary>Handles the <see cref="ResumeGameMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleResumeGameMessage(ResumeGameMessage message)
        {
            if (!_isGameSuspended)
            {
                return;
            }

            _serilogLogger.LogInformation("Game resumed");
            _isGameSuspended = false;
        }
    }
}