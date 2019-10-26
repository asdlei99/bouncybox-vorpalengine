using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Layer" /> COM interface.</summary>
    public unsafe class D2D1Layer : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Layer" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Layer(ID2D1Layer* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1Layer* Pointer => (ID2D1Layer*)base.Pointer;

        public D2D_SIZE_F GetSize()
        {
            return Pointer->GetSize();
        }

        public static implicit operator ID2D1Layer*(D2D1Layer value)
        {
            return value.Pointer;
        }
    }
}