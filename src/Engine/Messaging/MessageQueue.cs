using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Logging;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     <para>An in-memory non-thread-safe message queue.</para>
    ///     <para>Whether logging is performed is controlled by <see cref="MessageLogFilter" />.</para>
    /// </summary>
    public class MessageQueue<TMessageBase> : IMessageQueue<TMessageBase>
        where TMessageBase : IMessage
    {
        private readonly ContextSerilogLogger _serilogLogger;
        private readonly Dictionary<Type, HashSet<Subscription>> _subscriptionsByMessageType = new Dictionary<Type, HashSet<Subscription>>();

        /// <summary>Initializes a new instance of the <see cref="MessageQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <param name="queueName">The name of the queue, to be included in the nested context.</param>
        public MessageQueue(ISerilogLogger serilogLogger, NestedContext context, string queueName = nameof(MessageQueue<TMessageBase>))
        {
            _serilogLogger = new ContextSerilogLogger(serilogLogger, context.Push(queueName));
        }

        /// <summary>Initializes a new instance of the <see cref="MessageQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="queueName">The name of the queue, to be used in a new nested context.</param>
        public MessageQueue(ISerilogLogger serilogLogger, string queueName = nameof(MessageQueue<TMessageBase>))
            : this(serilogLogger, NestedContext.None(), queueName)
        {
        }

        /// <inheritdoc />
        public void Publish<TMessage>(TMessage message, NestedContext context)
            where TMessage : TMessageBase
        {
            Type messageType = typeof(TMessage);

            if (!_subscriptionsByMessageType.TryGetValue(messageType, out HashSet<Subscription>? subscriptions))
            {
                // No subscribers for the message type
                return;
            }

            bool shouldLog = message.ShouldLog();

            if (shouldLog)
            {
                _serilogLogger.LogDebug("{Context} is publishing {MessageType}", context.Context ?? "An unknown context", messageType.Name);
            }

            foreach (Subscription subscription in subscriptions)
            {
                if (shouldLog)
                {
                    _serilogLogger.LogDebug("{Context} is handling {MessageType}", subscription.Context.Context ?? "An unknown context", messageType.Name);
                }

                ((Action<TMessage>)subscription.HandlerDelegate)(message);

                if (shouldLog)
                {
                    _serilogLogger.LogVerbose("{Context} handled {MessageType}", subscription.Context.Context ?? "An unknown context", messageType.Name);
                }
            }

            if (shouldLog)
            {
                _serilogLogger.LogVerbose("{Context published {MessageType}", context.Context ?? "An unknown context", messageType.Name);
            }
        }

        /// <inheritdoc />
        public void Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase
        {
            Publish(message, NestedContext.None());
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
        public ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate, NestedContext context)
            where TMessage : TMessageBase
        {
            Type messageType = typeof(TMessage);
            var subscription = new Subscription(messageType, handlerDelegate, context);

            if (!_subscriptionsByMessageType.TryGetValue(messageType, out HashSet<Subscription>? subscriptions))
            {
                _subscriptionsByMessageType.Add(messageType, subscriptions = new HashSet<Subscription>());
            }

            subscriptions.Add(subscription);

            if (MessageLogFilter.ShouldLogMessageTypeDelegate(messageType))
            {
                _serilogLogger.LogDebug("{Context} subscribed to {MessageType}", context.Context ?? "An unknown context", messageType.Name);
            }

            return subscription;
        }

        /// <inheritdoc />
        public ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : TMessageBase
        {
            return Subscribe(handlerDelegate, NestedContext.None());
        }

        /// <inheritdoc />
        public void Unsubscribe(IEnumerable<ISubscriptionReceipt> subscriptionReceipts)
        {
            ImmutableArray<Subscription> subscriptions = subscriptionReceipts.OfType<Subscription>().ToImmutableArray();

            foreach (HashSet<Subscription> hashSet in _subscriptionsByMessageType.Values)
            foreach (Subscription subscription in subscriptions)
            {
                hashSet.Remove(subscription);
            }

            foreach (Subscription subscription in subscriptions.Where(a => MessageLogFilter.ShouldLogMessageTypeDelegate(a.MessageType)))
            {
                _serilogLogger.LogDebug(
                    "{Context} unsubscribed from {MessageType}",
                    subscription.Context.Context ?? "An unknown context",
                    subscription.MessageType.Name);
            }
        }

        /// <inheritdoc />
        public void Unsubscribe(params ISubscriptionReceipt[] subscriptionReceipts)
        {
            Unsubscribe((IEnumerable<ISubscriptionReceipt>)subscriptionReceipts);
        }

        private class Subscription : ISubscriptionReceipt
        {
            public Subscription(Type messageType, Delegate handlerDelegate, NestedContext context)
            {
                MessageType = messageType;
                HandlerDelegate = handlerDelegate;
                Context = context;
            }

            public Type MessageType { get; }
            public Delegate HandlerDelegate { get; }
            public NestedContext Context { get; }
        }
    }
}