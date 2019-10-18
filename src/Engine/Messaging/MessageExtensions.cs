namespace BouncyBox.VorpalEngine.Engine.Messaging
{
    /// <summary>Extension methods for the <see cref="IMessage" /> type.</summary>
    public static class MessageExtensions
    {
        /// <summary>
        ///     Determines if the specified message should be logged by calling
        ///     <see cref="MessageLogFilter.ShouldLogMessageDelegate" />.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <returns>Returns true if the message should be logged; otherwise, false.</returns>
        public static bool ShouldLog(this IMessage message)
        {
            return MessageLogFilter.ShouldLogMessageDelegate(message);
        }
    }
}