using BouncyBox.VorpalEngine.Common;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Proxies publishing messages to a message queue to allow for method chaining.</summary>
    public class MessagePublisher<TMessageBase>
        where TMessageBase : IMessage
    {
        private readonly NestedContext _context;
        private readonly IMessageQueue<TMessageBase> _messageQueue;

        /// <summary>Initializes a new instance of the <see cref="MessagePublisher{TMessageBase}" /> type.</summary>
        /// <param name="messageQueue">A message queue.</param>
        /// <param name="context">A nested context.</param>
        public MessagePublisher(IMessageQueue<TMessageBase> messageQueue, NestedContext context)
        {
            _messageQueue = messageQueue;
            _context = context;
        }

        /// <summary>Initializes a new instance of the <see cref="MessagePublisher{TMessageBase}" /> type.</summary>
        /// <param name="messageQueue">A message queue.</param>
        public MessagePublisher(IMessageQueue<TMessageBase> messageQueue)
            : this(messageQueue, NestedContext.None())
        {
        }

        /// <summary>Publishes a message to any subscribers subscribes to the message type.</summary>
        /// <returns>Returns the message publisher.</returns>
        public MessagePublisher<TMessageBase> Publish<TMessage>()
            where TMessage : TMessageBase, new()
        {
            _messageQueue.Publish<TMessage>(_context);

            return this;
        }

        /// <summary>Publishes a message to any subscribers subscribes to the message type.</summary>
        /// <returns>Returns the message publisher.</returns>
        public MessagePublisher<TMessageBase> Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase, new()
        {
            _messageQueue.Publish(message, _context);

            return this;
        }
    }
}