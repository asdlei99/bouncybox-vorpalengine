using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Logging;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     <para>
    ///         An in-memory thread-safe message queue that does not directly handle published messages but rather publishes them to a
    ///         concurrent message dispatch queue for handling by another object.
    ///     </para>
    ///     <para>Whether logging is performed is controlled by <see cref="MessageLogFilter" />.</para>
    /// </summary>
    public class ConcurrentMessageQueue<TMessageBase> : IConcurrentMessageQueue<TMessageBase>
        where TMessageBase : IGlobalMessage
    {
        private readonly NestedContext _context;
        private readonly ConcurrentQueue<TMessageBase> _publishQueue = new ConcurrentQueue<TMessageBase>();
        private readonly ContextSerilogLogger _serilogLogger;

        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<SubscriptionToken, ConcurrentMessageDispatchQueue<TMessageBase>>>
            _subscriptionsByMessageType =
                new ConcurrentDictionary<Type, ConcurrentDictionary<SubscriptionToken, ConcurrentMessageDispatchQueue<TMessageBase>>>();

        /// <summary>Initializes a new instance of the <see cref="ConcurrentMessageQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public ConcurrentMessageQueue(ISerilogLogger serilogLogger, NestedContext context)
        {
            _context = context.CopyAndPush(nameof(ConcurrentMessageQueue<TMessageBase>));
            _serilogLogger = new ContextSerilogLogger(serilogLogger, _context);
        }

        /// <summary>Initializes a new instance of the <see cref="ConcurrentMessageQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        public ConcurrentMessageQueue(ISerilogLogger serilogLogger)
            : this(serilogLogger, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public void Publish<TMessage>(TMessage message, NestedContext context)
            where TMessage : TMessageBase
        {
            // Enqueue the message
            _publishQueue.Enqueue(message);
        }

        /// <inheritdoc />
        public void Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase
        {
            Publish(message, _context);
        }

        /// <inheritdoc />
        public void Publish<TMessage>(NestedContext context)
            where TMessage : TMessageBase, new()
        {
            Publish(new TMessage(), context);
        }

        /// <inheritdoc />
        public void Publish<TMessage>()
            where TMessage : TMessageBase, new()
        {
            Publish(new TMessage(), NestedContext.None());
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the sub</exception>
        public SubscriptionToken Subscribe<TMessage>(ConcurrentMessageDispatchQueue<TMessageBase> messageDispatchQueue, NestedContext context)
            where TMessage : TMessageBase
        {
            Type messageType = typeof(TMessage);
            var subscriptionToken = new SubscriptionToken(messageType);
            ConcurrentDictionary<SubscriptionToken, ConcurrentMessageDispatchQueue<TMessageBase>> subscriptions =
                _subscriptionsByMessageType.GetOrAdd(
                    messageType,
                    a => new ConcurrentDictionary<SubscriptionToken, ConcurrentMessageDispatchQueue<TMessageBase>>());

            subscriptions.TryAdd(subscriptionToken, messageDispatchQueue);

            if (MessageLogFilter.ShouldLogMessageTypeDelegate(messageType))
            {
                _serilogLogger.LogDebug("{Context} subscribed to {MessageType}", context.BuildString(), messageType.Name);
            }

            return subscriptionToken;
        }

        /// <inheritdoc />
        public SubscriptionToken Subscribe<TMessage>(ConcurrentMessageDispatchQueue<TMessageBase> messageDispatchQueue)
            where TMessage : TMessageBase
        {
            return Subscribe<TMessage>(messageDispatchQueue, NestedContext.None());
        }

        /// <inheritdoc />
        public void Unsubscribe(IEnumerable<SubscriptionToken> tokens, NestedContext context)
        {
            tokens = tokens.ToImmutableArray();

            foreach (SubscriptionToken token in tokens)
            {
                _subscriptionsByMessageType[token.MessageType].TryRemove(token, out _);

                if (MessageLogFilter.ShouldLogMessageTypeDelegate(token.MessageType))
                {
                    _serilogLogger.LogDebug("{Context} unsubscribed from {MessageType}", context.BuildString(), token.MessageType.Name);
                }
            }
        }

        /// <inheritdoc />
        public void Unsubscribe(IEnumerable<SubscriptionToken> tokens)
        {
            Unsubscribe(tokens, NestedContext.None());
        }

        /// <inheritdoc />
        public void Unsubscribe(NestedContext context, params SubscriptionToken[] tokens)
        {
            Unsubscribe(tokens, context);
        }

        /// <inheritdoc />
        public void Unsubscribe(params SubscriptionToken[] tokens)
        {
            Unsubscribe(tokens, NestedContext.None());
        }

        /// <inheritdoc />
        public void DispatchQueued()
        {
            // Process all queued messages in publish order
            while (_publishQueue.TryDequeue(out TMessageBase message))
            {
                if (!_subscriptionsByMessageType.TryGetValue(
                        message.GetType(),
                        out ConcurrentDictionary<SubscriptionToken, ConcurrentMessageDispatchQueue<TMessageBase>>? subscriptions))
                {
                    // No subscribers for the message type
                    continue;
                }

                foreach ((SubscriptionToken _, ConcurrentMessageDispatchQueue<TMessageBase> messageDispatchQueue) in subscriptions)
                {
                    messageDispatchQueue.Dispatch(message);
                }
            }
        }
    }
}