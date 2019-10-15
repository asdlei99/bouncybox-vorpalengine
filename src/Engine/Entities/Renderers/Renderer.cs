using System;
using System.Diagnostics;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Messaging;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Entities.Renderers
{
    /// <inheritdoc />
    public abstract class Renderer<TRenderState> : IRenderer<TRenderState>
        where TRenderState : class
    {
        private DirectXResources? _directXResources;
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Renderer{TRenderState}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <param name="order">The entity order. The order must be unique for all renderers in an <see cref="EntityCollection{TEntity}" />.</param>
        protected Renderer(IInterfaces interfaces, NestedContext context, uint order = 0)
        {
            Interfaces = interfaces;
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
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void InitializeResources(DirectXResources resources)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            _directXResources = resources;

            OnInitializeResources(resources);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void ResizeResources(D2D_SIZE_U clientSize)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            Debug.Assert(_directXResources != null);

            _directXResources = new DirectXResources(_directXResources.Value, clientSize);

            OnResizeResources(_directXResources.Value, clientSize);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void ReleaseResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            _directXResources = null;

            OnReleaseResources();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Renders a render state unless a combination of <see cref="IsPaused" />, <see cref="IsSuspended" />,
        ///     <see cref="RenderWhenPaused" />, and <see cref="RenderWhenSuspended" /> values determine that rendering should not occur.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Render(TRenderState renderState, IEngineStats engineStats)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            if (!ShouldRender())
            {
                return;
            }

            Debug.Assert(_directXResources != null);

            OnRender(_directXResources.Value, renderState, engineStats);
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

        /// <summary>
        ///     Initializes DirectX resources.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to initialize other resources.</param>
        protected virtual void OnInitializeResources(DirectXResources resources)
        {
        }

        /// <summary>
        ///     Resizes resources to account for the new render window client size.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to initialize other resources.</param>
        /// <param name="clientSize">The size of the render window's client area.</param>
        protected virtual void OnResizeResources(DirectXResources resources, D2D_SIZE_U clientSize)
        {
        }

        /// <summary>
        ///     Releases resources created by this entity.
        /// </summary>
        protected virtual void OnReleaseResources()
        {
        }

        /// <summary>
        ///     Renders the entity.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to render.</param>
        /// <param name="renderState">The render state to render.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        protected abstract void OnRender(DirectXResources resources, TRenderState renderState, IEngineStats engineStats);

        /// <summary>
        ///     Allows the entity to respond to the game execution state being paused.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to render.</param>
        protected virtual void OnPause(DirectXResources resources)
        {
        }

        /// <summary>
        ///     Allows the entity to respond to the game execution state being unpaused.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to render.</param>
        protected virtual void OnUnpause(DirectXResources resources)
        {
        }

        /// <summary>
        ///     Allows the entity to respond to the game execution state being suspended.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to render.</param>
        protected virtual void OnSuspend(DirectXResources resources)
        {
        }

        /// <summary>
        ///     Allows the entity to respond to the game execution state being resumed.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to render.</param>
        protected virtual void OnResume(DirectXResources resources)
        {
        }

        private bool ShouldRender()
        {
            return (!IsPaused || RenderWhenPaused) && (!IsSuspended || RenderWhenSuspended);
        }
    }
}