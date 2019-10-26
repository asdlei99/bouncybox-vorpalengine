using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1TransformedGeometry
    {
        public D2D1Geometry GetSourceGeometry()
        {
            ID2D1Geometry* sourceGeometry;

            Pointer->GetSourceGeometry(&sourceGeometry);

            return new D2D1Geometry(sourceGeometry);
        }

        public D2D_MATRIX_3X2_F GetTransform()
        {
            D2D_MATRIX_3X2_F transform;

            Pointer->GetTransform(&transform);

            return transform;
        }
    }
}