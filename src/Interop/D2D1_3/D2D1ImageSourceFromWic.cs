using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1ImageSourceFromWic" /> COM interface.</summary>
    public unsafe partial class D2D1ImageSourceFromWic : D2D1ImageSource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1ImageSourceFromWic" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1ImageSourceFromWic(ID2D1ImageSourceFromWic* pointer) : base((ID2D1ImageSource*)pointer)
        {
        }

        public new ID2D1ImageSourceFromWic* Pointer => (ID2D1ImageSourceFromWic*)base.Pointer;

        public HResult EnsureCached(D2D_RECT_U* rectangleToFill = null)
        {
            return Pointer->EnsureCached(rectangleToFill);
        }

        public void GetSource(IWICBitmapSource** wicBitmapSource)
        {
            Pointer->GetSource(wicBitmapSource);
        }

        public HResult TrimCache(D2D_RECT_U* rectangleToPreserve = null)
        {
            return Pointer->TrimCache(rectangleToPreserve);
        }

        public static implicit operator ID2D1ImageSourceFromWic*(D2D1ImageSourceFromWic value)
        {
            return value.Pointer;
        }
    }
}