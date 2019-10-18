using System;
using System.Diagnostics;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Messaging;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Entities.Renderers
{
    /// <inheritdoc />
    public abstract class Renderer<TRenderState> : IRenderer<TRenderState>
        where TRenderState : class
    {
        private readonly object _directXResourcesLockObject = new object();
        private DirectXResources? _directXResources;
        private bool _isDisposed;
        private bool _isInitialized;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Renderer{TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <param name="order">The entity order. The order must be unique for all renderers in an <see cref="EntityCollection{TEntity}" />.</param>
        protected Renderer(IInterfaces interfaces, NestedContext context, uint order = 0)
        {
            Interfaces = interfaces;
            Context = context;
            Order = order;
            GlobalMessagePublisherSubscriber = ConcurrentMessagePublisherSubscriber<IGlobalMessage>.Create(interfaces, context);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Renderer{TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="order">The entity order. The order must be unique for all renderers in an <see cref="EntityCollection{TEntity}" />.</param>
        protected Renderer(IInterfaces interfaces, uint order = 0)
            : this(interfaces, new NestedContext("Renderer"), order)
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
        ///     <para>Gets the global message publisher/subscriber.</para>
        ///     <para>Use the global message queue to publish or subscribe to messages intended to be processed globally.</para>
        ///     <para>Do not use the global message queue to send update-queue-specific messages.</para>
        /// </summary>
        protected ConcurrentMessagePublisherSubscriber<IGlobalMessage> GlobalMessagePublisherSubscriber { get; }

        /// <summary>
        ///     Gets a value indicating if the renderer is neither paused nor suspended.
        /// </summary>
        protected bool IsRunning => !IsPaused && !IsSuspended;

        /// <summary>
        ///     Gets a value indicating if the renderer is paused.
        /// </summary>
        protected bool IsPaused { get; private set; }

        /// <summary>
        ///     Gets a value indicating if the renderer is suspended.
        /// </summary>
        protected bool IsSuspended { get; private set; }

        /// <summary>
        ///     Gets or sets a value that determines whether to render when the game execution state is paused.
        /// </summary>
        protected virtual bool RenderWhenPaused { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value that determines whether to render when the game execution state is suspended.
        /// </summary>
        protected virtual bool RenderWhenSuspended { get; set; } = false;

        /// <inheritdoc />
        public uint Order { get; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the renderer resources thread.</exception>
        public void InitializeResources(DirectXResources resources)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RendererResources);

            OnInitializeResources(resources);

            lock (_directXResourcesLockObject)
            {
                _directXResources = resources;
            }

            _isInitialized = true;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the renderer resources thread.</exception>
        public void ResizeResources(D2D_SIZE_U clientSize)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RendererResources);

            OnResizeResources(clientSize);

            lock (_directXResourcesLockObject)
            {
                Debug.Assert(_directXResources != null);

                _directXResources = new DirectXResources(_directXResources.Value, clientSize);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the renderer resources thread.</exception>
        public void ReleaseResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RendererResources);

            OnReleaseResources();

            lock (_directXResourcesLockObject)
            {
                _directXResources = null;
            }

            _isInitialized = false;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Renders a render state unless a combination of <see cref="IsPaused" />, <see cref="IsSuspended" />,
        ///     <see cref="RenderWhenPaused" />, and <see cref="RenderWhenSuspended" /> values determine that rendering should not occur.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Render(TRenderState renderState)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            if (!ShouldRender())
            {
                return;
            }

            DirectXResources resources;

            lock (_directXResourcesLockObject)
            {
                Debug.Assert(_directXResources != null);

                resources = _directXResources.Value;
            }

            OnRender(resources, renderState);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Pause()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            IsPaused = true;

            Debug.Assert(_directXResources != null);

            OnPause(_directXResources.Value);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Unpause()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            IsPaused = false;

            Debug.Assert(_directXResources != null);

            OnUnpause(_directXResources.Value);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Suspend()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            IsSuspended = true;

            Debug.Assert(_directXResources != null);

            OnSuspend(_directXResources.Value);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Resume()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            IsSuspended = false;

            Debug.Assert(_directXResources != null);

            OnResume(_directXResources.Value);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(GlobalMessagePublisherSubscriber.Dispose, ref _isDisposed, Interfaces.ThreadManager, ProcessThread.Main);
        }

        /// <inheritdoc cref="IRenderer.InitializeResources" />
        protected virtual void OnInitializeResources(DirectXResources resources)
        {
        }

        /// <inheritdoc cref="IRenderer.ResizeResources" />
        protected virtual void OnResizeResources(D2D_SIZE_U clientSize)
        {
        }

        /// <inheritdoc cref="IRenderer.ReleaseResources" />
        protected virtual void OnReleaseResources()
        {
        }

        /// <inheritdoc cref="IRenderer{TRenderState}.Render" />
        protected abstract void OnRender(DirectXResources resources, TRenderState renderState);

        /// <inheritdoc cref="IEntity.Pause" />
        protected virtual void OnPause(DirectXResources resources)
        {
        }

        /// <inheritdoc cref="IEntity.Unpause" />
        protected virtual void OnUnpause(DirectXResources resources)
        {
        }

        /// <inheritdoc cref="IEntity.Suspend" />
        protected virtual void OnSuspend(DirectXResources resources)
        {
        }

        /// <inheritdoc cref="IEntity.Resume" />
        protected virtual void OnResume(DirectXResources resources)
        {
        }

        /// <summary>
        ///     Determines if the entity should render a render state.
        /// </summary>
        /// <returns>Returns a value indicating whether the entity should render a render state.</returns>
        private bool ShouldRender()
        {
            return _isInitialized && (!IsPaused || RenderWhenPaused) && (!IsSuspended || RenderWhenSuspended);
        }
    }
}