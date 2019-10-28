using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1Geometry
    {
        public HResult CompareWithGeometry(
            ID2D1Geometry* inputGeometry,
            [Optional] D2D_MATRIX_3X2_F* inputGeometryTransform,
            out D2D1_GEOMETRY_RELATION relation)
        {
            fixed (D2D1_GEOMETRY_RELATION* pRelation = &relation)
            {
                return Pointer->CompareWithGeometry(inputGeometry, inputGeometryTransform, pRelation);
            }
        }

        public HResult CompareWithGeometry(
            ID2D1Geometry* inputGeometry,
            [Optional] D2D_MATRIX_3X2_F* inputGeometryTransform,
            float flatteningTolerance,
            out D2D1_GEOMETRY_RELATION relation)
        {
            fixed (D2D1_GEOMETRY_RELATION* pRelation = &relation)
            {
                return Pointer->CompareWithGeometry(inputGeometry, inputGeometryTransform, flatteningTolerance, pRelation);
            }
        }

        public HResult ComputeArea([Optional] D2D_MATRIX_3X2_F* worldTransform, float flatteningTolerance, out float area)
        {
            fixed (float* pArea = &area)
            {
                return Pointer->ComputeArea(worldTransform, flatteningTolerance, pArea);
            }
        }

        public HResult ComputeArea([Optional] D2D_MATRIX_3X2_F* worldTransform, out float area)
        {
            fixed (float* pArea = &area)
            {
                return Pointer->ComputeArea(worldTransform, pArea);
            }
        }

        public HResult ComputeLength([Optional] D2D_MATRIX_3X2_F* worldTransform, out float length)
        {
            fixed (float* pLength = &length)
            {
                return Pointer->ComputeLength(worldTransform, pLength);
            }
        }

        public HResult ComputeLength([Optional] D2D_MATRIX_3X2_F* worldTransform, float flatteningTolerance, out float length)
        {
            fixed (float* pLength = &length)
            {
                return Pointer->ComputeLength(worldTransform, flatteningTolerance, pLength);
            }
        }

        public HResult GetBounds([Optional] D2D_MATRIX_3X2_F* worldTransform, out D2D_RECT_F bounds)
        {
            fixed (D2D_RECT_F* pBounds = &bounds)
            {
                return Pointer->GetBounds(worldTransform, pBounds);
            }
        }

        public HResult GetWidenedBounds(
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            out D2D_RECT_F bounds)
        {
            fixed (D2D_RECT_F* pBounds = &bounds)
            {
                return Pointer->GetWidenedBounds(strokeWidth, strokeStyle, worldTransform, flatteningTolerance, pBounds);
            }
        }

        public HResult GetWidenedBounds(
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            out D2D_RECT_F bounds)
        {
            fixed (D2D_RECT_F* pBounds = &bounds)
            {
                return Pointer->GetWidenedBounds(strokeWidth, strokeStyle, worldTransform, pBounds);
            }
        }
    }
}