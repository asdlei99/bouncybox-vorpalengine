using System;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>An engine thread worker that manages requests to initialize, resize, and release DirectX resources.</summary>
    internal sealed class RenderResourcesWorker<TGameState> : EngineThreadWorker
        where TGameState : class
    {
        private readonly IEntityManager<TGameState> _entityManager;
        private readonly TimeSpan _sleepDuration = TimeSpan.FromMilliseconds(10);

        /// <summary>Initializes a new instance of the <see cref="RenderResourcesWorker{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public RenderResourcesWorker(IInterfaces interfaces, IEntityManager<TGameState> entityManager, NestedContext context)
            : base(interfaces, EngineThread.RenderResources, context.CopyAndPush(nameof(RenderResourcesWorker<TGameState>)))
        {
            _entityManager = entityManager;
        }

        /// <summary>Initializes a new instance of the <see cref="RenderResourcesWorker{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        public RenderResourcesWorker(IInterfaces interfaces, IEntityManager<TGameState> entityManager)
            : this(interfaces, entityManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        protected override void OnDoWork(CancellationToken cancellationToken)
        {
            // Handle dispatched messages
            _entityManager.HandleDispatchedRenderResourcesMessages();

            // Reduce CPU utilization
            cancellationToken.WaitHandle.WaitOne(_sleepDuration);
        }

        /// <inheritdoc />
        protected override void OnCleanUp()
        {
            _entityManager.ReleaseRenderResources(CancellationToken.None);
        }
    }
}