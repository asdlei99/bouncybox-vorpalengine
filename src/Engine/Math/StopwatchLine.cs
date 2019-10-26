using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>A line that makes it easier to calculate a y-axis value as a function of time.</summary>
    public class StopwatchLine
    {
        private static readonly Func<TimeSpan, float> DefaultTimeSpanUnitDelegate = a => (float)a.TotalMilliseconds;
        private readonly Line _line;
        private readonly float _maximumValue;
        private readonly float _minimumValue;
        private readonly Stopwatch _stopwatch;

        /// <param name="startValue">The starting y-axis value.</param>
        /// <param name="endValue">The ending y-axis value.</param>
        /// <param name="duration">The duration used to calculate the slope and y-intercept.</param>
        /// <param name="stopwatch">A stopwatch that provides a dynamic x-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public StopwatchLine(
            float startValue,
            float endValue,
            TimeSpan duration,
            Stopwatch stopwatch,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
        {
            timeSpanUnitDelegate ??= DefaultTimeSpanUnitDelegate;

            _minimumValue = MathF.Min(startValue, endValue);
            _maximumValue = MathF.Max(startValue, endValue);
            _stopwatch = stopwatch;
            _line = startValue <= endValue
                        ? new Line((endValue - startValue) / timeSpanUnitDelegate(duration), startValue, stopwatch, timeSpanUnitDelegate)
                        : new Line((startValue - endValue) / timeSpanUnitDelegate(duration), endValue, stopwatch, timeSpanUnitDelegate);
        }

        /// <param name="startValue">The starting y-axis value.</param>
        /// <param name="endValue">The ending y-axis value.</param>
        /// <param name="duration">The duration used to calculate the slope and y-intercept.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public StopwatchLine(
            float startValue,
            float endValue,
            TimeSpan duration,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
            : this(startValue, endValue, duration, new Stopwatch(), timeSpanUnitDelegate)
        {
        }

        /// <summary>Gets the y-axis value for the current x-axis.</summary>
        public float Value => System.Math.Clamp(_line.Value, _minimumValue, _maximumValue);

        /// <summary>Starts the stopwatch.</summary>
        public void Start()
        {
            _stopwatch.Start();
        }

        /// <summary>Stops the stopwatch.</summary>
        public void Stop()
        {
            _stopwatch.Stop();
        }

        /// <summary>Resets the stopwatch.</summary>
        public void Reset()
        {
            _stopwatch.Reset();
        }

        /// <summary>Restarts the stopwatch.</summary>
        public void Restart()
        {
            _stopwatch.Restart();
        }
    }
}