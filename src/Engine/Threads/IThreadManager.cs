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
        ///     Starts and tracks an engine thread.
        /// </summary>
        /// <param name="thread">The engine thread to start and track.</param>
        /// <param name="delegate">A delegate to invoke when the thread spawns.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StartEngineThread(EngineThread thread, Action @delegate);

        /// <summary>
        ///     Verifies that the currently-executing thread is the specified thread.
        /// </summary>
        /// <param name="thread">The process thread to verify.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void VerifyProcessThread(ProcessThread thread);
    }
}