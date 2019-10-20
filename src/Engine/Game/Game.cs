using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.Engine.Threads;
using BouncyBox.VorpalEngine.Engine.Windows;
using EnumsNET;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>Base class for all games. This class manages the update and render loops and the windows message loop.</summary>
    public abstract class Game<TGameState, TSceneKey> : IDisposable
        where TGameState : class
        where TSceneKey : struct, Enum
    {
        private readonly IEntityManager<TGameState> _entityManager;
        private readonly ManualResetEventSlim _exitManualResetEvent = new ManualResetEventSlim();
        private readonly IGameExecutionStateManager _gameExecutionStateManager;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly ProgramOptions _programOptions;
        private readonly ISceneManager _sceneManager;
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;
        private RenderForm? _renderForm;

        /// <summary>Initializes a new instance of the <see cref="Game{TGameState,TSceneKey}" /> type.</summary>
        /// <remarks>Subscribes to the <see cref="RenderWindowClosingMessage" /> global message.</remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        /// <param name="sceneManager">An <see cref="ISceneManager" /> implementation.</param>
        /// <param name="programOptions">Parsed command line arguments.</param>
        /// <param name="context">A nested context.</param>
        protected Game(
            IInterfaces interfaces,
            IGameExecutionStateManager gameExecutionStateManager,
            IEntityManager<TGameState> entityManager,
            ISceneManager sceneManager,
            ProgramOptions programOptions,
            NestedContext context)
        {
            context = context.CopyAndPush(nameof(Game<TGameState, TSceneKey>));

            Interfaces = interfaces;
            _gameExecutionStateManager = gameExecutionStateManager;
            _entityManager = entityManager;
            _sceneManager = sceneManager;
            _programOptions = programOptions;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<RenderWindowClosingMessage>(HandleRenderWindowClosingMessage);
        }

        /// <summary>Initializes a new instance of the <see cref="Game{TGameState,TSceneKey}" /> type.</summary>
        /// <remarks>Subscribes to the <see cref="RenderWindowClosingMessage" /> global message.</remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        /// <param name="sceneManager">An <see cref="ISceneManager" /> implementation.</param>
        /// <param name="programOptions">Parsed command line arguments.</param>
        protected Game(
            IInterfaces interfaces,
            IGameExecutionStateManager gameExecutionStateManager,
            IEntityManager<TGameState> entityManager,
            ISceneManager sceneManager,
            ProgramOptions programOptions)
            : this(interfaces, gameExecutionStateManager, entityManager, sceneManager, programOptions, NestedContext.None())
        {
        }

        /// <summary>Gets the <see cref="IInterfaces" /> implementation.</summary>
        protected IInterfaces Interfaces { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _globalMessagePublisherSubscriber.Dispose();
                    _renderForm?.Dispose();
                },
                ref _isDisposed);
        }

        /// <summary>Runs the game.</summary>
        /// <remarks>Publishes the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</remarks>
        /// <param name="initialSceneKey">The scene to load.</param>
        /// <returns>Returns the result of the run.</returns>
        /// <exception cref="Exception">Thrown when an engine thread threw an unhandled exception.</exception>
        public RunResult Run(TSceneKey initialSceneKey)
        {
            var updateWorker = new UpdateWorker<TGameState>(Interfaces, _entityManager, _sceneManager);
            var renderWorker = new RenderWorker<TGameState>(Interfaces, _entityManager);
            var renderResourcesWorker = new RenderResourcesWorker<TGameState>(Interfaces, _entityManager);
            // The main thread increments the count by 1
            var countdownEvent = new CountdownEvent(1);

            // Create render window

            _serilogLogger.LogDebug("Creating render window");

            _renderForm = new RenderForm(Interfaces, _programOptions);

            // Start tasks (cancellation is handled by the methods themselves)

            _serilogLogger.LogDebug("Starting threads");

            Interfaces.ThreadManager.StartEngineThread(updateWorker, EngineThread.Update, countdownEvent, _exitManualResetEvent);
            Interfaces.ThreadManager.StartEngineThread(renderResourcesWorker, EngineThread.RenderResources, countdownEvent, _exitManualResetEvent);
            Interfaces.ThreadManager.StartEngineThread(renderWorker, EngineThread.Render, countdownEvent, _exitManualResetEvent);

            // Show the render window

            _serilogLogger.LogDebug("Showing render window");

            _renderForm.Show();

            // Load the initial scene
            _globalMessagePublisherSubscriber.Publish(new LoadSceneMessage<TSceneKey>(initialSceneKey));

            // Process Win32 messages

            do
            {
                // Process Win32 messages
                ProcessWin32Messages();

                // Dispatch queued messages to their destination queues
                Interfaces.GlobalConcurrentMessageQueue.DispatchQueued();

                // Handle dispatched messages
                _globalMessagePublisherSubscriber.HandleDispatched();

                // Allow subclasses to handle dispatched messages
                OnHandleDispatchedMessages();

                // Handle dispatched messages

                _renderForm.HandleDispatchedMessages();
                _gameExecutionStateManager.HandleDispatchedMessages();
            } while (!_exitManualResetEvent.IsSet);

            // The user tried to close the render window or an unhandled exception occurred on an engine thread
            _serilogLogger.LogDebug("Exit requested");

            // The main thread decrements the count by 1
            countdownEvent.Signal();

            IReadOnlyCollection<(EngineThread thread, Exception exception)> unhandledExceptions =
                Interfaces.ThreadManager.RequestEngineThreadTerminationAndWaitForTermination(countdownEvent);

            // The render window must be disposed of last to avoid additional unhandled exceptions on render threads
            // (e.g., the render thread attempting to use resources tied to the window handle)
            _renderForm.Dispose();

            if (unhandledExceptions.Count == 0)
            {
                return RunResult.Success;
            }

            (EngineThread thread, Exception exception) = unhandledExceptions.First();

            throw new Exception($"An unhandled exception occurred on the {thread.GetName()} thread.", exception);
        }

        /// <summary>Handle dispatched messages.</summary>
        protected virtual void OnHandleDispatchedMessages()
        {
        }

        /// <summary>Process Win32 messages.</summary>
        private static unsafe void ProcessWin32Messages()
        {
            MSG msg;

            while (User32.PeekMessageW(&msg, IntPtr.Zero, 0, 0, User32.PM_REMOVE) == TerraFX.Interop.Windows.TRUE)
            {
                User32.TranslateMessage(&msg);
                User32.DispatchMessageW(&msg);
            }
        }

        /// <summary>Handles the <see cref="RenderWindowClosingMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowClosingMessage(RenderWindowClosingMessage message)
        {
            _exitManualResetEvent.Set();
        }
    }
}