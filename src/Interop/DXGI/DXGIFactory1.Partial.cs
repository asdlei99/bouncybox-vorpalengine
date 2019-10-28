using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIFactory1
    {
        public HResult EnumAdapters1(uint Adapter, out DXGIAdapter1? adapter)
        {
            IDXGIAdapter1* pAdapter;
            int hr = Pointer->EnumAdapters1(Adapter, &pAdapter);

            adapter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIAdapter1(pAdapter) : null;

            return hr;
        }
    }
}