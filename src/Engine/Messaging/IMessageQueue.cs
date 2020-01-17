using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Common;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Represents an in-memory non-thread-safe message queue.</summary>
    public interface IMessageQueue<in TMessageBase>
        where TMessageBase : IMessage
    {
        /// <summary>Publishes a message to any subscribers subscribed to the message type.</summary>
        /// <param name="message">The message to publish.</param>
        /// <param name="context">A nested context.</param>
        void Publish<TMessage>(TMessage message, NestedContext context)
            where TMessage : TMessageBase;

        /// <summary>Publishes a message to any subscribers subscribed to the message type.</summary>
        /// <param name="message">The message to publish.</param>
        void Publish<TMessage>(TMessage message)
            where TMessage : TMessageBase;

        /// <summary>Publishes a message to any subscribers subscribed to the message type.</summary>
        /// <param name="context">A nested context.</param>
        void Publish<TMessage>(NestedContext context)
            where TMessage : TMessageBase, new();

        /// <summary>Publishes a message to any subscribers subscribed to the message type.</summary>
        void Publish<TMessage>()
            where TMessage : TMessageBase, new();

        /// <summary>Subscribes to a message and provides a handler delegate that will process published messages of the specified type.</summary>
        /// <param name="handlerDelegate">A delegate that is invoked when a message of the specified type is published.</param>
        /// <param name="context">A nested context.</param>
        /// <returns>Returns a subscription receipt that may be used later to unsubscribe.</returns>
        ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate, NestedContext context)
            where TMessage : TMessageBase;

        /// <summary>Subscribes to a message and provides a handler delegate that will process published messages of the specified type.</summary>
        /// <param name="handlerDelegate">A delegate that is invoked when a message of the specified type is published.</param>
        /// <returns>Returns a subscription receipt that may be used later to unsubscribe.</returns>
        ISubscriptionReceipt Subscribe<TMessage>(Action<TMessage> handlerDelegate)
            where TMessage : TMessageBase;

        /// <summary>Unsubscribes subscriptions.</summary>
        /// <param name="subscriptionReceipts">Subscription receipts to unsubscribe.</param>
        void Unsubscribe(IEnumerable<ISubscriptionReceipt> subscriptionReceipts);

        /// <summary>Unsubscribes subscriptions.</summary>
        /// <param name="subscriptionReceipts">Subscription receipts to unsubscribe.</param>
        void Unsubscribe(params ISubscriptionReceipt[] subscriptionReceipts);
    }
}