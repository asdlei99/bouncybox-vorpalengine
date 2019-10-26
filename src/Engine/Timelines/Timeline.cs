using System;
using System.Collections.Generic;
using System.Diagnostics;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Logging;

namespace BouncyBox.VorpalEngine.Engine.Timelines
{
    /// <summary>A sequence of delegates that are invoked at timed offsets.</summary>
    public class Timeline : ITimeline
    {
        private readonly Queue<TimelineAction> _actions = new Queue<TimelineAction>();
        private readonly ContextSerilogLogger _serilogLogger;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _elapsedTime;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Timeline" /> type.
        /// </summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        /// <param name="timelineName">The name of the timeline, to be included in the nested context.</param>
        public Timeline(ISerilogLogger serilogLogger, NestedContext context, string? timelineName = null)
        {
            context = context.CopyAndPush(timelineName ?? nameof(Timeline));

            _serilogLogger = new ContextSerilogLogger(serilogLogger, context);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Timeline" /> type.
        /// </summary>
        /// <param name="serilogLogger">An <see cref="ISerilogLogger" /> implementation.</param>
        /// <param name="timelineName">The name of the timeline, to be included in the nested context.</param>
        public Timeline(ISerilogLogger serilogLogger, string? timelineName = null) : this(serilogLogger, NestedContext.None(), timelineName)
        {
        }

        /// <inheritdoc />
        public ITimeline ExecuteAfter(TimeSpan offset, Action @delegate, string? description = null)
        {
            _actions.Enqueue(new TimelineAction(offset, @delegate, description));

            return this;
        }

        /// <inheritdoc />
        public ITimeline Execute(Action @delegate, string? description = null)
        {
            return ExecuteAfter(TimeSpan.Zero, @delegate, description);
        }

        /// <inheritdoc />
        public void Start()
        {
            _stopwatch.Start();
        }

        /// <inheritdoc />
        public void Stop()
        {
            _stopwatch.Stop();
        }

        /// <inheritdoc />
        public void ProcessActions()
        {
            while (_actions.Count > 0)
            {
                // Ensure the next queue'd action's offset has been reached
                if (_stopwatch.Elapsed + _elapsedTime < _actions.Peek().Offset)
                {
                    return;
                }

                TimelineAction action = _actions.Dequeue();

                string description = action.Description ?? "No description";

                _serilogLogger.LogVerbose("Executing action: {Description}", description);

                // Execute the action
                action.Delegate();

                _serilogLogger.LogDebug("Executed action: {Description}", description);

                // Increment the elapsed time marker by the action's offset
                _elapsedTime += action.Offset;
            }
        }

        /// <summary>A timeline action.</summary>
        private readonly struct TimelineAction
        {
            /// <summary>Initializes a new instance of the <see cref="TimelineAction" /> type.</summary>
            /// <param name="offset">The amount of time that must elapse before the delegate is invoked.</param>
            /// <param name="delegate">The delegate to invoke.</param>
            /// <param name="description">A description of the action.</param>
            public TimelineAction(TimeSpan offset, Action @delegate, string? description = null)
            {
                Offset = offset;
                Delegate = @delegate;
                Description = description;
            }

            /// <summary>Get the amount of time that must elapse before the delegate is invoked.</summary>
            public TimeSpan Offset { get; }

            /// <summary>Get the delegate to invoke.</summary>
            public Action Delegate { get; }

            /// <summary>Get a description of the action.</summary>
            public string? Description { get; }
        }
    }
}