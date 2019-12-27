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
            Pointer<ID2D1Geometry>[]? pGeometriesArray = null;
            Span<Pointer<ID2D1Geometry>> geometriesSpan =
                AllocationHelper.CanStackAlloc<Pointer<ID2D1Geometry>>((uint)geometries.Length)
                    ? stackalloc Pointer<ID2D1Geometry>[geometries.Length]
                    : pGeometriesArray = ArrayPool<Pointer<ID2D1Geometry>>.Shared.Rent(geometries.Length);

            try
            {
                fixed (Pointer<ID2D1Geometry>* ppGeometries = geometriesSpan)
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

                    geometries[i] = new D2D1Geometry(geometriesSpan[i].Ptr);
                }
            }
            finally
            {
                if (pGeometriesArray is object)
                {
                    ArrayPool<Pointer<ID2D1Geometry>>.Shared.Return(pGeometriesArray);
                }
            }
        }
    }
}