using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontFace" /> COM interface.</summary>
    public unsafe partial class DWriteFontFace : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontFace" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontFace(IDWriteFontFace* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontFace* Pointer => (IDWriteFontFace*)base.Pointer;

        public HResult GetDesignGlyphMetrics(ushort* glyphIndices, uint glyphCount, DWRITE_GLYPH_METRICS* glyphMetrics, bool isSideways)
        {
            return Pointer->GetDesignGlyphMetrics(
                glyphIndices,
                glyphCount,
                glyphMetrics,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public HResult GetFiles(uint* numberOfFiles, IDWriteFontFile** fontFiles)
        {
            return Pointer->GetFiles(numberOfFiles, fontFiles);
        }

        public HResult GetGdiCompatibleGlyphMetrics(
            float emSize,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            bool useGdiNatural,
            ushort* glyphIndices,
            uint glyphCount,
            DWRITE_GLYPH_METRICS* glyphMetrics,
            bool isSideways = false)
        {
            return Pointer->GetGdiCompatibleGlyphMetrics(
                emSize,
                pixelsPerDip,
                transform,
                useGdiNatural ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                glyphIndices,
                glyphCount,
                glyphMetrics,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public HResult GetGdiCompatibleMetrics(float emSize, float pixelsPerDip, [Optional] DWRITE_MATRIX* transform, DWRITE_FONT_METRICS* fontFaceMetrics)
        {
            return Pointer->GetGdiCompatibleMetrics(emSize, pixelsPerDip, transform, fontFaceMetrics);
        }

        public ushort GetGlyphCount()
        {
            return Pointer->GetGlyphCount();
        }

        public HResult GetGlyphIndices(uint* codePoints, uint codePointCount, ushort* glyphIndices)
        {
            return Pointer->GetGlyphIndices(codePoints, codePointCount, glyphIndices);
        }

        public HResult GetGlyphRunOutline(
            float emSize,
            ushort* glyphIndices,
            [Optional] float* glyphAdvances,
            [Optional] DWRITE_GLYPH_OFFSET* glyphOffsets,
            uint glyphCount,
            bool isSideways,
            bool isRightToLeft,
            IUnknown* geometrySink)
        {
            return Pointer->GetGlyphRunOutline(
                emSize,
                glyphIndices,
                glyphAdvances,
                glyphOffsets,
                glyphCount,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                geometrySink);
        }

        public uint GetIndex()
        {
            return Pointer->GetIndex();
        }

        public void GetMetrics(DWRITE_FONT_METRICS* fontFaceMetrics)
        {
            Pointer->GetMetrics(fontFaceMetrics);
        }

        public HResult GetRecommendedRenderingMode(
            float emSize,
            float pixelsPerDip,
            DWRITE_MEASURING_MODE measuringMode,
            IDWriteRenderingParams* renderingParams,
            DWRITE_RENDERING_MODE* renderingMode)
        {
            return Pointer->GetRecommendedRenderingMode(emSize, pixelsPerDip, measuringMode, renderingParams, renderingMode);
        }

        public DWRITE_FONT_SIMULATIONS GetSimulations()
        {
            return Pointer->GetSimulations();
        }

        public bool IsSymbolFont()
        {
            return Pointer->IsSymbolFont() == TerraFX.Interop.Windows.TRUE;
        }

        public void ReleaseFontTable(void* tableContext)
        {
            Pointer->ReleaseFontTable(tableContext);
        }

        public HResult TryGetFontTable(uint openTypeTableTag, void** tableData, uint* tableSize, void** tableContext, out bool exists)
        {
            int iExists;
            int hr = Pointer->TryGetFontTable(openTypeTableTag, tableData, tableSize, tableContext, &iExists);

            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IDWriteFontFace*(DWriteFontFace value)
        {
            return value.Pointer;
        }
    }
}