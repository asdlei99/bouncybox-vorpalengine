using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>Manages a collection of scenes.</summary>
    public class SceneManager<TGameState, TSceneKey> : ISceneManager
        where TGameState : class
        where TSceneKey : struct, Enum
    {
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly IInterfaces _interfaces;
        private readonly ISceneFactory<TSceneKey> _sceneFactory;

        private readonly Dictionary<TSceneKey, IScene<TSceneKey>> _scenesBySceneKey =
            new Dictionary<TSceneKey, IScene<TSceneKey>>();

        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="SceneManager{TGameState,TSceneKey}" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnloadSceneMessage{TSceneKey}" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="sceneFactory">An <see cref="ISceneFactory{TSceneKey}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public SceneManager(IInterfaces interfaces, ISceneFactory<TSceneKey> sceneFactory, NestedContext context)
        {
            context = context.CopyAndPush(nameof(SceneManager<TGameState, TSceneKey>));

            _interfaces = interfaces;
            _sceneFactory = sceneFactory;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<LoadSceneMessage<TSceneKey>>(HandleLoadSceneMessage)
                    .Subscribe<UnloadSceneMessage<TSceneKey>>(HandleUnloadSceneMessage);
        }

        /// <summary>Initializes a new instance of the <see cref="SceneManager{TGameState,TSceneKey}" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnloadSceneMessage{TSceneKey}" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="sceneFactory">An <see cref="ISceneFactory{TSceneKey}" /> implementation.</param>
        public SceneManager(IInterfaces interfaces, ISceneFactory<TSceneKey> sceneFactory)
            : this(interfaces, sceneFactory, NestedContext.None())
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        public void HandleDispatchedMessages()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            _globalMessagePublisherSubscriber.HandleDispatched();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        public void Dispose()
        {
            _interfaces.ThreadManager.DisposeHelper(() => { _globalMessagePublisherSubscriber?.Dispose(); }, ref _isDisposed, ProcessThread.Main);
        }

        /// <summary>Handles the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        /// <exception cref="InvalidOperationException">Thrown when a scene with the same update order was already loaded.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a scene with the same render order was already loaded.</exception>
        private void HandleLoadSceneMessage(LoadSceneMessage<TSceneKey> message)
        {
            string sceneKeyName = message.SceneKey.AsString();

            if (_scenesBySceneKey.ContainsKey(message.SceneKey))
            {
                _serilogLogger.LogWarning("Scene {Scene} is already loaded", sceneKeyName);
                return;
            }

            _serilogLogger.LogVerbose("Loading scene {Scene}", sceneKeyName);

            IScene<TSceneKey> scene = _sceneFactory.Create(message.SceneKey);

            _scenesBySceneKey.Add(message.SceneKey, scene);

            scene.Load();

            _serilogLogger.LogDebug("Loaded scene {Scene}", sceneKeyName);
        }

        /// <summary>Handles the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</summary>
        /// <remarks>Publishes the <see cref="DisposeObjectMessage" /> global message.</remarks>
        /// <param name="message">The message being handled.</param>
        private void HandleUnloadSceneMessage(UnloadSceneMessage<TSceneKey> message)
        {
            string sceneKeyName = message.SceneKey.AsString();

            if (!_scenesBySceneKey.Remove(message.SceneKey, out IScene<TSceneKey>? scene))
            {
                _serilogLogger.LogWarning("Scene {Scene} is already unloaded", sceneKeyName);
                return;
            }

            _serilogLogger.LogVerbose("Unloading scene {Scene}", sceneKeyName);

            scene.Unload();

            _serilogLogger.LogDebug("Unloaded scene {Scene}", sceneKeyName);

            _globalMessagePublisherSubscriber.Publish(new DisposeObjectMessage(scene));
        }
    }
}