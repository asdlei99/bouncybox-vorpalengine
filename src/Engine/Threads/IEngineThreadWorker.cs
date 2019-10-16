using System.Threading;
using BouncyBox.VorpalEngine.Engine.Messaging;

namespace BouncyBox.VorpalEngine.Engine.Threads
{
    /// <summary>
    ///     Represents an engine thread worker.
    /// </summary>
    public interface IEngineThreadWorker
    {
        /// <summary>
        ///     Gets the nested context;
        /// </summary>
        NestedContext Context { get; }

        /// <summary>
        ///     Subscribes to global messages.
        /// </summary>
        /// <param name="globalMessagePublisherSubscriber">A global message publisher/subscriber.</param>
        void SubscribeToMessages(ConcurrentMessagePublisherSubscriber<IGlobalMessage> globalMessagePublisherSubscriber);

        /// <summary>
        ///     Performs the thread's work.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token whose cancellation signals the thread is attempting to shut down.</param>
        void DoWork(CancellationToken cancellationToken);

        /// <summary>
        ///     Performs post-work clean-up.
        /// </summary>
        void CleanUp();
    }
}