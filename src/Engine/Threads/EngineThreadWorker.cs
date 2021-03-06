﻿using System;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Messaging;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>A base class for all engine thread workers.</summary>
    public abstract class EngineThreadWorker : IEngineThreadWorker
    {
        private readonly ProcessThread _thread;

        /// <summary>Initializes a new instance of the <see cref="EngineThreadWorker" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="thread">The thread on which the worker will perform work.</param>
        /// <param name="context">A nested context.</param>
        protected EngineThreadWorker(IInterfaces interfaces, EngineThread thread, NestedContext context)
        {
            Context = context;
            Interfaces = interfaces;
            _thread = Enums.Parse<ProcessThread>(thread.AsString());

            GlobalMessageQueue = new GlobalMessageQueueHelper(interfaces.GlobalMessageQueue, context);
        }

        /// <summary>Initializes a new instance of the <see cref="EngineThreadWorker" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="thread">The thread on which the worker will perform work.</param>
        protected EngineThreadWorker(IInterfaces interfaces, EngineThread thread) : this(interfaces, thread, NestedContext.None())
        {
        }

        /// <summary>Gets the <see cref="IInterfaces" /> implementation.</summary>
        protected IInterfaces Interfaces { get; }

        /// <summary>Gets the nested context.</summary>
        protected NestedContext Context { get; }

        /// <summary>Gets the global message queue.</summary>
        protected GlobalMessageQueueHelper GlobalMessageQueue { get; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the correct thread.</exception>
        public void Prepare()
        {
            Interfaces.ThreadManager.VerifyProcessThread(_thread);

            OnPrepare();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the correct thread.</exception>
        public void DoWork(CancellationToken cancellationToken)
        {
            Interfaces.ThreadManager.VerifyProcessThread(_thread);

            // Dispatch messages queued on the worker's thread
            GlobalMessageQueue.DispatchQueued(_thread);

            OnDoWork(cancellationToken);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the correct thread.</exception>
        public void CleanUp()
        {
            Interfaces.ThreadManager.VerifyProcessThread(_thread);

            OnCleanUp();

            GlobalMessageQueue.Dispose();
        }

        /// <inheritdoc cref="IEngineThreadWorker.DoWork" />
        protected abstract void OnDoWork(CancellationToken cancellationToken);

        /// <inheritdoc cref="IEngineThreadWorker.Prepare" />
        protected virtual void OnPrepare()
        {
        }

        /// <inheritdoc cref="IEngineThreadWorker.CleanUp" />
        protected virtual void OnCleanUp()
        {
        }
    }
}