using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIResource" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIResource : DXGIDeviceSubObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIResource" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIResource(IDXGIResource* pointer) : base((IDXGIDeviceSubObject*)pointer)
        {
        }

        public new IDXGIResource* Pointer => (IDXGIResource*)base.Pointer;

        public HResult GetEvictionPriority(uint* pEvictionPriority)
        {
            return Pointer->GetEvictionPriority(pEvictionPriority);
        }

        public HResult GetSharedHandle(IntPtr* pSharedHandle)
        {
            return Pointer->GetSharedHandle(pSharedHandle);
        }

        public HResult GetUsage(uint* pUsage)
        {
            return Pointer->GetUsage(pUsage);
        }

        public HResult SetEvictionPriority(uint EvictionPriority)
        {
            return Pointer->SetEvictionPriority(EvictionPriority);
        }

        public static implicit operator IDXGIResource*(DXGIResource value)
        {
            return value.Pointer;
        }
    }
}