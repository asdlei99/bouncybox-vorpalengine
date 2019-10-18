using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine.Calculators
{
    /// <summary>Calculates how often certain events occur over the total elapsed time of a timer.</summary>
    public class EventFrequencyCalculator
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private ulong _eventCount;

        /// <summary>Initializes a new instance of the <see cref="EventFrequencyCalculator" /> type.</summary>
        /// <param name="start">Determines whether to start the timer.</param>
        public EventFrequencyCalculator(bool start = false)
        {
            if (start)
            {
                _stopwatch.Start();
            }
        }

        /// <summary>Gets a value indicating whether the timer is running.</summary>
        public bool IsRunning => _stopwatch.IsRunning;

        /// <summary>Starts the timer.</summary>
        public void Start()
        {
            _stopwatch.Start();
        }

        /// <summary>Stops the timer.</summary>
        public void Stop()
        {
            _stopwatch.Stop();
        }

        /// <summary>Resets the event count to zero and resets the timer.</summary>
        public void Reset()
        {
            _stopwatch.Reset();
            _eventCount = 0;
        }

        /// <summary>Resets the event count to zero and restarts the timer.</summary>
        public void Restart()
        {
            _stopwatch.Restart();
            _eventCount = 0;
        }

        /// <summary>Increments the event count.</summary>
        /// <param name="increment">The number of events to add.</param>
        public void IncrementEvents(ulong increment = 1)
        {
            _eventCount += increment;
        }

        /// <summary>Calculates the event frequency.</summary>
        /// <param name="unit">The unit of time to use in the calculation.</param>
        /// <returns>Returns the event frequency.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="unit" /> is an unexpected value.</exception>
        public double GetFrequency(EventFrequencyUnit unit = EventFrequencyUnit.PerSecond)
        {
            return unit switch
            {
                EventFrequencyUnit.PerDay => (_eventCount / _stopwatch.Elapsed.TotalDays),
                EventFrequencyUnit.PerHour => (_eventCount / _stopwatch.Elapsed.TotalHours),
                EventFrequencyUnit.PerMinute => (_eventCount / _stopwatch.Elapsed.TotalMinutes),
                EventFrequencyUnit.PerSecond => (_eventCount / _stopwatch.Elapsed.TotalSeconds),
                EventFrequencyUnit.PerMillisecond => (_eventCount / _stopwatch.Elapsed.TotalMilliseconds),
                EventFrequencyUnit.PerTick => (_eventCount / (ulong)_stopwatch.Elapsed.Ticks),
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };
        }
    }
}