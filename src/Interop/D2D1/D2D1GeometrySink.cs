using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1GeometrySink" /> COM interface.</summary>
    public unsafe partial class D2D1GeometrySink : D2D1SimplifiedGeometrySink
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GeometrySink" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GeometrySink(ID2D1GeometrySink* pointer) : base((ID2D1SimplifiedGeometrySink*)pointer)
        {
        }

        public new ID2D1GeometrySink* Pointer => (ID2D1GeometrySink*)base.Pointer;

        public void AddArc(D2D1_ARC_SEGMENT* arc)
        {
            Pointer->AddArc(arc);
        }

        public void AddBezier(D2D1_BEZIER_SEGMENT* bezier)
        {
            Pointer->AddBezier(bezier);
        }

        public void AddLine(D2D_POINT_2F point)
        {
            Pointer->AddLine(point);
        }

        public void AddQuadraticBezier(D2D1_QUADRATIC_BEZIER_SEGMENT* bezier)
        {
            Pointer->AddQuadraticBezier(bezier);
        }

        public void AddQuadraticBeziers(D2D1_QUADRATIC_BEZIER_SEGMENT* beziers, uint beziersCount)
        {
            Pointer->AddQuadraticBeziers(beziers, beziersCount);
        }

        public static implicit operator ID2D1GeometrySink*(D2D1GeometrySink value)
        {
            return value.Pointer;
        }
    }
}