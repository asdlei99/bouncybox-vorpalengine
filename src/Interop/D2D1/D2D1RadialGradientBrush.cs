using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1RadialGradientBrush" /> COM interface.</summary>
    public unsafe partial class D2D1RadialGradientBrush : D2D1Brush
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1RadialGradientBrush" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1RadialGradientBrush(ID2D1RadialGradientBrush* pointer) : base((ID2D1Brush*)pointer)
        {
        }

        public new ID2D1RadialGradientBrush* Pointer => (ID2D1RadialGradientBrush*)base.Pointer;

        public D2D_POINT_2F GetCenter()
        {
            return Pointer->GetCenter();
        }

        public D2D_POINT_2F GetGradientOriginOffset()
        {
            return Pointer->GetGradientOriginOffset();
        }

        public void GetGradientStopCollection(ID2D1GradientStopCollection** gradientStopCollection)
        {
            Pointer->GetGradientStopCollection(gradientStopCollection);
        }

        public float GetRadiusX()
        {
            return Pointer->GetRadiusX();
        }

        public float GetRadiusY()
        {
            return Pointer->GetRadiusY();
        }

        public void SetCenter(D2D_POINT_2F center)
        {
            Pointer->SetCenter(center);
        }

        public void SetRadiusX(float radiusX)
        {
            Pointer->SetRadiusX(radiusX);
        }

        public void SetRadiusY(float radiusY)
        {
            Pointer->SetRadiusY(radiusY);
        }

        public static implicit operator ID2D1RadialGradientBrush*(D2D1RadialGradientBrush value)
        {
            return value.Pointer;
        }
    }
}