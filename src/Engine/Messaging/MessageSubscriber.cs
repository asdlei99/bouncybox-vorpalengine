using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Common;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Proxies subscriptions to a message queue in order to make unsubscription easier.</summary>
    public class MessageSubscriber<TMessageBase> : IDisposable
        where TMessageBase : IMessage
    {
        private readonly NestedContext _context;
        private readonly IMessageQueue<TMessageBase> _messageQueue;
        private readonly HashSet<SubscriptionToken> _tokens = new HashSet<SubscriptionToken>();
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="MessageSubscriber{TMessageBase}" /> type.</summary>
        /// <param name="messageQueue">The message queue that issued the subscription tokens.</param>
        /// <param name="context">A nested context.</param>
        public MessageSubscriber(IMessageQueue<TMessageBase> messageQueue, NestedContext context)
        {
            _messageQueue = messageQueue;
            _context = context;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageSubscriber{TMessageBase}" /> type.</summary>
        /// <param name="messageQueue">The message queue that issued the subscription tokens.</param>
        public MessageSubscriber(IMessageQueue<TMessageBase> messageQueue)
            : this(messageQueue, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _messageQueue.Unsubscribe(_tokens, _context);
                    _tokens.Clear();
                },
                ref _isDisposed);
        }

        /// <summary>
        ///     Subscribes to a message and provides a handler delegate that will process published messages of the specified type. Tracks
        ///     the subscription token for unsubscription later.
        /// </summary>
        /// <param name="handlerDelegate">A delegate that is invoked when a message of the specified type is published.</param>
        /// <returns>Returns the message subscriber.</returns>
        public MessageSubscriber<TMessageBase> Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : TMessageBase
        {
            _tokens.Add(_messageQueue.Subscribe(handlerDelegate, _context));

            return this;
        }
    }
}