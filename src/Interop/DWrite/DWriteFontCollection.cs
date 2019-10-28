using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontCollection" /> COM interface.</summary>
    public unsafe partial class DWriteFontCollection : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontCollection" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontCollection(IDWriteFontCollection* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontCollection* Pointer => (IDWriteFontCollection*)base.Pointer;

        public HResult FindFamilyName(ushort* familyName, uint* index, int* exists)
        {
            return Pointer->FindFamilyName(familyName, index, exists);
        }

        public HResult GetFontFamily(uint index, IDWriteFontFamily** fontFamily)
        {
            return Pointer->GetFontFamily(index, fontFamily);
        }

        public uint GetFontFamilyCount()
        {
            return Pointer->GetFontFamilyCount();
        }

        public HResult GetFontFromFontFace(IDWriteFontFace* fontFace, IDWriteFont** font)
        {
            return Pointer->GetFontFromFontFace(fontFace, font);
        }

        public static implicit operator IDWriteFontCollection*(DWriteFontCollection value)
        {
            return value.Pointer;
        }
    }
}