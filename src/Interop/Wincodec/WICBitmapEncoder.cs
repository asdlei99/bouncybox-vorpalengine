using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapEncoder" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapEncoder : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapEncoder" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapEncoder(IWICBitmapEncoder* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICBitmapEncoder* Pointer => (IWICBitmapEncoder*)base.Pointer;

        public HResult Commit()
        {
            return Pointer->Commit();
        }

        public HResult CreateNewFrame(IWICBitmapFrameEncode** frameEncode, IPropertyBag2** encoderOptions)
        {
            return Pointer->CreateNewFrame(frameEncode, encoderOptions);
        }

        public HResult GetContainerFormat(Guid* containerFormat)
        {
            return Pointer->GetContainerFormat(containerFormat);
        }

        public HResult GetEncoderInfo(IWICBitmapEncoderInfo** encoderInfo)
        {
            return Pointer->GetEncoderInfo(encoderInfo);
        }

        public HResult GetMetadataQueryWriter(IWICMetadataQueryWriter** metadataQueryWriter)
        {
            return Pointer->GetMetadataQueryWriter(metadataQueryWriter);
        }

        public HResult Initialize([Optional] IStream* stream, WICBitmapEncoderCacheOption cacheOption)
        {
            return Pointer->Initialize(stream, cacheOption);
        }

        public HResult SetColorContexts(uint count, IWICColorContext** colorContext)
        {
            return Pointer->SetColorContexts(count, colorContext);
        }

        public HResult SetPalette([Optional] IWICPalette* palette)
        {
            return Pointer->SetPalette(palette);
        }

        public HResult SetPreview([Optional] IWICBitmapSource* preview)
        {
            return Pointer->SetPreview(preview);
        }

        public HResult SetThumbnail([Optional] IWICBitmapSource* thumbnail)
        {
            return Pointer->SetThumbnail(thumbnail);
        }

        public static implicit operator IWICBitmapEncoder*(WICBitmapEncoder value)
        {
            return value.Pointer;
        }
    }
}