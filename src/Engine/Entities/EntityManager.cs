using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.Engine.Entities.Renderers;
using BouncyBox.VorpalEngine.Engine.Entities.Updaters;
using BouncyBox.VorpalEngine.Engine.Game;
using TerraFX.Interop;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Manages all entities and their interactions with the engine.</summary>
    public class EntityManager<TGameState, TRenderState> : IEntityManager<TGameState, TRenderState>
        where TGameState : class
        where TRenderState : class, new()
    {
        private readonly IGameExecutionStateManager _gameExecutionStateManager;
        private readonly IInterfaces _interfaces;
        private readonly EntityCollection<IRenderer<TRenderState>> _renderers = new EntityCollection<IRenderer<TRenderState>>();
        private readonly object _renderersLockObject = new object();
        private readonly IRenderStateManager<TRenderState> _renderStateManager;
        private readonly EntityCollection<IUpdater<TRenderState>> _updaters = new EntityCollection<IUpdater<TRenderState>>();
        private readonly object _updatersLockObject = new object();
        private DirectXResources? _directXResources;

        /// <summary>Initializes a new instance of the <see cref="EntityManager{TGameState,TRenderState}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        public EntityManager(IInterfaces interfaces, IGameExecutionStateManager gameExecutionStateManager, IRenderStateManager<TRenderState> renderStateManager)
        {
            _interfaces = interfaces;
            _gameExecutionStateManager = gameExecutionStateManager;
            _renderStateManager = renderStateManager;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Add(IEnumerable<IUpdater<TRenderState>> updaters)
        {
            updaters = updaters.ToImmutableArray();

            PrepareForAdd(updaters);

            lock (_updatersLockObject)
            {
                _updaters.Add(updaters);
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Add(params IUpdater<TRenderState>[] updaters)
        {
            return Add((IEnumerable<IUpdater<TRenderState>>)updaters);
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Add(IEnumerable<IRenderer<TRenderState>> renderers)
        {
            renderers = renderers.ToImmutableArray();

            PrepareForAdd(renderers);

            lock (_renderersLockObject)
            {
                _renderers.Add(renderers);
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Add(params IRenderer<TRenderState>[] renderers)
        {
            return Add((IEnumerable<IRenderer<TRenderState>>)renderers);
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Remove(IEnumerable<IUpdater<TRenderState>> updaters)
        {
            lock (_updatersLockObject)
            {
                _updaters.Remove(updaters);
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Remove(params IUpdater<TRenderState>[] updaters)
        {
            return Remove((IEnumerable<IUpdater<TRenderState>>)updaters);
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Remove(IEnumerable<IRenderer<TRenderState>> renderers)
        {
            lock (_renderersLockObject)
            {
                _renderers.Remove(renderers);
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState, TRenderState> Remove(params IRenderer<TRenderState>[] renderers)
        {
            return Remove((IEnumerable<IRenderer<TRenderState>>)renderers);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Update(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            ImmutableArray<IUpdater<TRenderState>> updaters;

            lock (_renderersLockObject)
            {
                updaters = _updaters.SortedByOrder.ToImmutableArray();
            }

            foreach (IUpdater<TRenderState> updater in updaters)
            {
                updater.UpdateGameState(cancellationToken);
            }

            var renderState = new TRenderState();

            foreach (IUpdater<TRenderState> updater in updaters)
            {
                updater.PrepareRenderState(renderState);
            }

            _renderStateManager.ProvideNextRenderState(renderState);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the renderer resources thread.</exception>
        public void InitializeRendererResources(DirectXResources resources)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RendererResources);

            ImmutableArray<IRenderer<TRenderState>> renderers;

            lock (_renderersLockObject)
            {
                _directXResources = resources;
                renderers = _renderers.ToImmutableArray();
            }

            foreach (IRenderer<TRenderState> renderer in renderers)
            {
                renderer.InitializeResources(resources);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the renderer resources thread.</exception>
        public void ResizeRendererResources(D2D_SIZE_U clientSize)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RendererResources);

            ImmutableArray<IRenderer<TRenderState>> renderers;

            lock (_renderersLockObject)
            {
                Debug.Assert(_directXResources != null);

                _directXResources = new DirectXResources(_directXResources.Value, clientSize);
                renderers = _renderers.ToImmutableArray();
            }

            foreach (IRenderer<TRenderState> renderer in renderers)
            {
                renderer.ResizeResources(clientSize);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the renderer resources thread.</exception>
        public void ReleaseRendererResources()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.RendererResources);

            ImmutableArray<IRenderer<TRenderState>> renderers;

            lock (_renderersLockObject)
            {
                _directXResources = null;
                renderers = _renderers.ToImmutableArray();
            }

            foreach (IRenderer<TRenderState> renderer in renderers)
            {
                renderer.ReleaseResources();
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public unsafe (RenderResult result, TimeSpan frametime) Render(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            if (_directXResources == null)
            {
                // DirectX resources have not yet been initialized
                return (RenderResult.FrameSkipped, TimeSpan.Zero);
            }

            // Ensure there is a state to render
            TRenderState? renderState = _renderStateManager.GetNextRenderState();

            if (renderState == null)
            {
                // No render state was available to render
                return (RenderResult.FrameSkipped, TimeSpan.Zero);
            }

            ImmutableArray<IRenderer<TRenderState>> renderers;

            lock (_renderersLockObject)
            {
                renderers = _renderers.SortedByOrder.ToImmutableArray();
            }

            // Frametime measurements should start only after a render state is retrieved
            long startTimestamp = Stopwatch.GetTimestamp();

            // Begin drawing

            _directXResources.Value.D2D1DeviceContext.BeginDraw();
            _directXResources.Value.D2D1DeviceContext.Clear();

            // Render renderers
            foreach (IRenderer<TRenderState> renderer in renderers)
            {
                renderer.Render(renderState, cancellationToken);
            }

            // End drawing
            int endDrawResult = _directXResources.Value.D2D1DeviceContext.EndDraw();

            // Calculate frametime
            TimeSpan frametime = TimeSpan.FromTicks(Stopwatch.GetTimestamp() - startTimestamp);

            // Check if the render target needs to be recreated
            if (endDrawResult == TerraFX.Interop.Windows.D2DERR_RECREATE_TARGET)
            {
                return (RenderResult.RecreateTarget, TimeSpan.Zero);
            }

            ComObject.CheckResultHandle(endDrawResult, "Failed to end drawing.");

            // Present

            using D2D1Factory d2d1Factory = _directXResources.Value.D2D1Device.GetFactory();
            using D2D1Multithread d2d1Multithread = d2d1Factory.QueryD2D1Multithread()!;

            // Ensure underlying DXGI and Direct3D resources are safe during presentation
            d2d1Multithread.Enter();

            try
            {
                _directXResources.Value.DXGISwapChain1.Present(_interfaces.CommonGameSettings.EnableVSync ? 1u : 0u);
            }
            finally
            {
                // Release the lock
                d2d1Multithread.Leave();
            }

            return (RenderResult.FrameRendered, frametime);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Main);

            _renderers?.Dispose();
            _updaters?.Dispose();
        }

        /// <summary>Prepares entities for adding by informing them of game execution state changes.</summary>
        /// <param name="entities"></param>
        private void PrepareForAdd(IEnumerable<IEntity> entities)
        {
            GameExecutionState gameExecutionState = _gameExecutionStateManager.GameExecutionState;

            foreach (IEntity entity in entities)
            {
                if (gameExecutionState.IsPaused)
                {
                    entity.Pause();
                }
                if (gameExecutionState.IsSuspended)
                {
                    entity.Suspend();
                }
            }
        }
    }
}