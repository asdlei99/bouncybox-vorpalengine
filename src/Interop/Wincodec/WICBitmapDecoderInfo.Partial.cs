using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapDecoderInfo
    {
        public HResult CreateInstance(out WICBitmapDecoder? bitmapDecoder)
        {
            IWICBitmapDecoder* pBitmapDecoder;
            int hr = Pointer->CreateInstance(&pBitmapDecoder);

            bitmapDecoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapDecoder(pBitmapDecoder) : null;

            return hr;
        }

        public HResult GetPatterns(Span<WICBitmapPattern> patterns, out uint patternsSupported, out uint patternsActual)
        {
            fixed (WICBitmapPattern* pPatterns = patterns)
            fixed (uint* pPatternsSupported = &patternsSupported)
            fixed (uint* pPatternsActual = &patternsActual)
            {
                return Pointer->GetPatterns((uint)patterns.Length, pPatterns, pPatternsSupported, pPatternsActual);
            }
        }
    }
}