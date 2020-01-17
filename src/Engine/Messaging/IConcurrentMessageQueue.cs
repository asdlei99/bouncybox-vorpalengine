using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Represents an in-memory thread-safe message queue.</summary>
    public interface IConcurrentMessageQueue<in TMessageBase>
        where TMessageBase : IGlobalMessage
    {
        /// <summary>Queues a message for dispatch in a subsequent call to <see cref="DispatchQueued" />.</summary>
        /// <param name="message">The message to publish.</param>
        /// <param name="context">A nested context.</param>
        void Publish<TMessage>(TMessage message, NestedContext context)
            where TMessage : TMessageBase;

        /// <summary>Queues a message for dispatch in a subsequent call to <see cref="DispatchQueued" />.</summary>
        /// <param name="message">The message to publish.</param>
        void Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase;

        /// <summary>Queues a message for dispatch in a subsequent call to <see cref="DispatchQueued" />.</summary>
        /// <param name="context">A nested context.</param>
        void Publish<TMessage>(NestedContext context)
            where TMessage : TMessageBase, new();

        /// <summary>Queues a message for dispatch in a subsequent call to <see cref="DispatchQueued" />.</summary>
        void Publish<TMessage>()
            where TMessage : TMessageBase, new();

        /// <summary>Subscribes to a message and provides a handler delegate that will process published messages of the specified type.</summary>
        /// <param name="handlerDelegate">The delegate to execute when a message with the specified type is published.</param>
        /// <param name="thread">The thread that will handle published message of the specified type.</param>
        /// <param name="context">A nested context.</param>
        /// <returns>Returns a subscription receipt that may be used later to unsubscribe.</returns>
        ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate, ProcessThread thread, NestedContext context)
            where TMessage : TMessageBase;

        /// <summary>Subscribes to a message and provides a handler delegate that will process published messages of the specified type.</summary>
        /// <param name="handlerDelegate">The delegate to execute when a message with the specified type is published.</param>
        /// <param name="thread">The thread that will handle published message of the specified type.</param>
        /// <returns>Returns a subscription receipt that may be used later to unsubscribe.</returns>
        ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate, ProcessThread thread)
            where TMessage : TMessageBase;

        /// <summary>Unsubscribes subscriptions.</summary>
        /// <param name="subscriptionReceipts">Subscriptions to unsubscribe.</param>
        void Unsubscribe(IEnumerable<ISubscriptionReceipt> subscriptionReceipts);

        /// <summary>Unsubscribes subscriptions.</summary>
        /// <param name="subscriptionReceipts">Subscriptions to unsubscribe.</param>
        void Unsubscribe(params ISubscriptionReceipt[] subscriptionReceipts);

        /// <summary>Dispatches all published messages for the specified thread.</summary>
        /// <param name="thread">The thread whose subscriptions will receive published messages.</param>
        void DispatchQueued(ProcessThread thread);
    }
}