using System;
using System.Drawing;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Entities.Updaters
{
    /// <inheritdoc />
    public abstract class Updater<TRenderState> : IUpdater<TRenderState>
        where TRenderState : class
    {
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Updater{TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <param name="order">The entity order. The order must be unique for all updaters in an <see cref="EntityCollection{TEntity}" />.</param>
        protected Updater(IInterfaces interfaces, NestedContext context, uint order = 0)
        {
            Interfaces = interfaces;
            Context = context;
            Order = order;
            GlobalMessagePublisherSubscriber = ConcurrentMessagePublisherSubscriber<IGlobalMessage>.Create(interfaces, context);
            UpdateMessagePublisherSubscriber = MessagePublisherSubscriber<IUpdateMessage>.Create(interfaces.UpdateMessageQueue, context);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Updater{TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="order">The entity order. The order must be unique for all updaters in an <see cref="EntityCollection{TEntity}" />.</param>
        protected Updater(IInterfaces interfaces, uint order = 0) : this(interfaces, new NestedContext("Updater"), order)
        {
        }

        /// <summary>
        ///     Gets the nested context.
        /// </summary>
        protected NestedContext Context { get; }

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
        ///     Gets a value indicating if the updater is neither paused nor suspended.
        /// </summary>
        protected bool IsRunning => !IsPaused && !IsSuspended;

        /// <summary>
        ///     Gets a value indicating if the updater is paused.
        /// </summary>
        protected bool IsPaused { get; private set; }

        /// <summary>
        ///     Gets a value indicating if the updater is suspended.
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
        public void UpdateGameState()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            // Handle dispatched messages
            GlobalMessagePublisherSubscriber.HandleDispatched();

            if (ShouldUpdateGameStateAndPrepareRenderState())
            {
                OnUpdateGameState();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Prepares a render state for rendering unless a combination of <see cref="IsPaused" />, <see cref="IsSuspended" />,
        ///     <see cref="UpdateWhenPaused" />, and <see cref="UpdateWhenSuspended" /> values determine that updating should not occur.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void PrepareRenderState(TRenderState renderState)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (ShouldUpdateGameStateAndPrepareRenderState())
            {
                OnPrepareRenderState(renderState);
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

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the updater resources thread.</exception>
        public void InitializeResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.UpdaterResources);

            OnInitializeResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the updater resources thread.</exception>
        public void ResizeResources(Size clientSize)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.UpdaterResources);

            OnResizeResources(clientSize);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the updater resources thread.</exception>
        public void ReleaseResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.UpdaterResources);

            OnReleaseResources();
        }

        /// <inheritdoc cref="IUpdater.InitializeResources" />
        protected virtual void OnInitializeResources()
        {
        }

        /// <inheritdoc cref="IUpdater.ResizeResources" />
        protected virtual void OnResizeResources(Size clientSize)
        {
        }

        /// <inheritdoc cref="IUpdater.ReleaseResources" />
        protected virtual void OnReleaseResources()
        {
        }

        /// <summary>
        ///     Updates the game state.
        /// </summary>
        protected abstract void OnUpdateGameState();

        /// <summary>
        ///     Prepares a render state for rendering.
        /// </summary>
        /// <param name="renderState">A render state.</param>
        protected abstract void OnPrepareRenderState(TRenderState renderState);

        /// <inheritdoc cref="IEntity.Pause" />
        protected virtual void OnPause()
        {
        }

        /// <inheritdoc cref="IEntity.Unpause" />
        protected virtual void OnUnpause()
        {
        }

        /// <inheritdoc cref="IEntity.Suspend" />
        protected virtual void OnSuspend()
        {
        }

        /// <inheritdoc cref="IEntity.Resume" />
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