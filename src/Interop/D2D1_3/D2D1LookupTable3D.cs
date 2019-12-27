using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1LookupTable3D" /> COM interface.</summary>
    public unsafe class D2D1LookupTable3D : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1LookupTable3D" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1LookupTable3D(ID2D1LookupTable3D* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1LookupTable3D* Pointer => (ID2D1LookupTable3D*)base.Pointer;

        public static implicit operator ID2D1LookupTable3D*(D2D1LookupTable3D value)
        {
            return value.Pointer;
        }
    }
}