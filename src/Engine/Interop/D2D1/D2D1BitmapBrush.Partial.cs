using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1BitmapBrush
    {
        public D2D1Bitmap GetBitmap()
        {
            ID2D1Bitmap* bitmap;

            Pointer->GetBitmap(&bitmap);

            return new D2D1Bitmap(bitmap);
        }
    }
}