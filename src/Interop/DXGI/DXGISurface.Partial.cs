using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISurface
    {
        public HResult GetDesc(out DXGI_SURFACE_DESC desc)
        {
            fixed (DXGI_SURFACE_DESC* pDesc = &desc)
            {
                return Pointer->GetDesc(pDesc);
            }
        }

        public HResult Map(out DXGI_MAPPED_RECT lockedRect, uint mapFlags)
        {
            fixed (DXGI_MAPPED_RECT* pLockedRect = &lockedRect)
            {
                return Pointer->Map(pLockedRect, mapFlags);
            }
        }
    }
}