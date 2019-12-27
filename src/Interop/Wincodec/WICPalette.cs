using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICPalette" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPalette : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICPalette" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICPalette(IWICPalette* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICPalette* Pointer => (IWICPalette*)base.Pointer;

        public HResult GetColorCount(uint* count)
        {
            return Pointer->GetColorCount(count);
        }

        public HResult GetColors(uint count, uint* colors, uint* actualColorCount)
        {
            return Pointer->GetColors(count, colors, actualColorCount);
        }

        public HResult GetType(WICBitmapPaletteType* paletteType)
        {
            return Pointer->GetType(paletteType);
        }

        public HResult HasAlpha(out bool hasAlpha)
        {
            int iHasAlpha;
            int hr = Pointer->HasAlpha(&iHasAlpha);

            hasAlpha = iHasAlpha == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult InitializeCustom(uint* colors, uint count)
        {
            return Pointer->InitializeCustom(colors, count);
        }

        public HResult InitializeFromBitmap([Optional] IWICBitmapSource* surface, uint count, bool addTransparentColor)
        {
            return Pointer->InitializeFromBitmap(surface, count, addTransparentColor ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public HResult InitializeFromPalette([Optional] IWICPalette* palette)
        {
            return Pointer->InitializeFromPalette(palette);
        }

        public HResult InitializePredefined(WICBitmapPaletteType paletteType, bool addTransparentColor)
        {
            return Pointer->InitializePredefined(paletteType, addTransparentColor ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public HResult IsBlackWhite(out bool isBlackWhite)
        {
            int iIsBlackWhite;
            int hr = Pointer->IsBlackWhite(&iIsBlackWhite);

            isBlackWhite = iIsBlackWhite == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult IsGrayscale(out bool isGrayscale)
        {
            int iIsGrayscale;
            int hr = Pointer->IsGrayscale(&iIsGrayscale);

            isGrayscale = iIsGrayscale == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IWICPalette*(WICPalette value)
        {
            return value.Pointer;
        }
    }
}