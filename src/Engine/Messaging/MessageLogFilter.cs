using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>
    ///     Message log filter delegates.
    /// </summary>
    public static class MessageLogFilter
    {
        /// <summary>
        ///     A delegate that determines whether to log a specific message.
        /// </summary>
        public static Func<IMessage, bool> ShouldLogMessageDelegate = a => false;

        /// <summary>
        ///     A delegate that determines whether to log a specific message type.
        /// </summary>
        public static Func<Type, bool> ShouldLogMessageTypeDelegate = a => false;
    }
}