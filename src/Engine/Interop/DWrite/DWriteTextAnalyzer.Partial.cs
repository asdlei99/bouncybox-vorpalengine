using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteTextAnalyzer
    {
        public HResult GetGdiCompatibleGlyphPlacements(
            ReadOnlySpan<ushort> textString,
            ReadOnlySpan<ushort> clusterMap,
            ReadOnlySpan<DWRITE_SHAPING_TEXT_PROPERTIES> textProps,
            ReadOnlySpan<ushort> glyphIndices,
            ReadOnlySpan<DWRITE_SHAPING_GLYPH_PROPERTIES> glyphProps,
            IDWriteFontFace* fontFace,
            float fontEmSize,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            bool useGdiNatural,
            bool isSideways,
            bool isRightToLeft,
            DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [Optional] ReadOnlySpan<char> localeName,
            [Optional] ReadOnlySpan<IDWriteTypographicFeaturesPointer> features,
            [Optional] ReadOnlySpan<uint> featureRangeLengths,
            Span<float> glyphAdvances,
            Span<DWRITE_GLYPH_OFFSET> glyphOffsets)
        {
            if (textString.Length != clusterMap.Length || textString.Length != textProps.Length)
            {
                throw new ArgumentException("Text string, cluster map, and text props must all have the same length.");
            }
            if (glyphIndices.Length != glyphProps.Length || glyphIndices.Length != glyphAdvances.Length || glyphIndices.Length != glyphOffsets.Length)
            {
                throw new ArgumentException("Glyph indices, glyph props, glyph advances, and glyph offsets must all have the same length.");
            }
            if (features.Length != featureRangeLengths.Length)
            {
                throw new ArgumentException("Features and feature range lengths must have the same length.");
            }

            fixed (ushort* pTextString = textString)
            fixed (ushort* pClusterMap = clusterMap)
            fixed (DWRITE_SHAPING_TEXT_PROPERTIES* pTextProps = textProps)
            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (DWRITE_SHAPING_GLYPH_PROPERTIES* pGlyphProps = glyphProps)
            fixed (char* pLocaleName = localeName)
            fixed (IDWriteTypographicFeaturesPointer* pFeatures = features)
            fixed (uint* pFeatureRangeLengths = featureRangeLengths)
            fixed (float* pGlyphAdvances = glyphAdvances)
            fixed (DWRITE_GLYPH_OFFSET* pGlyphOffsets = glyphOffsets)
            {
                return Pointer->GetGdiCompatibleGlyphPlacements(
                    pTextString,
                    pClusterMap,
                    pTextProps,
                    (uint)textString.Length,
                    pGlyphIndices,
                    pGlyphProps,
                    (uint)glyphIndices.Length,
                    fontFace,
                    fontEmSize,
                    pixelsPerDip,
                    transform,
                    useGdiNatural ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    scriptAnalysis,
                    (ushort*)pLocaleName,
                    (DWRITE_TYPOGRAPHIC_FEATURES**)pFeatures,
                    pFeatureRangeLengths,
                    (uint)features.Length,
                    pGlyphAdvances,
                    pGlyphOffsets);
            }
        }

        public HResult GetGlyphPlacements(
            ReadOnlySpan<ushort> textString,
            ReadOnlySpan<ushort> clusterMap,
            Span<DWRITE_SHAPING_TEXT_PROPERTIES> textProps,
            ReadOnlySpan<ushort> glyphIndices,
            ReadOnlySpan<DWRITE_SHAPING_GLYPH_PROPERTIES> glyphProps,
            IDWriteFontFace* fontFace,
            float fontEmSize,
            bool isSideways,
            bool isRightToLeft,
            DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [Optional] ReadOnlySpan<char> localeName,
            [Optional] ReadOnlySpan<IDWriteTypographicFeaturesPointer> features,
            [Optional] ReadOnlySpan<uint> featureRangeLengths,
            Span<float> glyphAdvances,
            Span<DWRITE_GLYPH_OFFSET> glyphOffsets)
        {
            if (textString.Length != clusterMap.Length || textString.Length != textProps.Length)
            {
                throw new ArgumentException("Text string, cluster map, and text props must all have the same length.");
            }
            if (glyphIndices.Length != glyphProps.Length || glyphIndices.Length != glyphAdvances.Length || glyphIndices.Length != glyphOffsets.Length)
            {
                throw new ArgumentException("Glyph indices, glyph props, glyph advances, and glyph offsets must all have the same length.");
            }
            if (localeName.Length != clusterMap.Length || features.Length != featureRangeLengths.Length)
            {
                throw new ArgumentException("Locale name, cluster map, and feature range lengths must all have the same length.");
            }

            fixed (ushort* pTextString = textString)
            fixed (ushort* pClusterMap = clusterMap)
            fixed (DWRITE_SHAPING_TEXT_PROPERTIES* pTextProps = textProps)
            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (DWRITE_SHAPING_GLYPH_PROPERTIES* pGlyphProps = glyphProps)
            fixed (char* pLocaleName = localeName)
            fixed (IDWriteTypographicFeaturesPointer* pFeatures = features)
            fixed (uint* pFeatureRangeLengths = featureRangeLengths)
            fixed (float* pGlyphAdvances = glyphAdvances)
            fixed (DWRITE_GLYPH_OFFSET* pGlyphOffsets = glyphOffsets)
            {
                return Pointer->GetGlyphPlacements(
                    pTextString,
                    pClusterMap,
                    pTextProps,
                    (uint)textString.Length,
                    pGlyphIndices,
                    pGlyphProps,
                    (uint)glyphIndices.Length,
                    fontFace,
                    fontEmSize,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    scriptAnalysis,
                    (ushort*)pLocaleName,
                    (DWRITE_TYPOGRAPHIC_FEATURES**)pFeatures,
                    pFeatureRangeLengths,
                    (uint)features.Length,
                    pGlyphAdvances,
                    pGlyphOffsets);
            }
        }

        public HResult GetGlyphs(
            ReadOnlySpan<ushort> textString,
            IDWriteFontFace* fontFace,
            bool isSideways,
            bool isRightToLeft,
            DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [Optional] ReadOnlySpan<char> localeName,
            [Optional] IDWriteNumberSubstitution* numberSubstitution,
            [Optional] ReadOnlySpan<IDWriteTypographicFeaturesPointer> features,
            [Optional] ReadOnlySpan<uint> featureRangeLengths,
            Span<ushort> clusterMap,
            Span<DWRITE_SHAPING_TEXT_PROPERTIES> textProps,
            ref Span<ushort> glyphIndices,
            ref Span<DWRITE_SHAPING_GLYPH_PROPERTIES> glyphProps)
        {
            if (features.Length != featureRangeLengths.Length)
            {
                throw new ArgumentException("Features and feature range lengths must have the same length.");
            }
            if (clusterMap.Length != textProps.Length)
            {
                throw new ArgumentException("Cluster map and text props must have the same length.");
            }
            if (glyphIndices.Length != glyphProps.Length)
            {
                throw new ArgumentException("Glyph indices and glyph props must have the same length.");
            }

            uint actualGlyphCount;
            int hr;

            fixed (ushort* pTextString = textString)
            fixed (char* pLocaleName = localeName)
            fixed (IDWriteTypographicFeaturesPointer* pFeatures = features)
            fixed (uint* pFeatureRangeLengths = featureRangeLengths)
            fixed (ushort* pClusterMap = clusterMap)
            fixed (DWRITE_SHAPING_TEXT_PROPERTIES* pTextProps = textProps)
            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (DWRITE_SHAPING_GLYPH_PROPERTIES* pGlyphProps = glyphProps)
            {
                hr = Pointer->GetGlyphs(
                    pTextString,
                    (uint)textString.Length,
                    fontFace,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    scriptAnalysis,
                    (ushort*)pLocaleName,
                    numberSubstitution,
                    (DWRITE_TYPOGRAPHIC_FEATURES**)pFeatures,
                    pFeatureRangeLengths,
                    (uint)features.Length,
                    (uint)glyphIndices.Length,
                    pClusterMap,
                    pTextProps,
                    pGlyphIndices,
                    pGlyphProps,
                    &actualGlyphCount);
            }

            // ReSharper disable once InvertIf
            if (TerraFX.Interop.Windows.SUCCEEDED(hr))
            {
                glyphIndices = glyphIndices.Slice(0, (int)actualGlyphCount);
                glyphProps = glyphProps.Slice(0, (int)actualGlyphCount);
            }

            return hr;
        }
    }
}