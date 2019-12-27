using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    /// <summary>Proxies the <see cref="IEnumString" /> COM interface.</summary>
    public unsafe partial class EnumString : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="EnumString" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public EnumString(IEnumString* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IEnumString* Pointer => (IEnumString*)base.Pointer;

        public HResult Clone(IEnumString** @enum)
        {
            return Pointer->Clone(@enum);
        }

        public HResult Next(uint elt, ushort** gelt, uint* eltFetchedCount = null)
        {
            return Pointer->Next(elt, gelt, eltFetchedCount);
        }

        public HResult Reset()
        {
            return Pointer->Reset();
        }

        public HResult Skip(uint elt)
        {
            return Pointer->Skip(elt);
        }

        public static implicit operator IEnumString*(EnumString value)
        {
            return value.Pointer;
        }
    }
}