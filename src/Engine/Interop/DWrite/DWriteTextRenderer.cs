using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteTextRenderer" /> COM interface.</summary>
    public unsafe class DWriteTextRenderer : DWritePixelSnapping
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteTextRenderer" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteTextRenderer(IDWriteTextRenderer* pointer) : base((IDWritePixelSnapping*)pointer)
        {
        }

        public new IDWriteTextRenderer* Pointer => (IDWriteTextRenderer*)base.Pointer;

        public HResult DrawGlyphRun(
            [Optional] void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_MEASURING_MODE measuringMode,
            DWRITE_GLYPH_RUN* glyphRun,
            DWRITE_GLYPH_RUN_DESCRIPTION* glyphRunDescription,
            [Optional] IUnknown* clientDrawingEffect)
        {
            return Pointer->DrawGlyphRun(
                clientDrawingContext,
                baselineOriginX,
                baselineOriginY,
                measuringMode,
                glyphRun,
                glyphRunDescription,
                clientDrawingEffect);
        }

        public HResult DrawInlineObject(
            [Optional] void* clientDrawingContext,
            float originX,
            float originY,
            IDWriteInlineObject* inlineObject,
            bool isSideways,
            bool isRightToLeft,
            [Optional] IUnknown* clientDrawingEffect)
        {
            return Pointer->DrawInlineObject(
                clientDrawingContext,
                originX,
                originY,
                inlineObject,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                clientDrawingEffect);
        }

        public HResult DrawStrikethrough(
            [Optional] void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_STRIKETHROUGH* strikethrough,
            [Optional] IUnknown* clientDrawingEffect)
        {
            return Pointer->DrawStrikethrough(clientDrawingContext, baselineOriginX, baselineOriginY, strikethrough, clientDrawingEffect);
        }

        public HResult DrawUnderline(
            [Optional] void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_UNDERLINE* underline,
            [Optional] IUnknown* clientDrawingEffect)
        {
            return Pointer->DrawUnderline(clientDrawingContext, baselineOriginX, baselineOriginY, underline, clientDrawingEffect);
        }

        public static implicit operator IDWriteTextRenderer*(DWriteTextRenderer value)
        {
            return value.Pointer;
        }
    }
}