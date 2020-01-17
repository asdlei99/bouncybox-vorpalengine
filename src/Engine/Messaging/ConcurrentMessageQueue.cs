using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Threads;
using ConcurrentCollections;
using EnumsNET;

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
        private readonly ConcurrentDictionary<ProcessThread, ConcurrentQueue<Action>> _queuedHandlerDelegatesByProcessThread =
            new ConcurrentDictionary<ProcessThread, ConcurrentQueue<Action>>();

        private readonly ContextSerilogLogger _serilogLogger;

        private readonly ConcurrentDictionary<Type, ConcurrentHashSet<Subscription>> _subscriptionReceiptsByMessageType =
            new ConcurrentDictionary<Type, ConcurrentHashSet<Subscription>>();

        /// <summary>Initializes a new instance of the <see cref="ConcurrentMessageQueue{TMessageBase}" /> type.</summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public ConcurrentMessageQueue(ISerilogLogger serilogLogger, NestedContext context)
        {
            _serilogLogger = new ContextSerilogLogger(serilogLogger, context.Push(nameof(ConcurrentMessageQueue<TMessageBase>)));

            foreach (ProcessThread thread in Enums.GetValues<ProcessThread>())
            {
                _queuedHandlerDelegatesByProcessThread.TryAdd(thread, new ConcurrentQueue<Action>());
            }
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
            Type messageType = typeof(TMessage);

            if (!_subscriptionReceiptsByMessageType.TryGetValue(messageType, out ConcurrentHashSet<Subscription>? subscriptions))
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
                _queuedHandlerDelegatesByProcessThread[subscription.Thread].Enqueue(
                    () =>
                    {
                        if (shouldLog)
                        {
                            _serilogLogger.LogDebug(
                                "{Context} is handling {MessageType}",
                                subscription.Context.Context ?? "An unknown context",
                                messageType.Name);
                        }

                        ((Action<TMessage>)subscription.HandlerDelegate)(message);

                        if (shouldLog)
                        {
                            _serilogLogger.LogVerbose(
                                "{Context} handled {MessageType}",
                                subscription.Context.Context ?? "An unknown context",
                                messageType.Name);
                        }
                    });
            }

            if (shouldLog)
            {
                _serilogLogger.LogVerbose("{Context} published {MessageType}", context.Context ?? "An unknown context", messageType.Name);
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
        public ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate, ProcessThread thread, NestedContext context)
            where TMessage : TMessageBase
        {
            Type messageType = typeof(TMessage);
            var subscription = new Subscription(messageType, thread, handlerDelegate, context);
            ConcurrentHashSet<Subscription> subscriptions =
                _subscriptionReceiptsByMessageType.GetOrAdd(messageType, a => new ConcurrentHashSet<Subscription>());

            if (subscriptions.Add(subscription) && MessageLogFilter.ShouldLogMessageTypeDelegate(messageType))
            {
                _serilogLogger.LogDebug("{Context} subscribed to {MessageType}", context.Context ?? "An unknown context", messageType.Name);
            }

            return subscription;
        }

        /// <inheritdoc />
        public ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate, ProcessThread thread)
            where TMessage : TMessageBase
        {
            return Subscribe(handlerDelegate, thread, NestedContext.None());
        }

        /// <inheritdoc />
        public void Unsubscribe(IEnumerable<ISubscriptionReceipt> subscriptionReceipts)
        {
            IEnumerable<IGrouping<Type, Subscription>> groupings = subscriptionReceipts.OfType<Subscription>().GroupBy(a => a.MessageType, a => a);

            foreach (IGrouping<Type, Subscription> grouping in groupings)
            {
                ConcurrentHashSet<Subscription> subscriptions = _subscriptionReceiptsByMessageType[grouping.Key];

                foreach (Subscription subscription in grouping)
                {
                    if (subscriptions.TryRemove(subscription))
                    {
                        _serilogLogger.LogDebug(
                            "{Context} unsubscribed from {MessageType}",
                            subscription.Context.Context ?? "An unknown context",
                            subscription.MessageType.Name);
                    }
                }
            }
        }

        /// <inheritdoc />
        public void Unsubscribe(params ISubscriptionReceipt[] subscriptionReceipts)
        {
            Unsubscribe((IEnumerable<ISubscriptionReceipt>)subscriptionReceipts);
        }

        /// <inheritdoc />
        public void DispatchQueued(ProcessThread thread)
        {
            ConcurrentQueue<Action> queue = _queuedHandlerDelegatesByProcessThread[thread];

            // Process all queued messages in publish order
            while (queue.TryDequeue(out Action? handlerDelegate))
            {
                handlerDelegate();
            }
        }

        private class Subscription : ISubscriptionReceipt
        {
            public Subscription(Type messageType, ProcessThread thread, Delegate handlerDelegate, NestedContext context)
            {
                MessageType = messageType;
                Thread = thread;
                HandlerDelegate = handlerDelegate;
                Context = context;
            }

            public Type MessageType { get; }
            public ProcessThread Thread { get; }
            public Delegate HandlerDelegate { get; }
            public NestedContext Context { get; }
        }
    }
}