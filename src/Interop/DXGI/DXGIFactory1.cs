using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIFactory1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIFactory1 : DXGIFactory
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIFactory1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIFactory1(IDXGIFactory1* pointer) : base((IDXGIFactory*)pointer)
        {
        }

        public new IDXGIFactory1* Pointer => (IDXGIFactory1*)base.Pointer;

        public HResult EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
        {
            return Pointer->EnumAdapters1(Adapter, ppAdapter);
        }

        public bool IsCurrent()
        {
            return Pointer->IsCurrent() == TerraFX.Interop.Windows.TRUE;
        }

        public static implicit operator IDXGIFactory1*(DXGIFactory1 value)
        {
            return value.Pointer;
        }
    }
}