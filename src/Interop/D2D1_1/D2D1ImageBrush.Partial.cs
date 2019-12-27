using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    public unsafe partial class D2D1ImageBrush
    {
        public D2D1Image? GetImage()
        {
            ID2D1Image* pImage;

            Pointer->GetImage(&pImage);

            return pImage != null ? new D2D1Image(pImage) : null;
        }

        public D2D_RECT_F GetSourceRectangle()
        {
            D2D_RECT_F sourceRectangle;

            Pointer->GetSourceRectangle(&sourceRectangle);

            return sourceRectangle;
        }
    }
}