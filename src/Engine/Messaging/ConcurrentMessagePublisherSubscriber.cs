using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     <para>
    ///         A composition of a concurrent message publisher, a concurrent message subscriber, and a concurrent message dispatch
    ///         queue.
    ///     </para>
    ///     <para>This class is the preferred way to publishing to and subscribing to messages with a concurrent message queue.</para>
    /// </summary>
    public class ConcurrentMessagePublisherSubscriber<TMessageBase> : IDisposable
        where TMessageBase : IGlobalMessage
    {
        private readonly ConcurrentMessageDispatchQueue<TMessageBase> _messageDispatchQueue;
        private readonly ConcurrentMessagePublisher<TMessageBase> _messagePublisher;
        private readonly ConcurrentMessageSubscriber<TMessageBase> _messageSubscriber;
        private bool _isDisposed;

        private ConcurrentMessagePublisherSubscriber(
            ConcurrentMessagePublisher<TMessageBase> messagePublisher,
            ConcurrentMessageSubscriber<TMessageBase> messageSubscriber,
            ConcurrentMessageDispatchQueue<TMessageBase> messageDispatchQueue)
        {
            _messagePublisher = messagePublisher;
            _messageSubscriber = messageSubscriber;
            _messageDispatchQueue = messageDispatchQueue;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _messageSubscriber?.Dispose();
                    _messageDispatchQueue.Dispose();
                },
                ref _isDisposed);
        }

        /// <summary>
        ///     Queues a message for dispatch.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>Returns the concurrent message publisher/subscriber.</returns>
        public ConcurrentMessagePublisherSubscriber<TMessageBase> Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase
        {
            _messagePublisher.Publish(message);

            return this;
        }

        /// <summary>
        ///     Queues a message for dispatch.
        /// </summary>
        /// <returns>Returns the concurrent message publisher/subscriber.</returns>
        public ConcurrentMessagePublisherSubscriber<TMessageBase> Publish<TMessage>()
            where TMessage : TMessageBase, new()
        {
            _messagePublisher.Publish<TMessage>();

            return this;
        }

        /// <summary>
        ///     Subscribes to messages of the specified type and invokes the specified delegate when a dispatched message of the specified
        ///     type is handled.
        /// </summary>
        /// <param name="handlerDelegate">A delegate that is invoked when a dispatched message of the specified type is handled.</param>
        /// <returns>Returns the concurrent message publisher/subscriber.</returns>
        public ConcurrentMessagePublisherSubscriber<TMessageBase> Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : TMessageBase
        {
            _messageDispatchQueue.Handle(handlerDelegate);
            _messageSubscriber.Subscribe<TMessage>(_messageDispatchQueue);

            return this;
        }

        /// <summary>
        ///     Dequeues dispatched messages, calling the appropriate handler for each message.
        /// </summary>
        public void HandleDispatched()
        {
            _messageDispatchQueue.HandleDispatched();
        }

        /// <summary>
        ///     Creates a new concurrent message publisher/subscriber.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <returns>Returns a new concurrent message publisher/subscriber.</returns>
        public static ConcurrentMessagePublisherSubscriber<TMessageBase> Create(IInterfaces interfaces, NestedContext context)
        {
            return new ConcurrentMessagePublisherSubscriber<TMessageBase>(
                new ConcurrentMessagePublisher<TMessageBase>((IConcurrentMessageQueue<TMessageBase>)interfaces.GlobalConcurrentMessageQueue, context),
                new ConcurrentMessageSubscriber<TMessageBase>((IConcurrentMessageQueue<TMessageBase>)interfaces.GlobalConcurrentMessageQueue, context),
                new ConcurrentMessageDispatchQueue<TMessageBase>(interfaces.SerilogLogger, context));
        }

        /// <summary>
        ///     Creates a new concurrent message publisher/subscriber.
        /// </summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <returns>Returns a new concurrent message publisher/subscriber.</returns>
        public static ConcurrentMessagePublisherSubscriber<TMessageBase> Create(IInterfaces interfaces)
        {
            return Create(interfaces, NestedContext.None());
        }
    }
}