using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1ImageSource" /> COM interface.</summary>
    public unsafe class D2D1ImageSource : D2D1Image
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1ImageSource" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1ImageSource(ID2D1ImageSource* pointer) : base((ID2D1Image*)pointer)
        {
        }

        public new ID2D1ImageSource* Pointer => (ID2D1ImageSource*)base.Pointer;

        public HResult OfferResources()
        {
            return Pointer->OfferResources();
        }

        public HResult TryReclaimResources(out bool resourcesDiscarded)
        {
            int iResourcesDiscarded;
            int hr = Pointer->TryReclaimResources(&iResourcesDiscarded);

            resourcesDiscarded = iResourcesDiscarded == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator ID2D1ImageSource*(D2D1ImageSource value)
        {
            return value.Pointer;
        }
    }
}