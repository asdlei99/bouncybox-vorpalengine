using System;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using BouncyBox.VorpalEngine.Interop.D2D1_1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1DeviceContext4
    {
        public HResult CreateSvgGlyphStyle(out D2D1SvgGlyphStyle? svgGlyphStyle)
        {
            ID2D1SvgGlyphStyle* pSvgGlyphStyle;
            int hr = Pointer->CreateSvgGlyphStyle(&pSvgGlyphStyle);

            svgGlyphStyle = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgGlyphStyle(pSvgGlyphStyle) : null;

            return hr;
        }

        public void DrawText(
            ReadOnlySpan<char> @string,
            IDWriteTextFormat* textFormat,
            D2D_RECT_F* layoutRect,
            [Optional] ID2D1Brush* defaultFillBrush,
            [Optional] ID2D1SvgGlyphStyle* svgGlyphStyle,
            uint colorPaletteIndex,
            D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS.D2D1_DRAW_TEXT_OPTIONS_ENABLE_COLOR_FONT,
            DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE.DWRITE_MEASURING_MODE_NATURAL)
        {
            fixed (char* pString = @string)
            {
                Pointer->DrawText(
                    (ushort*)pString,
                    (uint)@string.Length,
                    textFormat,
                    layoutRect,
                    defaultFillBrush,
                    svgGlyphStyle,
                    colorPaletteIndex,
                    options,
                    measuringMode);
            }
        }

        public HResult GetColorBitmapGlyphImage(
            DWRITE_GLYPH_IMAGE_FORMATS glyphImageFormat,
            D2D_POINT_2F glyphOrigin,
            IDWriteFontFace* fontFace,
            float fontEmSize,
            ushort glyphIndex,
            bool isSideways,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            float dpiX,
            float dpiY,
            D2D_MATRIX_3X2_F* glyphTransform,
            out D2D1Image? glyphImage)
        {
            ID2D1Image* pGlyphImage;
            int hr = Pointer->GetColorBitmapGlyphImage(
                glyphImageFormat,
                glyphOrigin,
                fontFace,
                fontEmSize,
                glyphIndex,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                worldTransform,
                dpiX,
                dpiY,
                glyphTransform,
                &pGlyphImage);

            glyphImage = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Image(pGlyphImage) : null;

            return hr;
        }

        public HResult GetSvgGlyphImage(
            D2D_POINT_2F glyphOrigin,
            IDWriteFontFace* fontFace,
            float fontEmSize,
            ushort glyphIndex,
            bool isSideways,
            [Optional] D2D_MATRIX_3X2_F* worldTransform,
            [Optional] ID2D1Brush* defaultFillBrush,
            [Optional] ID2D1SvgGlyphStyle* svgGlyphStyle,
            uint colorPaletteIndex,
            D2D_MATRIX_3X2_F* glyphTransform,
            out D2D1CommandList? glyphImage)
        {
            ID2D1CommandList* pGlyphImage;
            int hr = Pointer->GetSvgGlyphImage(
                glyphOrigin,
                fontFace,
                fontEmSize,
                glyphIndex,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                worldTransform,
                defaultFillBrush,
                svgGlyphStyle,
                colorPaletteIndex,
                glyphTransform,
                &pGlyphImage);

            glyphImage = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1CommandList(pGlyphImage) : null;

            return hr;
        }
    }
}