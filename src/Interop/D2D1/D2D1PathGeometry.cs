using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1PathGeometry" /> COM interface.</summary>
    public unsafe partial class D2D1PathGeometry : D2D1Geometry
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1PathGeometry" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1PathGeometry(ID2D1PathGeometry* pointer) : base((ID2D1Geometry*)pointer)
        {
        }

        public new ID2D1PathGeometry* Pointer => (ID2D1PathGeometry*)base.Pointer;

        public HResult GetFigureCount(uint* count)
        {
            return Pointer->GetFigureCount(count);
        }

        public HResult GetSegmentCount(uint* count)
        {
            return Pointer->GetSegmentCount(count);
        }

        public HResult Open(ID2D1GeometrySink** geometrySink)
        {
            return Pointer->Open(geometrySink);
        }

        public HResult Stream(ID2D1GeometrySink* geometrySink)
        {
            return Pointer->Stream(geometrySink);
        }

        public static implicit operator ID2D1PathGeometry*(D2D1PathGeometry value)
        {
            return value.Pointer;
        }
    }
}