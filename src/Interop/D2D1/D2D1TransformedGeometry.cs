using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1TransformedGeometry" /> COM interface.</summary>
    public unsafe partial class D2D1TransformedGeometry : D2D1Geometry
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1TransformedGeometry" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1TransformedGeometry(ID2D1TransformedGeometry* pointer) : base((ID2D1Geometry*)pointer)
        {
        }

        public new ID2D1TransformedGeometry* Pointer => (ID2D1TransformedGeometry*)base.Pointer;

        public void GetSourceGeometry(ID2D1Geometry** sourceGeometry)
        {
            Pointer->GetSourceGeometry(sourceGeometry);
        }

        public void GetTransform(D2D_MATRIX_3X2_F* transform)
        {
            Pointer->GetTransform(transform);
        }

        public static implicit operator ID2D1TransformedGeometry*(D2D1TransformedGeometry value)
        {
            return value.Pointer;
        }
    }
}