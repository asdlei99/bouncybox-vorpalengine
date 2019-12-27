using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1DeviceContext6" /> COM interface.</summary>
    public unsafe class D2D1DeviceContext6 : D2D1DeviceContext5
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DeviceContext6" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DeviceContext6(ID2D1DeviceContext6* pointer) : base((ID2D1DeviceContext5*)pointer)
        {
        }

        public new ID2D1DeviceContext6* Pointer => (ID2D1DeviceContext6*)base.Pointer;

        public void BlendImage(
            ID2D1Image* image,
            D2D1_BLEND_MODE blendMode,
            D2D_POINT_2F* targetOffset = null,
            D2D_RECT_F* imageRectangle = null,
            D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_LINEAR)
        {
            Pointer->BlendImage(image, blendMode, targetOffset, imageRectangle, interpolationMode);
        }

        public static implicit operator ID2D1DeviceContext6*(D2D1DeviceContext6 value)
        {
            return value.Pointer;
        }
    }
}