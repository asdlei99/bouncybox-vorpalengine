using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapFrameDecode
    {
        public HResult GetColorContexts(uint count, out WICColorContext? colorContexts, out uint actualCount)
        {
            IWICColorContext* pIColorContexts;
            int hr;

            fixed (uint* pActualCount = &actualCount)
            {
                hr = Pointer->GetColorContexts(count, &pIColorContexts, pActualCount);
            }

            colorContexts = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICColorContext(pIColorContexts) : null;

            return hr;
        }
    }
}