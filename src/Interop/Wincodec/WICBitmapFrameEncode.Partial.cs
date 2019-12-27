using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapFrameEncode
    {
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

        public HResult WritePixels(uint lineCount, uint stride, ReadOnlySpan<byte> pixels)
        {
            fixed (byte* pPixels = pixels)
            {
                return Pointer->WritePixels(lineCount, stride, (uint)pixels.Length, pPixels);
            }
        }
    }
}