using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWritePixelSnapping
    {
        public HResult GetCurrentTransform([Optional] void* clientDrawingContext, out DWRITE_MATRIX transform)
        {
            fixed (DWRITE_MATRIX* pTransform = &transform)
            {
                return Pointer->GetCurrentTransform(clientDrawingContext, pTransform);
            }
        }

        public HResult GetPixelsPerDip([Optional] void* clientDrawingContext, out float pixelsPerDip)
        {
            fixed (float* pPixelsPerDip = &pixelsPerDip)
            {
                return Pointer->GetPixelsPerDip(clientDrawingContext, pPixelsPerDip);
            }
        }
    }
}