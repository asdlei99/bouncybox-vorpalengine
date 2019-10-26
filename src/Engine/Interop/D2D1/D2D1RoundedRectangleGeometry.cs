using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1RoundedRectangleGeometry" /> COM interface.</summary>
    public unsafe partial class D2D1RoundedRectangleGeometry : D2D1Geometry
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1RoundedRectangleGeometry" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1RoundedRectangleGeometry(ID2D1RoundedRectangleGeometry* pointer) : base((ID2D1Geometry*)pointer)
        {
        }

        public new ID2D1RoundedRectangleGeometry* Pointer => (ID2D1RoundedRectangleGeometry*)base.Pointer;

        public void GetRoundedRect(D2D1_ROUNDED_RECT* roundedRect)
        {
            Pointer->GetRoundedRect(roundedRect);
        }

        public static implicit operator ID2D1RoundedRectangleGeometry*(D2D1RoundedRectangleGeometry value)
        {
            return value.Pointer;
        }
    }
}