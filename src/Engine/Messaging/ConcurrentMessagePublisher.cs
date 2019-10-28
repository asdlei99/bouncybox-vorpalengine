using BouncyBox.VorpalEngine.Common;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Proxies publishing messages to a concurrent message queue to allow for method chaining.</summary>
    public class ConcurrentMessagePublisher<TMessageBase>
        where TMessageBase : IGlobalMessage
    {
        private readonly IConcurrentMessageQueue<TMessageBase> _concurrentMessageQueue;
        private readonly NestedContext _context;

        /// <summary>Initializes a new instance of the <see cref="ConcurrentMessagePublisher{TMessageBase}" /> type.</summary>
        /// <param name="concurrentMessageQueue">A concurrent message queue.</param>
        /// <param name="context">A nested context.</param>
        public ConcurrentMessagePublisher(IConcurrentMessageQueue<TMessageBase> concurrentMessageQueue, NestedContext context)
        {
            _concurrentMessageQueue = concurrentMessageQueue;
            _context = context;
        }

        /// <summary>Initializes a new instance of the <see cref="ConcurrentMessagePublisher{TMessageBase}" /> type.</summary>
        /// <param name="concurrentMessageQueue">A concurrent message queue.</param>
        public ConcurrentMessagePublisher(IConcurrentMessageQueue<TMessageBase> concurrentMessageQueue)
            : this(concurrentMessageQueue, NestedContext.None())
        {
        }

        /// <summary>Queues a message for dispatch.</summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>Returns the concurrent message publisher.</returns>
        public ConcurrentMessagePublisher<TMessageBase> Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase
        {
            _concurrentMessageQueue.Publish(message, _context);

            return this;
        }

        /// <summary>Queues a message for dispatch.</summary>
        /// <returns>Returns the concurrent message publisher.</returns>
        public ConcurrentMessagePublisher<TMessageBase> Publish<TMessage>()
            where TMessage : TMessageBase, new()
        {
            _concurrentMessageQueue.Publish<TMessage>(_context);

            return this;
        }
    }
}