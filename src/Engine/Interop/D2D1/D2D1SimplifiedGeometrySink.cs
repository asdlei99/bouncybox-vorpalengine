using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1SimplifiedGeometrySink" /> COM interface.</summary>
    public unsafe partial class D2D1SimplifiedGeometrySink : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SimplifiedGeometrySink" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SimplifiedGeometrySink(ID2D1SimplifiedGeometrySink* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1SimplifiedGeometrySink* Pointer => (ID2D1SimplifiedGeometrySink*)base.Pointer;

        public void AddBeziers(D2D1_BEZIER_SEGMENT* beziers, uint beziersCount)
        {
            Pointer->AddBeziers(beziers, beziersCount);
        }

        public void AddLines(D2D_POINT_2F* points, uint pointsCount)
        {
            Pointer->AddLines(points, pointsCount);
        }

        public void BeginFigure(D2D_POINT_2F startPoint, D2D1_FIGURE_BEGIN figureBegin)
        {
            Pointer->BeginFigure(startPoint, figureBegin);
        }

        public HResult Close()
        {
            return Pointer->Close();
        }

        public void EndFigure(D2D1_FIGURE_END figureEnd)
        {
            Pointer->EndFigure(figureEnd);
        }

        public void SetFillMode(D2D1_FILL_MODE fillMode)
        {
            Pointer->SetFillMode(fillMode);
        }

        public void SetSegmentFlags(D2D1_PATH_SEGMENT vertexFlags)
        {
            Pointer->SetSegmentFlags(vertexFlags);
        }

        public static implicit operator ID2D1SimplifiedGeometrySink*(D2D1SimplifiedGeometrySink value)
        {
            return value.Pointer;
        }
    }
}