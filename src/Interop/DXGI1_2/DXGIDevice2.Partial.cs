using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice2
    {
        public HResult OfferResources(ReadOnlySpan<Pointer<IDXGIResource>> resources, DXGI_OFFER_RESOURCE_PRIORITY priority)
        {
            fixed (Pointer<IDXGIResource>* ppResources = resources)
            {
                return Pointer->OfferResources((uint)resources.Length, (IDXGIResource**)ppResources, priority);
            }
        }

        public HResult ReclaimResources(ReadOnlySpan<Pointer<IDXGIResource>> resources, out bool? discarded)
        {
            int iDiscarded;
            int hr;

            fixed (Pointer<IDXGIResource>* ppResources = resources)
            {
                hr = Pointer->ReclaimResources((uint)resources.Length, (IDXGIResource**)ppResources, &iDiscarded);
            }

            discarded = TerraFX.Interop.Windows.SUCCEEDED(hr) ? iDiscarded == TerraFX.Interop.Windows.TRUE : (bool?)null;

            return hr;
        }
    }
}