using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
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

        public HResult GetEvictionPriority(uint* evictionPriority)
        {
            return Pointer->GetEvictionPriority(evictionPriority);
        }

        public HResult GetSharedHandle(IntPtr* sharedHandle)
        {
            return Pointer->GetSharedHandle(sharedHandle);
        }

        public HResult GetUsage(uint* usage)
        {
            return Pointer->GetUsage(usage);
        }

        public HResult SetEvictionPriority(uint evictionPriority)
        {
            return Pointer->SetEvictionPriority(evictionPriority);
        }

        public static implicit operator IDXGIResource*(DXGIResource value)
        {
            return value.Pointer;
        }
    }
}