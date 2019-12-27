using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapDecoder
    {
        public HResult GetColorContexts(Span<Pointer<IWICColorContext>> colorContexts, out uint actualCount)
        {
            fixed (Pointer<IWICColorContext>* pColorContexts = colorContexts)
            fixed (uint* pActualCount = &actualCount)
            {
                return Pointer->GetColorContexts((uint)colorContexts.Length, (IWICColorContext**)pColorContexts, pActualCount);
            }
        }

        public HResult GetContainerFormat(out Guid containerFormat)
        {
            fixed (Guid* pContainerFormat = &containerFormat)
            {
                return Pointer->GetContainerFormat(pContainerFormat);
            }
        }

        public HResult GetDecoderInfo(out WICBitmapDecoderInfo? decoderInfo)
        {
            IWICBitmapDecoderInfo* pDecoderInfo;
            int hr = Pointer->GetDecoderInfo(&pDecoderInfo);

            decoderInfo = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapDecoderInfo(pDecoderInfo) : null;

            return hr;
        }

        public HResult GetFrame(uint index, out WICBitmapFrameDecode? bitmapFrame)
        {
            IWICBitmapFrameDecode* pBitmapFrame;
            int hr = Pointer->GetFrame(index, &pBitmapFrame);

            bitmapFrame = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapFrameDecode(pBitmapFrame) : null;

            return hr;
        }

        public HResult GetFrameCount(out uint count)
        {
            fixed (uint* pCount = &count)
            {
                return Pointer->GetFrameCount(pCount);
            }
        }

        public HResult GetMetadataQueryReader(out WICMetadataQueryReader? metadataQueryReader)
        {
            IWICMetadataQueryReader* pMetadataQueryReader;
            int hr = Pointer->GetMetadataQueryReader(&pMetadataQueryReader);

            metadataQueryReader = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICMetadataQueryReader(pMetadataQueryReader) : null;

            return hr;
        }

        public HResult GetPreview(out WICBitmapSource? bitmapSource)
        {
            IWICBitmapSource* pBitmapSource;
            int hr = Pointer->GetPreview(&pBitmapSource);

            bitmapSource = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapSource(pBitmapSource) : null;

            return hr;
        }

        public HResult GetThumbnail(out WICBitmapSource? thumbnail)
        {
            IWICBitmapSource* pThumbnail;
            int hr = Pointer->GetThumbnail(&pThumbnail);

            thumbnail = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapSource(pThumbnail) : null;

            return hr;
        }

        public HResult QueryCapability([Optional] IStream* stream, out uint capability)
        {
            fixed (uint* pCapability = &capability)
            {
                return Pointer->QueryCapability(stream, pCapability);
            }
        }
    }
}