using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_2
{
    /// <summary>Proxies the <see cref="ID2D1GeometryRealization" /> COM interface.</summary>
    public unsafe class D2D1GeometryRealization : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GeometryRealization" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GeometryRealization(ID2D1GeometryRealization* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1GeometryRealization* Pointer => (ID2D1GeometryRealization*)base.Pointer;

        public static implicit operator ID2D1GeometryRealization*(D2D1GeometryRealization value)
        {
            return value.Pointer;
        }
    }
}