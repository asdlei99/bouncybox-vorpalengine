using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICEnumMetadataItem
    {
        public HResult Clone(out WICEnumMetadataItem? enumMetadataItem)
        {
            IWICEnumMetadataItem* pEnumMetadataItem;
            int hr = Pointer->Clone(&pEnumMetadataItem);

            enumMetadataItem = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICEnumMetadataItem(pEnumMetadataItem) : null;

            return hr;
        }
    }
}