using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1RectangleGeometry" /> COM interface.</summary>
    public unsafe partial class D2D1RectangleGeometry : D2D1Geometry
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1RectangleGeometry" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1RectangleGeometry(ID2D1RectangleGeometry* pointer) : base((ID2D1Geometry*)pointer)
        {
        }

        public new ID2D1RectangleGeometry* Pointer => (ID2D1RectangleGeometry*)base.Pointer;

        public void GetRect(D2D_RECT_F* rect)
        {
            Pointer->GetRect(rect);
        }

        public static implicit operator ID2D1RectangleGeometry*(D2D1RectangleGeometry value)
        {
            return value.Pointer;
        }
    }
}