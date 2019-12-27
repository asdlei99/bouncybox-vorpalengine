using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1InkStyle
    {
        public D2D_MATRIX_3X2_F GetNibTransform()
        {
            D2D_MATRIX_3X2_F transform;

            Pointer->GetNibTransform(&transform);

            return transform;
        }
    }
}