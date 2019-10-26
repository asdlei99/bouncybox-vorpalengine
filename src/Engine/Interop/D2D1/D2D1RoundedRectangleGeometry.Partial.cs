using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1RoundedRectangleGeometry
    {
        public D2D1_ROUNDED_RECT GetRoundedRect()
        {
            D2D1_ROUNDED_RECT roundedRect;

            Pointer->GetRoundedRect(&roundedRect);

            return roundedRect;
        }
    }
}