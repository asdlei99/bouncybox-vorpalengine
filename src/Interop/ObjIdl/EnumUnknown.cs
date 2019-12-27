using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    /// <summary>Proxies the <see cref="IEnumUnknown" /> COM interface.</summary>
    public unsafe partial class EnumUnknown : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="EnumUnknown" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public EnumUnknown(IEnumUnknown* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IEnumUnknown* Pointer => (IEnumUnknown*)base.Pointer;

        public HResult Clone(IEnumUnknown** @enum = null)
        {
            return Pointer->Clone(@enum);
        }

        public HResult Next(uint eltCount, IUnknown** gelt, uint* eltFetchedCount = null)
        {
            return Pointer->Next(eltCount, gelt, eltFetchedCount);
        }

        public HResult Reset()
        {
            return Pointer->Reset();
        }

        public HResult Skip(uint eltCount)
        {
            return Pointer->Skip(eltCount);
        }

        public static implicit operator IEnumUnknown*(EnumUnknown value)
        {
            return value.Pointer;
        }
    }
}