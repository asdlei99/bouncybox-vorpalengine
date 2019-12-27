using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmap" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmap : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmap" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmap(IWICBitmap* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICBitmap* Pointer => (IWICBitmap*)base.Pointer;

        public HResult Lock(WICRect* lockRect, uint flags, IWICBitmapLock** ppILock)
        {
            return Pointer->Lock(lockRect, flags, ppILock);
        }

        public HResult SetPalette(IWICPalette* palette = null)
        {
            return Pointer->SetPalette(palette);
        }

        public HResult SetResolution(double dpiX, double dpiY)
        {
            return Pointer->SetResolution(dpiX, dpiY);
        }

        public static implicit operator IWICBitmap*(WICBitmap value)
        {
            return value.Pointer;
        }
    }
}