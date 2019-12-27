using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICFastMetadataEncoder
    {
        public HResult GetMetadataQueryWriter(out WICMetadataQueryWriter? metadataQueryWriter)
        {
            IWICMetadataQueryWriter* pMetadataQueryWriter;
            int hr = Pointer->GetMetadataQueryWriter(&pMetadataQueryWriter);

            metadataQueryWriter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICMetadataQueryWriter(pMetadataQueryWriter) : null;

            return hr;
        }
    }
}