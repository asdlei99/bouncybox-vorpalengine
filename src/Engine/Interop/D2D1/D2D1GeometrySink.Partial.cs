using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1GeometrySink
    {
        public void AddQuadraticBeziers(ReadOnlySpan<D2D1_QUADRATIC_BEZIER_SEGMENT> beziers)
        {
            fixed (D2D1_QUADRATIC_BEZIER_SEGMENT* pBeziers = beziers)
            {
                Pointer->AddQuadraticBeziers(pBeziers, (uint)beziers.Length);
            }
        }
    }
}