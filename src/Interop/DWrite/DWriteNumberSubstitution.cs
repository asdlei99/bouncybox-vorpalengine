using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteNumberSubstitution" /> COM interface.</summary>
    public unsafe class DWriteNumberSubstitution : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteNumberSubstitution" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteNumberSubstitution(IDWriteNumberSubstitution* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteNumberSubstitution* Pointer => (IDWriteNumberSubstitution*)base.Pointer;

        public static implicit operator IDWriteNumberSubstitution*(DWriteNumberSubstitution value)
        {
            return value.Pointer;
        }
    }
}