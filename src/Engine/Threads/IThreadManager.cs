using System;
using System.Collections.Generic;
using System.Threading;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>Represents the spawning and tracking of various threads used by the engine.</summary>
    public interface IThreadManager : IDisposable
    {
        /// <summary>Verifies that the currently-executing thread is the specified thread.</summary>
        /// <param name="thread">The currently-executing thread must be the specified thread.</param>
        void VerifyProcessThread(ProcessThread thread);

        /// <summary>Starts an engine thread.</summary>
        /// <param name="threadWorker">A thread worker.</param>
        /// <param name="thread">The engine thread to start.</param>
        /// <param name="terminationCountdownEvent">
        ///     A countdown event that is incremented when the thread starts and decremented when the
        ///     thread terminates.
        /// </param>
        /// <param name="unhandledExceptionManualResetEvent">
        ///     A manual reset event that is set if an engine thread throws an unhandled
        ///     exception.
        /// </param>
        void StartEngineThread(
            IEngineThreadWorker threadWorker,
            EngineThread thread,
            CountdownEvent terminationCountdownEvent,
            ManualResetEventSlim unhandledExceptionManualResetEvent);

        /// <summary>Request that engine threads gracefully terminate, then wait for all threads to complete.</summary>
        /// <param name="terminationCountdownEvent">A countdown event that is decremented as each thread terminates.</param>
        /// <returns>Returns a collection of tuples containing unhandled exceptions and the engine threads they occurred on.</returns>
        IReadOnlyCollection<(EngineThread thread, Exception exception)> RequestEngineThreadTerminationAndWaitForTermination(
            CountdownEvent terminationCountdownEvent);

        /// <summary>Disposes an object only if the provided flag is false. The flag is set to true after disposal.</summary>
        /// <param name="delegate">A delegate that disposes the object.</param>
        /// <param name="isDisposed">A flag indicating if the object was disposed.</param>
        /// <param name="thread">The currently-executing thread must be the specified thread.</param>
        void DisposeHelper(Action @delegate, ref bool isDisposed, ProcessThread thread);
    }
}