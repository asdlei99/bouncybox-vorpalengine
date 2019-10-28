using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIAdapter
    {
        public HResult CheckInterfaceSupport<T>(out LARGE_INTEGER pUMDVersion)
            where T : unmanaged
        {
            Guid interfaceName = typeof(T).GUID;

            fixed (LARGE_INTEGER* pPUMDVersion = &pUMDVersion)
            {
                return Pointer->CheckInterfaceSupport(&interfaceName, pPUMDVersion);
            }
        }

        public HResult EnumOutputs(uint Output, out DXGIOutput? output)
        {
            IDXGIOutput* pOutput;
            int hr = Pointer->EnumOutputs(Output, &pOutput);

            output = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIOutput(pOutput) : null;

            return hr;
        }

        public HResult GetDesc(out DXGI_ADAPTER_DESC desc)
        {
            fixed (DXGI_ADAPTER_DESC* pDesc = &desc)
            {
                return Pointer->GetDesc(pDesc);
            }
        }
    }
}