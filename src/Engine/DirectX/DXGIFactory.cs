using System.Runtime.CompilerServices;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX
{
    /// <summary>
    ///     Factory for various DXGI structs.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class DXGIFactory
    {
        /// <summary>
        ///     Creates a <see cref="DXGI_RATIONAL" />.
        /// </summary>
        /// <param name="numerator">The numerator value.</param>
        /// <param name="denominator">The denominator value.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DXGI_RATIONAL CreateRational(uint numerator, uint denominator)
        {
            return
                new DXGI_RATIONAL
                {
                    Numerator = numerator,
                    Denominator = denominator
                };
        }

        /// <summary>
        ///     Creates a <see cref="DXGI_RGBA" />.
        /// </summary>
        /// <param name="r">The r value.</param>
        /// <param name="g">The g value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="a">The a value.</param>
        /// <returns>Returns a <see cref="DXGI_RGBA" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DXGI_RGBA CreateRgba(float r, float g, float b, float a)
        {
            return
                new DXGI_RGBA
                {
                    r = r,
                    g = g,
                    b = b,
                    a = a
                };
        }
    }
}