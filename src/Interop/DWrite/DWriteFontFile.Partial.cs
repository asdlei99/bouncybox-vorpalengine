using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteFontFile
    {
        public HResult Analyze(
            out bool isSupportedFontType,
            DWRITE_FONT_FILE_TYPE* fontFileType,
            [Optional] DWRITE_FONT_FACE_TYPE* fontFaceType,
            out uint numberOfFaces)
        {
            int iIsSupportedFontType;
            int hr;

            fixed (uint* pNumberOfFaces = &numberOfFaces)
            {
                hr = Pointer->Analyze(&iIsSupportedFontType, fontFileType, fontFaceType, pNumberOfFaces);
            }

            isSupportedFontType = iIsSupportedFontType == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetLoader(out DWriteFontFileLoader? fontFileLoader)
        {
            IDWriteFontFileLoader* pFontFileLoader;
            int hr = Pointer->GetLoader(&pFontFileLoader);

            fontFileLoader = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFileLoader(pFontFileLoader) : null;

            return hr;
        }

        public HResult GetReferenceKey(out ReadOnlySpan<byte> fontFileReferenceKey)
        {
            void* pFontFileReferenceKey;
            uint fontFileReferenceKeySize;
            int hr = Pointer->GetReferenceKey(&pFontFileReferenceKey, &fontFileReferenceKeySize);

            fontFileReferenceKey = new ReadOnlySpan<byte>(pFontFileReferenceKey, checked((int)fontFileReferenceKeySize));

            return hr;
        }
    }
}