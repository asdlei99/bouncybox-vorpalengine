using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe struct ID2D1GeometryPointer
    {
        public ID2D1Geometry* Pointer;

        public static implicit operator ID2D1GeometryPointer(ID2D1Geometry* value)
        {
            return
                new ID2D1GeometryPointer
                {
                    Pointer = value
                };
        }
    }
}