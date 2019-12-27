using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICFormatConverter" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICFormatConverter : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICFormatConverter" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICFormatConverter(IWICFormatConverter* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICFormatConverter* Pointer => (IWICFormatConverter*)base.Pointer;

        public HResult CanConvert(Guid* srcPixelFormat, Guid* dstPixelFormat, out bool canConvert)
        {
            int iCanConvert;
            int hr = Pointer->CanConvert(srcPixelFormat, dstPixelFormat, &iCanConvert);

            canConvert = iCanConvert == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult Initialize(
            [Optional] IWICBitmapSource* source,
            Guid* dstFormat,
            WICBitmapDitherType dither,
            [Optional] IWICPalette* palette,
            double alphaThresholdPercent,
            WICBitmapPaletteType paletteTranslate)
        {
            return Pointer->Initialize(source, dstFormat, dither, palette, alphaThresholdPercent, paletteTranslate);
        }

        public static implicit operator IWICFormatConverter*(WICFormatConverter value)
        {
            return value.Pointer;
        }
    }
}