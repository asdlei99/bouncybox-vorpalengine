using System;
using System.Runtime.CompilerServices;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>
    ///     Represents the spawning and tracking of various threads used by the engine.
    /// </summary>
    public interface IThreadManager : IDisposable
    {
        /// <summary>
        ///     Starts an engine thread.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="threadWorker">A thread worker.</param>
        /// <param name="thread">The engine thread to start.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StartEngineThread(IInterfaces interfaces, IEngineThreadWorker threadWorker, EngineThread thread);

        /// <summary>
        ///     Stops all engine threads.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StopEngineThreads();

        /// <summary>
        ///     Verifies that the currently-executing thread is the specified thread.
        /// </summary>
        /// <param name="thread">The process thread to verify.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void VerifyProcessThread(ProcessThread thread);
    }
}