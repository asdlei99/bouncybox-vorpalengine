using System.Threading;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>Represents an engine thread worker.</summary>
    public interface IEngineThreadWorker
    {
        /// <summary>Prepares the worker for work.</summary>
        void Prepare();

        /// <summary>Performs the work.</summary>
        /// <param name="cancellationToken">A cancellation token whose cancellation signals the thread is attempting to shut down.</param>
        void DoWork(CancellationToken cancellationToken = default);

        /// <summary>Performs post-work clean-up.</summary>
        void CleanUp();

        /// <summary>Handles dispatched messages.</summary>
        void HandleDispatchedMessages();
    }
}