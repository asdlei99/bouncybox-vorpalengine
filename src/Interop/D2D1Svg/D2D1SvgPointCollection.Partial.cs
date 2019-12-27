using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    public unsafe partial class D2D1SvgPointCollection
    {
        public HResult GetPoints(Span<D2D_POINT_2F> points, uint startIndex = 0)
        {
            fixed (D2D_POINT_2F* pPoints = points)
            {
                return Pointer->GetPoints(pPoints, (uint)points.Length, startIndex);
            }
        }

        public HResult UpdatePoints(ReadOnlySpan<D2D_POINT_2F> points, uint startIndex = 0)
        {
            fixed (D2D_POINT_2F* pPoints = points)
            {
                return Pointer->UpdatePoints(pPoints, (uint)points.Length, startIndex);
            }
        }
    }
}