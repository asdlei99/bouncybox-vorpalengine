using System;

namespace BouncyBox.VorpalEngine.Engine.Timelines
{
    /// <summary>
    ///     Represents a sequence of delegates that are invoked at timed offsets.
    /// </summary>
    public interface ITimeline
    {
        /// <summary>Queues an action to be executed after a certain amount of time has elapsed.</summary>
        /// <param name="offset">The amount of time that must elapse before the delegate is invoked.</param>
        /// <param name="delegate">The delegate to invoke.</param>
        /// <param name="description">A description of the action.</param>
        /// <returns>The timeline.</returns>
        ITimeline ExecuteAfter(TimeSpan offset, Action @delegate, string? description = null);

        /// <summary>Queues an action to be executed with an offset of <see cref="TimeSpan.Zero" />.</summary>
        /// <param name="delegate">The delegate to invoke.</param>
        /// <param name="description">A description of the action.</param>
        /// <returns>The timeline.</returns>
        ITimeline Execute(Action @delegate, string? description = null);

        /// <summary>Starts the timer.</summary>
        void Start();

        /// <summary>Stops the timer.</summary>
        void Stop();

        /// <summary></summary>
        void ProcessActions();
    }
}