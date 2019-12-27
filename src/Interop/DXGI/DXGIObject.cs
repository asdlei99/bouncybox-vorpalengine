using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIObject" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIObject : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIObject" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIObject(IDXGIObject* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDXGIObject* Pointer => (IDXGIObject*)base.Pointer;

        public HResult GetParent(Guid* iid, void** parent)
        {
            return Pointer->GetParent(iid, parent);
        }

        public HResult GetPrivateData(Guid* name, uint* dataSize, void* data)
        {
            return Pointer->GetPrivateData(name, dataSize, data);
        }

        public HResult SetPrivateData(Guid* name, uint dataSize, void* data)
        {
            return Pointer->SetPrivateData(name, dataSize, data);
        }

        public HResult SetPrivateDataInterface(Guid* name, IUnknown* unknown = null)
        {
            return Pointer->SetPrivateDataInterface(name, unknown);
        }

        public static implicit operator IDXGIObject*(DXGIObject value)
        {
            return value.Pointer;
        }
    }
}