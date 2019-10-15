using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     A token provided after subscribing to a message type that can later be used to unsubscribe.
    /// </summary>
    public class SubscriptionToken
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SubscriptionToken" /> type.
        /// </summary>
        /// <param name="messageType">The message type that was subscribed to.</param>
        public SubscriptionToken(Type messageType)
        {
            MessageType = messageType;
        }

        /// <summary>
        ///     Gets the message type that was subscribed to.
        /// </summary>
        public Type MessageType { get; }
    }
}