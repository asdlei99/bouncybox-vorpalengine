using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Common;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Represents an in-memory thread-safe message queue.</summary>
    public interface IConcurrentMessageQueue<TMessageBase>
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
        /// <param name="messageDispatchQueue">The concurrent message dispatch queue to where published messages will be dispatched.</param>
        /// <param name="context">A nested context.</param>
        /// <returns>Returns a subscription token that may be used later to unsubscribe.</returns>
        SubscriptionToken Subscribe<TMessage>(ConcurrentMessageDispatchQueue<TMessageBase> messageDispatchQueue, NestedContext context)
            where TMessage : TMessageBase;

        /// <summary>Subscribes to a message and provides a handler delegate that will process published messages of the specified type.</summary>
        /// <param name="messageDispatchQueue">The concurrent message dispatch queue to where published messages will be dispatched.</param>
        /// <returns>Returns a subscription token that may be used later to unsubscribe.</returns>
        SubscriptionToken Subscribe<TMessage>(ConcurrentMessageDispatchQueue<TMessageBase> messageDispatchQueue)
            where TMessage : TMessageBase;

        /// <summary>Unsubscribes a subscription.</summary>
        /// <param name="tokens">Subscription tokens to unsubscribe.</param>
        /// <param name="context">A nested context.</param>
        void Unsubscribe(IEnumerable<SubscriptionToken> tokens, NestedContext context);

        /// <summary>Unsubscribes a subscription.</summary>
        /// <param name="tokens">Subscription tokens to unsubscribe.</param>
        void Unsubscribe(IEnumerable<SubscriptionToken> tokens);

        /// <summary>Unsubscribes a subscription.</summary>
        /// <param name="context">A nested context.</param>
        /// <param name="tokens">Subscription tokens to unsubscribe.</param>
        void Unsubscribe(NestedContext context, params SubscriptionToken[] tokens);

        /// <summary>Unsubscribes a subscription.</summary>
        /// <param name="tokens">Subscription tokens to unsubscribe.</param>
        void Unsubscribe(params SubscriptionToken[] tokens);

        /// <summary>Dequeues all queued messages, dispatching each message to its associated concurrent message dispatch queue.</summary>
        void DispatchQueued();
    }
}