using System;
using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Calculators
{
    /// <summary>
    ///     Accumulates values up to a capacity, then drops the oldest value as a new value is accumulated.
    /// </summary>
    public abstract class Accumulator<T>
    {
        private int _currentIndex = -1;
        private T[] _values;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Accumulator{T}" /> type.
        /// </summary>
        /// <param name="capacity">The maximum number of accumulated values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when <paramref name="capacity" /> is less than zero.</exception>
        protected Accumulator(int capacity)
        {
            Capacity = capacity >= 0 ? capacity : throw new ArgumentOutOfRangeException(nameof(capacity), capacity, null);
            _values = new T[Capacity];
        }

        /// <summary>
        ///     Gets the maximum number of accumulated values.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        ///     Gets the number of accumulated values.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Gets the accumulated values.
        /// </summary>
        protected Span<T> Values => new Span<T>(_values, 0, Count);

        /// <summary>
        ///     Accumulates a value.
        /// </summary>
        /// <param name="value">A value.</param>
        public void Accumulate(T value)
        {
            _currentIndex = _currentIndex + 1 == Capacity ? 0 : _currentIndex + 1;
            _values[_currentIndex] = value;
            Count = System.Math.Min(Count + 1, _values.Length);
        }

        /// <summary>
        ///     Accumulates values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Accumulate(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                Accumulate(value);
            }
        }

        /// <summary>
        ///     Accumulates values.
        /// </summary>
        /// <param name="values">Values.</param>
        public void Accumulate(params T[] values)
        {
            Accumulate((IEnumerable<T>)values);
        }

        /// <summary>
        ///     Removes all accumulated values.
        /// </summary>
        public void Reset()
        {
            _values = new T[Capacity];
            Count = 0;
            _currentIndex = -1;
        }
    }
}