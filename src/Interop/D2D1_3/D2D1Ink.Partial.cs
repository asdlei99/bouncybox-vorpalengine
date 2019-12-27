using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1Ink
    {
        public HResult AddSegments(ReadOnlySpan<D2D1_INK_BEZIER_SEGMENT> segments)
        {
            fixed (D2D1_INK_BEZIER_SEGMENT* pSegments = segments)
            {
                return Pointer->AddSegments(pSegments, (uint)segments.Length);
            }
        }

        public HResult GetBounds([Optional] ID2D1InkStyle* inkStyle, [Optional] D2D_MATRIX_3X2_F* worldTransform, out D2D_RECT_F bounds)
        {
            fixed (D2D_RECT_F* pBounds = &bounds)
            {
                return Pointer->GetBounds(inkStyle, worldTransform, pBounds);
            }
        }

        public HResult GetSegments(uint startSegment, Span<D2D1_INK_BEZIER_SEGMENT> segments)
        {
            fixed (D2D1_INK_BEZIER_SEGMENT* pSegments = segments)
            {
                return Pointer->GetSegments(startSegment, pSegments, (uint)segments.Length);
            }
        }

        public HResult SetSegments(uint startSegment, ReadOnlySpan<D2D1_INK_BEZIER_SEGMENT> segments)
        {
            fixed (D2D1_INK_BEZIER_SEGMENT* pSegments = segments)
            {
                return Pointer->SetSegments(startSegment, pSegments, (uint)segments.Length);
            }
        }
    }
}