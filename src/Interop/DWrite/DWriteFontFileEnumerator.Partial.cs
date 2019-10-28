using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteFontFileEnumerator
    {
        public HResult GetCurrentFontFile(out DWriteFontFile? fontFile)
        {
            IDWriteFontFile* pFontFile;
            int hr = Pointer->GetCurrentFontFile(&pFontFile);

            fontFile = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFile(pFontFile) : null;

            return hr;
        }
    }
}