using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteTextAnalyzer" /> COM interface.</summary>
    public unsafe partial class DWriteTextAnalyzer : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteTextAnalyzer" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteTextAnalyzer(IDWriteTextAnalyzer* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteTextAnalyzer* Pointer => (IDWriteTextAnalyzer*)base.Pointer;

        public HResult AnalyzeBidi(IDWriteTextAnalysisSource* analysisSource, uint textPosition, uint textLength, IDWriteTextAnalysisSink* analysisSink)
        {
            return Pointer->AnalyzeBidi(analysisSource, textPosition, textLength, analysisSink);
        }

        public HResult AnalyzeLineBreakpoints(
            IDWriteTextAnalysisSource* analysisSource,
            uint textPosition,
            uint textLength,
            IDWriteTextAnalysisSink* analysisSink)
        {
            return Pointer->AnalyzeLineBreakpoints(analysisSource, textPosition, textLength, analysisSink);
        }

        public HResult AnalyzeNumberSubstitution(
            IDWriteTextAnalysisSource* analysisSource,
            uint textPosition,
            uint textLength,
            IDWriteTextAnalysisSink* analysisSink)
        {
            return Pointer->AnalyzeNumberSubstitution(analysisSource, textPosition, textLength, analysisSink);
        }

        public HResult AnalyzeScript(IDWriteTextAnalysisSource* analysisSource, uint textPosition, uint textLength, IDWriteTextAnalysisSink* analysisSink)
        {
            return Pointer->AnalyzeScript(analysisSource, textPosition, textLength, analysisSink);
        }

        public HResult GetGdiCompatibleGlyphPlacements(
            ushort* textString,
            ushort* clusterMap,
            DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
            uint textLength,
            ushort* glyphIndices,
            DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps,
            uint glyphCount,
            IDWriteFontFace* fontFace,
            float fontEmSize,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            bool useGdiNatural,
            bool isSideways,
            bool isRightToLeft,
            DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [Optional] ushort* localeName,
            [Optional] DWRITE_TYPOGRAPHIC_FEATURES** features,
            [Optional] uint* featureRangeLengths,
            uint featureRanges,
            float* glyphAdvances,
            DWRITE_GLYPH_OFFSET* glyphOffsets)
        {
            return Pointer->GetGdiCompatibleGlyphPlacements(
                textString,
                clusterMap,
                textProps,
                textLength,
                glyphIndices,
                glyphProps,
                glyphCount,
                fontFace,
                fontEmSize,
                pixelsPerDip,
                transform,
                useGdiNatural ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                scriptAnalysis,
                localeName,
                features,
                featureRangeLengths,
                featureRanges,
                glyphAdvances,
                glyphOffsets);
        }

        public HResult GetGlyphPlacements(
            ushort* textString,
            ushort* clusterMap,
            DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
            uint textLength,
            ushort* glyphIndices,
            DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps,
            uint glyphCount,
            IDWriteFontFace* fontFace,
            float fontEmSize,
            bool isSideways,
            bool isRightToLeft,
            DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [Optional] ushort* localeName,
            [Optional] DWRITE_TYPOGRAPHIC_FEATURES** features,
            [Optional] uint* featureRangeLengths,
            uint featureRanges,
            float* glyphAdvances,
            DWRITE_GLYPH_OFFSET* glyphOffsets)
        {
            return Pointer->GetGlyphPlacements(
                textString,
                clusterMap,
                textProps,
                textLength,
                glyphIndices,
                glyphProps,
                glyphCount,
                fontFace,
                fontEmSize,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                scriptAnalysis,
                localeName,
                features,
                featureRangeLengths,
                featureRanges,
                glyphAdvances,
                glyphOffsets);
        }

        public HResult GetGlyphs(
            ushort* textString,
            uint textLength,
            IDWriteFontFace* fontFace,
            bool isSideways,
            bool isRightToLeft,
            DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [Optional] ushort* localeName,
            [Optional] IDWriteNumberSubstitution* numberSubstitution,
            [Optional] DWRITE_TYPOGRAPHIC_FEATURES** features,
            [Optional] uint* featureRangeLengths,
            uint featureRanges,
            uint maxGlyphCount,
            ushort* clusterMap,
            DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
            ushort* glyphIndices,
            DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps,
            uint* actualGlyphCount)
        {
            return Pointer->GetGlyphs(
                textString,
                textLength,
                fontFace,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                scriptAnalysis,
                localeName,
                numberSubstitution,
                features,
                featureRangeLengths,
                featureRanges,
                maxGlyphCount,
                clusterMap,
                textProps,
                glyphIndices,
                glyphProps,
                actualGlyphCount);
        }

        public static implicit operator IDWriteTextAnalyzer*(DWriteTextAnalyzer value)
        {
            return value.Pointer;
        }
    }
}