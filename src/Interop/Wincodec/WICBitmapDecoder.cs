using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapDecoder" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapDecoder : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapDecoder" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapDecoder(IWICBitmapDecoder* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICBitmapDecoder* Pointer => (IWICBitmapDecoder*)base.Pointer;

        public HResult CopyPalette(IWICPalette* palette = null)
        {
            return Pointer->CopyPalette(palette);
        }

        public HResult GetColorContexts(uint count, IWICColorContext** colorContexts, uint* actualCount)
        {
            return Pointer->GetColorContexts(count, colorContexts, actualCount);
        }

        public HResult GetContainerFormat(Guid* containerFormat)
        {
            return Pointer->GetContainerFormat(containerFormat);
        }

        public HResult GetDecoderInfo(IWICBitmapDecoderInfo** decoderInfo)
        {
            return Pointer->GetDecoderInfo(decoderInfo);
        }

        public HResult GetFrame(uint index, IWICBitmapFrameDecode** bitmapFrame)
        {
            return Pointer->GetFrame(index, bitmapFrame);
        }

        public HResult GetFrameCount(uint* count)
        {
            return Pointer->GetFrameCount(count);
        }

        public HResult GetMetadataQueryReader(IWICMetadataQueryReader** metadataQueryReader)
        {
            return Pointer->GetMetadataQueryReader(metadataQueryReader);
        }

        public HResult GetPreview(IWICBitmapSource** bitmapSource)
        {
            return Pointer->GetPreview(bitmapSource);
        }

        public HResult GetThumbnail(IWICBitmapSource** thumbnail)
        {
            return Pointer->GetThumbnail(thumbnail);
        }

        public HResult Initialize([Optional] IStream* stream, WICDecodeOptions cacheOptions)
        {
            return Pointer->Initialize(stream, cacheOptions);
        }

        public HResult QueryCapability([Optional] IStream* stream, uint* capability)
        {
            return Pointer->QueryCapability(stream, capability);
        }

        public static implicit operator IWICBitmapDecoder*(WICBitmapDecoder value)
        {
            return value.Pointer;
        }
    }
}