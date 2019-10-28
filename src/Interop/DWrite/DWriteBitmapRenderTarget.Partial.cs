using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteBitmapRenderTarget
    {
        public HResult GetCurrentTransform(out DWRITE_MATRIX transform)
        {
            fixed (DWRITE_MATRIX* pTransform = &transform)
            {
                return Pointer->GetCurrentTransform(pTransform);
            }
        }

        public HResult GetSize(out SIZE size)
        {
            fixed (SIZE* pSize = &size)
            {
                return Pointer->GetSize(pSize);
            }
        }
    }
}