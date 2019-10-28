using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1EllipseGeometry" /> COM interface.</summary>
    public unsafe partial class D2D1EllipseGeometry : D2D1Geometry
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1EllipseGeometry" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1EllipseGeometry(ID2D1EllipseGeometry* pointer) : base((ID2D1Geometry*)pointer)
        {
        }

        public new ID2D1EllipseGeometry* Pointer => (ID2D1EllipseGeometry*)base.Pointer;

        public void GetEllipse(D2D1_ELLIPSE* ellipse)
        {
            Pointer->GetEllipse(ellipse);
        }

        public static implicit operator ID2D1EllipseGeometry*(D2D1EllipseGeometry value)
        {
            return value.Pointer;
        }
    }
}