using System;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Represents engine statistics.
    /// </summary>
    public interface IEngineStats
    {
        /// <summary>
        ///     The number of updates per second.
        /// </summary>
        double UpdatesPerSecond { get; }

        /// <summary>
        ///     The number of rendered frames per second.
        /// </summary>
        double FramesPerSecond { get; }

        /// <summary>
        ///     The total number of frames rendered.
        /// </summary>
        ulong FrameCount { get; }

        /// <summary>
        ///     The mean frametime.
        /// </summary>
        TimeSpan? MeanFrametime { get; }

        /// <summary>
        ///     The minimum frametime.
        /// </summary>
        TimeSpan? MinimumFrametime { get; }

        /// <summary>
        ///     The maximum frametime.
        /// </summary>
        TimeSpan? MaximumFrametime { get; }
    }
}