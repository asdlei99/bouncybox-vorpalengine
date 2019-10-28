using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontList" /> COM interface.</summary>
    public unsafe partial class DWriteFontList : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontList" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontList(IDWriteFontList* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontList* Pointer => (IDWriteFontList*)base.Pointer;

        public HResult GetFont(uint index, IDWriteFont** font)
        {
            return Pointer->GetFont(index, font);
        }

        public HResult GetFontCollection(IDWriteFontCollection** fontCollection)
        {
            return Pointer->GetFontCollection(fontCollection);
        }

        public uint GetFontCount()
        {
            return Pointer->GetFontCount();
        }

        public static implicit operator IDWriteFontList*(DWriteFontList value)
        {
            return value.Pointer;
        }
    }
}