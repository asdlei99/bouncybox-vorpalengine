using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1GradientStopCollection1" /> COM interface.</summary>
    public unsafe partial class D2D1GradientStopCollection1 : D2D1GradientStopCollection
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GradientStopCollection1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GradientStopCollection1(ID2D1GradientStopCollection1* pointer) : base((ID2D1GradientStopCollection*)pointer)
        {
        }

        public new ID2D1GradientStopCollection1* Pointer => (ID2D1GradientStopCollection1*)base.Pointer;

        public D2D1_BUFFER_PRECISION GetBufferPrecision()
        {
            return Pointer->GetBufferPrecision();
        }

        public D2D1_COLOR_INTERPOLATION_MODE GetColorInterpolationMode()
        {
            return Pointer->GetColorInterpolationMode();
        }

        public void GetGradientStops1(D2D1_GRADIENT_STOP* gradientStops, uint gradientStopsCount)
        {
            Pointer->GetGradientStops1(gradientStops, gradientStopsCount);
        }

        public D2D1_COLOR_SPACE GetPostInterpolationSpace()
        {
            return Pointer->GetPostInterpolationSpace();
        }

        public D2D1_COLOR_SPACE GetPreInterpolationSpace()
        {
            return Pointer->GetPreInterpolationSpace();
        }

        public static implicit operator ID2D1GradientStopCollection1*(D2D1GradientStopCollection1 value)
        {
            return value.Pointer;
        }
    }
}