using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>
    ///     Makes it easier to calculate a Y-axis value of a configurable wave function.
    /// </summary>
    public abstract class Wave
    {
        private static readonly Func<TimeSpan, float> DefaultTimeSpanUnitDelegate = a => (float)a.TotalMilliseconds;
        private WaveOffset _waveOffset;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SineWave" /> type.
        /// </summary>
        /// <param name="trough">The Y-axis value for the trough of the wave.</param>
        /// <param name="crest">The Y-axis value for the crest of the wave.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="waveOffset">The X-axis offset.</param>
        /// <param name="stopwatch">A stopwatch that provides a dynamic X-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        protected Wave(
            float trough,
            float crest,
            TimeSpan wavelength,
            WaveOffset waveOffset,
            Stopwatch stopwatch,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
            : this(
                trough,
                crest,
                wavelength,
                waveOffset,
                () => stopwatch.Elapsed,
                a => (timeSpanUnitDelegate ?? DefaultTimeSpanUnitDelegate)(a))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SineWave" /> type.
        /// </summary>
        /// <param name="trough">The Y-axis value for the trough of the wave.</param>
        /// <param name="crest">The Y-axis value for the crest of the wave.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="waveOffset">The X-axis offset.</param>
        /// <param name="valueDelegate">A delegate that determines the current X-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="wavelength"/> is less than or equal to <see cref="TimeSpan.Zero"/>.</exception>
        protected Wave(
            float trough,
            float crest,
            TimeSpan wavelength,
            WaveOffset waveOffset,
            Func<TimeSpan> valueDelegate,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
        {
            Trough = System.Math.Min(trough, crest);
            Crest = System.Math.Max(trough, crest);
            Wavelength = wavelength > TimeSpan.Zero ? wavelength : throw new ArgumentOutOfRangeException(nameof(wavelength), wavelength, null);
            ValueDelegate = valueDelegate;
            TimeSpanUnitDelegate = timeSpanUnitDelegate ?? DefaultTimeSpanUnitDelegate;
            WaveOffset = waveOffset;
        }

        /// <summary>
        ///     Gets the Y-axis value for the trough of the wave.
        /// </summary>
        public float Trough { get; set; }

        /// <summary>
        ///     Gets the Y-axis value for the crest of the wave.
        /// </summary>
        public float Crest { get; set; }

        /// <summary>
        ///     Gets the wavelength.
        /// </summary>
        public TimeSpan Wavelength { get; set; }

        /// <summary>
        ///     Gets the X-axis offset.
        /// </summary>
        public WaveOffset WaveOffset
        {
            get => _waveOffset;
            set
            {
                _waveOffset = value;
                WavelengthOffset = GetWavelengthOffset(value);
            }
        }

        /// <summary>
        ///     Gets the calculated wavelength offset.
        /// </summary>
        public float WavelengthOffset { get; private set; }

        /// <summary>
        ///     Gets the Y-axis value for the current X-axis.
        /// </summary>
        public abstract float Value { get; }

        /// <summary>
        ///     Gets a delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.
        /// </summary>
        protected Func<TimeSpan, float> TimeSpanUnitDelegate { get; }

        /// <summary>
        ///     Gets a delegate that determines the current X-axis value.
        /// </summary>
        protected Func<TimeSpan> ValueDelegate { get; }

        /// <summary>
        ///     Calculates the wavelength offset for the given offset. Different wave functions have different offset calculations.
        /// </summary>
        /// <param name="waveOffset">An offset.</param>
        /// <returns>Returns the calculated offset.</returns>
        protected abstract float GetWavelengthOffset(WaveOffset waveOffset);
    }
}