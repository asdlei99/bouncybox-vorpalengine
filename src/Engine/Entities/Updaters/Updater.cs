using System;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Entities.Updaters
{
    /// <inheritdoc />
    public abstract class Updater<TGameState, TRenderState> : IUpdater<TGameState, TRenderState>
        where TGameState : class
        where TRenderState : class
    {
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Updater{TGameState,TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <param name="order">The entity order. The order must be unique for all updaters in an <see cref="EntityCollection{TEntity}" />.</param>
        protected Updater(IInterfaces interfaces, NestedContext context, uint order = 0)
        {
            Interfaces = interfaces;
            Order = order;
            GlobalMessagePublisherSubscriber = ConcurrentMessagePublisherSubscriber<IGlobalMessage>.Create(interfaces, context);
            UpdateMessagePublisherSubscriber = MessagePublisherSubscriber<IUpdateMessage>.Create(interfaces.UpdateMessageQueue, context);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Updater{TGameState,TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="order">The entity order. The order must be unique for all updaters in an <see cref="EntityCollection{TEntity}" />.</param>
        protected Updater(IInterfaces interfaces, uint order = 0)
            : this(interfaces, new NestedContext("Updater"), order)
        {
        }

        /// <summary>
        ///     Gets the <see cref="IInterfaces" /> implementation.
        /// </summary>
        protected IInterfaces Interfaces { get; }

        /// <summary>
        ///     <para>Gets the <see cref="IKeyboard" /> implementation.</para>
        ///     <para>Use this property to interact with the keyboard.</para>
        /// </summary>
        protected IKeyboard Keyboard => Interfaces.Keyboard;

        /// <summary>
        ///     <para>Gets the <see cref="IStatefulGamepad" /> implementation.</para>
        ///     <para>Use this property to interact with a gamepad.</para>
        /// </summary>
        protected IStatefulGamepad StatefulGamepad => Interfaces.StatefulGamepad;

        /// <summary>
        ///     <para>Gets the global message publisher/subscriber.</para>
        ///     <para>Use the global message queue to publish or subscribe to messages intended to be processed globally.</para>
        ///     <para>Do not use the global message queue to send update-queue-specific messages.</para>
        /// </summary>
        protected ConcurrentMessagePublisherSubscriber<IGlobalMessage> GlobalMessagePublisherSubscriber { get; }

        /// <summary>
        ///     <para>Gets the update message publisher/subscriber.</para>
        ///     <para>Use the update message queue to publish or subscribe to messages intended to be processed only by updaters.</para>
        ///     <para>Do not use the global message queue to send global-queue-specific messages.</para>
        /// </summary>
        protected MessagePublisherSubscriber<IUpdateMessage> UpdateMessagePublisherSubscriber { get; }

        /// <summary>
        ///     <para>Gets a value indicating if the game execution state is running.</para>
        ///     <para>The game is considered running if it is neither paused nor suspended.</para>
        /// </summary>
        protected bool IsRunning => !IsPaused && !IsSuspended;

        /// <summary>
        ///     Gets a value indicating if the game execution state is paused.
        /// </summary>
        protected bool IsPaused { get; private set; }

        /// <summary>
        ///     Gets a value indicating if the game execution state is suspended.
        /// </summary>
        protected bool IsSuspended { get; private set; }

        /// <summary>
        ///     Gets or sets a value that determines whether to update when the game execution state is paused.
        /// </summary>
        protected virtual bool UpdateWhenPaused { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value that determines whether to update when the game execution state is suspended.
        /// </summary>
        protected virtual bool UpdateWhenSuspended { get; set; } = false;

        /// <inheritdoc />
        public uint Order { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Updates the game state unless a combination of <see cref="IsPaused" />, <see cref="IsSuspended" />,
        ///     <see cref="UpdateWhenPaused" />, and <see cref="UpdateWhenSuspended" /> values determine that updating should not occur.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void UpdateGameState(TGameState gameState)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (ShouldUpdateGameStateAndPrepareRenderState())
            {
                OnUpdateGameState(gameState);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Prepares a render state for rendering unless a combination of <see cref="IsPaused" />, <see cref="IsSuspended" />,
        ///     <see cref="UpdateWhenPaused" />, and <see cref="UpdateWhenSuspended" /> values determine that updating should not occur.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void PrepareRenderState(TGameState gameState, TRenderState renderState)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (ShouldUpdateGameStateAndPrepareRenderState())
            {
                OnPrepareRenderState(gameState, renderState);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Pause()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            IsPaused = true;

            OnPause();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Unpause()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            IsPaused = false;

            OnUnpause();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Suspend()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            IsSuspended = true;

            OnSuspend();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Resume()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            IsSuspended = false;

            OnResume();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    GlobalMessagePublisherSubscriber.Dispose();
                    UpdateMessagePublisherSubscriber.Dispose();
                },
                ref _isDisposed,
                Interfaces.ThreadManager,
                ProcessThread.Main);
        }

        /// <summary>
        ///     Updates the game state.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        protected abstract void OnUpdateGameState(TGameState gameState);

        /// <summary>
        ///     Prepares a render state for rendering.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        /// <param name="renderState">A render state.</param>
        protected abstract void OnPrepareRenderState(TGameState gameState, TRenderState renderState);

        /// <summary>
        ///     Allows the entity to respond to the game execution state being paused.
        /// </summary>
        protected virtual void OnPause()
        {
        }

        /// <summary>
        ///     Allows the entity to respond to the game execution state being unpaused.
        /// </summary>
        protected virtual void OnUnpause()
        {
        }

        /// <summary>
        ///     Allows the entity to respond to the game execution state being suspended.
        /// </summary>
        protected virtual void OnSuspend()
        {
        }

        /// <summary>
        ///     Allows the entity to respond to the game execution state being resumed.
        /// </summary>
        protected virtual void OnResume()
        {
        }

        /// <summary>
        ///     Determines if the entity should update game state and prepare render state.
        /// </summary>
        /// <returns>Returns a value indicating whether the entity should update game state and prepare render state.</returns>
        private bool ShouldUpdateGameStateAndPrepareRenderState()
        {
            return (!IsPaused || UpdateWhenPaused) && (!IsSuspended || UpdateWhenSuspended);
        }
    }
}