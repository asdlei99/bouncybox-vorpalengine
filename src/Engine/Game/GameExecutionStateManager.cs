using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Manages game execution state changes.
    /// </summary>
    internal class GameExecutionStateManager : IGameExecutionStateManager
    {
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="GameExecutionStateManager" /> type.</para>
        ///     <para>Subscribes to the <see cref="PauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnpauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="SuspendGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResumeGameMessage" /> global message.</para>
        /// </summary>
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

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="GameExecutionStateManager" /> type.</para>
        ///     <para>Subscribes to the <see cref="PauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnpauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="SuspendGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResumeGameMessage" /> global message.</para>
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        public GameExecutionStateManager(IInterfaces interfaces)
            : this(interfaces, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public GameExecutionState GameExecutionState => new GameExecutionState(IsGamePaused, IsGameSuspended);

        /// <inheritdoc />
        public bool IsGamePaused { get; private set; }

        /// <inheritdoc />
        public bool IsGameSuspended { get; private set; }

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

        /// <summary>
        ///     Handles the <see cref="PauseGameMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandlePauseGameMessage(PauseGameMessage message)
        {
            if (IsGamePaused)
            {
                return;
            }

            _serilogLogger.LogInformation("Game paused");
            IsGamePaused = true;
        }

        /// <summary>
        ///     Handles the <see cref="UnpauseGameMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleUnpauseGameMessage(UnpauseGameMessage message)
        {
            if (!IsGamePaused)
            {
                return;
            }

            _serilogLogger.LogInformation("Game unpaused");
            IsGamePaused = false;
        }

        /// <summary>
        ///     Handles the <see cref="SuspendGameMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleSuspendGameMessage(SuspendGameMessage message)
        {
            if (IsGameSuspended)
            {
                return;
            }

            _serilogLogger.LogInformation("Game suspended");
            IsGameSuspended = true;
        }

        /// <summary>
        ///     Handles the <see cref="ResumeGameMessage" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleResumeGameMessage(ResumeGameMessage message)
        {
            if (!IsGameSuspended)
            {
                return;
            }

            _serilogLogger.LogInformation("Game resumed");
            IsGameSuspended = false;
        }
    }
}