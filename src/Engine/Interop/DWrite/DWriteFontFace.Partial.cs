using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteFontFace
    {
        public HResult GetDesignGlyphMetrics(ReadOnlySpan<ushort> glyphIndices, out ReadOnlySpan<DWRITE_GLYPH_METRICS> glyphMetrics, bool isSideways)
        {
            var glyphMetricsArray = new DWRITE_GLYPH_METRICS[glyphIndices.Length];
            int hr;

            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (DWRITE_GLYPH_METRICS* pGlyphMetrics = glyphMetricsArray)
            {
                hr = Pointer->GetDesignGlyphMetrics(
                    pGlyphIndices,
                    (uint)glyphIndices.Length,
                    pGlyphMetrics,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
            }

            glyphMetrics = glyphMetricsArray;

            return hr;
        }

        public HResult GetFiles(Span<DWriteFontFile> fontFiles)
        {
            var numberOfFiles = (uint)fontFiles.Length;
            var pFontFilesArray = new IDWriteFontFile*[fontFiles.Length];
            int hr;

            fixed (IDWriteFontFile** ppFontFilesArray = pFontFilesArray)
            {
                hr = Pointer->GetFiles(&numberOfFiles, ppFontFilesArray);
            }

            // ReSharper disable once InvertIf
            if (TerraFX.Interop.Windows.SUCCEEDED(hr))
            {
                for (var i = 0; i < fontFiles.Length; i++)
                {
                    fontFiles[i] = new DWriteFontFile(pFontFilesArray[i]);
                }
            }

            return hr;
        }

        public HResult GetFileCount(out uint numberOfFiles)
        {
            fixed (uint* pNumberOfFiles = &numberOfFiles)
            {
                return Pointer->GetFiles(pNumberOfFiles, null);
            }
        }

        public HResult GetGdiCompatibleGlyphMetrics(
            float emSize,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            bool useGdiNatural,
            ReadOnlySpan<ushort> glyphIndices,
            out ReadOnlySpan<DWRITE_GLYPH_METRICS> glyphMetrics,
            bool isSideways = false)
        {
            var glyphMetricsArray = new DWRITE_GLYPH_METRICS[glyphIndices.Length];
            int hr;

            glyphMetrics = default;

            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (DWRITE_GLYPH_METRICS* pGlyphMetricsArray = glyphMetricsArray)
            {
                hr = Pointer->GetGdiCompatibleGlyphMetrics(
                    emSize,
                    pixelsPerDip,
                    transform,
                    useGdiNatural ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    pGlyphIndices,
                    (uint)glyphIndices.Length,
                    pGlyphMetricsArray,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);

                if (TerraFX.Interop.Windows.SUCCEEDED(hr))
                {
                    glyphMetrics = glyphMetricsArray;
                }
            }

            return hr;
        }

        public HResult GetGdiCompatibleMetrics(float emSize, float pixelsPerDip, [Optional] DWRITE_MATRIX* transform, out DWRITE_FONT_METRICS fontFaceMetrics)
        {
            fixed (DWRITE_FONT_METRICS* pFontFaceMetrics = &fontFaceMetrics)
            {
                return Pointer->GetGdiCompatibleMetrics(emSize, pixelsPerDip, transform, pFontFaceMetrics);
            }
        }

        public HResult GetGlyphIndices(ReadOnlySpan<uint> codePoints, out ReadOnlySpan<ushort> glyphIndices)
        {
            var glyphIndicesArray = new ushort[codePoints.Length];
            int hr;

            fixed (uint* pCodePoints = codePoints)
            fixed (ushort* pGlyphIndicesArray = glyphIndicesArray)
            {
                hr = Pointer->GetGlyphIndices(pCodePoints, (uint)codePoints.Length, pGlyphIndicesArray);

                glyphIndices = TerraFX.Interop.Windows.SUCCEEDED(hr) ? glyphIndicesArray : default;
            }

            return hr;
        }

        public HResult GetGlyphRunOutline(
            float emSize,
            ReadOnlySpan<ushort> glyphIndices,
            [Optional] ReadOnlySpan<float> glyphAdvances,
            [Optional] ReadOnlySpan<DWRITE_GLYPH_OFFSET> glyphOffsets,
            bool isSideways,
            bool isRightToLeft,
            IUnknown* geometrySink)
        {
            if (glyphAdvances.Length > 0 && glyphAdvances.Length != glyphIndices.Length ||
                glyphOffsets.Length > 0 && glyphOffsets.Length != glyphIndices.Length)
            {
                throw new ArgumentException("Glyph indices, glyph advances, and glyph offsets must all have the same length, or be empty.");
            }

            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (float* pGlyphAdvances = glyphAdvances)
            fixed (DWRITE_GLYPH_OFFSET* pGlyphOffsets = glyphOffsets)
            {
                return Pointer->GetGlyphRunOutline(
                    emSize,
                    pGlyphIndices,
                    pGlyphAdvances,
                    pGlyphOffsets,
                    (uint)glyphIndices.Length,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    geometrySink);
            }
        }

        public void GetMetrics(out DWRITE_FONT_METRICS fontFaceMetrics)
        {
            fixed (DWRITE_FONT_METRICS* pFontFaceMetrics = &fontFaceMetrics)
            {
                Pointer->GetMetrics(pFontFaceMetrics);
            }
        }

        public HResult GetRecommendedRenderingMode(
            float emSize,
            float pixelsPerDip,
            DWRITE_MEASURING_MODE measuringMode,
            IDWriteRenderingParams* renderingParams,
            out DWRITE_RENDERING_MODE renderingMode)
        {
            fixed (DWRITE_RENDERING_MODE* pRenderingMode = &renderingMode)
            {
                return Pointer->GetRecommendedRenderingMode(emSize, pixelsPerDip, measuringMode, renderingParams, pRenderingMode);
            }
        }

        public HResult TryGetFontTable(uint openTypeTableTag, out ReadOnlySpan<byte> tableData, out IntPtr tableContext, out bool exists)
        {
            int iExists;
            void* pTableData;
            uint tableSize;
            int hr;

            fixed (IntPtr* pTableContext = &tableContext)
            {
                hr = Pointer->TryGetFontTable(openTypeTableTag, &pTableData, &tableSize, (void**)pTableContext, &iExists);
            }

            tableData = new ReadOnlySpan<byte>(pTableData, checked((int)tableSize));
            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }
    }
}