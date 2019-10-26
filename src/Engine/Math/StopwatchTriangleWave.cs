using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>A triangle wave that uses a <see cref="Stopwatch" /> for x-axis values.</summary>
    public class StopwatchTriangleWave : TriangleWave
    {
        private readonly Stopwatch _stopwatch;

        /// <summary>Initializes a new instance of the <see cref="StopwatchTriangleWave" /> type.</summary>
        /// <param name="trough">The y-axis value for the trough of the wave.</param>
        /// <param name="crest">The y-axis value for the crest of the wave.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="waveOffset">The x-axis offset.</param>
        /// <param name="stopwatch">A stopwatch whose elapsed time will be used for x-axis values.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public StopwatchTriangleWave(
            float trough,
            float crest,
            TimeSpan wavelength,
            WaveOffset waveOffset,
            Stopwatch stopwatch,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
            : base(trough, crest, wavelength, waveOffset, () => stopwatch.Elapsed, timeSpanUnitDelegate)
        {
            _stopwatch = stopwatch;
        }

        /// <summary>Initializes a new instance of the <see cref="StopwatchTriangleWave" /> type.</summary>
        /// <param name="trough">The y-axis value for the trough of the wave.</param>
        /// <param name="crest">The y-axis value for the crest of the wave.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="waveOffset">The x-axis offset.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public StopwatchTriangleWave(float trough, float crest, TimeSpan wavelength, WaveOffset waveOffset, Func<TimeSpan, float>? timeSpanUnitDelegate = null)
            : this(trough, crest, wavelength, waveOffset, new Stopwatch(), timeSpanUnitDelegate)
        {
        }

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