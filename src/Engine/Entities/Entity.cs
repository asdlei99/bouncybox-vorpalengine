using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>An entity may update and/or render game state.</summary>
    public abstract class Entity : IEntity
    {
        private readonly IInterfaces _interfaces;
        private readonly object _renderResourcesLockObject = new object();
        private DirectXResources? _directXResources;
        private bool _isDisposed;
        private bool _isInitialized;

        /// <summary>Initializes a new instance of the <see cref="Entity" /></summary>
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
        protected Entity(IInterfaces interfaces, uint updateOrder, uint renderOrder, NestedContext context)
        {
            _interfaces = interfaces;
            UpdateOrder = updateOrder;
            RenderOrder = renderOrder;
            Context = context;

            GlobalMessagePublisherSubscriber = ConcurrentMessagePublisherSubscriber<IGlobalMessage>.Create(interfaces, context);
            UpdateMessagePublisherSubscriber = MessagePublisherSubscriber<IUpdateMessage>.Create(interfaces.UpdateMessageQueue, context);
        }

        /// <summary>Initializes a new instance of the <see cref="Entity" /></summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="updateOrder">
        ///     The entity's position in update order, which is determined by sorting all entities' update orders in
        ///     ascending order.
        /// </param>
        /// <param name="renderOrder">
        ///     The entity's position in render order, which is determined by sorting all entities' render orders in
        ///     ascending order.
        /// </param>
        protected Entity(IInterfaces interfaces, uint updateOrder, uint renderOrder) : this(interfaces, updateOrder, renderOrder, NestedContext.None())
        {
        }

        /// <summary>Gets the nested context.</summary>
        protected NestedContext Context { get; }

        /// <summary>
        ///     <para>Gets the <see cref="IKeyboard" /> implementation.</para>
        ///     <para>Use this property to interact with the keyboard.</para>
        /// </summary>
        protected IKeyboard Keyboard => _interfaces.Keyboard;

        /// <summary>
        ///     <para>Gets the <see cref="IStatefulGamepad" /> implementation.</para>
        ///     <para>Use this property to interact with a gamepad.</para>
        /// </summary>
        protected IStatefulGamepad StatefulGamepad => _interfaces.StatefulGamepad;

        /// <summary>
        ///     <para>Gets the global message publisher/subscriber.</para>
        ///     <para>Use the global message queue to publish or subscribe to messages intended to be processed globally.</para>
        ///     <para>Do not use the global message queue to send update-queue-specific messages.</para>
        /// </summary>
        protected ConcurrentMessagePublisherSubscriber<IGlobalMessage> GlobalMessagePublisherSubscriber { get; }

        /// <summary>
        ///     <para>Gets the update message publisher/subscriber.</para>
        ///     <para>
        ///         Use the update message queue to publish or subscribe to messages intended to be processed only by entities while
        ///         updating the game state.
        ///     </para>
        ///     <para>Do not use the global message queue to send global-queue-specific messages.</para>
        /// </summary>
        protected MessagePublisherSubscriber<IUpdateMessage> UpdateMessagePublisherSubscriber { get; }

        /// <summary>Gets a value indicating if the entity is neither paused nor suspended.</summary>
        protected bool IsRunning => !IsPaused && !IsSuspended;

        /// <summary>Gets a value indicating if the entity is paused.</summary>
        protected bool IsPaused { get; private set; }

        /// <summary>Gets a value indicating if the entity is suspended.</summary>
        protected bool IsSuspended { get; private set; }

        /// <summary>Gets a value that determines whether to update when the game execution state is paused.</summary>
        protected virtual bool UpdateWhenPaused { get; } = false;

        /// <summary>Gets a value that determines whether to update when the game execution state is suspended.</summary>
        protected virtual bool UpdateWhenSuspended { get; } = false;

        /// <summary>Gets a value that determines whether to render when the game execution state is paused.</summary>
        protected virtual bool RenderWhenPaused { get; } = true;

        /// <summary>Gets a value that determines whether to render when the game execution state is suspended.</summary>
        protected virtual bool RenderWhenSuspended { get; } = false;

        /// <inheritdoc />
        public uint UpdateOrder { get; }

        /// <inheritdoc />
        public uint RenderOrder { get; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="Threads.ProcessThread.Update" /> thread.
        /// </exception>
        public void Pause()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (IsPaused)
            {
                return;
            }

            IsPaused = true;

            OnPause();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Unpause()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (!IsPaused)
            {
                return;
            }

            IsPaused = false;

            OnUnpause();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Suspend()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (IsSuspended)
            {
                return;
            }

            IsSuspended = true;

            OnSuspend();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Resume()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            if (!IsSuspended)
            {
                return;
            }

            IsSuspended = false;

            OnResume();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void InitializeUpdateResources()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnInitializeUpdaterResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void ResizeUpdateResources(Size clientSize)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnResizeUpdateResources(clientSize);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void ReleaseUpdateResources()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnReleaseUpdateResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public UpdateGameStateResult UpdateGameState(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            // Handle dispatched messages
            GlobalMessagePublisherSubscriber.HandleDispatched();

            return ShouldUpdateGameState() ? OnUpdateGameState(cancellationToken) : UpdateGameStateResult.DoNotRender;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public Action<DirectXResources, CancellationToken>? GetRenderDelegate()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            return ShouldGetRenderDelegate() ? OnGetRenderDelegate() : null;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void InitializeRenderResources(DirectXResources resources)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            lock (_renderResourcesLockObject)
            {
                _directXResources = resources;
                _isInitialized = true;
            }

            OnInitializeRenderResources(resources);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void ResizeRenderResources(D2D_SIZE_U clientSize)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            lock (_renderResourcesLockObject)
            {
                Debug.Assert(_directXResources != null);

                _directXResources = new DirectXResources(_directXResources.Value, clientSize);
            }

            OnResizeRenderResources(clientSize);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.RenderResources" /> thread.
        /// </exception>
        public void ReleaseRenderResources()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            lock (_renderResourcesLockObject)
            {
                _directXResources = null;
                _isInitialized = false;
            }

            OnReleaseRenderResources();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        public void Dispose()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            DisposeHelper.Dispose(
                () =>
                {
                    GlobalMessagePublisherSubscriber.Dispose();
                    UpdateMessagePublisherSubscriber.Dispose();
                },
                ref _isDisposed);
        }

        /// <inheritdoc cref="Pause" />
        protected virtual void OnPause()
        {
        }

        /// <inheritdoc cref="Unpause" />
        protected virtual void OnUnpause()
        {
        }

        /// <inheritdoc cref="Suspend" />
        protected virtual void OnSuspend()
        {
        }

        /// <inheritdoc cref="Resume" />
        protected virtual void OnResume()
        {
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
        protected virtual UpdateGameStateResult OnUpdateGameState(CancellationToken cancellationToken)
        {
            return UpdateGameStateResult.DoNotRender;
        }

        /// <inheritdoc cref="GetRenderDelegate" />
        protected virtual Action<DirectXResources, CancellationToken>? OnGetRenderDelegate()
        {
            return null;
        }

        /// <inheritdoc cref="InitializeRenderResources" />
        protected virtual void OnInitializeRenderResources(DirectXResources resources)
        {
        }

        /// <inheritdoc cref="ResizeRenderResources" />
        protected virtual void OnResizeRenderResources(D2D_SIZE_U clientSize)
        {
        }

        /// <inheritdoc cref="ReleaseRenderResources" />
        protected virtual void OnReleaseRenderResources()
        {
        }

        /// <summary>Determines if the entity should update the game state.</summary>
        /// <returns>Returns a value indicating whether the entity should update the game state.</returns>
        private bool ShouldUpdateGameState()
        {
            return (!IsPaused || UpdateWhenPaused) && (!IsSuspended || UpdateWhenSuspended);
        }

        /// <summary>Determines if the entity should get a render delegate.</summary>
        /// <returns>Returns a value indicating whether the entity should get a render delegate.</returns>
        private bool ShouldGetRenderDelegate()
        {
            bool isInitialized;

            lock (_renderResourcesLockObject)
            {
                isInitialized = _isInitialized;
            }

            return isInitialized && (!IsPaused || RenderWhenPaused) && (!IsSuspended || RenderWhenSuspended);
        }
    }
}