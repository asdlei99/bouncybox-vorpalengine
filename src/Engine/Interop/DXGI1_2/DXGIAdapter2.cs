using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGIAdapter2" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIAdapter2 : DXGIAdapter1
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIAdapter2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIAdapter2(IDXGIAdapter2* pointer) : base((IDXGIAdapter1*)pointer)
        {
        }

        public new IDXGIAdapter2* Pointer => (IDXGIAdapter2*)base.Pointer;

        public HResult GetDesc2(DXGI_ADAPTER_DESC2* pDesc)
        {
            return Pointer->GetDesc2(pDesc);
        }

        public static implicit operator IDXGIAdapter2*(DXGIAdapter2 value)
        {
            return value.Pointer;
        }
    }
}