using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapFrameEncode" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapFrameEncode : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapFrameEncode" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapFrameEncode(IWICBitmapFrameEncode* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICBitmapFrameEncode* Pointer => (IWICBitmapFrameEncode*)base.Pointer;

        public HResult Commit()
        {
            return Pointer->Commit();
        }

        public HResult GetMetadataQueryWriter(IWICMetadataQueryWriter** metadataQueryWriter)
        {
            return Pointer->GetMetadataQueryWriter(metadataQueryWriter);
        }

        public HResult Initialize(IPropertyBag2* encoderOptions = null)
        {
            return Pointer->Initialize(encoderOptions);
        }

        public HResult SetColorContexts(uint count, IWICColorContext** colorContext)
        {
            return Pointer->SetColorContexts(count, colorContext);
        }

        public HResult SetPalette(IWICPalette* palette = null)
        {
            return Pointer->SetPalette(palette);
        }

        public HResult SetPixelFormat(Guid* pixelFormat)
        {
            return Pointer->SetPixelFormat(pixelFormat);
        }

        public HResult SetResolution(double dpiX, double dpiY)
        {
            return Pointer->SetResolution(dpiX, dpiY);
        }

        public HResult SetSize(uint width, uint height)
        {
            return Pointer->SetSize(width, height);
        }

        public HResult SetThumbnail(IWICBitmapSource* thumbnail = null)
        {
            return Pointer->SetThumbnail(thumbnail);
        }

        public HResult WritePixels(uint lineCount, uint stride, uint bufferSize, byte* pixels)
        {
            return Pointer->WritePixels(lineCount, stride, bufferSize, pixels);
        }

        public HResult WriteSource(IWICBitmapSource* bitmapSource = null, WICRect* rect = null)
        {
            return Pointer->WriteSource(bitmapSource, rect);
        }

        public static implicit operator IWICBitmapFrameEncode*(WICBitmapFrameEncode value)
        {
            return value.Pointer;
        }
    }
}