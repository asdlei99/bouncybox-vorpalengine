using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Entities.Renderers;
using BouncyBox.VorpalEngine.Engine.Entities.Updaters;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Threads;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>
    ///     A collection of updaters and renderers that form one logical game unit.
    /// </summary>
    public abstract class Scene<TGameState, TRenderState, TSceneKey> : IScene<TGameState, TRenderState, TSceneKey>
        where TGameState : class
        where TRenderState : class, new()
        where TSceneKey : struct, Enum
    {
        private readonly IInterfaces _interfaces;
        private readonly IEntityCollection<IRenderer<TRenderState>> _renderers;
        private readonly ConcurrentQueue<Action> _renderQueue = new ConcurrentQueue<Action>();
        private readonly ConcurrentQueue<Action> _updateQueue = new ConcurrentQueue<Action>();
        private readonly IEntityCollection<IUpdater<TGameState, TRenderState>> _updaters;
        private DirectXResources? _directXResources;
        private bool _isDisposed;
        private bool _isLoaded;
        private GameExecutionState _previousRenderGameExecutionState;
        private GameExecutionState _previousUpdateGameExecutionState;
        private bool _resourcesInitialized;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Scene{TGameState,TRenderState,TSceneKey}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="key">The scene's key.</param>
        /// <param name="updateOrder">The order in which to update scenes when compared to other scenes.</param>
        /// <param name="renderOrder">The order in which to render scenes when compared to other scenes.</param>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        protected Scene(IInterfaces interfaces, TSceneKey key, uint updateOrder = 0, uint renderOrder = 0)
        {
            // This constructor must execute on the main thread
            interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Main);

            _interfaces = interfaces;
            _updaters = new EntityCollection<IUpdater<TGameState, TRenderState>>(
                // Provide a delegate that will pause and/or suspend updaters, if necessary, as soon as they are added to the entity collection
                a =>
                {
                    if (_previousUpdateGameExecutionState.IsPaused)
                    {
                        a.Pause();
                    }
                    if (_previousUpdateGameExecutionState.IsSuspended)
                    {
                        a.Suspend();
                    }
                });
            _renderers = new EntityCollection<IRenderer<TRenderState>>(
                // Provide a delegate that will initialize DirectX resources and pause and/or suspend renderers, if necessary,
                // as soon as they are added to the entity collection
                a =>
                {
                    if (_directXResources != null)
                    {
                        a.InitializeResources(_directXResources.Value);
                    }
                    if (_previousRenderGameExecutionState.IsPaused)
                    {
                        a.Pause();
                    }
                    if (_previousRenderGameExecutionState.IsSuspended)
                    {
                        a.Suspend();
                    }
                });

            Key = key;
            UpdateOrder = updateOrder;
            RenderOrder = renderOrder;
        }

        /// <inheritdoc />
        public TSceneKey Key { get; }

        /// <inheritdoc />
        public uint UpdateOrder { get; }

        /// <inheritdoc />
        public uint RenderOrder { get; }

        /// <inheritdoc />
        public bool IsReadyForRender => _isLoaded && _resourcesInitialized;

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Load(TGameState gameState)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnLoad(gameState);

            _isLoaded = true;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Unload(TGameState gameState)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnUnload(gameState);

            _isLoaded = false;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void UpdateGameState(TGameState gameState, GameExecutionState gameExecutionState)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            // Process the update queue, which will add and remove updaters
            while (_updateQueue.TryDequeue(out Action? @delegate))
            {
                @delegate();
            }

            foreach (IUpdater<TGameState, TRenderState> updater in _updaters.SortedByOrder)
            {
                // Process changes in game execution state

                if (gameExecutionState.IsPaused && !_previousUpdateGameExecutionState.IsPaused)
                {
                    updater.Pause();
                }
                else if (!gameExecutionState.IsPaused && _previousUpdateGameExecutionState.IsPaused)
                {
                    updater.Unpause();
                }
                if (gameExecutionState.IsSuspended && !_previousUpdateGameExecutionState.IsSuspended)
                {
                    updater.Suspend();
                }
                else if (!gameExecutionState.IsSuspended && _previousUpdateGameExecutionState.IsSuspended)
                {
                    updater.Resume();
                }

                // Update game state
                updater.UpdateGameState(gameState);
            }

            _previousUpdateGameExecutionState = gameExecutionState;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void PrepareRenderState(TGameState gameState, TRenderState renderState)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            foreach (IUpdater<TGameState, TRenderState> updater in _updaters.SortedByOrder)
            {
                // Prepare render state
                updater.PrepareRenderState(gameState, renderState);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void InitializeResources(DirectXResources resources)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            foreach (IRenderer<TRenderState> renderer in _renderers.SortedByOrder)
            {
                renderer.InitializeResources(resources);
            }

            _directXResources = resources;
            _resourcesInitialized = true;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void ResizeResources(D2D_SIZE_U clientSize)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            foreach (IRenderer<TRenderState> renderer in _renderers.SortedByOrder)
            {
                renderer.ResizeResources(clientSize);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void ReleaseResources()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            foreach (IRenderer<TRenderState> renderer in _renderers.SortedByOrder)
            {
                renderer.ReleaseResources();
            }

            _directXResources = null;
            _resourcesInitialized = false;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _updaters.Dispose();
                    _renderers.Dispose();
                },
                ref _isDisposed,
                _interfaces.ThreadManager,
                ProcessThread.Main);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Render(TRenderState renderState, GameExecutionState gameExecutionState, IEngineStats engineStats)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            // Process the render queue, which will add and remove renderers
            while (_renderQueue.TryDequeue(out Action? @delegate))
            {
                @delegate();
            }

            foreach (IRenderer<TRenderState> renderer in _renderers.SortedByOrder)
            {
                if (gameExecutionState.IsPaused && !_previousRenderGameExecutionState.IsPaused)
                {
                    renderer.Pause();
                }
                else if (!gameExecutionState.IsPaused && _previousRenderGameExecutionState.IsPaused)
                {
                    renderer.Unpause();
                }
                if (gameExecutionState.IsSuspended && !_previousRenderGameExecutionState.IsSuspended)
                {
                    renderer.Suspend();
                }
                else if (!gameExecutionState.IsSuspended && _previousRenderGameExecutionState.IsSuspended)
                {
                    renderer.Resume();
                }

                renderer.Render(renderState, engineStats);
            }

            _previousRenderGameExecutionState = gameExecutionState;
        }

        /// <summary>
        ///     Initializes the parts of the game state owned by the scene.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        protected virtual void OnLoad(TGameState gameState)
        {
        }

        /// <summary>
        ///     Uninitializes the parts of the game state owned by the scene.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        protected virtual void OnUnload(TGameState gameState)
        {
        }

        /// <summary>
        ///     Adds an updater to the scene.
        /// </summary>
        /// <param name="updater">The updater to add.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> AddUpdater(IUpdater<TGameState, TRenderState> updater)
        {
            _updateQueue.Enqueue(() => _updaters.Add(updater));

            return this;
        }

        /// <summary>
        ///     Adds updaters to the scene.
        /// </summary>
        /// <param name="updaters">The updaters to add.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> AddUpdaters(IEnumerable<IUpdater<TGameState, TRenderState>> updaters)
        {
            _updateQueue.Enqueue(() => _updaters.Add(updaters));

            return this;
        }

        /// <summary>
        ///     Adds updaters to the scene.
        /// </summary>
        /// <param name="updaters">The updaters to add.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> AddUpdaters(params IUpdater<TGameState, TRenderState>[] updaters)
        {
            _updateQueue.Enqueue(() => _updaters.Add(updaters));

            return this;
        }

        /// <summary>
        ///     Adds a renderer to the scene.
        /// </summary>
        /// <param name="renderer">The renderer to add.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> AddRenderer(IRenderer<TRenderState> renderer)
        {
            _renderQueue.Enqueue(() => _renderers.Add(renderer));

            return this;
        }

        /// <summary>
        ///     Adds renderers to the scene.
        /// </summary>
        /// <param name="renderers">The renderers to add.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> AddRenderers(IEnumerable<IRenderer<TRenderState>> renderers)
        {
            _renderQueue.Enqueue(() => _renderers.Add(renderers));

            return this;
        }

        /// <summary>
        ///     Adds renderers to the scene.
        /// </summary>
        /// <param name="renderers">The renderers to add.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> AddRenderers(params IRenderer<TRenderState>[] renderers)
        {
            _renderQueue.Enqueue(() => _renderers.Add(renderers));

            return this;
        }

        /// <summary>
        ///     Removes an updater from the scene.
        /// </summary>
        /// <param name="updater">The updater to remove.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> RemoveUpdater(IUpdater<TGameState, TRenderState> updater)
        {
            _updateQueue.Enqueue(() => _updaters.Remove(updater));

            return this;
        }

        /// <summary>
        ///     Removes updaters from the scene.
        /// </summary>
        /// <param name="updaters">The updaters to remove.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> RemoveUpdater(IEnumerable<IUpdater<TGameState, TRenderState>> updaters)
        {
            _updateQueue.Enqueue(() => _updaters.Remove(updaters));

            return this;
        }

        /// <summary>
        ///     Removes updaters from the scene.
        /// </summary>
        /// <param name="updaters">The updaters to remove.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> RemoveUpdater(params IUpdater<TGameState, TRenderState>[] updaters)
        {
            _updateQueue.Enqueue(() => _updaters.Remove(updaters));

            return this;
        }

        /// <summary>
        ///     Removes a renderer from the scene.
        /// </summary>
        /// <param name="renderer">The renderer to remove.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> RemoveRenderer(IRenderer<TRenderState> renderer)
        {
            _renderQueue.Enqueue(() => _renderers.Remove(renderer));

            return this;
        }

        /// <summary>
        ///     Removes renderers from the scene.
        /// </summary>
        /// <param name="renderers">The renderers to remove.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> RemoveRenderer(IEnumerable<IRenderer<TRenderState>> renderers)
        {
            _renderQueue.Enqueue(() => _renderers.Remove(renderers));

            return this;
        }

        /// <summary>
        ///     Removes renderers from the scene.
        /// </summary>
        /// <param name="renderers">The renderers to remove.</param>
        /// <returns>Returns the scene.</returns>
        protected IScene<TGameState, TRenderState, TSceneKey> RemoveRenderer(params IRenderer<TRenderState>[] renderers)
        {
            _renderQueue.Enqueue(() => _renderers.Remove(renderers));

            return this;
        }

        /// <summary>
        ///     Removes all updaters from the scene.
        /// </summary>
        protected void ClearUpdaters()
        {
            _updateQueue.Enqueue(() => _updaters.Clear());
        }

        /// <summary>
        ///     Removes all renderers from the scene.
        /// </summary>
        protected void ClearRenderers()
        {
            _renderQueue.Enqueue(() => _renderers.Clear());
        }

        /// <summary>
        ///     Removes all updaters and renderers from the scene.
        /// </summary>
        protected void ClearEntities()
        {
            _updateQueue.Enqueue(ClearUpdaters);
            _renderQueue.Enqueue(ClearRenderers);
        }
    }
}