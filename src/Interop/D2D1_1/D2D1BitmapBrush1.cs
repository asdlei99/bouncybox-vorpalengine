using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1BitmapBrush1" /> COM interface.</summary>
    public unsafe class D2D1BitmapBrush1 : D2D1BitmapBrush
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1BitmapBrush1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1BitmapBrush1(ID2D1BitmapBrush1* pointer) : base((ID2D1BitmapBrush*)pointer)
        {
        }

        public new ID2D1BitmapBrush1* Pointer => (ID2D1BitmapBrush1*)base.Pointer;

        public D2D1_INTERPOLATION_MODE GetInterpolationMode1()
        {
            return Pointer->GetInterpolationMode1();
        }

        public void SetInterpolationMode1(D2D1_INTERPOLATION_MODE interpolationMode)
        {
            Pointer->SetInterpolationMode1(interpolationMode);
        }

        public static implicit operator ID2D1BitmapBrush1*(D2D1BitmapBrush1 value)
        {
            return value.Pointer;
        }
    }
}