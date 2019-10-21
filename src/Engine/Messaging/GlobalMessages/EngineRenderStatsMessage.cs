using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>A global message that reports engine render statistics.</summary>
    public readonly struct EngineRenderStatsMessage : IGlobalMessage
    {
        /// <summary>Initializes a new instance of the <see cref="EngineRenderStatsMessage" /> type.</summary>
        /// <param name="framesPerSecond">The number of rendered frames per second.</param>
        /// <param name="frameCount">The total number of frames rendered.</param>
        /// <param name="meanFrametime">The mean frametime.</param>
        /// <param name="minimumFrametime">The minimum frametime.</param>
        /// <param name="maximumFrametime">The maximum frametime.</param>
        public EngineRenderStatsMessage(
            double framesPerSecond,
            ulong frameCount,
            TimeSpan? meanFrametime,
            TimeSpan? minimumFrametime,
            TimeSpan? maximumFrametime)
        {
            FramesPerSecond = framesPerSecond;
            FrameCount = frameCount;
            MeanFrametime = meanFrametime;
            MinimumFrametime = minimumFrametime;
            MaximumFrametime = maximumFrametime;
        }

        /// <summary>Gets the number of rendered frames per second.</summary>
        public double FramesPerSecond { get; }

        /// <summary>Gets the total number of frames rendered.</summary>
        public ulong FrameCount { get; }

        /// <summary>Gets the mean frametime.</summary>
        public TimeSpan? MeanFrametime { get; }

        /// <summary>Gets the minimum frametime.</summary>
        public TimeSpan? MinimumFrametime { get; }

        /// <summary>Gets the maximum frametime.</summary>
        public TimeSpan? MaximumFrametime { get; }
    }
}