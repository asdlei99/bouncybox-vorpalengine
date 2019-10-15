using System;
using System.Diagnostics;

namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>
    ///     A triangle wave that makes it easier to calculate a Y-axis value as a function of wave shape and time.
    /// </summary>
    public class TriangleWave : Wave
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SineWave" /> type.
        /// </summary>
        /// <param name="trough">The Y-axis value for the trough of the wave.</param>
        /// <param name="crest">The Y-axis value for the crest of the wave.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="waveOffset">The X-axis offset.</param>
        /// <param name="stopwatch">A stopwatch that provides a dynamic X-axis value.</param>
        /// <param name="timeSpanUnitDelegate">A delegate that determines what <see cref="TimeSpan" /> component to use for all calculations.</param>
        public TriangleWave(
            float trough,
            float crest,
            TimeSpan wavelength,
            WaveOffset waveOffset,
            Stopwatch stopwatch,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
            : base(trough, crest, wavelength, waveOffset, stopwatch, timeSpanUnitDelegate)
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
        public TriangleWave(
            float trough,
            float crest,
            TimeSpan wavelength,
            WaveOffset waveOffset,
            Func<TimeSpan> valueDelegate,
            Func<TimeSpan, float>? timeSpanUnitDelegate = null)
            : base(trough, crest, wavelength, waveOffset, valueDelegate, timeSpanUnitDelegate)
        {
        }

        /// <inheritdoc />
        public override float Value => Waves.Triangle(TimeSpanUnitDelegate(ValueDelegate()), Trough, Crest, TimeSpanUnitDelegate(Wavelength), WavelengthOffset);

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="waveOffset"/> is an unexpected value.</exception>
        protected override float GetWavelengthOffset(WaveOffset waveOffset)
        {
            return
                TimeSpanUnitDelegate(
                    waveOffset switch
                    {
                        WaveOffset.None => TimeSpan.Zero,
                        WaveOffset.Trough => Wavelength / 2,
                        WaveOffset.Crest => TimeSpan.Zero,
                        _ => throw new ArgumentOutOfRangeException(nameof(waveOffset), waveOffset, null)
                    });
        }
    }
}