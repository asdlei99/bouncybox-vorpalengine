namespace BouncyBox.VorpalEngine.Engine.Calculators
{
    /// <summary>
    ///     Units of time to use when calculating event frequency with <see cref="EventFrequencyCalculator" />.
    /// </summary>
    public enum EventFrequencyUnit
    {
        /// <summary>
        ///     Events per day.
        /// </summary>
        PerDay,

        /// <summary>
        ///     Events per hour.
        /// </summary>
        PerHour,

        /// <summary>
        ///     Events per minute.
        /// </summary>
        PerMinute,

        /// <summary>
        ///     Events per second.
        /// </summary>
        PerSecond,

        /// <summary>
        ///     Events per millisecond.
        /// </summary>
        PerMillisecond,

        /// <summary>
        ///     Events per tick.
        /// </summary>
        PerTick
    }
}