using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
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

        public HResult GetParent(Guid* riid, void** ppParent)
        {
            return Pointer->GetParent(riid, ppParent);
        }

        public HResult GetPrivateData(Guid* Name, uint* pDataSize, void* pData)
        {
            return Pointer->GetPrivateData(Name, pDataSize, pData);
        }

        public HResult SetPrivateData(Guid* Name, uint DataSize, void* pData)
        {
            return Pointer->SetPrivateData(Name, DataSize, pData);
        }

        public HResult SetPrivateDataInterface(Guid* Name, [Optional] IUnknown* pUnknown)
        {
            return Pointer->SetPrivateDataInterface(Name, pUnknown);
        }

        public static implicit operator IDXGIObject*(DXGIObject value)
        {
            return value.Pointer;
        }
    }
}