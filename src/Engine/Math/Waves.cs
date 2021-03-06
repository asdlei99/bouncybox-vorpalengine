﻿using System;

namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>Mathematical wave formulas.</summary>
    public static class Waves
    {
        /// <summary>
        ///     <para>A configurable triangle wave formula.</para>
        ///     <para>
        ///         See <a href="https://www.desmos.com/calculator/dxxfj66eha">this online graphing calculator</a>.
        ///     </para>
        /// </summary>
        /// <param name="x">The x-axis value.</param>
        /// <param name="trough">The minimum y-axis value.</param>
        /// <param name="crest">The maximum y-axis value.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="wavelengthOffset">An x-axis offset.</param>
        /// <returns>Returns the calculated y-axis value.</returns>
        public static float Triangle(float x, float trough, float crest, float wavelength, float wavelengthOffset)
        {
            float halfWavelength = wavelength / 2;

            return System.Math.Abs(crest - trough) * (System.Math.Abs((x + wavelengthOffset) % wavelength - halfWavelength) / halfWavelength) + trough;
        }

        /// <summary>
        ///     <para>A configurable sine wave formula.</para>
        ///     <para>
        ///         See <a href="https://www.desmos.com/calculator/smaqwwyqls">this online graphing calculator</a>.
        ///     </para>
        /// </summary>
        /// <param name="x">The x-axis value.</param>
        /// <param name="trough">The minimum y-axis value.</param>
        /// <param name="crest">The maximum y-axis value.</param>
        /// <param name="wavelength">The wavelength.</param>
        /// <param name="wavelengthOffset">An x-axis offset.</param>
        /// <returns>Returns the calculated y-axis value.</returns>
        public static float Sine(float x, float trough, float crest, float wavelength, float wavelengthOffset)
        {
            float halfAmplitude = (crest - trough) / 2;

            return halfAmplitude * MathF.Sin(x * (4 * MathF.PI / 2) / wavelength + wavelengthOffset) + halfAmplitude + trough;
        }
    }
}