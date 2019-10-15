using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     <para>A composition of a message publisher and message subscriber.</para>
    ///     <para>This class is the preferred way to publishing to and subscribing to messages with a message queue.</para>
    /// </summary>
    public class MessagePublisherSubscriber<TMessageBase> : IDisposable
        where TMessageBase : IMessage
    {
        private readonly MessagePublisher<TMessageBase> _messagePublisher;
        private readonly MessageSubscriber<TMessageBase> _messageSubscriber;
        private bool _isDisposed;

        private MessagePublisherSubscriber(MessagePublisher<TMessageBase> messagePublisher, MessageSubscriber<TMessageBase> messageSubscriber)
        {
            _messagePublisher = messagePublisher;
            _messageSubscriber = messageSubscriber;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(() => { _messageSubscriber?.Dispose(); }, ref _isDisposed);
        }

        /// <summary>
        ///     Publishes a message to any subscribers subscribes to the message type.
        /// </summary>
        /// <returns>Returns the message publisher/subscriber.</returns>
        public MessagePublisherSubscriber<TMessageBase> Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase, new()
        {
            _messagePublisher.Publish(message);

            return this;
        }

        /// <summary>
        ///     Publishes a message to any subscribers subscribes to the message type.
        /// </summary>
        /// <returns>Returns the message publisher/subscriber.</returns>
        public MessagePublisherSubscriber<TMessageBase> Publish<TMessage>()
            where TMessage : TMessageBase, new()
        {
            _messagePublisher.Publish<TMessage>();

            return this;
        }

        /// <summary>
        ///     Subscribes to a message and provides a handler delegate that will process published messages of the specified type. Tracks
        ///     the subscription token for unsubscription later.
        /// </summary>
        /// <param name="handlerDelegate">A delegate that is invoked when a message of the specified type is published.</param>
        /// <returns>Returns the message publisher/subscriber.</returns>
        public MessagePublisherSubscriber<TMessageBase> Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : TMessageBase
        {
            _messageSubscriber.Subscribe(handlerDelegate);

            return this;
        }

        /// <summary>
        ///     Creates a new message publisher/subscriber.
        /// </summary>
        /// <param name="messageQueue">The message queue to proxy.</param>
        /// <param name="context">A nested context.</param>
        /// <returns>Returns a new message publisher/subscriber.</returns>
        public static MessagePublisherSubscriber<TMessageBase> Create(IMessageQueue<TMessageBase> messageQueue, NestedContext context)
        {
            return new MessagePublisherSubscriber<TMessageBase>(
                new MessagePublisher<TMessageBase>(messageQueue, context),
                new MessageSubscriber<TMessageBase>(messageQueue, context));
        }

        /// <summary>
        ///     Creates a new message publisher/subscriber.
        /// </summary>
        /// <param name="messageQueue">The message queue to proxy.</param>
        /// <returns>Returns a new message publisher/subscriber.</returns>
        public static MessagePublisherSubscriber<TMessageBase> Create(IMessageQueue<TMessageBase> messageQueue)
        {
            return new MessagePublisherSubscriber<TMessageBase>(
                new MessagePublisher<TMessageBase>(messageQueue),
                new MessageSubscriber<TMessageBase>(messageQueue));
        }
    }
}