using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1RadialGradientBrush
    {
        public void GetGradientStopCollection(out D2D1GradientStopCollection gradientStopCollection)
        {
            ID2D1GradientStopCollection* pGradientStopCollection;

            Pointer->GetGradientStopCollection(&pGradientStopCollection);

            gradientStopCollection = new D2D1GradientStopCollection(pGradientStopCollection);
        }
    }
}