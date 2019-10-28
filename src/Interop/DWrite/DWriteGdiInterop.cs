using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteGdiInterop" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DWriteGdiInterop : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteGdiInterop" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteGdiInterop(IDWriteGdiInterop* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteGdiInterop* Pointer => (IDWriteGdiInterop*)base.Pointer;

        public HResult ConvertFontFaceToLOGFONT(IDWriteFontFace* font, LOGFONTW* logFont)
        {
            return Pointer->ConvertFontFaceToLOGFONT(font, logFont);
        }

        public HResult ConvertFontToLOGFONT(IDWriteFont* font, LOGFONTW* logFont, out bool isSystemFont)
        {
            int iIsSystemFont;
            int hr = Pointer->ConvertFontToLOGFONT(font, logFont, &iIsSystemFont);

            isSystemFont = iIsSystemFont == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult CreateBitmapRenderTarget([Optional] IntPtr hdc, uint width, uint height, IDWriteBitmapRenderTarget** renderTarget)
        {
            return Pointer->CreateBitmapRenderTarget(hdc, width, height, renderTarget);
        }

        public HResult CreateFontFaceFromHdc(IntPtr hdc, IDWriteFontFace** fontFace)
        {
            return Pointer->CreateFontFaceFromHdc(hdc, fontFace);
        }

        public HResult CreateFontFromLOGFONT(LOGFONTW* logFont, IDWriteFont** font)
        {
            return Pointer->CreateFontFromLOGFONT(logFont, font);
        }

        public static implicit operator IDWriteGdiInterop*(DWriteGdiInterop value)
        {
            return value.Pointer;
        }
    }
}