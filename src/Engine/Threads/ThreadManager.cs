using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.Messaging;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>
    ///     Spawns and tracks various threads used by the engine.
    /// </summary>
    public class ThreadManager : IThreadManager
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly Dictionary<ProcessThread, ThreadWrapper> _threadsByEngineThread = new Dictionary<ProcessThread, ThreadWrapper>();
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThreadManager" /> type.
        /// </summary>
        /// <param name="mainThread">The main thread.</param>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public ThreadManager(Thread mainThread)
        {
            mainThread.Name = "Main Thread";

            _threadsByEngineThread.Add(ProcessThread.Main, new ThreadWrapper(mainThread));

            VerifyProcessThread(ProcessThread.Main);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the specified thread is already started.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StartEngineThread(IInterfaces interfaces, IEngineThreadWorker threadWorker, EngineThread thread)
        {
            VerifyProcessThread(ProcessThread.Main);

            var processThread = Enums.Parse<ProcessThread>(thread.GetName());
            string name = thread.GetName();

            if (_threadsByEngineThread.ContainsKey(processThread))
            {
                throw new InvalidOperationException($"{name} thread is already started.");
            }

            _threadsByEngineThread.Add(processThread, new ThreadWrapper(interfaces, threadWorker, name, _cancellationTokenSource.Token));
        }

        /// <inheritdoc />
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StopEngineThreads()
        {
            _cancellationTokenSource.Cancel();

            foreach (ThreadWrapper threadWrapper in _threadsByEngineThread.Values)
            {
                threadWrapper.WaitForCompletion();
            }
        }

        /// <summary>
        ///     <para>Verifies that the currently-executing thread is the specified thread.</para>
        ///     <para>This method is a no-op when compiling in Debug configuration.</para>
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the specified thread has not been started.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the current thread is not the specified thread.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void VerifyProcessThread(ProcessThread thread)
        {
#if DEBUG
            if (!_threadsByEngineThread.TryGetValue(thread, out ThreadWrapper? expectedThreadWrapper))
            {
                throw new InvalidOperationException($"{thread} thread has not been started.");
            }

            expectedThreadWrapper.Verify();
#endif
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _threadsByEngineThread.Clear();
                    _cancellationTokenSource.Dispose();
                },
                ref _isDisposed,
                this,
                ProcessThread.Main);
        }

        private class ThreadWrapper
        {
            private readonly ManualResetEventSlim _manualResetEvent = new ManualResetEventSlim();
            private readonly Thread _thread;

            public ThreadWrapper(Thread thread)
            {
                _thread = thread;
                _manualResetEvent.Set();
            }

            public ThreadWrapper(IInterfaces interfaces, IEngineThreadWorker threadWorker, string name, CancellationToken cancellationToken)
            {
                _thread = new Thread(
                    () =>
                    {
                        ConcurrentMessagePublisherSubscriber<IGlobalMessage> globalMessagePublisherSubscriber =
                            ConcurrentMessagePublisherSubscriber<IGlobalMessage>
                                .Create(interfaces, threadWorker.Context);

                        Thread.CurrentThread.Name = $"{name} Thread";

                        threadWorker.SubscribeToMessages(globalMessagePublisherSubscriber);

                        while (!cancellationToken.IsCancellationRequested)
                        {
                            // Handle dispatched messages
                            globalMessagePublisherSubscriber.HandleDispatched();

                            threadWorker.DoWork(cancellationToken);
                        }

                        threadWorker.CleanUp();

                        _manualResetEvent.Set();
                    });

                _thread.Start();
            }

            public void Verify()
            {
                Thread actualThread = Thread.CurrentThread;

                if (actualThread != _thread)
                {
                    throw new InvalidOperationException($"Current thread should be {_thread.Name} but is actually {actualThread.Name ?? "an unknown thread"}.");
                }
            }

            public void WaitForCompletion()
            {
                _manualResetEvent.Wait();
            }
        }
    }
}