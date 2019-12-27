using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICPixelFormatInfo" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPixelFormatInfo : WICComponentInfo
    {
        /// <summary>Initializes a new instance of the <see cref="WICPixelFormatInfo" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICPixelFormatInfo(IWICPixelFormatInfo* pointer) : base((IWICComponentInfo*)pointer)
        {
        }

        public new IWICPixelFormatInfo* Pointer => (IWICPixelFormatInfo*)base.Pointer;

        public HResult GetBitsPerPixel(uint* bitsPerPixel)
        {
            return Pointer->GetBitsPerPixel(bitsPerPixel);
        }

        public HResult GetChannelCount(uint* channelCount)
        {
            return Pointer->GetChannelCount(channelCount);
        }

        public HResult GetChannelMask(uint channelIndex, uint maskBufferCount, byte* maskBuffer, uint* actualCount)
        {
            return Pointer->GetChannelMask(channelIndex, maskBufferCount, maskBuffer, actualCount);
        }

        public HResult GetColorContext(IWICColorContext** colorContext)
        {
            return Pointer->GetColorContext(colorContext);
        }

        public HResult GetFormatGUID(Guid* format)
        {
            return Pointer->GetFormatGUID(format);
        }

        public static implicit operator IWICPixelFormatInfo*(WICPixelFormatInfo value)
        {
            return value.Pointer;
        }
    }
}