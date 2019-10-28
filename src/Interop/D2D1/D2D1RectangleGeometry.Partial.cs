using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1RectangleGeometry
    {
        public D2D_RECT_F GetRect()
        {
            D2D_RECT_F rect;

            Pointer->GetRect(&rect);

            return rect;
        }
    }
}