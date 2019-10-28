using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1TessellationSink
    {
        public void AddTriangles(ReadOnlySpan<D2D1_TRIANGLE> triangles)
        {
            fixed (D2D1_TRIANGLE* pTriangles = triangles)
            {
                Pointer->AddTriangles(pTriangles, (uint)triangles.Length);
            }
        }
    }
}