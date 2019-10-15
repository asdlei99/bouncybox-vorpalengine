using System;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Engine statistics.
    /// </summary>
    internal class EngineStats : IEngineStats
    {
        /// <inheritdoc />
        public double UpdatesPerSecond { get; set; }

        /// <inheritdoc />
        public ulong FrameCount { get; set; }

        /// <inheritdoc />
        public double FramesPerSecond { get; set; }

        /// <inheritdoc />
        public TimeSpan? MeanFrametime { get; set; }

        /// <inheritdoc />
        public TimeSpan? MinimumFrametime { get; set; }

        /// <inheritdoc />
        public TimeSpan? MaximumFrametime { get; set; }
    }
}