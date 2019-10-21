using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>An entity that renders itself.</summary>
    public class RenderingEntity : Entity, IRenderingEntity
    {
        private readonly object _renderResourcesLockObject = new object();
        private DirectXResources? _directXResources;

        /// <summary>Initializes a new instance of the <see cref="RenderingEntity" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="renderOrder">
        ///     The entity's position in render order, which is determined by sorting all entities' render orders in
        ///     ascending order.
        /// </param>
        /// <param name="context">A nested context.</param>
        protected RenderingEntity(IInterfaces interfaces, uint renderOrder, NestedContext context) : base(interfaces, context)
        {
            RenderOrder = renderOrder;
        }

        /// <summary>Initializes a new instance of the <see cref="RenderingEntity" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="renderOrder">
        ///     The entity's position in render order, which is determined by sorting all entities' render orders in
        ///     ascending order.
        /// </param>
        protected RenderingEntity(IInterfaces interfaces, uint renderOrder) : this(interfaces, renderOrder, NestedContext.None())
        {
        }

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
        public void InitializeRenderResources(DirectXResources resources)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            lock (_renderResourcesLockObject)
            {
                _directXResources = resources;
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
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

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
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            lock (_renderResourcesLockObject)
            {
                _directXResources = null;
            }

            OnReleaseRenderResources();
        }

        /// <inheritdoc />
        public EntityRenderResult Render(CancellationToken cancellationToken)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            if (!ShouldRender(out DirectXResources? resources))
            {
                return EntityRenderResult.FrameSkipped;
            }

            Debug.Assert(resources != null);

            return OnRender(resources.Value, cancellationToken);
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

        /// <inheritdoc cref="Render" />
        /// <param name="resources">DirectX resources.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns the result of the entity's render attempt.</returns>
        protected virtual EntityRenderResult OnRender(DirectXResources resources, CancellationToken cancellationToken)
        {
            return EntityRenderResult.FrameSkipped;
        }

        /// <summary>Determines if the entity should render.</summary>
        /// <param name="resources">The DirectX resources to use when rendering.</param>
        /// <returns>Returns a value indicating whether the entity should render.</returns>
        private bool ShouldRender(out DirectXResources? resources)
        {
            lock (_renderResourcesLockObject)
            {
                if (_directXResources == null || IsPaused && !RenderWhenPaused || IsSuspended && !RenderWhenSuspended)
                {
                    resources = null;
                    return false;
                }

                resources = _directXResources.Value;
                return true;
            }
        }
    }
}