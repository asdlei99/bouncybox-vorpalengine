using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIAdapter1
    {
        public HResult GetDesc1(out DXGI_ADAPTER_DESC1 desc)
        {
            fixed (DXGI_ADAPTER_DESC1* pDesc = &desc)
            {
                return Pointer->GetDesc1(pDesc);
            }
        }
    }
}