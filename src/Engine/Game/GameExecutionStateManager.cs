﻿using System;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>Manages game execution state changes.</summary>
    internal class GameExecutionStateManager : IGameExecutionStateManager
    {
        private readonly GlobalMessageQueueHelper _globalMessageQueue;
        private readonly IInterfaces _interfaces;
        private readonly ContextSerilogLogger _serilogLogger;
        private bool _isDisposed;
        private bool _isPaused;
        private bool _isSuspended;

        /// <summary>Initializes a new instance of the <see cref="GameExecutionStateManager" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="PauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnpauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="SuspendGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResumeGameMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public GameExecutionStateManager(IInterfaces interfaces, NestedContext context)
        {
            context = context.Push(nameof(GameExecutionStateManager));

            _interfaces = interfaces;
            _serilogLogger = new ContextSerilogLogger(interfaces.SerilogLogger, context);
            _globalMessageQueue =
                new GlobalMessageQueueHelper(interfaces.GlobalMessageQueue, context)
                    .WithThread(ProcessThread.Update)
                    .Subscribe<PauseGameMessage>(HandlePauseGameMessage)
                    .Subscribe<UnpauseGameMessage>(HandleUnpauseGameMessage)
                    .Subscribe<SuspendGameMessage>(HandleSuspendGameMessage)
                    .Subscribe<ResumeGameMessage>(HandleResumeGameMessage);
        }

        /// <summary>Initializes a new instance of the <see cref="GameExecutionStateManager" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="PauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="UnpauseGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="SuspendGameMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="ResumeGameMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        public GameExecutionStateManager(IInterfaces interfaces)
            : this(interfaces, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public GameExecutionState GameExecutionState => new GameExecutionState(_isPaused, _isSuspended);

        /// <inheritdoc />
        public void Dispose()
        {
            _interfaces.ThreadManager.DisposeHelper(_globalMessageQueue.Dispose, ref _isDisposed, ProcessThread.Main);
        }

        /// <summary>Handles the <see cref="PauseGameMessage" /> global message.</summary>
        /// <remarks>Publishes the <see cref="GamePausedMessage" /> global message.</remarks>
        /// <param name="message">The message being handled.</param>
        private void HandlePauseGameMessage(PauseGameMessage message)
        {
            if (_isPaused)
            {
                return;
            }

            _serilogLogger.LogInformation("Game paused");
            _isPaused = true;

            _globalMessageQueue.Publish<GamePausedMessage>();
        }

        /// <summary>Handles the <see cref="UnpauseGameMessage" /> global message.</summary>
        /// <remarks>Publishes the <see cref="GameUnpausedMessage" /> global message.</remarks>
        /// <param name="message">The message being handled.</param>
        private void HandleUnpauseGameMessage(UnpauseGameMessage message)
        {
            if (!_isPaused)
            {
                return;
            }

            _serilogLogger.LogInformation("Game unpaused");
            _isPaused = false;

            _globalMessageQueue.Publish<GameUnpausedMessage>();
        }

        /// <summary>Handles the <see cref="SuspendGameMessage" /> global message.</summary>
        /// <remarks>Publishes the <see cref="GameSuspendedMessage" /> global message.</remarks>
        /// <param name="message">The message being handled.</param>
        private void HandleSuspendGameMessage(SuspendGameMessage message)
        {
            if (_isSuspended)
            {
                return;
            }

            _serilogLogger.LogInformation("Game suspended");
            _isSuspended = true;

            _globalMessageQueue.Publish<GameSuspendedMessage>();
        }

        /// <summary>Handles the <see cref="ResumeGameMessage" /> global message.</summary>
        /// <remarks>Publishes the <see cref="GameResumedMessage" /> global message.</remarks>
        /// <param name="message">The message being handled.</param>
        private void HandleResumeGameMessage(ResumeGameMessage message)
        {
            if (!_isSuspended)
            {
                return;
            }

            _serilogLogger.LogInformation("Game resumed");
            _isSuspended = false;

            _globalMessageQueue.Publish<GameResumedMessage>();
        }
    }
}