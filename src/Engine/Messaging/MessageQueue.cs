using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Logging;
using Subscriptions = System.Collections.Generic.Dictionary<BouncyBox.VorpalEngine.Engine.Messaging.SubscriptionToken, (System.Delegate handlerDelegate,
    BouncyBox.VorpalEngine.Engine.NestedContext context)>;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     <para>An in-memory non-thread-safe message queue.</para>
    ///     <para>Whether logging is performed is controlled by <see cref="MessageLogFilter" />.</para>
    /// </summary>
    public class MessageQueue<TMessageBase> : IMessageQueue<TMessageBase>
        where TMessageBase : IMessage
    {
        private readonly NestedContext _context;
        private readonly ContextSerilogLogger _serilogLogger;
        private readonly Dictionary<Type, Subscriptions> _subscriptionsByMessageType = new Dictionary<Type, Subscriptions>();

        /// <summary>Initializes a new instance of the <see cref="MessageQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <param name="queueName">The name of the queue, to be included in the nested context.</param>
        public MessageQueue(ISerilogLogger serilogLogger, NestedContext context, string queueName = nameof(MessageQueue<TMessageBase>))
        {
            _context = context.CopyAndPush(queueName);
            _serilogLogger = new ContextSerilogLogger(serilogLogger, _context);
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
            if (!_subscriptionsByMessageType.TryGetValue(message.GetType(), out Subscriptions? subscriptions))
            {
                // No subscribers for the message type
                return;
            }

            bool shouldLogMessage = message.ShouldLog();
            string sourceContext = context.BuildString();
            string messageType = typeof(TMessage).Name;

            foreach ((Delegate handlerDelegate, NestedContext subscriptionContext) in subscriptions.Values)
            {
                string destinationContext = subscriptionContext.BuildString();

                if (shouldLogMessage)
                {
                    _serilogLogger.LogDebug(
                        "{SourceContext} is dispatching {MessageType} to {DestinationContext}",
                        sourceContext,
                        messageType,
                        destinationContext);
                }

                ((Action<TMessage>)handlerDelegate)(message);

                if (shouldLogMessage)
                {
                    _serilogLogger.LogVerbose(
                        "{SourceContext} dispatched {MessageType} to {DestinationContext}",
                        sourceContext,
                        messageType,
                        destinationContext);
                }
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
        public SubscriptionToken Subscribe<TMessage>(Action<TMessage> handlerDelegate, NestedContext context)
            where TMessage : TMessageBase
        {
            Type messageType = typeof(TMessage);
            var subscriptionToken = new SubscriptionToken(messageType);

            if (!_subscriptionsByMessageType.TryGetValue(messageType, out Subscriptions? subscriptions))
            {
                _subscriptionsByMessageType.Add(messageType, subscriptions = new Subscriptions());
            }

            subscriptions.Add(subscriptionToken, (handlerDelegate, context));

            if (MessageLogFilter.ShouldLogMessageTypeDelegate(messageType))
            {
                _serilogLogger.LogDebug("{Context} subscribed to {MessageType}", context.BuildString(), messageType.Name);
            }

            return subscriptionToken;
        }

        /// <inheritdoc />
        public SubscriptionToken Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : TMessageBase
        {
            return Subscribe(handlerDelegate, NestedContext.None());
        }

        /// <inheritdoc />
        public void Unsubscribe(IEnumerable<SubscriptionToken> tokens, NestedContext context)
        {
            tokens = tokens.ToImmutableArray();

            foreach (Subscriptions subscriptions in _subscriptionsByMessageType.Values)
            {
                foreach (SubscriptionToken token in tokens)
                {
                    subscriptions.Remove(token);
                }
            }

            foreach (SubscriptionToken token in tokens.Where(a => MessageLogFilter.ShouldLogMessageTypeDelegate(a.MessageType)))
            {
                _serilogLogger.LogDebug("{Context} unsubscribed from {MessageType}", context.BuildString(), token.MessageType.Name);
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
            Unsubscribe(tokens, _context);
        }
    }
}