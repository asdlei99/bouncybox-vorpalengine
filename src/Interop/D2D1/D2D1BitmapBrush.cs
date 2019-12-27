using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1BitmapBrush" /> COM interface.</summary>
    public unsafe partial class D2D1BitmapBrush : D2D1Brush
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1BitmapBrush" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1BitmapBrush(ID2D1BitmapBrush* pointer) : base((ID2D1Brush*)pointer)
        {
        }

        public new ID2D1BitmapBrush* Pointer => (ID2D1BitmapBrush*)base.Pointer;

        public void GetBitmap(ID2D1Bitmap** bitmap)
        {
            Pointer->GetBitmap(bitmap);
        }

        public D2D1_EXTEND_MODE GetExtendModeX()
        {
            return Pointer->GetExtendModeX();
        }

        public D2D1_EXTEND_MODE GetExtendModeY()
        {
            return Pointer->GetExtendModeY();
        }

        public D2D1_BITMAP_INTERPOLATION_MODE GetInterpolationMode()
        {
            return Pointer->GetInterpolationMode();
        }

        public void SetBitmap(ID2D1Bitmap* bitmap = null)
        {
            Pointer->SetBitmap(bitmap);
        }

        public void SetExtendModeX(D2D1_EXTEND_MODE extendModeX)
        {
            Pointer->SetExtendModeX(extendModeX);
        }

        public void SetExtendModeY(D2D1_EXTEND_MODE extendModeY)
        {
            Pointer->SetExtendModeY(extendModeY);
        }

        public void SetInterpolationMode(D2D1_BITMAP_INTERPOLATION_MODE interpolationMode)
        {
            Pointer->SetInterpolationMode(interpolationMode);
        }

        public static implicit operator ID2D1BitmapBrush*(D2D1BitmapBrush value)
        {
            return value.Pointer;
        }
    }
}