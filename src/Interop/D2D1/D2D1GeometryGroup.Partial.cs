using System;
using System.Buffers;
using BouncyBox.VorpalEngine.Common;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1GeometryGroup
    {
        public void GetSourceGeometries(ref Span<D2D1Geometry> geometries)
        {
            ID2D1GeometryPointer[]? pGeometriesArray = null;
            Span<ID2D1GeometryPointer> geometriesSpan =
                AllocationHelper.CanStackAlloc<ID2D1GeometryPointer>((uint)geometries.Length)
                    ? stackalloc ID2D1GeometryPointer[geometries.Length]
                    : pGeometriesArray = ArrayPool<ID2D1GeometryPointer>.Shared.Rent(geometries.Length);

            try
            {
                fixed (ID2D1GeometryPointer* ppGeometries = geometriesSpan)
                {
                    Pointer->GetSourceGeometries((ID2D1Geometry**)ppGeometries, (uint)geometries.Length);
                }

                for (var i = 0; i < geometries.Length; i++)
                {
                    if (geometries[i] is null)
                    {
                        geometries = geometries.Slice(0, i);
                        break;
                    }

                    geometries[i] = new D2D1Geometry(geometriesSpan[i].Pointer);
                }
            }
            finally
            {
                if (pGeometriesArray is object)
                {
                    ArrayPool<ID2D1GeometryPointer>.Shared.Return(pGeometriesArray);
                }
            }
        }
    }
}