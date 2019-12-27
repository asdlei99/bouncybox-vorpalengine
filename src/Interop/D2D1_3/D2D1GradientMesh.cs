using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1GradientMesh" /> COM interface.</summary>
    public unsafe partial class D2D1GradientMesh : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GradientMesh" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GradientMesh(ID2D1GradientMesh* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1GradientMesh* Pointer => (ID2D1GradientMesh*)base.Pointer;

        public uint GetPatchCount()
        {
            return Pointer->GetPatchCount();
        }

        public HResult GetPatches(uint startIndex, D2D1_GRADIENT_MESH_PATCH* patches, uint patchesCount)
        {
            return Pointer->GetPatches(startIndex, patches, patchesCount);
        }

        public static implicit operator ID2D1GradientMesh*(D2D1GradientMesh value)
        {
            return value.Pointer;
        }
    }
}