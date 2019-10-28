using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1EllipseGeometry
    {
        public D2D1_ELLIPSE GetEllipse()
        {
            D2D1_ELLIPSE ellipse;

            Pointer->GetEllipse(&ellipse);

            return ellipse;
        }
    }
}