using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using EnumsNET;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>
    ///     Spawns and tracks various threads used by the engine.
    /// </summary>
    public class ThreadManager : IThreadManager
    {
        private readonly Dictionary<ProcessThread, Thread> _threadsByEngineThread = new Dictionary<ProcessThread, Thread>();
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThreadManager" /> type.
        /// </summary>
        /// <param name="mainThread">The main thread.</param>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the main thread.</exception>
        public ThreadManager(Thread mainThread)
        {
            _threadsByEngineThread.Add(ProcessThread.Main, mainThread);

            VerifyProcessThread(ProcessThread.Main);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the specified thread is already started.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StartEngineThread(EngineThread thread, Action @delegate)
        {
            VerifyProcessThread(ProcessThread.Main);

            var processThread = Enums.Parse<ProcessThread>(thread.GetName());
            string threadName = thread.GetName();

            if (_threadsByEngineThread.ContainsKey(processThread))
            {
                throw new InvalidOperationException($"{threadName} thread is already started.");
            }

            var newThread =
                new Thread(
                    () =>
                    {
                        Thread.CurrentThread.Name = $"{threadName} Thread";
                        @delegate();
                    });

            _threadsByEngineThread.Add(processThread, newThread);

            newThread.Start();
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
            Thread currentThread = Thread.CurrentThread;

            if (!_threadsByEngineThread.TryGetValue(thread, out Thread? expectedThread))
            {
                throw new InvalidOperationException($"{thread} thread has not been started.");
            }
            if (currentThread != expectedThread)
            {
                throw new InvalidOperationException($"Current thread should be {expectedThread.Name} but is actually {currentThread.Name}.");
            }
#endif
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(_threadsByEngineThread.Clear, ref _isDisposed);
        }
    }
}