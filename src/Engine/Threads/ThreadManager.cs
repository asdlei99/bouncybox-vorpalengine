using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Logging;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>Spawns and tracks various threads used by the engine.</summary>
    public class ThreadManager : IThreadManager
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ISerilogLogger _serilogLogger;
        private readonly Dictionary<ProcessThread, ThreadWrapper> _threadsByProcessThread = new Dictionary<ProcessThread, ThreadWrapper>();
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="ThreadManager" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="mainThread">The main thread.</param>
        /// <param name="context">A nested context.</param>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        public ThreadManager(ISerilogLogger serilogLogger, Thread mainThread, NestedContext context)
        {
            context = context.CopyAndPush(nameof(ThreadManager));

            _serilogLogger = new ContextSerilogLogger(serilogLogger, context);

            mainThread.Name = "Main Thread";

            _threadsByProcessThread.Add(ProcessThread.Main, new ThreadWrapper(mainThread));

            VerifyProcessThread(ProcessThread.Main);
        }

        /// <summary>Initializes a new instance of the <see cref="ThreadManager" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="mainThread">The main thread.</param>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        public ThreadManager(ISerilogLogger serilogLogger, Thread mainThread) : this(serilogLogger, mainThread, NestedContext.None())
        {
        }

        /// <summary>
        ///     <para>Verifies that the currently-executing thread is the specified thread.</para>
        ///     <para>This method is a no-op when compiling in Debug configuration.</para>
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the specified thread has not been started.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the current thread is not the specified thread.</exception>
        [DebuggerStepThrough]
        public void VerifyProcessThread(ProcessThread thread)
        {
#if DEBUG
            if (!_threadsByProcessThread.TryGetValue(thread, out ThreadWrapper? expectedThreadWrapper))
            {
                throw new InvalidOperationException($"{thread} thread has not been started.");
            }

            expectedThreadWrapper.Verify();
#endif
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown when the specified thread is already started.</exception>
        public void StartEngineThread(
            IEngineThreadWorker threadWorker,
            EngineThread thread,
            CountdownEvent terminationCountdownEvent,
            ManualResetEventSlim unhandledExceptionManualResetEvent)
        {
            VerifyProcessThread(ProcessThread.Main);

            var processThread = Enums.Parse<ProcessThread>(thread.AsString());

            if (_threadsByProcessThread.ContainsKey(processThread))
            {
                throw new InvalidOperationException($"{thread.AsString()} thread is already started.");
            }

            var threadWrapper = new ThreadWrapper(
                threadWorker,
                thread,
                terminationCountdownEvent,
                unhandledExceptionManualResetEvent,
                _cancellationTokenSource.Token);

            _threadsByProcessThread.Add(processThread, threadWrapper);

            threadWrapper.Start();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Main" /> thread.
        /// </exception>
        public IReadOnlyCollection<(EngineThread thread, Exception exception)> RequestEngineThreadTerminationAndWaitForTermination(
            CountdownEvent terminationCountdownEvent)
        {
            VerifyProcessThread(ProcessThread.Main);

            _serilogLogger.LogDebug("Requesting engine thread termination");

            _cancellationTokenSource.Cancel();

            _serilogLogger.LogDebug("Waiting for engine threads to terminate");

            terminationCountdownEvent.Wait();

            _serilogLogger.LogDebug("Engine threads terminated");

            ImmutableArray<(EngineThread thread, Exception exception)> unhandledExceptions =
                _threadsByProcessThread.Values.Where(a => a.UnhandledException is object).Select(a => a.UnhandledException!.Value).ToImmutableArray();

            // Remove all engine threads from the dictionary
            ImmutableArray<ProcessThread> threadsToRemove =
                _threadsByProcessThread.Where(a => a.Value.ProcessThread != ProcessThread.Main).Select(a => a.Key).ToImmutableArray();

            foreach (ProcessThread thread in threadsToRemove)
            {
                _threadsByProcessThread.Remove(thread);
            }

            return unhandledExceptions;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the specified thread has not been started.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the current thread is not the specified thread.</exception>
        public void DisposeHelper(Action @delegate, ref bool isDisposed, ProcessThread thread)
        {
            VerifyProcessThread(thread);

            Common.DisposeHelper.Dispose(@delegate, ref isDisposed);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper(
                () =>
                {
                    _threadsByProcessThread.Clear();
                    _cancellationTokenSource.Dispose();
                },
                ref _isDisposed,
                ProcessThread.Main);
        }

        /// <summary>Wraps a thread.</summary>
        private class ThreadWrapper
        {
            private readonly Thread _thread;

            /// <summary>Initializes a new instance of the <see cref="ThreadWrapper" /> type.</summary>
            /// <param name="thread">The main thread.</param>
            public ThreadWrapper(Thread thread)
            {
                _thread = thread;

                ProcessThread = ProcessThread.Main;
            }

            /// <summary>Initializes a new instance of the <see cref="ThreadWrapper" /> type.</summary>
            /// <param name="threadWorker">The engine thread worker that will do work on the thread.</param>
            /// <param name="thread">The thread being wrapped.</param>
            /// <param name="terminationCountdownEvent">
            ///     A countdown event that is incremented when the thread starts and decremented when the
            ///     thread terminates.
            /// </param>
            /// <param name="unhandledExceptionManualResetEvent">
            ///     A manual reset event that can be set to indicate that an unhandled exception occurred on
            ///     a thread.
            /// </param>
            /// <param name="cancellationToken">A cancellation token that shuts down the thread.</param>
            public ThreadWrapper(
                IEngineThreadWorker threadWorker,
                EngineThread thread,
                CountdownEvent terminationCountdownEvent,
                ManualResetEventSlim unhandledExceptionManualResetEvent,
                CancellationToken cancellationToken)
            {
                ProcessThread = Enums.Parse<ProcessThread>(thread.AsString());

                _thread = new Thread(
                    () =>
                    {
                        // Indicate that the thread has started
                        terminationCountdownEvent.AddCount();

                        try
                        {
                            // Set the thread's name for easier debugging
                            Thread.CurrentThread.Name = $"{thread.AsString()} Thread";

                            // Prepare the worker for work
                            threadWorker.Prepare();

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                // Handle dispatched messages
                                threadWorker.HandleDispatchedMessages();

                                // Perform the thread's work
                                threadWorker.DoWork(cancellationToken);
                            }

                            // Clean up the worker
                            threadWorker.CleanUp();
                        }
                        catch (Exception exception)
                        {
                            UnhandledException = (thread, exception);

                            // Indicate that an unhandled exception occurred
                            unhandledExceptionManualResetEvent.Set();
                        }
                        finally
                        {
                            // Indicate that the thread has terminated
                            terminationCountdownEvent.Signal();
                        }
                    });
            }

            /// <summary>Gets the process thread type of the thread being wrapped.</summary>
            public ProcessThread ProcessThread { get; }

            /// <summary>Gets a tuple containing an unhandled exception and what engine thread it occurred on.</summary>
            public (EngineThread thread, Exception exception)? UnhandledException { get; private set; }

            /// <summary>Starts the thread.</summary>
            public void Start()
            {
                _thread.Start();
            }

            /// <summary>Verifies that the currently-executing thread is the specified thread.</summary>
            /// <exception cref="InvalidOperationException">Thrown when the current thread is not the specified thread.</exception>
            [DebuggerStepThrough]
            [Conditional("DEBUG")]
            public void Verify()
            {
                var actualThread = Thread.CurrentThread;

                if (actualThread != _thread)
                {
                    throw new InvalidOperationException($"Current thread should be {_thread.Name} but is actually {actualThread.Name ?? "an unknown thread"}.");
                }
            }
        }
    }
}