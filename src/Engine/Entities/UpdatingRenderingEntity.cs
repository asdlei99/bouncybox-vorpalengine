using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>An entity that updates the game state and renders itself.</summary>
    public class UpdatingRenderingEntity<TRenderState> : Entity, IUpdatingEntity, IRenderingEntity
        where TRenderState : struct
    {
        private readonly object _renderStateLockObject = new object();
        private TRenderState? _renderState;

        /// <summary>Initializes a new instance of the <see cref="UpdatingRenderingEntity{TRenderState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="updateOrder">
        ///     The entity's position in update order, which is determined by sorting all entities' update orders in
        ///     ascending order.
        /// </param>
        /// <param name="renderOrder">
        ///     The entity's position in render order, which is determined by sorting all entities' render orders in
        ///     ascending order.
        /// </param>
        /// <param name="context">A nested context.</param>
        protected internal UpdatingRenderingEntity(IInterfaces interfaces, uint updateOrder, uint renderOrder, NestedContext context)
            : base(interfaces, context)
        {
            UpdateOrder = updateOrder;
            RenderOrder = renderOrder;

            UpdateMessagePublisherSubscriber = MessagePublisherSubscriber<IUpdateMessage>.Create(interfaces.UpdateMessageQueue, context);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdatingRenderingEntity{TRenderState}" /></summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="updateOrder">
        ///     The entity's position in update order, which is determined by sorting all entities' update orders in
        ///     ascending order.
        /// </param>
        /// <param name="renderOrder">
        ///     The entity's position in render order, which is determined by sorting all entities' render orders in
        ///     ascending order.
        /// </param>
        protected internal UpdatingRenderingEntity(IInterfaces interfaces, uint updateOrder, uint renderOrder) : this(
            interfaces,
            updateOrder,
            renderOrder,
            NestedContext.None())
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

        /// <summary>Gets a value that determines whether to render when the game execution state is paused.</summary>
        protected virtual bool RenderWhenPaused { get; } = true;

        /// <summary>Gets a value that determines whether to render when the game execution state is suspended.</summary>
        protected virtual bool RenderWhenSuspended { get; } = false;

        /// <inheritdoc />
        public uint RenderOrder { get; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void InitializeRenderResources(in DirectXResources resources)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            OnInitializeRenderResources(resources);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void ResizeRenderResources(in DirectXResources resources, in D2D_SIZE_U clientSize)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            OnResizeRenderResources(resources, clientSize);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void ReleaseRenderResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            OnReleaseRenderResources();
        }

        /// <inheritdoc />
        public EntityRenderResult Render(in DirectXResources resources, in CancellationToken cancellationToken)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            if (!ShouldRender(out TRenderState? renderState))
            {
                return EntityRenderResult.FrameSkipped;
            }

            Debug.Assert(!(renderState is null));

            return OnRender(resources, renderState.Value, cancellationToken);
        }

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
        public void UpdateGameState(in CancellationToken cancellationToken)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            // Handle dispatched messages
            GlobalMessagePublisherSubscriber.HandleDispatched();

            if (!ShouldUpdateGameState())
            {
                return;
            }

            OnUpdateGameState(cancellationToken);
            lock (_renderStateLockObject)
            {
                _renderState = OnGetRenderState();
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
        protected virtual void OnUpdateGameState(in CancellationToken cancellationToken)
        {
        }

        /// <summary>
        ///     Gets a render state for the current game state.
        /// </summary>
        /// <returns>Returns a render state.</returns>
        protected virtual TRenderState? OnGetRenderState()
        {
            return null;
        }

        /// <inheritdoc cref="InitializeRenderResources" />
        protected virtual void OnInitializeRenderResources(in DirectXResources resources)
        {
        }

        /// <inheritdoc cref="ResizeRenderResources" />
        protected virtual void OnResizeRenderResources(in DirectXResources resources, in D2D_SIZE_U clientSize)
        {
        }

        /// <inheritdoc cref="ReleaseRenderResources" />
        protected virtual void OnReleaseRenderResources()
        {
        }

        /// <inheritdoc cref="Render" />
        /// <param name="resources">DirectX resources.</param>
        /// <param name="renderState">The render state to render.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns the result of the entity's render attempt.</returns>
        protected virtual EntityRenderResult OnRender(in DirectXResources resources, in TRenderState renderState, in CancellationToken cancellationToken)
        {
            return EntityRenderResult.FrameSkipped;
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

        /// <summary>Determines if the entity should render.</summary>
        /// <param name="renderState">The render state to render.</param>
        /// <returns>Returns a value indicating whether the entity should render.</returns>
        private bool ShouldRender(out TRenderState? renderState)
        {
            renderState = null;

            if (IsPaused && !RenderWhenPaused || IsSuspended && !RenderWhenSuspended)
            {
                return false;
            }

            lock (_renderStateLockObject)
            {
                if (_renderState is null)
                {
                    return false;
                }

                // Consume the render state

                renderState = _renderState.Value;
                _renderState = null;
            }

            return true;
        }
    }
}