using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1LinearGradientBrush" /> COM interface.</summary>
    public unsafe partial class D2D1LinearGradientBrush : D2D1Brush
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1LinearGradientBrush" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1LinearGradientBrush(ID2D1LinearGradientBrush* pointer) : base((ID2D1Brush*)pointer)
        {
        }

        public new ID2D1LinearGradientBrush* Pointer => (ID2D1LinearGradientBrush*)base.Pointer;

        public D2D_POINT_2F GetEndPoint()
        {
            return Pointer->GetEndPoint();
        }

        public void GetGradientStopCollection(ID2D1GradientStopCollection** gradientStopCollection)
        {
            Pointer->GetGradientStopCollection(gradientStopCollection);
        }

        public D2D_POINT_2F GetStartPoint()
        {
            return Pointer->GetStartPoint();
        }

        public void SetEndPoint(D2D_POINT_2F endPoint)
        {
            Pointer->SetEndPoint(endPoint);
        }

        public void SetStartPoint(D2D_POINT_2F startPoint)
        {
            Pointer->SetStartPoint(startPoint);
        }

        public static implicit operator ID2D1LinearGradientBrush*(D2D1LinearGradientBrush value)
        {
            return value.Pointer;
        }
    }
}