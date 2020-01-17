using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Proxies subscriptions to a concurrent message queue in order to make unsubscription easier.</summary>
    public class GlobalMessageQueueHelper : IDisposable
    {
        private readonly IConcurrentMessageQueue<IGlobalMessage> _concurrentMessageQueue;
        private readonly NestedContext _context;
        private readonly List<ISubscriptionReceipt> _subscriptionReceipts = new List<ISubscriptionReceipt>();
        private ProcessThread? _thread;
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="GlobalMessageQueueHelper" /> class.</summary>
        /// <param name="concurrentMessageQueue">An <see cref="IConcurrentMessageQueue{TMessageBase}" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public GlobalMessageQueueHelper(IConcurrentMessageQueue<IGlobalMessage> concurrentMessageQueue, NestedContext context)
        {
            _concurrentMessageQueue = concurrentMessageQueue;
            _context = context;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    _concurrentMessageQueue.Unsubscribe(_subscriptionReceipts);
                    _subscriptionReceipts.Clear();
                },
                ref _isDisposed);
        }

        /// <summary>Future subscription handler delegates will be executed on the specified thread.</summary>
        /// <returns>Returns the global message queue helper.</returns>
        public GlobalMessageQueueHelper WithThread(ProcessThread thread)
        {
            _thread = thread;

            return this;
        }

        /// <summary>Queues a message for dispatch.</summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>Returns the global message queue helper.</returns>
        public GlobalMessageQueueHelper Publish<TMessage>(TMessage message)
            where TMessage : IGlobalMessage
        {
            _concurrentMessageQueue.Publish(message, _context);

            return this;
        }

        /// <summary>Queues a message for dispatch.</summary>
        /// <returns>Returns the global message queue helper.</returns>
        public GlobalMessageQueueHelper Publish<TMessage>()
            where TMessage : IGlobalMessage, new()
        {
            _concurrentMessageQueue.Publish<TMessage>(_context);

            return this;
        }

        /// <summary>Subscribes to a message and provides a handler delegate that will process published messages of the specified type.</summary>
        /// <param name="handlerDelegate">The delegate to execute when a message with the specified type is published.</param>
        /// <returns>Returns the global message queue helper.</returns>
        public GlobalMessageQueueHelper Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : IGlobalMessage
        {
            _ = _thread ?? throw new InvalidOperationException("No thread was specified.");

            ISubscriptionReceipt subscriptionReceipt = _concurrentMessageQueue.Subscribe(handlerDelegate, _thread.Value, _context);

            _subscriptionReceipts.Add(subscriptionReceipt);

            return this;
        }

        /// <summary>Dispatches queued messages that were published to the specified thread.</summary>
        /// <param name="thread">The thread whose subscriptions will receive published messages.</param>
        public void DispatchQueued(ProcessThread thread)
        {
            _concurrentMessageQueue.DispatchQueued(thread);
        }
    }
}