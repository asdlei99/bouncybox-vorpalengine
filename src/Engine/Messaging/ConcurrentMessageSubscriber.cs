using System;
using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     Proxies subscriptions to a concurrent message queue in order to make unsubscription easier.
    /// </summary>
    public class ConcurrentMessageSubscriber<TMessageBase> : IDisposable
        where TMessageBase : IGlobalMessage
    {
        private readonly NestedContext _context;
        private readonly IConcurrentMessageQueue<TMessageBase> _messageQueue;
        private readonly HashSet<SubscriptionToken> _tokens = new HashSet<SubscriptionToken>();
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of <see cref="ConcurrentMessageSubscriber{TMessageBase}" />.
        /// </summary>
        /// <param name="messageQueue">A concurrent message queue.</param>
        /// <param name="context">A nested context.</param>
        public ConcurrentMessageSubscriber(IConcurrentMessageQueue<TMessageBase> messageQueue, NestedContext context)
        {
            _messageQueue = messageQueue;
            _context = context;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
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
        ///     Subscribes to a message and provides a concurrent message dispatch queue that will receive dispatched messages of the
        ///     specified type. Tracks the subscription token for unsubscription later.
        /// </summary>
        /// <param name="messageDispatchQueue">A concurrent message dispatch queue.</param>
        /// <returns>Returns the concurrent message subscriber.</returns>
        public ConcurrentMessageSubscriber<TMessageBase> Subscribe<TMessage>(ConcurrentMessageDispatchQueue<TMessageBase> messageDispatchQueue)
            where TMessage : TMessageBase
        {
            _tokens.Add(_messageQueue.Subscribe<TMessage>(messageDispatchQueue, _context));

            return this;
        }
    }
}