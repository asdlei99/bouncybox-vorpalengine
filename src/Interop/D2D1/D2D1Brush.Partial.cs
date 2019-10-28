using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1Brush
    {
        public D2D_MATRIX_3X2_F GetTransform()
        {
            D2D_MATRIX_3X2_F transform;

            Pointer->GetTransform(&transform);

            return transform;
        }
    }
}