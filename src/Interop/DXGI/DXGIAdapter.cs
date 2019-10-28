using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIAdapter" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIAdapter : DXGIObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIAdapter" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIAdapter(IDXGIAdapter* pointer) : base((IDXGIObject*)pointer)
        {
        }

        public new IDXGIAdapter* Pointer => (IDXGIAdapter*)base.Pointer;

        public HResult CheckInterfaceSupport(Guid* InterfaceName, LARGE_INTEGER* pUMDVersion)
        {
            return Pointer->CheckInterfaceSupport(InterfaceName, pUMDVersion);
        }

        public HResult EnumOutputs(uint Output, IDXGIOutput** ppOutput)
        {
            return Pointer->EnumOutputs(Output, ppOutput);
        }

        public HResult GetDesc(DXGI_ADAPTER_DESC* pDesc)
        {
            return Pointer->GetDesc(pDesc);
        }

        public static implicit operator IDXGIAdapter*(DXGIAdapter value)
        {
            return value.Pointer;
        }
    }
}