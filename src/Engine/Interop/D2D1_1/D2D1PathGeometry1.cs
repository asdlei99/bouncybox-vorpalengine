using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Engine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1PathGeometry1" /> COM interface.</summary>
    public unsafe class D2D1PathGeometry1 : D2D1PathGeometry
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1PathGeometry1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1PathGeometry1(ID2D1PathGeometry1* pointer) : base((ID2D1PathGeometry*)pointer)
        {
        }

        public new ID2D1PathGeometry1* Pointer => (ID2D1PathGeometry1*)base.Pointer;

        public HResult ComputePointAndSegmentAtLength(
            float length,
            uint startSegment,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float flatteningTolerance,
            D2D1_POINT_DESCRIPTION* pointDescription)
        {
            return Pointer->ComputePointAndSegmentAtLength(length, startSegment, worldTransform, flatteningTolerance, pointDescription);
        }

        public HResult ComputePointAndSegmentAtLength(
            float length,
            uint startSegment,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            D2D1_POINT_DESCRIPTION* pointDescription)
        {
            return Pointer->ComputePointAndSegmentAtLength(length, startSegment, worldTransform, pointDescription);
        }

        public static implicit operator ID2D1PathGeometry1*(D2D1PathGeometry1 value)
        {
            return value.Pointer;
        }
    }
}