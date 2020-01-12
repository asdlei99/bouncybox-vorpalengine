using System;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>An engine thread worker that manages requests to initialize, resize, and release DirectX resources.</summary>
    internal sealed class RenderResourcesWorker<TGameState> : EngineThreadWorker
        where TGameState : class
    {
        private readonly IDirectXResourceManager<TGameState> _directXResourceManager;
        private readonly TimeSpan _sleepDuration = TimeSpan.FromMilliseconds(10);

        /// <summary>Initializes a new instance of the <see cref="RenderResourcesWorker{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="directXResourceManager">An <see cref="IDirectXResourceManager{TGameState}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public RenderResourcesWorker(IInterfaces interfaces, IDirectXResourceManager<TGameState> directXResourceManager, NestedContext context)
            : base(interfaces, EngineThread.RenderResources, context.Push(nameof(RenderResourcesWorker<TGameState>)))
        {
            _directXResourceManager = directXResourceManager;
        }

        /// <summary>Initializes a new instance of the <see cref="RenderResourcesWorker{TGameState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="directXResourceManager">An <see cref="IDirectXResourceManager{TGameState}" /> implementation.</param>
        public RenderResourcesWorker(IInterfaces interfaces, IDirectXResourceManager<TGameState> directXResourceManager)
            : this(interfaces, directXResourceManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        protected override void OnDoWork(CancellationToken cancellationToken)
        {
            // Handle dispatched messages
            _directXResourceManager.HandleDispatchedRenderResourcesMessages();

            // Reduce CPU utilization
            cancellationToken.WaitHandle.WaitOne(_sleepDuration);
        }

        /// <inheritdoc />
        protected override void OnCleanUp()
        {
            _directXResourceManager.ReleaseRenderResources(CancellationToken.None);
        }
    }
}