#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1Bitmap
    {
        public (float dpiX, float dpiY) GetDpi()
        {
            float dpiX;
            float dpiY;

            GetDpi(&dpiX, &dpiY);

            return (dpiX, dpiY);
        }
    }
}