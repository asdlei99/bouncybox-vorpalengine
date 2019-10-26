using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1SimplifiedGeometrySink
    {
        public void AddBeziers(ReadOnlySpan<D2D1_BEZIER_SEGMENT> beziers)
        {
            fixed (D2D1_BEZIER_SEGMENT* pBeziers = beziers)
            {
                Pointer->AddBeziers(pBeziers, (uint)beziers.Length);
            }
        }

        public void AddLines(ReadOnlySpan<D2D_POINT_2F> points)
        {
            fixed (D2D_POINT_2F* pPoints = points)
            {
                Pointer->AddLines(pPoints, (uint)points.Length);
            }
        }
    }
}