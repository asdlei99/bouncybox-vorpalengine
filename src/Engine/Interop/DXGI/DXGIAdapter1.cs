using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIAdapter1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIAdapter1 : DXGIAdapter
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIAdapter1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIAdapter1(IDXGIAdapter1* pointer) : base((IDXGIAdapter*)pointer)
        {
        }

        public new IDXGIAdapter1* Pointer => (IDXGIAdapter1*)base.Pointer;

        public HResult GetDesc1(DXGI_ADAPTER_DESC1* pDesc)
        {
            return Pointer->GetDesc1(pDesc);
        }

        public static implicit operator IDXGIAdapter1*(DXGIAdapter1 value)
        {
            return value.Pointer;
        }
    }
}