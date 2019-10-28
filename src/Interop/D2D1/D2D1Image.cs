using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Image" /> COM interface.</summary>
    public unsafe class D2D1Image : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Image" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Image(ID2D1Image* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1Image* Pointer => (ID2D1Image*)base.Pointer;

        public static implicit operator ID2D1Image*(D2D1Image value)
        {
            return value.Pointer;
        }
    }
}