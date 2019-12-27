using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1Ink" /> COM interface.</summary>
    public unsafe partial class D2D1Ink : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Ink" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Ink(ID2D1Ink* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1Ink* Pointer => (ID2D1Ink*)base.Pointer;

        public HResult AddSegments(D2D1_INK_BEZIER_SEGMENT* segments, uint segmentsCount)
        {
            return Pointer->AddSegments(segments, segmentsCount);
        }

        public HResult GetBounds([Optional] ID2D1InkStyle* inkStyle, [Optional] D2D_MATRIX_3X2_F* worldTransform, D2D_RECT_F* bounds)
        {
            return Pointer->GetBounds(inkStyle, worldTransform, bounds);
        }

        public uint GetSegmentCount()
        {
            return Pointer->GetSegmentCount();
        }

        public HResult GetSegments(uint startSegment, D2D1_INK_BEZIER_SEGMENT* segments, uint segmentsCount)
        {
            return Pointer->GetSegments(startSegment, segments, segmentsCount);
        }

        public D2D1_INK_POINT GetStartPoint()
        {
            return Pointer->GetStartPoint();
        }

        public HResult RemoveSegmentsAtEnd(uint segmentsCount)
        {
            return Pointer->RemoveSegmentsAtEnd(segmentsCount);
        }

        public HResult SetSegmentAtEnd(D2D1_INK_BEZIER_SEGMENT* segment)
        {
            return Pointer->SetSegmentAtEnd(segment);
        }

        public HResult SetSegments(uint startSegment, D2D1_INK_BEZIER_SEGMENT* segments, uint segmentsCount)
        {
            return Pointer->SetSegments(startSegment, segments, segmentsCount);
        }

        public void SetStartPoint(D2D1_INK_POINT* startPoint)
        {
            Pointer->SetStartPoint(startPoint);
        }

        public HResult StreamAsGeometry(
            [Optional] ID2D1InkStyle* inkStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->StreamAsGeometry(inkStyle, worldTransform, geometrySink);
        }

        public HResult StreamAsGeometry(
            [Optional] ID2D1InkStyle* inkStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->StreamAsGeometry(inkStyle, worldTransform, flatteningTolerance, geometrySink);
        }

        public static implicit operator ID2D1Ink*(D2D1Ink value)
        {
            return value.Pointer;
        }
    }
}