using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1GradientMesh
    {
        public HResult GetPatches(uint startIndex, Span<D2D1_GRADIENT_MESH_PATCH> patches)
        {
            fixed (D2D1_GRADIENT_MESH_PATCH* pPatches = patches)
            {
                return Pointer->GetPatches(startIndex, pPatches, (uint)patches.Length);
            }
        }
    }
}