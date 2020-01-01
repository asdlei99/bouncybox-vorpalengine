using System;
using System.Drawing;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>An entity that updates the game state.</summary>
    public class UpdatingEntity : Entity, IUpdatingEntity
    {
        /// <summary>Initializes a new instance of the <see cref="Entity" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="updateOrder">
        ///     The entity's position in update order, which is determined by sorting all entities' update orders in
        ///     ascending order.
        /// </param>
        /// <param name="context">A nested context.</param>
        protected UpdatingEntity(IInterfaces interfaces, uint updateOrder, NestedContext context)
            : base(interfaces, context)
        {
            UpdateOrder = updateOrder;

            UpdateMessagePublisherSubscriber = MessagePublisherSubscriber<IUpdateMessage>.Create(interfaces.UpdateMessageQueue, context);
        }

        /// <summary>Initializes a new instance of the <see cref="Entity" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="updateOrder">
        ///     The entity's position in update order, which is determined by sorting all entities' update orders in
        ///     ascending order.
        /// </param>
        protected UpdatingEntity(IInterfaces interfaces, uint updateOrder) : this(interfaces, updateOrder, NestedContext.None())
        {
        }

        /// <summary>
        ///     <para>Gets the update message publisher/subscriber.</para>
        ///     <para>
        ///         Use the update message queue to publish or subscribe to messages intended to be processed only by entities while
        ///         updating the game state.
        ///     </para>
        ///     <para>Do not use the global message queue to send global-queue-specific messages.</para>
        /// </summary>
        protected MessagePublisherSubscriber<IUpdateMessage> UpdateMessagePublisherSubscriber { get; }

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

        /// <summary>Gets a value that determines whether to update when the game execution state is paused.</summary>
        protected virtual bool UpdateWhenPaused { get; } = false;

        /// <summary>Gets a value that determines whether to update when the game execution state is suspended.</summary>
        protected virtual bool UpdateWhenSuspended { get; } = false;

        /// <inheritdoc />
        public uint UpdateOrder { get; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.Update" /> thread.
        /// </exception>
        public void InitializeUpdateResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnInitializeUpdaterResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.Update" /> thread.
        /// </exception>
        public void ResizeUpdateResources(Size clientSize)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnResizeUpdateResources(clientSize);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.Update" /> thread.
        /// </exception>
        public void ReleaseUpdateResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnReleaseUpdateResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.Update" /> thread.
        /// </exception>
        public void UpdateGameState(CancellationToken cancellationToken)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            // Handle dispatched messages
            GlobalMessagePublisherSubscriber.HandleDispatched();

            if (ShouldUpdateGameState())
            {
                OnUpdateGameState(cancellationToken);
            }
        }

        /// <inheritdoc cref="InitializeUpdateResources" />
        protected virtual void OnInitializeUpdaterResources()
        {
        }

        /// <inheritdoc cref="ReleaseUpdateResources" />
        protected virtual void OnResizeUpdateResources(Size clientSize)
        {
        }

        /// <inheritdoc cref="ReleaseUpdateResources" />
        protected virtual void OnReleaseUpdateResources()
        {
        }

        /// <inheritdoc cref="UpdateGameState" />
        protected virtual void OnUpdateGameState(CancellationToken cancellationToken)
        {
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            UpdateMessagePublisherSubscriber.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>Determines if the entity should update the game state.</summary>
        /// <returns>Returns a value indicating whether the entity should update the game state.</returns>
        private bool ShouldUpdateGameState()
        {
            return (!IsPaused || UpdateWhenPaused) && (!IsSuspended || UpdateWhenSuspended);
        }
    }
}