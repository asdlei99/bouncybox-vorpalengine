using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1LinearGradientBrush
    {
        public D2D1GradientStopCollection GetGradientStopCollection()
        {
            ID2D1GradientStopCollection* pGradientStopCollection;

            Pointer->GetGradientStopCollection(&pGradientStopCollection);

            return new D2D1GradientStopCollection(pGradientStopCollection);
        }
    }
}