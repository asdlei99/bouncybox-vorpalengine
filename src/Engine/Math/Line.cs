using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>A line that makes it easier to calculate a y-axis value as a function of time.</summary>
    public class Line
    {
        private static readonly Func<TimeSpan, float> DefaultTimeSpanUnitDelegate = a => (float)a.TotalMilliseconds;
        private readonly float _slope;
        private readonly Func<TimeSpan, float> _timeSpanUnitDelegate;
        private readonly Func<TimeSpan> _valueDelegate;
        private readonly float _yIntercept;

        /// <param name="slope">The slope of the line, usually called "m."</param>
        /// <param name="yIntercept">The y-intercept, usually called "b."</param>
        /// <param name="valueDelegate">A delegate that determines the current x-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public Line(
            float slope,
            float yIntercept,
            Func<TimeSpan> valueDelegate,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
        {
            _slope = slope;
            _yIntercept = yIntercept;
            _valueDelegate = valueDelegate;
            _timeSpanUnitDelegate = timeSpanUnitDelegate ?? DefaultTimeSpanUnitDelegate;
        }

        /// <param name="slope">The slope of the line, usually called "m."</param>
        /// <param name="yIntercept">The y-intercept, usually called "b."</param>
        /// <param name="stopwatch">A stopwatch that provides a dynamic x-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public Line(
            float slope,
            float yIntercept,
            Stopwatch stopwatch,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null) : this(slope, yIntercept, () => stopwatch.Elapsed, timeSpanUnitDelegate)
        {
        }

        /// <summary>Gets the y-axis value for the current x-axis.</summary>
        public float Value => Lines.Line(_timeSpanUnitDelegate(_valueDelegate()), _slope, _yIntercept);
    }
}