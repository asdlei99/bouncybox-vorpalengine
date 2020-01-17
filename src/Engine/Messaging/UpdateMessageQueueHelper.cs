using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Common;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Proxies subscriptions to an update message queue in order to make unsubscription easier.</summary>
    public class UpdateMessageQueueHelper : IDisposable
    {
        private readonly NestedContext _context;
        private readonly IMessageQueue<IUpdateMessage> _messageQueue;
        private readonly HashSet<ISubscriptionReceipt> _subscriptionReceipts = new HashSet<ISubscriptionReceipt>();
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="UpdateMessageQueueHelper" /> class.</summary>
        /// <param name="messageQueue">An <see cref="IMessageQueue{TMessageBase}" /> implementation.</param>
        /// <param name="context"></param>
        public UpdateMessageQueueHelper(IMessageQueue<IUpdateMessage> messageQueue, NestedContext context)
        {
            _messageQueue = messageQueue;
            _context = context;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _messageQueue.Unsubscribe(_subscriptionReceipts);
                    _subscriptionReceipts.Clear();
                },
                ref _isDisposed);
        }

        /// <summary>Publishes a message to any subscribers subscribed to the message type.</summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>Returns the update message queue helper.</returns>
        public UpdateMessageQueueHelper Publish<TMessage>(TMessage message)
            where TMessage : IUpdateMessage
        {
            _messageQueue.Publish(message, _context);

            return this;
        }

        /// <summary>Publishes a message to any subscribers subscribed to the message type.</summary>
        /// <returns>Returns the update message queue helper.</returns>
        public UpdateMessageQueueHelper Publish<TMessage>()
            where TMessage : IUpdateMessage, new()
        {
            _messageQueue.Publish<TMessage>(_context);

            return this;
        }

        /// <summary>Subscribes to a message and provides a handler delegate that will handle published messages of the specified type.</summary>
        /// <param name="handlerDelegate">A delegate that is invoked when a message of the specified type is published.</param>
        /// <returns>Returns the update message queue helper.</returns>
        public UpdateMessageQueueHelper Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : IUpdateMessage
        {
            ISubscriptionReceipt subscriptionReceipt = _messageQueue.Subscribe(handlerDelegate, _context);

            _subscriptionReceipts.Add(subscriptionReceipt);

            return this;
        }
    }
}