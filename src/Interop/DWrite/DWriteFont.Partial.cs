using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DWriteFont
    {
        public HResult CreateFontFace(out DWriteFontFace? fontFace)
        {
            IDWriteFontFace* pFontFace;
            int hr = Pointer->CreateFontFace(&pFontFace);

            fontFace = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFace(pFontFace) : null;

            return hr;
        }

        public HResult GetFaceNames(out DWriteLocalizedStrings? names)
        {
            IDWriteLocalizedStrings* pNames;
            int hr = Pointer->GetFaceNames(&pNames);

            names = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteLocalizedStrings(pNames) : null;

            return hr;
        }

        public HResult GetFontFamily(out DWriteFontFamily? fontFamily)
        {
            IDWriteFontFamily* pFontFamily;
            int hr = Pointer->GetFontFamily(&pFontFamily);

            fontFamily = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFamily(pFontFamily) : null;

            return hr;
        }

        public HResult GetInformationalStrings(
            DWRITE_INFORMATIONAL_STRING_ID informationalStringID,
            out DWriteLocalizedStrings? informationalStrings,
            out bool exists)
        {
            IDWriteLocalizedStrings* pInformationalStrings;
            int iExists;
            int hr = Pointer->GetInformationalStrings(informationalStringID, &pInformationalStrings, &iExists);

            informationalStrings = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteLocalizedStrings(pInformationalStrings) : null;
            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public void GetMetrics(out DWRITE_FONT_METRICS fontMetrics)
        {
            fixed (DWRITE_FONT_METRICS* pFontMetrics = &fontMetrics)
            {
                Pointer->GetMetrics(pFontMetrics);
            }
        }
    }
}