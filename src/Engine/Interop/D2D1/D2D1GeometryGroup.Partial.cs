using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1GeometryGroup
    {
        public void GetSourceGeometries(ref Span<D2D1Geometry> geometries)
        {
            var geometriesArray = new ID2D1Geometry*[geometries.Length];

            fixed (ID2D1Geometry** pGeometriesArray = geometriesArray)
            {
                Pointer->GetSourceGeometries(pGeometriesArray, (uint)geometries.Length);
            }

            for (var i = 0; i < geometries.Length; i++)
            {
                if (geometriesArray[i] is null)
                {
                    geometries = geometries.Slice(0, i);
                    break;
                }

                geometries[i] = new D2D1Geometry(geometriesArray[i]);
            }
        }
    }
}