using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.OCIdl;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapEncoder
    {
        public HResult CreateNewFrame(out WICBitmapFrameEncode? frameEncode, out PropertyBag2? encoderOptions)
        {
            IWICBitmapFrameEncode* pFrameEncode;
            IPropertyBag2* pEncoderOptions;
            int hr = Pointer->CreateNewFrame(&pFrameEncode, &pEncoderOptions);
            bool succeeded = TerraFX.Interop.Windows.SUCCEEDED(hr);

            frameEncode = succeeded ? new WICBitmapFrameEncode(pFrameEncode) : null;
            encoderOptions = succeeded ? new PropertyBag2(pEncoderOptions) : null;

            return hr;
        }

        public HResult CreateNewFrame(out WICBitmapFrameEncode? frameEncode)
        {
            IWICBitmapFrameEncode* pFrameEncode;
            int hr = Pointer->CreateNewFrame(&pFrameEncode, null);

            frameEncode = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapFrameEncode(pFrameEncode) : null;

            return hr;
        }

        public HResult GetContainerFormat(out Guid containerFormat)
        {
            fixed (Guid* pContainerFormat = &containerFormat)
            {
                return Pointer->GetContainerFormat(pContainerFormat);
            }
        }

        public HResult GetEncoderInfo(out WICBitmapEncoderInfo? encoderInfo)
        {
            IWICBitmapEncoderInfo* pEncoderInfo;
            int hr = Pointer->GetEncoderInfo(&pEncoderInfo);

            encoderInfo = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapEncoderInfo(pEncoderInfo) : null;

            return hr;
        }

        public HResult GetMetadataQueryWriter(out WICMetadataQueryWriter? metadataQueryWriter)
        {
            IWICMetadataQueryWriter* pMetadataQueryWriter;
            int hr = Pointer->GetMetadataQueryWriter(&pMetadataQueryWriter);

            metadataQueryWriter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICMetadataQueryWriter(pMetadataQueryWriter) : null;

            return hr;
        }

        public HResult SetColorContexts(ReadOnlySpan<Pointer<IWICColorContext>> colorContexts)
        {
            fixed (Pointer<IWICColorContext>* pColorContexts = colorContexts)
            {
                return Pointer->SetColorContexts((uint)colorContexts.Length, (IWICColorContext**)pColorContexts);
            }
        }
    }
}