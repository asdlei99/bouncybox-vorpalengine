using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Messaging.RenderMessages;
using EnumsNET;
using ProcessThread = BouncyBox.VorpalEngine.Engine.Threads.ProcessThread;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>
    ///     Manages a collection of scenes.
    /// </summary>
    public class SceneManager<TGameState, TRenderState, TSceneKey> : ISceneManager<TRenderState>
        where TGameState : class
        where TRenderState : class, new()
        where TSceneKey : struct, Enum
    {
        private readonly IGameExecutionStateManager _gameExecutionStateManager;
        private readonly IGameStateManager<TGameState> _gameStateManager;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly IInterfaces _interfaces;
        private readonly MessagePublisherSubscriber<IRenderMessage> _renderMessagePublisherSubscriber;
        private readonly IRenderStateManager<TRenderState> _renderStateManager;
        private readonly ISceneFactory<TGameState, TRenderState, TSceneKey> _sceneFactory;
        private readonly ConcurrentQueue<Action<DirectXResources>> _sceneLoadedRenderQueue = new ConcurrentQueue<Action<DirectXResources>>();
        private readonly ConcurrentQueue<Action<TGameState>> _sceneLoadedUnloadedUpdateQueue = new ConcurrentQueue<Action<TGameState>>();

        private readonly Dictionary<TSceneKey, IScene<TGameState, TRenderState, TSceneKey>> _scenesBySceneKey =
            new Dictionary<TSceneKey, IScene<TGameState, TRenderState, TSceneKey>>();

        private readonly object _scenesBySceneKeyLockObject = new object();

        private readonly SortedSet<IScene<TGameState, TRenderState, TSceneKey>> _scenesSortedByRenderOrder =
            new SortedSet<IScene<TGameState, TRenderState, TSceneKey>>(new RenderSceneComparer());

        private readonly SortedSet<IScene<TGameState, TRenderState, TSceneKey>> _scenesSortedByUpdateOrder =
            new SortedSet<IScene<TGameState, TRenderState, TSceneKey>>(new UpdateSceneComparer());

        private readonly ConcurrentQueue<Action> _sceneUnloadedMainQueue = new ConcurrentQueue<Action>();
        private readonly ConcurrentQueue<Action> _sceneUnloadedRenderQueue = new ConcurrentQueue<Action>();
        private readonly ContextSerilogLogger _serilogLogger;
        private DirectXResources? _directXResources;
        private bool _isDisposed;

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="SceneManager{TGameState,TRenderState,TSceneKey}" /> type.</para>
        ///     <para>Subscribes to the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnloadSceneMessage{TSceneKey}" /> global message.</para>
        ///     <para>Subscribes to the <see cref="DirectXResourcesInitializedMessage" /> render message.</para>
        ///     <para>Subscribes to the <see cref="DirectXResourcesReleasedMessage" /> render message.</para>
        ///     <para>Subscribes to the <see cref="DirectXResourcesResizedMessage" /> render message.</para>
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameStateManager">An <see cref="IGameStateManager{TGameState}" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="sceneFactory">An <see cref="ISceneFactory{TGameState,TRenderState,TSceneKey}" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public SceneManager(
            IInterfaces interfaces,
            IGameStateManager<TGameState> gameStateManager,
            IRenderStateManager<TRenderState> renderStateManager,
            ISceneFactory<TGameState, TRenderState, TSceneKey> sceneFactory,
            IGameExecutionStateManager gameExecutionStateManager,
            NestedContext context)
        {
            // This constructor must execute on the main thread
            interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Main);

            context = context.CopyAndPush(nameof(SceneManager<TGameState, TRenderState, TSceneKey>));

            _interfaces = interfaces;
            _gameStateManager = gameStateManager;
            _renderStateManager = renderStateManager;
            _sceneFactory = sceneFactory;
            _gameExecutionStateManager = gameExecutionStateManager;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<LoadSceneMessage<TSceneKey>>(HandleLoadSceneMessage)
                    .Subscribe<UnloadSceneMessage<TSceneKey>>(HandleUnloadSceneMessage);
            _renderMessagePublisherSubscriber =
                MessagePublisherSubscriber<IRenderMessage>
                    .Create(interfaces.RenderMessageQueue, context)
                    .Subscribe<DirectXResourcesInitializedMessage>(HandleDirectXResourcesInitializedMessage)
                    .Subscribe<DirectXResourcesReleasedMessage>(HandleDirectXResourcesReleasedMessage)
                    .Subscribe<DirectXResourcesResizedMessage>(HandleDirectXResourcesResizedMessage);
        }

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="SceneManager{TGameState,TRenderState,TSceneKey}" /> type.</para>
        ///     <para>Subscribes to the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnloadSceneMessage{TSceneKey}" /> global message.</para>
        ///     <para>Subscribes to the <see cref="DirectXResourcesInitializedMessage" /> render message.</para>
        ///     <para>Subscribes to the <see cref="DirectXResourcesReleasedMessage" /> render message.</para>
        ///     <para>Subscribes to the <see cref="DirectXResourcesResizedMessage" /> render message.</para>
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameStateManager">An <see cref="IGameStateManager{TGameState}" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="sceneFactory">An <see cref="ISceneFactory{TGameState,TRenderState,TSceneKey}" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        public SceneManager(
            IInterfaces interfaces,
            IGameStateManager<TGameState> gameStateManager,
            IRenderStateManager<TRenderState> renderStateManager,
            ISceneFactory<TGameState, TRenderState, TSceneKey> sceneFactory,
            IGameExecutionStateManager gameExecutionStateManager)
            : this(interfaces, gameStateManager, renderStateManager, sceneFactory, gameExecutionStateManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public void Update()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            TGameState gameState = _gameStateManager.GameState;

            // Process loaded and unloaded scenes
            while (_sceneLoadedUnloadedUpdateQueue.TryDequeue(out Action<TGameState>? @delegate))
            {
                @delegate(gameState);
            }

            // Update scene game states
            foreach (IScene<TGameState, TRenderState, TSceneKey> scene in _scenesSortedByUpdateOrder)
            {
                scene.UpdateGameState(gameState, _gameExecutionStateManager.GameExecutionState);
            }

            var renderState = new TRenderState();

            // Prepare scene render states
            foreach (IScene<TGameState, TRenderState, TSceneKey> scene in _scenesSortedByUpdateOrder)
            {
                scene.PrepareRenderState(gameState, renderState);
            }

            _renderStateManager.ProvideNextRenderState(renderState);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        public bool Render(TRenderState renderState, IEngineStats engineStats)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            Debug.Assert(_directXResources != null);

            // Process unloaded scenes
            while (_sceneUnloadedRenderQueue.TryDequeue(out Action? @delegate))
            {
                @delegate();
            }

            // Process loaded scenes
            while (_sceneLoadedRenderQueue.TryDequeue(out Action<DirectXResources>? @delegate))
            {
                @delegate(_directXResources.Value);
            }

            // Render scenes
            foreach (IScene<TGameState, TRenderState, TSceneKey> scene in _scenesSortedByRenderOrder)
            {
                scene.Render(renderState, _gameExecutionStateManager.GameExecutionState, engineStats);
            }

            return true;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void HandleDispatchedMessages()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Main);

            // Process unloaded scenes
            if (_sceneUnloadedMainQueue.TryDequeue(out Action? @delegate))
            {
                @delegate();
            }

            _globalMessagePublisherSubscriber.HandleDispatched();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    foreach (IScene<TGameState, TRenderState, TSceneKey> scene in _scenesBySceneKey.Values)
                    {
                        scene.Dispose();
                    }

                    _globalMessagePublisherSubscriber?.Dispose();
                    _renderMessagePublisherSubscriber?.Dispose();
                },
                ref _isDisposed,
                _interfaces.ThreadManager,
                ProcessThread.Main);
        }

        /// <summary>
        ///     Handles the <see cref="LoadSceneMessage{TSceneKey}" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        /// <exception cref="InvalidOperationException">Thrown when a scene with the same update order was already loaded.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a scene with the same render order was already loaded.</exception>
        private void HandleLoadSceneMessage(LoadSceneMessage<TSceneKey> message)
        {
            IScene<TGameState, TRenderState, TSceneKey> scene;
            string sceneKeyName = message.SceneKey.GetName();

            lock (_scenesBySceneKeyLockObject)
            {
                if (_scenesBySceneKey.ContainsKey(message.SceneKey))
                {
                    _serilogLogger.LogWarning("Scene {Scene} is already loaded", sceneKeyName);
                    return;
                }

                _serilogLogger.LogVerbose("Loading scene {Scene}", sceneKeyName);

                scene = _sceneFactory.Create(message.SceneKey);

                _scenesBySceneKey.Add(message.SceneKey, scene);
            }

            _sceneLoadedUnloadedUpdateQueue.Enqueue(
                gameState =>
                {
                    scene.Load(gameState);

                    if (!_scenesSortedByUpdateOrder.Add(scene))
                    {
                        throw new InvalidOperationException($"A scene with update order {scene.UpdateOrder} was already loaded.");
                    }

                    _serilogLogger.LogDebug("Loaded scene {Scene}", sceneKeyName);

                    // Ensure the scene is loaded before making it available for rendering
                    _sceneLoadedRenderQueue.Enqueue(
                        resources =>
                        {
                            _serilogLogger.LogVerbose("Initializing resources for scene {Scene}", sceneKeyName);

                            scene.InitializeResources(resources);

                            if (!_scenesSortedByRenderOrder.Add(scene))
                            {
                                throw new InvalidOperationException($"A scene with render order {scene.RenderOrder} already exists.");
                            }

                            _serilogLogger.LogDebug("Initialized resources for scene {Scene}", sceneKeyName);
                        });
                });
        }

        /// <summary>
        ///     Handles the <see cref="LoadSceneMessage{TSceneKey}" /> global message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleUnloadSceneMessage(UnloadSceneMessage<TSceneKey> message)
        {
            IScene<TGameState, TRenderState, TSceneKey>? scene;
            string sceneKeyName = message.SceneKey.GetName();

            lock (_scenesBySceneKeyLockObject)
            {
                if (!_scenesBySceneKey.Remove(message.SceneKey, out scene))
                {
                    _serilogLogger.LogWarning("Scene {Scene} is already unloaded", sceneKeyName);
                    return;
                }
            }

            _serilogLogger.LogVerbose("Unloading scene {Scene}", sceneKeyName);

            _sceneLoadedUnloadedUpdateQueue.Enqueue(
                gameState =>
                {
                    scene.Unload(gameState);

                    _scenesSortedByUpdateOrder.Remove(scene);

                    _serilogLogger.LogDebug("Unloaded scene {Scene}", sceneKeyName);

                    // Ensure the scene is unloaded before cleaning up its resources
                    _sceneUnloadedRenderQueue.Enqueue(
                        () =>
                        {
                            _serilogLogger.LogVerbose("Releasing resources for scene {Scene}", sceneKeyName);

                            scene.ReleaseResources();

                            _scenesSortedByRenderOrder.Remove(scene);

                            _serilogLogger.LogDebug("Released resources for scene {Scene}", sceneKeyName);

                            // Ensure the scene's resources have been released before disposing it
                            _sceneUnloadedMainQueue.Enqueue(
                                () =>
                                {
                                    _serilogLogger.LogVerbose("Disposing scene {Scene}", sceneKeyName);

                                    scene.Dispose();

                                    _serilogLogger.LogInformation("Disposed scene {Scene}", sceneKeyName);
                                });
                        });
                });
        }

        /// <summary>
        ///     Handles the <see cref="DirectXResourcesInitializedMessage" /> render message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleDirectXResourcesInitializedMessage(DirectXResourcesInitializedMessage message)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            _directXResources = message.Resources;

            foreach (IScene<TGameState, TRenderState, TSceneKey> scene in _scenesSortedByRenderOrder)
            {
                scene.InitializeResources(message.Resources);
            }
        }

        /// <summary>
        ///     Handles the <see cref="DirectXResourcesReleasedMessage" /> render message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleDirectXResourcesReleasedMessage(DirectXResourcesReleasedMessage message)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            _directXResources = null;

            foreach (IScene<TGameState, TRenderState, TSceneKey> scene in _scenesSortedByRenderOrder)
            {
                scene.ReleaseResources();
            }
        }

        /// <summary>
        ///     Handles the <see cref="DirectXResourcesResizedMessage" /> render message.
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleDirectXResourcesResizedMessage(DirectXResourcesResizedMessage message)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            foreach (IScene<TGameState, TRenderState, TSceneKey> scene in _scenesSortedByRenderOrder)
            {
                scene.ResizeResources(message.ClientSize);
            }
        }

        /// <summary>
        ///     A comparer that orders scenes by <see cref="IScene{TGameState,TRenderState,TSceneKey}.UpdateOrder" />.
        /// </summary>
        private class UpdateSceneComparer : IComparer<IScene<TGameState, TRenderState, TSceneKey>>
        {
            /// <inheritdoc />
            public int Compare(IScene<TGameState, TRenderState, TSceneKey> x, IScene<TGameState, TRenderState, TSceneKey> y)
            {
                return x == null ? 1 : y == null ? -1 : x.UpdateOrder.CompareTo(y.UpdateOrder);
            }
        }

        /// <summary>
        ///     A comparer that orders scenes by <see cref="IScene{TGameState,TRenderState,TSceneKey}.RenderOrder" />.
        /// </summary>
        private class RenderSceneComparer : IComparer<IScene<TGameState, TRenderState, TSceneKey>>
        {
            /// <inheritdoc />
            public int Compare(IScene<TGameState, TRenderState, TSceneKey> x, IScene<TGameState, TRenderState, TSceneKey> y)
            {
                return x == null ? 1 : y == null ? -1 : x.RenderOrder.CompareTo(y.RenderOrder);
            }
        }
    }
}