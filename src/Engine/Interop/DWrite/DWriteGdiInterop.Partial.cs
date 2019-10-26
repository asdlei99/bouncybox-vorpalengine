using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DWriteGdiInterop
    {
        public HResult ConvertFontFaceToLOGFONT(IDWriteFontFace* font, out LOGFONTW logFont)
        {
            fixed (LOGFONTW* pLogFont = &logFont)
            {
                return Pointer->ConvertFontFaceToLOGFONT(font, pLogFont);
            }
        }

        public HResult ConvertFontToLOGFONT(IDWriteFont* font, out LOGFONTW logFont, out bool isSystemFont)
        {
            int iIsSystemFont;
            int hr;

            fixed (LOGFONTW* pLogFont = &logFont)
            {
                hr = Pointer->ConvertFontToLOGFONT(font, pLogFont, &iIsSystemFont);
            }

            isSystemFont = iIsSystemFont == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult CreateBitmapRenderTarget([Optional] IntPtr hdc, uint width, uint height, out DWriteBitmapRenderTarget? renderTarget)
        {
            IDWriteBitmapRenderTarget* pRenderTarget;
            int hr = Pointer->CreateBitmapRenderTarget(hdc, width, height, &pRenderTarget);

            renderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteBitmapRenderTarget(pRenderTarget) : null;

            return hr;
        }

        public HResult CreateFontFaceFromHdc(IntPtr hdc, out DWriteFontFace? fontFace)
        {
            IDWriteFontFace* pFontFace;
            int hr = Pointer->CreateFontFaceFromHdc(hdc, &pFontFace);

            fontFace = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFace(pFontFace) : null;

            return hr;
        }

        public HResult CreateFontFromLOGFONT(LOGFONTW* logFont, out DWriteFont? font)
        {
            IDWriteFont* pFont;
            int hr = Pointer->CreateFontFromLOGFONT(logFont, &pFont);

            font = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFont(pFont) : null;

            return hr;
        }
    }
}