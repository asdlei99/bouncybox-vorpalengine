using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPlanarFormatConverter
    {
        public HResult CanConvert(ReadOnlySpan<Guid> srcPixelFormats, Guid* dstPixelFormat, out bool canConvert)
        {
            int iCanConvert;
            int hr;

            fixed (Guid* pSrcPixelFormats = srcPixelFormats)
            {
                hr = Pointer->CanConvert(pSrcPixelFormats, (uint)srcPixelFormats.Length, dstPixelFormat, &iCanConvert);
            }

            canConvert = iCanConvert == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult Initialize(
            ReadOnlySpan<Pointer<IWICBitmapSource>> planes,
            Guid* dstFormat,
            WICBitmapDitherType dither,
            [Optional] IWICPalette* palette,
            double alphaThresholdPercent,
            WICBitmapPaletteType paletteTranslate)
        {
            fixed (Pointer<IWICBitmapSource>* pPlanes = planes)
            {
                return Pointer->Initialize(
                    (IWICBitmapSource**)pPlanes,
                    (uint)planes.Length,
                    dstFormat,
                    dither,
                    palette,
                    alphaThresholdPercent,
                    paletteTranslate);
            }
        }
    }
}