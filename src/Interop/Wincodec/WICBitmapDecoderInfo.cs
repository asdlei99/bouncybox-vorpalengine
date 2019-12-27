using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapDecoderInfo" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapDecoderInfo : WICBitmapCodecInfo
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapDecoderInfo" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapDecoderInfo(IWICBitmapDecoderInfo* pointer) : base((IWICBitmapCodecInfo*)pointer)
        {
        }

        public new IWICBitmapDecoderInfo* Pointer => (IWICBitmapDecoderInfo*)base.Pointer;

        public HResult CreateInstance(IWICBitmapDecoder** bitmapDecoder)
        {
            return Pointer->CreateInstance(bitmapDecoder);
        }

        public HResult GetPatterns(uint patternsSize, WICBitmapPattern* patterns, uint* patternsSupported, uint* patternsActual)
        {
            return Pointer->GetPatterns(patternsSize, patterns, patternsSupported, patternsActual);
        }

        public HResult MatchesPattern([Optional] IStream* stream, out bool matches)
        {
            int iMatches;
            int hr = Pointer->MatchesPattern(stream, &iMatches);

            matches = iMatches == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IWICBitmapDecoderInfo*(WICBitmapDecoderInfo value)
        {
            return value.Pointer;
        }
    }
}