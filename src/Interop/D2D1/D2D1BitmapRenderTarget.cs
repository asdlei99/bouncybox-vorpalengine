using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1BitmapRenderTarget" /> COM interface.</summary>
    public unsafe partial class D2D1BitmapRenderTarget : D2D1RenderTarget
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1BitmapRenderTarget" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1BitmapRenderTarget(ID2D1BitmapRenderTarget* pointer) : base((ID2D1RenderTarget*)pointer)
        {
        }

        public new ID2D1BitmapRenderTarget* Pointer => (ID2D1BitmapRenderTarget*)base.Pointer;

        public HResult GetBitmap(ID2D1Bitmap** bitmap)
        {
            return Pointer->GetBitmap(bitmap);
        }

        public static implicit operator ID2D1BitmapRenderTarget*(D2D1BitmapRenderTarget value)
        {
            return value.Pointer;
        }
    }
}