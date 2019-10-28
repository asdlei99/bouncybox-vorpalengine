using System;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Threads;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>An entity that renders itself.</summary>
    public class RenderingEntity : Entity, IRenderingEntity
    {
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
        public void ReleaseRenderResources()
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RenderResources);

            OnReleaseRenderResources();
        }

        /// <inheritdoc />
        public EntityRenderResult Render(in DirectXResources resources, in CancellationToken cancellationToken)
        {
            Interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            return ShouldRender() ? OnRender(resources, cancellationToken) : EntityRenderResult.FrameSkipped;
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
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns the result of the entity's render attempt.</returns>
        protected virtual EntityRenderResult OnRender(in DirectXResources resources, in CancellationToken cancellationToken)
        {
            return EntityRenderResult.FrameSkipped;
        }

        /// <summary>Determines if the entity should render.</summary>
        /// <returns>Returns a value indicating whether the entity should render.</returns>
        private bool ShouldRender()
        {
            return (!IsPaused || RenderWhenPaused) && (!IsSuspended || RenderWhenSuspended);
        }
    }
}