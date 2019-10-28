using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    public unsafe partial class D2D1GradientStopCollection1
    {
        public void GetGradientStops1(Span<D2D1_GRADIENT_STOP> gradientStops)
        {
            fixed (D2D1_GRADIENT_STOP* pGradientStops = gradientStops)
            {
                Pointer->GetGradientStops1(pGradientStops, (uint)gradientStops.Length);
            }
        }
    }
}