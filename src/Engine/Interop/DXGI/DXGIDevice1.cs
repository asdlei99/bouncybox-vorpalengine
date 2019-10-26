using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIDevice1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice1 : DXGIDevice
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIDevice1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIDevice1(IDXGIDevice1* pointer) : base((IDXGIDevice*)pointer)
        {
        }

        public new IDXGIDevice1* Pointer => (IDXGIDevice1*)base.Pointer;

        public HResult GetMaximumFrameLatency(uint* pMaxLatency)
        {
            return Pointer->GetMaximumFrameLatency(pMaxLatency);
        }

        public HResult SetMaximumFrameLatency(uint MaxLatency)
        {
            return Pointer->SetMaximumFrameLatency(MaxLatency);
        }

        public static implicit operator IDXGIDevice1*(DXGIDevice1 value)
        {
            return value.Pointer;
        }
    }
}