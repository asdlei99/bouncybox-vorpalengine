using System;

namespace BouncyBox.VorpalEngine.Engine.Calculators
{
    /// <summary>Accumulates <see cref="TimeSpan" /> values.</summary>
    public class TimeSpanAccumulator : Accumulator<TimeSpan>
    {
        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="TimeSpanAccumulator" /> type.</summary>
        public TimeSpanAccumulator(int capacity) : base(capacity)
        {
        }

        /// <summary>Gets the mean of the accumulated values.</summary>
        public TimeSpan? Mean
        {
            get
            {
                Span<TimeSpan> accumulatedValues = Values;
                TimeSpan? value = null;

                foreach (TimeSpan accumulatedValue in accumulatedValues)
                {
                    value = (value ?? TimeSpan.Zero) + accumulatedValue;
                }

                return value / accumulatedValues.Length;
            }
        }

        /// <summary>Gets the minimum accumulated value.</summary>
        public TimeSpan? Minimum
        {
            get
            {
                Span<TimeSpan> accumulatedValues = Values;
                TimeSpan? value = null;

                foreach (TimeSpan accumulatedValue in accumulatedValues)
                {
                    if (value == null || accumulatedValue < value)
                    {
                        value = accumulatedValue;
                    }
                }

                return value;
            }
        }

        /// <summary>Gets the maximum accumulated value.</summary>
        public TimeSpan? Maximum
        {
            get
            {
                Span<TimeSpan> accumulatedValues = Values;
                TimeSpan? value = null;

                foreach (TimeSpan accumulatedValue in accumulatedValues)
                {
                    if (value == null || accumulatedValue > value)
                    {
                        value = accumulatedValue;
                    }
                }

                return value;
            }
        }
    }
}