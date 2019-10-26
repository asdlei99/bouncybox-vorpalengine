using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>A sine wave.</summary>
    public class SineWave : Wave
    {
        /// <summary>Initializes a new instance of the <see cref="SineWave" /> type.</summary>
        /// <param name="trough">The y-axis value for the trough of the wave.</param>
        /// <param name="crest">The y-axis value for the crest of the wave.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="waveOffset">The x-axis offset.</param>
        /// <param name="valueDelegate">A delegate that determines the current x-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public SineWave(
            float trough,
            float crest,
            TimeSpan wavelength,
            WaveOffset waveOffset,
            Func<TimeSpan> valueDelegate,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
            : base(trough, crest, wavelength, waveOffset, valueDelegate, timeSpanUnitDelegate)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SineWave" /> type.</summary>
        /// <param name="trough">The y-axis value for the trough of the wave.</param>
        /// <param name="crest">The y-axis value for the crest of the wave.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="waveOffset">The x-axis offset.</param>
        /// <param name="stopwatch">A stopwatch that provides a dynamic x-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public SineWave(
            float trough,
            float crest,
            TimeSpan wavelength,
            WaveOffset waveOffset,
            Stopwatch stopwatch,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null) : this(trough, crest, wavelength, waveOffset, () => stopwatch.Elapsed, timeSpanUnitDelegate)
        {
        }

        /// <inheritdoc />
        public override float Value => Waves.Sine(TimeSpanUnitDelegate(ValueDelegate()), Trough, Crest, TimeSpanUnitDelegate(Wavelength), WavelengthOffset);

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="waveOffset" /> is an unexpected value.</exception>
        protected override float GetWavelengthOffset(WaveOffset waveOffset)
        {
            return waveOffset switch
            {
                WaveOffset.None => 0,
                WaveOffset.Trough => 3 * MathF.PI / 2,
                WaveOffset.Crest => MathF.PI / 2,
                _ => throw new ArgumentOutOfRangeException(nameof(waveOffset), waveOffset, null)
            };
        }
    }
}