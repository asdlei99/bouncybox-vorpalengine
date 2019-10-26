using BouncyBox.VorpalEngine.Engine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    public unsafe partial class D2D1ImageBrush
    {
        public void GetImage(out D2D1Image? image)
        {
            ID2D1Image* pImage;

            Pointer->GetImage(&pImage);

            image = pImage is null ? null : new D2D1Image(pImage);
        }

        public D2D_RECT_F GetSourceRectangle()
        {
            D2D_RECT_F sourceRectangle;

            Pointer->GetSourceRectangle(&sourceRectangle);

            return sourceRectangle;
        }
    }
}