using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    /// <summary>Proxies the <see cref="ID2D1SvgPointCollection" /> COM interface.</summary>
    public unsafe partial class D2D1SvgPointCollection : D2D1SvgAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgPointCollection" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgPointCollection(ID2D1SvgPointCollection* pointer) : base((ID2D1SvgAttribute*)pointer)
        {
        }

        public new ID2D1SvgPointCollection* Pointer => (ID2D1SvgPointCollection*)base.Pointer;

        public HResult GetPoints(D2D_POINT_2F* points, uint pointsCount, uint startIndex = 0)
        {
            return Pointer->GetPoints(points, pointsCount, startIndex);
        }

        public uint GetPointsCount()
        {
            return Pointer->GetPointsCount();
        }

        public HResult RemovePointsAtEnd(uint pointsCount)
        {
            return Pointer->RemovePointsAtEnd(pointsCount);
        }

        public HResult UpdatePoints(D2D_POINT_2F* points, uint pointsCount, uint startIndex = 0)
        {
            return Pointer->UpdatePoints(points, pointsCount, startIndex);
        }

        public static implicit operator ID2D1SvgPointCollection*(D2D1SvgPointCollection value)
        {
            return value.Pointer;
        }
    }
}