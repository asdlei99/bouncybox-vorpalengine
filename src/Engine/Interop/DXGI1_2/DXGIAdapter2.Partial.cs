using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIAdapter2
    {
        public HResult GetDesc2(out DXGI_ADAPTER_DESC2 desc)
        {
            fixed (DXGI_ADAPTER_DESC2* pDesc = &desc)
            {
                return Pointer->GetDesc2(pDesc);
            }
        }
    }
}