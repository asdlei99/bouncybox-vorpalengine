using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1GradientStopCollection" /> COM interface.</summary>
    public unsafe partial class D2D1GradientStopCollection : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GradientStopCollection" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GradientStopCollection(ID2D1GradientStopCollection* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1GradientStopCollection* Pointer => (ID2D1GradientStopCollection*)base.Pointer;

        public D2D1_GAMMA GetColorInterpolationGamma()
        {
            return Pointer->GetColorInterpolationGamma();
        }

        public D2D1_EXTEND_MODE GetExtendMode()
        {
            return Pointer->GetExtendMode();
        }

        public uint GetGradientStopCount()
        {
            return Pointer->GetGradientStopCount();
        }

        public void GetGradientStops(D2D1_GRADIENT_STOP* gradientStops, uint gradientStopsCount)
        {
            Pointer->GetGradientStops(gradientStops, gradientStopsCount);
        }

        public static implicit operator ID2D1GradientStopCollection*(D2D1GradientStopCollection value)
        {
            return value.Pointer;
        }
    }
}