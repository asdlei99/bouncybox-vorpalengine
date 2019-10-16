using System;
using System.Runtime.CompilerServices;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Forms;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.Engine.Threads;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Base class for all games. This class manages the update and render loops and the windows message loop.
    /// </summary>
    public abstract class Game<TRenderState, TSceneKey> : IDisposable
        where TRenderState : class, new()
        where TSceneKey : struct, Enum
    {
        private readonly EngineStats _engineStats = new EngineStats();
        private readonly IGameExecutionStateManager _gameExecutionStateManager;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly ProgramOptions _programOptions;
        private readonly IRenderStateManager<TRenderState> _renderStateManager;
        private readonly ISceneManager<TRenderState> _sceneManager;
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;
        private RenderForm? _renderForm;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game{TRenderState,TSceneKey}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="sceneManager">An <see cref="ISceneManager{TRenderState}" /> implementation.</param>
        /// <param name="programOptions">Parsed command line arguments.</param>
        /// <param name="context">A nested context.</param>
        protected Game(
            IInterfaces interfaces,
            IGameExecutionStateManager gameExecutionStateManager,
            IRenderStateManager<TRenderState> renderStateManager,
            ISceneManager<TRenderState> sceneManager,
            ProgramOptions programOptions,
            NestedContext context)
        {
            context = context.CopyAndPush(nameof(Game<TRenderState, TSceneKey>));

            Interfaces = interfaces;
            _gameExecutionStateManager = gameExecutionStateManager;
            _renderStateManager = renderStateManager;
            _sceneManager = sceneManager;
            _programOptions = programOptions;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _globalMessagePublisherSubscriber =
                ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                    .Create(interfaces, context)
                    .Subscribe<RenderWindowClosingMessage>(HandleRenderWindowClosingMessage);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game{TRenderState,TSceneKey}" /> type.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="renderStateManager">An <see cref="IRenderStateManager{TRenderState}" /> implementation.</param>
        /// <param name="sceneManager">An <see cref="ISceneManager{TRenderState}" /> implementation.</param>
        /// <param name="programOptions">Parsed command line arguments.</param>
        protected Game(
            IInterfaces interfaces,
            IGameExecutionStateManager gameExecutionStateManager,
            IRenderStateManager<TRenderState> renderStateManager,
            ISceneManager<TRenderState> sceneManager,
            ProgramOptions programOptions)
            : this(interfaces, gameExecutionStateManager, renderStateManager, sceneManager, programOptions, NestedContext.None())
        {
        }

        /// <summary>
        ///     Gets the <see cref="IInterfaces" /> implementation.
        /// </summary>
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

        /// <summary>
        ///     <para>Runs the game.</para>
        ///     <para>Publishes the <see cref="LoadSceneMessage{TSceneKey}" /> global message.</para>
        /// </summary>
        /// <param name="initialSceneKey">The scene to load.</param>
        /// <returns>Returns the result of the run.</returns>
        public RunResult Run(TSceneKey initialSceneKey)
        {
            var updateLoop = new UpdateLoop(Interfaces, _engineStats, () => _sceneManager.Update());
            var renderLoop = new RenderLoop<TRenderState>(Interfaces, _renderStateManager, _engineStats, a => _sceneManager.Render(a, _engineStats));

            // Create render window

            _serilogLogger.LogDebug("Creating render window");

            _renderForm = new RenderForm(Interfaces, _programOptions);

            // Start tasks (cancellation is handled by the methods themselves)

            _serilogLogger.LogDebug("Starting threads");

            Interfaces.ThreadManager.StartEngineThread(Interfaces, updateLoop, EngineThread.Update);
            Interfaces.ThreadManager.StartEngineThread(Interfaces, renderLoop, EngineThread.Render);

            // Show the render window

            _serilogLogger.LogDebug("Showing render window");

            _renderForm.Show();

            // Load the initial scene
            _globalMessagePublisherSubscriber.Publish(new LoadSceneMessage<TSceneKey>(initialSceneKey));

            // Process Win32 messages

            UIntPtr? wParam;

            do
            {
                // Process Win32 messages
                wParam = ProcessWin32Messages();

                // Dispatch queued messages to their destination queues
                Interfaces.GlobalConcurrentMessageQueue.DispatchQueued();

                // Handle dispatched messages
                _globalMessagePublisherSubscriber.HandleDispatched();

                // Allow subclasses to handle dispatched messages
                OnHandleDispatchedMessages();

                // Handle dispatched messages

                _renderForm.HandleDispatchedMessages();
                _gameExecutionStateManager.HandleDispatchedMessages();
                _sceneManager.HandleDispatchedMessages();
            } while (wParam == null);

            return RunResult.Success;
        }

        /// <summary>
        ///     Handle dispatched messages.
        /// </summary>
        protected virtual void OnHandleDispatchedMessages()
        {
        }

        /// <summary>
        ///     Process Win32 messages.
        /// </summary>
        /// <returns>Returns the WParam of the WM_QUIT message if it's received; otherwise, returns null.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe UIntPtr? ProcessWin32Messages()
        {
            MSG msg;

            while (User32.PeekMessageW(&msg, IntPtr.Zero, 0, 0, User32.PM_REMOVE) == Windows.TRUE)
            {
                if (msg.message == User32.WM_QUIT)
                {
                    return msg.wParam;
                }

                User32.TranslateMessage(&msg);
                User32.DispatchMessageW(&msg);
            }

            return null;
        }

        /// <summary>
        ///     <para>Handles the <see cref="RenderWindowClosingMessage" /> global message.</para>
        ///     <para>Publishes the <see cref="EngineThreadsTerminatedMessage" /> global message.</para>
        /// </summary>
        /// <param name="message">The message being handled.</param>
        private void HandleRenderWindowClosingMessage(RenderWindowClosingMessage message)
        {
            Interfaces.ThreadManager.StopEngineThreads();

            Interfaces.GlobalConcurrentMessageQueue.Publish<EngineThreadsTerminatedMessage>();
        }
    }
}