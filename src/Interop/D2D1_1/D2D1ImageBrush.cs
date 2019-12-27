using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1ImageBrush" /> COM interface.</summary>
    public unsafe partial class D2D1ImageBrush : D2D1Brush
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1ImageBrush" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1ImageBrush(ID2D1ImageBrush* pointer) : base((ID2D1Brush*)pointer)
        {
        }

        public new ID2D1ImageBrush* Pointer => (ID2D1ImageBrush*)base.Pointer;

        public D2D1_EXTEND_MODE GetExtendModeX()
        {
            return Pointer->GetExtendModeX();
        }

        public D2D1_EXTEND_MODE GetExtendModeY()
        {
            return Pointer->GetExtendModeY();
        }

        public void GetImage(ID2D1Image** image)
        {
            Pointer->GetImage(image);
        }

        public D2D1_INTERPOLATION_MODE GetInterpolationMode()
        {
            return Pointer->GetInterpolationMode();
        }

        public void GetSourceRectangle(D2D_RECT_F* sourceRectangle)
        {
            Pointer->GetSourceRectangle(sourceRectangle);
        }

        public void SetExtendModeX(D2D1_EXTEND_MODE extendModeX)
        {
            Pointer->SetExtendModeX(extendModeX);
        }

        public void SetExtendModeY(D2D1_EXTEND_MODE extendModeY)
        {
            Pointer->SetExtendModeY(extendModeY);
        }

        public void SetImage(ID2D1Image* image = null)
        {
            Pointer->SetImage(image);
        }

        public void SetInterpolationMode(D2D1_INTERPOLATION_MODE interpolationMode)
        {
            Pointer->SetInterpolationMode(interpolationMode);
        }

        public void SetSourceRectangle(D2D_RECT_F* sourceRectangle)
        {
            Pointer->SetSourceRectangle(sourceRectangle);
        }

        public static implicit operator ID2D1ImageBrush*(D2D1ImageBrush value)
        {
            return value.Pointer;
        }
    }
}