using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteLocalizedStrings" /> COM interface.</summary>
    public unsafe partial class DWriteLocalizedStrings : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteLocalizedStrings" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteLocalizedStrings(IDWriteLocalizedStrings* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteLocalizedStrings* Pointer => (IDWriteLocalizedStrings*)base.Pointer;

        public HResult FindLocaleName(ushort* localeName, uint* index, out bool exists)
        {
            int iExists;
            int hr = Pointer->FindLocaleName(localeName, index, &iExists);

            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public uint GetCount()
        {
            return Pointer->GetCount();
        }

        public HResult GetLocaleName(uint index, ushort* localeName, uint size)
        {
            return Pointer->GetLocaleName(index, localeName, size);
        }

        public HResult GetLocaleNameLength(uint index, uint* length)
        {
            return Pointer->GetLocaleNameLength(index, length);
        }

        public HResult GetString(uint index, ushort* stringBuffer, uint size)
        {
            return Pointer->GetString(index, stringBuffer, size);
        }

        public HResult GetStringLength(uint index, uint* length)
        {
            return Pointer->GetStringLength(index, length);
        }

        public static implicit operator IDWriteLocalizedStrings*(DWriteLocalizedStrings value)
        {
            return value.Pointer;
        }
    }
}