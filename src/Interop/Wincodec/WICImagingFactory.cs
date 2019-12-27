using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICImagingFactory" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICImagingFactory : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICImagingFactory" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICImagingFactory(IWICImagingFactory* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICImagingFactory* Pointer => (IWICImagingFactory*)base.Pointer;

        public HResult CreateBitmap(uint width, uint height, Guid* pixelFormat, WICBitmapCreateCacheOption option, IWICBitmap** bitmap)
        {
            return Pointer->CreateBitmap(width, height, pixelFormat, option, bitmap);
        }

        public HResult CreateBitmapClipper(IWICBitmapClipper** bitmapClipper)
        {
            return Pointer->CreateBitmapClipper(bitmapClipper);
        }

        public HResult CreateBitmapFlipRotator(IWICBitmapFlipRotator** bitmapFlipRotator)
        {
            return Pointer->CreateBitmapFlipRotator(bitmapFlipRotator);
        }

        public HResult CreateBitmapFromHBITMAP(IntPtr hBitmap, [Optional] IntPtr hPalette, WICBitmapAlphaChannelOption options, IWICBitmap** bitmap)
        {
            return Pointer->CreateBitmapFromHBITMAP(hBitmap, hPalette, options, bitmap);
        }

        public HResult CreateBitmapFromHICON(IntPtr hIcon, IWICBitmap** bitmap)
        {
            return Pointer->CreateBitmapFromHICON(hIcon, bitmap);
        }

        public HResult CreateBitmapFromMemory(uint width, uint height, Guid* pixelFormat, uint stride, uint bufferSize, byte* buffer, IWICBitmap** bitmap)
        {
            return Pointer->CreateBitmapFromMemory(width, height, pixelFormat, stride, bufferSize, buffer, bitmap);
        }

        public HResult CreateBitmapFromSource([Optional] IWICBitmapSource* bitmapSource, WICBitmapCreateCacheOption option, IWICBitmap** bitmap)
        {
            return Pointer->CreateBitmapFromSource(bitmapSource, option, bitmap);
        }

        public HResult CreateBitmapFromSourceRect([Optional] IWICBitmapSource* bitmapSource, uint x, uint y, uint width, uint height, IWICBitmap** bitmap)
        {
            return Pointer->CreateBitmapFromSourceRect(bitmapSource, x, y, width, height, bitmap);
        }

        public HResult CreateBitmapScaler(IWICBitmapScaler** bitmapScaler)
        {
            return Pointer->CreateBitmapScaler(bitmapScaler);
        }

        public HResult CreateColorContext(IWICColorContext** colorContext)
        {
            return Pointer->CreateColorContext(colorContext);
        }

        public HResult CreateColorTransformer(IWICColorTransform** colorTransform)
        {
            return Pointer->CreateColorTransformer(colorTransform);
        }

        public HResult CreateComponentEnumerator(uint componentTypes, uint options, IEnumUnknown** enumUnknown)
        {
            return Pointer->CreateComponentEnumerator(componentTypes, options, enumUnknown);
        }

        public HResult CreateComponentInfo(Guid* clsidComponent, IWICComponentInfo** info)
        {
            return Pointer->CreateComponentInfo(clsidComponent, info);
        }

        public HResult CreateDecoder(Guid* containerFormat, [Optional] Guid* vendor, IWICBitmapDecoder** decoder)
        {
            return Pointer->CreateDecoder(containerFormat, vendor, decoder);
        }

        public HResult CreateDecoderFromFileHandle(UIntPtr hFile, [Optional] Guid* vendor, WICDecodeOptions metadataOptions, IWICBitmapDecoder** decoder)
        {
            return Pointer->CreateDecoderFromFileHandle(hFile, vendor, metadataOptions, decoder);
        }

        public HResult CreateDecoderFromFilename(
            ushort* filename,
            [Optional] Guid* vendor,
            uint desiredAccess,
            WICDecodeOptions metadataOptions,
            IWICBitmapDecoder** decoder)
        {
            return Pointer->CreateDecoderFromFilename(filename, vendor, desiredAccess, metadataOptions, decoder);
        }

        public HResult CreateDecoderFromStream([Optional] IStream* stream, Guid* vendor, WICDecodeOptions metadataOptions, IWICBitmapDecoder** decoder)
        {
            return Pointer->CreateDecoderFromStream(stream, vendor, metadataOptions, decoder);
        }

        public HResult CreateEncoder(Guid* containerFormat, [Optional] Guid* vendor, IWICBitmapEncoder** encoder)
        {
            return Pointer->CreateEncoder(containerFormat, vendor, encoder);
        }

        public HResult CreateFastMetadataEncoderFromDecoder([Optional] IWICBitmapDecoder* decoder, IWICFastMetadataEncoder** fastEncoder)
        {
            return Pointer->CreateFastMetadataEncoderFromDecoder(decoder, fastEncoder);
        }

        public HResult CreateFastMetadataEncoderFromFrameDecode([Optional] IWICBitmapFrameDecode* frameDecoder, IWICFastMetadataEncoder** fastEncoder)
        {
            return Pointer->CreateFastMetadataEncoderFromFrameDecode(frameDecoder, fastEncoder);
        }

        public HResult CreateFormatConverter(IWICFormatConverter** formatConverter)
        {
            return Pointer->CreateFormatConverter(formatConverter);
        }

        public HResult CreatePalette(IWICPalette** palette)
        {
            return Pointer->CreatePalette(palette);
        }

        public HResult CreateQueryWriter(Guid* metadataFormat, [Optional] Guid* vendor, IWICMetadataQueryWriter** queryWriter)
        {
            return Pointer->CreateQueryWriter(metadataFormat, vendor, queryWriter);
        }

        public HResult CreateQueryWriterFromReader(
            [Optional] IWICMetadataQueryReader* queryReader,
            [Optional] Guid* vendor,
            IWICMetadataQueryWriter** queryWriter)
        {
            return Pointer->CreateQueryWriterFromReader(queryReader, vendor, queryWriter);
        }

        public HResult CreateStream(IWICStream** stream)
        {
            return Pointer->CreateStream(stream);
        }

        public static implicit operator IWICImagingFactory*(WICImagingFactory value)
        {
            return value.Pointer;
        }
    }
}