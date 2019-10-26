using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteFontList
    {
        public HResult GetFont(uint index, out DWriteFont? font)
        {
            IDWriteFont* pFont;
            int hr = Pointer->GetFont(index, &pFont);

            font = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFont(pFont) : null;

            return hr;
        }

        public HResult GetFontCollection(out DWriteFontCollection? fontCollection)
        {
            IDWriteFontCollection* pFontCollection;
            int hr = Pointer->GetFontCollection(&pFontCollection);

            fontCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontCollection(pFontCollection) : null;

            return hr;
        }
    }
}