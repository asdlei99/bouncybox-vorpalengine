using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>
    ///     A global message indicating that the refresh period changed.
    /// </summary>
    public struct RefreshPeriodChangedMessage : IGlobalMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshPeriodChangedMessage" /> type.
        /// </summary>
        /// <param name="refreshPeriod">The new refresh period.</param>
        /// <param name="hz">The new refresh period in Hz.</param>
        public RefreshPeriodChangedMessage(TimeSpan refreshPeriod, double hz)
        {
            RefreshPeriod = refreshPeriod;
            Hz = hz;
        }

        /// <summary>
        ///     Gets the new refresh period.
        /// </summary>
        public TimeSpan RefreshPeriod { get; }

        /// <summary>
        ///     Gets the new refresh period in Hz.
        /// </summary>
        public double Hz { get; }
    }
}