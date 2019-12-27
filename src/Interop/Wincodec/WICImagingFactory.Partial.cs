using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.ObjIdl;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICImagingFactory
    {
        public HResult CreateBitmap(uint width, uint height, Guid* pixelFormat, WICBitmapCreateCacheOption option, out WICBitmap? bitmap)
        {
            IWICBitmap* pBitmap;
            int hr = Pointer->CreateBitmap(width, height, pixelFormat, option, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapClipper(out WICBitmapClipper? bitmapClipper)
        {
            IWICBitmapClipper* pBitmapClipper;
            int hr = Pointer->CreateBitmapClipper(&pBitmapClipper);

            bitmapClipper = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapClipper(pBitmapClipper) : null;

            return hr;
        }

        public HResult CreateBitmapFlipRotator(out WICBitmapFlipRotator? bitmapFlipRotator)
        {
            IWICBitmapFlipRotator* pBitmapFlipRotator;
            int hr = Pointer->CreateBitmapFlipRotator(&pBitmapFlipRotator);

            bitmapFlipRotator = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapFlipRotator(pBitmapFlipRotator) : null;

            return hr;
        }

        public HResult CreateBitmapFromHBITMAP(IntPtr hBitmap, [Optional] IntPtr hPalette, WICBitmapAlphaChannelOption options, out WICBitmap? bitmap)
        {
            IWICBitmap* pBitmap;
            int hr = Pointer->CreateBitmapFromHBITMAP(hBitmap, hPalette, options, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapFromHICON(IntPtr hIcon, out WICBitmap? bitmap)
        {
            IWICBitmap* pBitmap;
            int hr = Pointer->CreateBitmapFromHICON(hIcon, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapFromMemory(uint width, uint height, Guid* pixelFormat, uint stride, ReadOnlySpan<byte> buffer, out WICBitmap? bitmap)
        {
            IWICBitmap* pBitmap;
            int hr;

            fixed (byte* pBuffer = buffer)
            {
                hr = Pointer->CreateBitmapFromMemory(width, height, pixelFormat, stride, (uint)buffer.Length, pBuffer, &pBitmap);
            }

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapFromSource([Optional] IWICBitmapSource* bitmapSource, WICBitmapCreateCacheOption option, out WICBitmap? bitmap)
        {
            IWICBitmap* pBitmap;
            int hr = Pointer->CreateBitmapFromSource(bitmapSource, option, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapFromSourceRect([Optional] IWICBitmapSource* bitmapSource, uint x, uint y, uint width, uint height, out WICBitmap? bitmap)
        {
            IWICBitmap* pBitmap;
            int hr = Pointer->CreateBitmapFromSourceRect(bitmapSource, x, y, width, height, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapScaler(out WICBitmapScaler? bitmapScaler)
        {
            IWICBitmapScaler* pBitmapScaler;
            int hr = Pointer->CreateBitmapScaler(&pBitmapScaler);

            bitmapScaler = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapScaler(pBitmapScaler) : null;

            return hr;
        }

        public HResult CreateColorContext(out WICColorContext? colorContext)
        {
            IWICColorContext* pColorContext;
            int hr = Pointer->CreateColorContext(&pColorContext);

            colorContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICColorContext(pColorContext) : null;

            return hr;
        }

        public HResult CreateColorTransformer(out WICColorTransform? colorTransform)
        {
            IWICColorTransform* pColorTransform;
            int hr = Pointer->CreateColorTransformer(&pColorTransform);

            colorTransform = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICColorTransform(pColorTransform) : null;

            return hr;
        }

        public HResult CreateComponentEnumerator(uint componentTypes, uint options, out EnumUnknown? enumUnknown)
        {
            IEnumUnknown* pEnumUnknown;
            int hr = Pointer->CreateComponentEnumerator(componentTypes, options, &pEnumUnknown);

            enumUnknown = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new EnumUnknown(pEnumUnknown) : null;

            return hr;
        }

        public HResult CreateComponentInfo(Guid* clsidComponent, out WICComponentInfo? info)
        {
            IWICComponentInfo* pInfo;
            int hr = Pointer->CreateComponentInfo(clsidComponent, &pInfo);

            info = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICComponentInfo(pInfo) : null;

            return hr;
        }

        public HResult CreateDecoder(Guid* containerFormat, [Optional] Guid* vendor, out WICBitmapDecoder? decoder)
        {
            IWICBitmapDecoder* pDecoder;
            int hr = Pointer->CreateDecoder(containerFormat, vendor, &pDecoder);

            decoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapDecoder(pDecoder) : null;

            return hr;
        }

        public HResult CreateDecoderFromFileHandle(UIntPtr hFile, [Optional] Guid* vendor, WICDecodeOptions metadataOptions, out WICBitmapDecoder? decoder)
        {
            IWICBitmapDecoder* pDecoder;
            int hr = Pointer->CreateDecoderFromFileHandle(hFile, vendor, metadataOptions, &pDecoder);

            decoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapDecoder(pDecoder) : null;

            return hr;
        }

        public HResult CreateDecoderFromFilename(
            ReadOnlySpan<char> filename,
            [Optional] Guid* vendor,
            uint desiredAccess,
            WICDecodeOptions metadataOptions,
            out WICBitmapDecoder? decoder)
        {
            IWICBitmapDecoder* pDecoder;
            int hr;

            fixed (char* pFilename = filename)
            {
                hr = Pointer->CreateDecoderFromFilename((ushort*)pFilename, vendor, desiredAccess, metadataOptions, &pDecoder);
            }

            decoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapDecoder(pDecoder) : null;

            return hr;
        }

        public HResult CreateDecoderFromStream([Optional] IStream* stream, Guid* vendor, WICDecodeOptions metadataOptions, out WICBitmapDecoder? decoder)
        {
            IWICBitmapDecoder* pDecoder;
            int hr = Pointer->CreateDecoderFromStream(stream, vendor, metadataOptions, &pDecoder);

            decoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapDecoder(pDecoder) : null;

            return hr;
        }

        public HResult CreateEncoder(Guid* containerFormat, [Optional] Guid* vendor, out WICBitmapEncoder? encoder)
        {
            IWICBitmapEncoder* pEncoder;
            int hr = Pointer->CreateEncoder(containerFormat, vendor, &pEncoder);

            encoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapEncoder(pEncoder) : null;

            return hr;
        }

        public HResult CreateFastMetadataEncoderFromDecoder([Optional] IWICBitmapDecoder* decoder, out WICFastMetadataEncoder? fastEncoder)
        {
            IWICFastMetadataEncoder* pFastEncoder;
            int hr = Pointer->CreateFastMetadataEncoderFromDecoder(decoder, &pFastEncoder);

            fastEncoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICFastMetadataEncoder(pFastEncoder) : null;

            return hr;
        }

        public HResult CreateFastMetadataEncoderFromFrameDecode([Optional] IWICBitmapFrameDecode* frameDecoder, out WICFastMetadataEncoder? fastEncoder)
        {
            IWICFastMetadataEncoder* pFastEncoder;
            int hr = Pointer->CreateFastMetadataEncoderFromFrameDecode(frameDecoder, &pFastEncoder);

            fastEncoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICFastMetadataEncoder(pFastEncoder) : null;

            return hr;
        }

        public HResult CreateFormatConverter(out WICFormatConverter? formatConverter)
        {
            IWICFormatConverter* pFormatConverter;
            int hr = Pointer->CreateFormatConverter(&pFormatConverter);

            formatConverter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICFormatConverter(pFormatConverter) : null;

            return hr;
        }

        public HResult CreatePalette(out WICPalette? palette)
        {
            IWICPalette* pPalette;
            int hr = Pointer->CreatePalette(&pPalette);

            palette = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICPalette(pPalette) : null;

            return hr;
        }

        public HResult CreateQueryWriter(Guid* metadataFormat, [Optional] Guid* vendor, out WICMetadataQueryWriter? queryWriter)
        {
            IWICMetadataQueryWriter* pQueryWriter;
            int hr = Pointer->CreateQueryWriter(metadataFormat, vendor, &pQueryWriter);

            queryWriter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICMetadataQueryWriter(pQueryWriter) : null;

            return hr;
        }

        public HResult CreateQueryWriterFromReader(
            [Optional] IWICMetadataQueryReader* queryReader,
            [Optional] Guid* vendor,
            out WICMetadataQueryWriter? queryWriter)
        {
            IWICMetadataQueryWriter* pQueryWriter;
            int hr = Pointer->CreateQueryWriterFromReader(queryReader, vendor, &pQueryWriter);

            queryWriter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICMetadataQueryWriter(pQueryWriter) : null;

            return hr;
        }

        public HResult CreateStream(out WICStream? stream)
        {
            IWICStream* pStream;
            int hr = Pointer->CreateStream(&pStream);

            stream = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICStream(pStream) : null;

            return hr;
        }
    }
}