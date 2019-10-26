using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteTextFormat
    {
        public HResult GetFontCollection(out DWriteFontCollection? fontCollection)
        {
            IDWriteFontCollection* pFontCollection;
            int hr = Pointer->GetFontCollection(&pFontCollection);

            fontCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontCollection(pFontCollection) : null;

            return hr;
        }

        public HResult GetFontFamilyName(Span<char> fontFamilyName)
        {
            fixed (char* pFontFamilyName = fontFamilyName)
            {
                return Pointer->GetFontFamilyName((ushort*)pFontFamilyName, (uint)fontFamilyName.Length);
            }
        }

        public HResult GetLineSpacing(out DWRITE_LINE_SPACING_METHOD lineSpacingMethod, out float lineSpacing, out float baseline)
        {
            fixed (DWRITE_LINE_SPACING_METHOD* pLineSpacingMethod = &lineSpacingMethod)
            fixed (float* pLineSpacing = &lineSpacing)
            fixed (float* pBaseline = &baseline)
            {
                return Pointer->GetLineSpacing(pLineSpacingMethod, pLineSpacing, pBaseline);
            }
        }

        public HResult GetLocaleName(Span<char> localeName)
        {
            fixed (char* pLocaleName = localeName)
            {
                return Pointer->GetLocaleName((ushort*)pLocaleName, (uint)localeName.Length);
            }
        }

        public HResult GetTrimming(out DWRITE_TRIMMING trimmingOptions, out DWriteInlineObject? trimmingSign)
        {
            IDWriteInlineObject* pTrimmingSign;
            int hr;

            fixed (DWRITE_TRIMMING* pTrimmingOptions = &trimmingOptions)
            {
                hr = Pointer->GetTrimming(pTrimmingOptions, &pTrimmingSign);
            }

            trimmingSign = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteInlineObject(pTrimmingSign) : null;

            return hr;
        }
    }
}