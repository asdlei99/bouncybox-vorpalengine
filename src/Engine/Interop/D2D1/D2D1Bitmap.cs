using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Bitmap" /> COM interface.</summary>
    public unsafe partial class D2D1Bitmap : D2D1Image
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Bitmap" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Bitmap(ID2D1Bitmap* pointer) : base((ID2D1Image*)pointer)
        {
        }

        public new ID2D1Bitmap* Pointer => (ID2D1Bitmap*)base.Pointer;

        public HResult CopyFromBitmap([Optional] D2D_POINT_2U* destPoint, ID2D1Bitmap* bitmap, [Optional] D2D_RECT_U* srcRect)
        {
            return Pointer->CopyFromBitmap(destPoint, bitmap, srcRect);
        }

        public HResult CopyFromMemory([Optional] D2D_RECT_U* dstRect, void* srcData, uint pitch)
        {
            return Pointer->CopyFromMemory(dstRect, srcData, pitch);
        }

        public HResult CopyFromRenderTarget([Optional] D2D_POINT_2U* destPoint, ID2D1RenderTarget* renderTarget, [Optional] D2D_RECT_U* srcRect)
        {
            return Pointer->CopyFromRenderTarget(destPoint, renderTarget, srcRect);
        }

        public void GetDpi(float* dpiX, float* dpiY)
        {
            Pointer->GetDpi(dpiX, dpiY);
        }

        public D2D1_PIXEL_FORMAT GetPixelFormat()
        {
            return Pointer->GetPixelFormat();
        }

        public D2D_SIZE_U GetPixelSize()
        {
            return Pointer->GetPixelSize();
        }

        public D2D_SIZE_F GetSize()
        {
            return Pointer->GetSize();
        }

        public static implicit operator ID2D1Bitmap*(D2D1Bitmap value)
        {
            return value.Pointer;
        }
    }
}