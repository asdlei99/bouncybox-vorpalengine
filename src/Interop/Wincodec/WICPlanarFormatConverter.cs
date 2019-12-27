using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICPlanarFormatConverter" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPlanarFormatConverter : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICPlanarFormatConverter" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICPlanarFormatConverter(IWICPlanarFormatConverter* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICPlanarFormatConverter* Pointer => (IWICPlanarFormatConverter*)base.Pointer;

        public HResult CanConvert(Guid* srcPixelFormats, uint srcPlaneCount, Guid* dstPixelFormat, out bool canConvert)
        {
            int iCanConvert;
            int hr = Pointer->CanConvert(srcPixelFormats, srcPlaneCount, dstPixelFormat, &iCanConvert);

            canConvert = iCanConvert == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult Initialize(
            IWICBitmapSource** planes,
            uint planeCount,
            Guid* dstFormat,
            WICBitmapDitherType dither,
            [Optional] IWICPalette* palette,
            double alphaThresholdPercent,
            WICBitmapPaletteType paletteTranslate)
        {
            return Pointer->Initialize(planes, planeCount, dstFormat, dither, palette, alphaThresholdPercent, paletteTranslate);
        }

        public static implicit operator IWICPlanarFormatConverter*(WICPlanarFormatConverter value)
        {
            return value.Pointer;
        }
    }
}