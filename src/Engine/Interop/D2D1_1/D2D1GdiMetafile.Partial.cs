using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    public unsafe partial class D2D1GdiMetafile
    {
        public HResult GetBounds(out D2D_RECT_F bounds)
        {
            fixed (D2D_RECT_F* pBounds = &bounds)
            {
                return Pointer->GetBounds(pBounds);
            }
        }
    }
}