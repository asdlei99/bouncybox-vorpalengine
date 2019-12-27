using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIDeviceSubObject" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDeviceSubObject : DXGIObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIDeviceSubObject" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIDeviceSubObject(IDXGIDeviceSubObject* pointer) : base((IDXGIObject*)pointer)
        {
        }

        public new IDXGIDeviceSubObject* Pointer => (IDXGIDeviceSubObject*)base.Pointer;

        public HResult GetDevice(Guid* iid, void** device)
        {
            return Pointer->GetDevice(iid, device);
        }

        public static implicit operator IDXGIDeviceSubObject*(DXGIDeviceSubObject value)
        {
            return value.Pointer;
        }
    }
}