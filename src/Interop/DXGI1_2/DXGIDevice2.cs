using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGIDevice2" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice2 : DXGIDevice1
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIDevice2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIDevice2(IDXGIDevice2* pointer) : base((IDXGIDevice1*)pointer)
        {
        }

        public new IDXGIDevice2* Pointer => (IDXGIDevice2*)base.Pointer;

        public HResult EnqueueSetEvent(IntPtr hEvent)
        {
            return Pointer->EnqueueSetEvent(hEvent);
        }

        public HResult OfferResources(uint NumResources, IDXGIResource** ppResources, DXGI_OFFER_RESOURCE_PRIORITY Priority)
        {
            return Pointer->OfferResources(NumResources, ppResources, Priority);
        }

        public HResult ReclaimResources(uint NumResources, IDXGIResource** ppResources, out bool? discarded)
        {
            int iDiscarded;
            int hr = Pointer->ReclaimResources(NumResources, ppResources, &iDiscarded);

            discarded = iDiscarded == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IDXGIDevice2*(DXGIDevice2 value)
        {
            return value.Pointer;
        }
    }
}