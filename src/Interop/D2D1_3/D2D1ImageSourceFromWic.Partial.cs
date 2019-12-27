using BouncyBox.VorpalEngine.Interop.Wincodec;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1ImageSourceFromWic
    {
        public WICBitmapSource? GetSource()
        {
            IWICBitmapSource* pWicBitmapSource;

            Pointer->GetSource(&pWicBitmapSource);

            return pWicBitmapSource != null ? new WICBitmapSource(pWicBitmapSource) : null;
        }
    }
}