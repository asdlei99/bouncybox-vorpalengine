using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice2
    {
        public HResult OfferResources(ReadOnlySpan<IDXGIResourcePointer> resources, DXGI_OFFER_RESOURCE_PRIORITY Priority)
        {
            fixed (IDXGIResourcePointer* ppResources = resources)
            {
                return Pointer->OfferResources((uint)resources.Length, (IDXGIResource**)ppResources, Priority);
            }
        }

        public HResult ReclaimResources(ReadOnlySpan<IDXGIResourcePointer> resources, out bool? discarded)
        {
            int iDiscarded;
            int hr;

            fixed (IDXGIResourcePointer* ppResources = resources)
            {
                hr = Pointer->ReclaimResources((uint)resources.Length, (IDXGIResource**)ppResources, &iDiscarded);
            }

            discarded = TerraFX.Interop.Windows.SUCCEEDED(hr) ? iDiscarded == TerraFX.Interop.Windows.TRUE : (bool?)null;

            return hr;
        }
    }
}