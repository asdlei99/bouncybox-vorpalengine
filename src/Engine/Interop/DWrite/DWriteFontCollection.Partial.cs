using System;
using TerraFX.Interop;

#pragma warning disable 1591
namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteFontCollection
    {
        public HResult FindFamilyName(ReadOnlySpan<char> familyName, out uint index, out bool exists)
        {
            int iExists;
            int hr;

            fixed (char* pFamilyName = familyName)
            fixed (uint* pIndex = &index)
            {
                hr = Pointer->FindFamilyName((ushort*)pFamilyName, pIndex, &iExists);
            }

            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetFontFamily(uint index, out DWriteFontFamily? fontFamily)
        {
            IDWriteFontFamily* pFontFamily;
            int hr = Pointer->GetFontFamily(index, &pFontFamily);

            fontFamily = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFamily(pFontFamily) : null;

            return hr;
        }

        public HResult GetFontFromFontFace(IDWriteFontFace* fontFace, out DWriteFont? font)
        {
            IDWriteFont* pFont;
            int hr = Pointer->GetFontFromFontFace(fontFace, &pFont);

            font = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFont(pFont) : null;

            return hr;
        }
    }
}