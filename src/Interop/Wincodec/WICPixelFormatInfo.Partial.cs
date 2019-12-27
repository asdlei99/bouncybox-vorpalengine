using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPixelFormatInfo
    {
        public HResult GetBitsPerPixel(out uint bitsPerPixel)
        {
            fixed (uint* pBitsPerPixel = &bitsPerPixel)
            {
                return Pointer->GetBitsPerPixel(pBitsPerPixel);
            }
        }

        public HResult GetChannelCount(out uint channelCount)
        {
            fixed (uint* pChannelCount = &channelCount)
            {
                return Pointer->GetChannelCount(pChannelCount);
            }
        }

        public HResult GetChannelMask(uint channelIndex, ReadOnlySpan<byte> maskBuffer, out uint actualCount)
        {
            fixed (byte* pMaskBuffer = maskBuffer)
            fixed (uint* pActualCount = &actualCount)
            {
                return Pointer->GetChannelMask(channelIndex, (uint)maskBuffer.Length, pMaskBuffer, pActualCount);
            }
        }

        public HResult GetColorContext(out WICColorContext? colorContext)
        {
            IWICColorContext* pColorContext;
            int hr = Pointer->GetColorContext(&pColorContext);

            colorContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICColorContext(pColorContext) : null;

            return hr;
        }

        public HResult GetFormatGUID(out Guid format)
        {
            fixed (Guid* pFormat = &format)
            {
                return Pointer->GetFormatGUID(pFormat);
            }
        }
    }
}