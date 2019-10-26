using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteFontFamily
    {
        public HResult GetFamilyNames(out DWriteLocalizedStrings? names)
        {
            IDWriteLocalizedStrings* pNames;
            int hr = Pointer->GetFamilyNames(&pNames);

            names = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteLocalizedStrings(pNames) : null;

            return hr;
        }

        public HResult GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, out DWriteFont? matchingFont)
        {
            IDWriteFont* pMatchingFont;
            int hr = Pointer->GetFirstMatchingFont(weight, stretch, style, &pMatchingFont);

            matchingFont = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFont(pMatchingFont) : null;

            return hr;
        }

        public HResult GetMatchingFonts(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, out DWriteFontList? matchingFonts)
        {
            IDWriteFontList* pMatchingFonts;
            int hr = Pointer->GetMatchingFonts(weight, stretch, style, &pMatchingFonts);

            matchingFonts = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontList(pMatchingFonts) : null;

            return hr;
        }
    }
}