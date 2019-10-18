namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>
    ///     A global message that reports engine update statistics.
    /// </summary>
    public struct EngineUpdateStatsMessage : IGlobalMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EngineUpdateStatsMessage" /> type.
        /// </summary>
        /// <param name="updatesPerSecond">The number of updates per second.</param>
        public EngineUpdateStatsMessage(double updatesPerSecond)
        {
            UpdatesPerSecond = updatesPerSecond;
        }

        /// <summary>
        ///     Gets the number of updates per second.
        /// </summary>
        public double UpdatesPerSecond { get; }
    }
}