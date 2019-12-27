using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICFormatConverterInfo" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICFormatConverterInfo : WICComponentInfo
    {
        /// <summary>Initializes a new instance of the <see cref="WICFormatConverterInfo" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICFormatConverterInfo(IWICFormatConverterInfo* pointer) : base((IWICComponentInfo*)pointer)
        {
        }

        public new IWICFormatConverterInfo* Pointer => (IWICFormatConverterInfo*)base.Pointer;

        public HResult CreateInstance(IWICFormatConverter** ppIConverter)
        {
            return Pointer->CreateInstance(ppIConverter);
        }

        public HResult GetPixelFormats(uint formats, Guid* pixelFormatGUIDs, uint* actual)
        {
            return Pointer->GetPixelFormats(formats, pixelFormatGUIDs, actual);
        }

        public static implicit operator IWICFormatConverterInfo*(WICFormatConverterInfo value)
        {
            return value.Pointer;
        }
    }
}