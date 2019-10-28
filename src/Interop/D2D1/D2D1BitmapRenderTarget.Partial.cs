using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1BitmapRenderTarget
    {
        public HResult GetBitmap(out D2D1Bitmap? bitmap)
        {
            ID2D1Bitmap* pBitmap;
            int hr = Pointer->GetBitmap(&pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap(pBitmap) : null;

            return hr;
        }
    }
}