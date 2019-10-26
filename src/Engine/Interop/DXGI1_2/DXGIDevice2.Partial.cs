using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Engine.Interop.DWrite;
using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice2
    {
        public HResult OfferResources(ReadOnlySpan<IDXGIResourcePointer> resources, DXGI_OFFER_RESOURCE_PRIORITY Priority)
        {
            var pDxgiResources = new IDXGIResource*[resources.Length];

            fixed (IDXGIResource** ppDxgiResources = pDxgiResources)
            {
                return Pointer->OfferResources((uint)pDxgiResources.Length, ppDxgiResources, Priority);
            }
        }

        public HResult ReclaimResources(ReadOnlySpan<IDXGIResourcePointer> resources, out bool? discarded)
        {
            var pDxgiResources = new IDXGIResource*[resources.Length];
            int iDiscarded;
            int hr;

            fixed (IDXGIResource** ppDxgiResources = pDxgiResources)
            {
                hr = Pointer->ReclaimResources((uint)pDxgiResources.Length, ppDxgiResources, &iDiscarded);
            }

            discarded = TerraFX.Interop.Windows.SUCCEEDED(hr) ? iDiscarded == TerraFX.Interop.Windows.TRUE : (bool?)null;

            return hr;
        }
    }
}