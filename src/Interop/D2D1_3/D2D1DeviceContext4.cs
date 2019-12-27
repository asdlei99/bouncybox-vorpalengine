using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1DeviceContext4" /> COM interface.</summary>
    public unsafe partial class D2D1DeviceContext4 : D2D1DeviceContext3
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DeviceContext4" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DeviceContext4(ID2D1DeviceContext4* pointer) : base((ID2D1DeviceContext3*)pointer)
        {
        }

        public new ID2D1DeviceContext4* Pointer => (ID2D1DeviceContext4*)base.Pointer;

        public HResult CreateSvgGlyphStyle(ID2D1SvgGlyphStyle** svgGlyphStyle)
        {
            return Pointer->CreateSvgGlyphStyle(svgGlyphStyle);
        }

        public void DrawColorBitmapGlyphRun(
            DWRITE_GLYPH_IMAGE_FORMATS glyphImageFormat,
            D2D_POINT_2F baselineOrigin,
            DWRITE_GLYPH_RUN* glyphRun,
            DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE.DWRITE_MEASURING_MODE_NATURAL,
            D2D1_COLOR_BITMAP_GLYPH_SNAP_OPTION bitmapSnapOption = D2D1_COLOR_BITMAP_GLYPH_SNAP_OPTION.D2D1_COLOR_BITMAP_GLYPH_SNAP_OPTION_DEFAULT)
        {
            Pointer->DrawColorBitmapGlyphRun(glyphImageFormat, baselineOrigin, glyphRun, measuringMode, bitmapSnapOption);
        }

        public void DrawSvgGlyphRun(
            D2D_POINT_2F baselineOrigin,
            DWRITE_GLYPH_RUN* glyphRun,
            ID2D1Brush* defaultFillBrush = null,
            ID2D1SvgGlyphStyle* svgGlyphStyle = null,
            uint colorPaletteIndex = 0,
            DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE.DWRITE_MEASURING_MODE_NATURAL)
        {
            Pointer->DrawSvgGlyphRun(baselineOrigin, glyphRun, defaultFillBrush, svgGlyphStyle, colorPaletteIndex, measuringMode);
        }

        public void DrawText(
            ushort* @string,
            uint stringLength,
            IDWriteTextFormat* textFormat,
            D2D_RECT_F* layoutRect,
            [Optional] ID2D1Brush* defaultFillBrush,
            [Optional] ID2D1SvgGlyphStyle* svgGlyphStyle,
            uint colorPaletteIndex,
            D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS.D2D1_DRAW_TEXT_OPTIONS_ENABLE_COLOR_FONT,
            DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE.DWRITE_MEASURING_MODE_NATURAL)
        {
            Pointer->DrawText(@string, stringLength, textFormat, layoutRect, defaultFillBrush, svgGlyphStyle, colorPaletteIndex, options, measuringMode);
        }

        public void DrawTextLayout(
            D2D_POINT_2F origin,
            IDWriteTextLayout* textLayout,
            [Optional] ID2D1Brush* defaultFillBrush,
            [Optional] ID2D1SvgGlyphStyle* svgGlyphStyle,
            uint colorPaletteIndex = 0,
            D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS.D2D1_DRAW_TEXT_OPTIONS_ENABLE_COLOR_FONT)
        {
            Pointer->DrawTextLayout(origin, textLayout, defaultFillBrush, svgGlyphStyle, colorPaletteIndex, options);
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
            ID2D1Image** glyphImage)
        {
            return Pointer->GetColorBitmapGlyphImage(
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
                glyphImage);
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
            ID2D1CommandList** glyphImage)
        {
            return Pointer->GetSvgGlyphImage(
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
                glyphImage);
        }

        public static implicit operator ID2D1DeviceContext4*(D2D1DeviceContext4 value)
        {
            return value.Pointer;
        }
    }
}