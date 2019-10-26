using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontFamily" /> COM interface.</summary>
    public unsafe partial class DWriteFontFamily : DWriteFontList
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontFamily" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontFamily(IDWriteFontFamily* pointer) : base((IDWriteFontList*)pointer)
        {
        }

        public new IDWriteFontFamily* Pointer => (IDWriteFontFamily*)base.Pointer;

        public HResult GetFamilyNames(IDWriteLocalizedStrings** names)
        {
            return Pointer->GetFamilyNames(names);
        }

        public HResult GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, IDWriteFont** matchingFont)
        {
            return Pointer->GetFirstMatchingFont(weight, stretch, style, matchingFont);
        }

        public HResult GetMatchingFonts(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, IDWriteFontList** matchingFonts)
        {
            return Pointer->GetMatchingFonts(weight, stretch, style, matchingFonts);
        }

        public static implicit operator IDWriteFontFamily*(DWriteFontFamily value)
        {
            return value.Pointer;
        }
    }
}