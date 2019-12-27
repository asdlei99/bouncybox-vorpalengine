using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Geometry" /> COM interface.</summary>
    public unsafe partial class D2D1Geometry : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Geometry" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Geometry(ID2D1Geometry* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1Geometry* Pointer => (ID2D1Geometry*)base.Pointer;

        public HResult CombineWithGeometry(
            ID2D1Geometry* inputGeometry,
            D2D1_COMBINE_MODE combineMode,
            [Optional] D2D_MATRIX_3X2_F* inputGeometryTransform,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->CombineWithGeometry(inputGeometry, combineMode, inputGeometryTransform, geometrySink);
        }

        public HResult CombineWithGeometry(
            ID2D1Geometry* inputGeometry,
            D2D1_COMBINE_MODE combineMode,
            [Optional] D2D_MATRIX_3X2_F* inputGeometryTransform,
            float flatteningTolerance,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->CombineWithGeometry(inputGeometry, combineMode, inputGeometryTransform, flatteningTolerance, geometrySink);
        }

        public HResult CompareWithGeometry(ID2D1Geometry* inputGeometry, [Optional] D2D_MATRIX_3X2_F* inputGeometryTransform, D2D1_GEOMETRY_RELATION* relation)
        {
            return Pointer->CompareWithGeometry(inputGeometry, inputGeometryTransform, relation);
        }

        public HResult CompareWithGeometry(
            ID2D1Geometry* inputGeometry,
            [Optional] D2D_MATRIX_3X2_F* inputGeometryTransform,
            float flatteningTolerance,
            D2D1_GEOMETRY_RELATION* relation)
        {
            return Pointer->CompareWithGeometry(inputGeometry, inputGeometryTransform, flatteningTolerance, relation);
        }

        public HResult ComputeArea([Optional] D2D_MATRIX_3X2_F* worldTransform, float flatteningTolerance, float* area)
        {
            return Pointer->ComputeArea(worldTransform, flatteningTolerance, area);
        }

        public HResult ComputeArea([Optional] D2D_MATRIX_3X2_F* worldTransform, float* area)
        {
            return Pointer->ComputeArea(worldTransform, area);
        }

        public HResult ComputeLength([Optional] D2D_MATRIX_3X2_F* worldTransform, float* length)
        {
            return Pointer->ComputeLength(worldTransform, length);
        }

        public HResult ComputeLength([Optional] D2D_MATRIX_3X2_F* worldTransform, float flatteningTolerance, float* length)
        {
            return Pointer->ComputeLength(worldTransform, flatteningTolerance, length);
        }

        public HResult ComputePointAtLength(
            float length,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            D2D_POINT_2F* point = null,
            D2D_POINT_2F* unitTangentVector = null)
        {
            return Pointer->ComputePointAtLength(length, worldTransform, flatteningTolerance, point, unitTangentVector);
        }

        public HResult ComputePointAtLength(
            float length,
            D2D_MATRIX_3X2_F* worldTransform,
            D2D_POINT_2F* point = null,
            D2D_POINT_2F* unitTangentVector = null)
        {
            return Pointer->ComputePointAtLength(length, worldTransform, point, unitTangentVector);
        }

        public HResult FillContainsPoint(D2D_POINT_2F point, [Optional] D2D_MATRIX_3X2_F* worldTransform, float flatteningTolerance, out bool contains)
        {
            int iContains;
            int hr = Pointer->FillContainsPoint(point, worldTransform, flatteningTolerance, &iContains);

            contains = iContains == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult FillContainsPoint(D2D_POINT_2F point, [Optional] D2D_MATRIX_3X2_F* worldTransform, out bool contains)
        {
            int iContains;
            int hr = Pointer->FillContainsPoint(point, worldTransform, &iContains);

            contains = iContains == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetBounds([Optional] D2D_MATRIX_3X2_F* worldTransform, D2D_RECT_F* bounds)
        {
            return Pointer->GetBounds(worldTransform, bounds);
        }

        public HResult GetWidenedBounds(
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            D2D_RECT_F* bounds)
        {
            return Pointer->GetWidenedBounds(strokeWidth, strokeStyle, worldTransform, flatteningTolerance, bounds);
        }

        public HResult GetWidenedBounds(
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            D2D_RECT_F* bounds)
        {
            return Pointer->GetWidenedBounds(strokeWidth, strokeStyle, worldTransform, bounds);
        }

        public HResult Outline([Optional] D2D_MATRIX_3X2_F* worldTransform, float flatteningTolerance, ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->Outline(worldTransform, flatteningTolerance, geometrySink);
        }

        public HResult Outline([Optional] D2D_MATRIX_3X2_F* worldTransform, ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->Outline(worldTransform, geometrySink);
        }

        public HResult Simplify(
            D2D1_GEOMETRY_SIMPLIFICATION_OPTION simplificationOption,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->Simplify(simplificationOption, worldTransform, flatteningTolerance, geometrySink);
        }

        public HResult Simplify(
            D2D1_GEOMETRY_SIMPLIFICATION_OPTION simplificationOption,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->Simplify(simplificationOption, worldTransform, geometrySink);
        }

        public HResult StrokeContainsPoint(
            D2D_POINT_2F point,
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            out bool contains)
        {
            int iContains;
            int hr = Pointer->StrokeContainsPoint(point, strokeWidth, strokeStyle, worldTransform, &iContains);

            contains = iContains == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult StrokeContainsPoint(
            D2D_POINT_2F point,
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            out bool contains)
        {
            int iContains;
            int hr = Pointer->StrokeContainsPoint(point, strokeWidth, strokeStyle, worldTransform, flatteningTolerance, &iContains);

            contains = iContains == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult Tessellate([Optional] D2D_MATRIX_3X2_F* worldTransform, float flatteningTolerance, ID2D1TessellationSink* tessellationSink)
        {
            return Pointer->Tessellate(worldTransform, flatteningTolerance, tessellationSink);
        }

        public HResult Tessellate([Optional] D2D_MATRIX_3X2_F* worldTransform, ID2D1TessellationSink* tessellationSink)
        {
            return Pointer->Tessellate(worldTransform, tessellationSink);
        }

        public HResult Widen(
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->Widen(strokeWidth, strokeStyle, worldTransform, geometrySink);
        }

        public HResult Widen(
            float strokeWidth,
            [Optional] ID2D1StrokeStyle* strokeStyle,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            ID2D1SimplifiedGeometrySink* geometrySink)
        {
            return Pointer->Widen(strokeWidth, strokeStyle, worldTransform, flatteningTolerance, geometrySink);
        }

        public static implicit operator ID2D1Geometry*(D2D1Geometry value)
        {
            return value.Pointer;
        }
    }
}