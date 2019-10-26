using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1GeometryGroup" /> COM interface.</summary>
    public unsafe partial class D2D1GeometryGroup : D2D1Geometry
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GeometryGroup" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GeometryGroup(ID2D1GeometryGroup* pointer) : base((ID2D1Geometry*)pointer)
        {
        }

        public new ID2D1GeometryGroup* Pointer => (ID2D1GeometryGroup*)base.Pointer;

        public D2D1_FILL_MODE GetFillMode()
        {
            return Pointer->GetFillMode();
        }

        public void GetSourceGeometries(ID2D1Geometry** geometries, uint geometriesCount)
        {
            Pointer->GetSourceGeometries(geometries, geometriesCount);
        }

        public uint GetSourceGeometryCount()
        {
            return Pointer->GetSourceGeometryCount();
        }

        public static implicit operator ID2D1GeometryGroup*(D2D1GeometryGroup value)
        {
            return value.Pointer;
        }
    }
}