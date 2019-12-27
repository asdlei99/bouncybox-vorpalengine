using System;
using System.Buffers;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Common;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteFontFace
    {
        public HResult GetDesignGlyphMetrics(ReadOnlySpan<ushort> glyphIndices, Span<DWRITE_GLYPH_METRICS> glyphMetrics, bool isSideways)
        {
            if (glyphMetrics.Length < glyphIndices.Length)
            {
                throw new ArgumentException($"Must have at least as many elements as {nameof(glyphIndices)}.", nameof(glyphMetrics));
            }

            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (DWRITE_GLYPH_METRICS* pGlyphMetrics = glyphMetrics)
            {
                return Pointer->GetDesignGlyphMetrics(
                    pGlyphIndices,
                    (uint)glyphIndices.Length,
                    pGlyphMetrics,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
            }
        }

        public HResult GetFiles(Span<DWriteFontFile> fontFiles)
        {
            var numberOfFiles = (uint)fontFiles.Length;
            Pointer<IDWriteFontFile>[]? pFontFilesArray = null;
            Span<Pointer<IDWriteFontFile>> fontFilesSpan =
                AllocationHelper.CanStackAlloc<Pointer<IDWriteFontFile>>((uint)fontFiles.Length)
                    ? stackalloc Pointer<IDWriteFontFile>[fontFiles.Length]
                    : pFontFilesArray = ArrayPool<Pointer<IDWriteFontFile>>.Shared.Rent(fontFiles.Length);
            int hr;

            try
            {
                fixed (Pointer<IDWriteFontFile>* ppFontFiles = fontFilesSpan)
                {
                    hr = Pointer->GetFiles(&numberOfFiles, (IDWriteFontFile**)ppFontFiles);
                }

                // ReSharper disable once InvertIf
                if (TerraFX.Interop.Windows.SUCCEEDED(hr))
                {
                    for (var i = 0; i < fontFiles.Length; i++)
                    {
                        fontFiles[i] = new DWriteFontFile(fontFilesSpan[i].Ptr);
                    }
                }
            }
            finally
            {
                if (pFontFilesArray is object)
                {
                    ArrayPool<Pointer<IDWriteFontFile>>.Shared.Return(pFontFilesArray);
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
            Span<DWRITE_GLYPH_METRICS> glyphMetrics,
            bool isSideways = false)
        {
            if (glyphMetrics.Length < glyphIndices.Length)
            {
                throw new ArgumentException($"Must have at least as many elements as {nameof(glyphIndices)}.", nameof(glyphMetrics));
            }

            fixed (ushort* pGlyphIndices = glyphIndices)
            fixed (DWRITE_GLYPH_METRICS* pGlyphMetrics = glyphMetrics)
            {
                return Pointer->GetGdiCompatibleGlyphMetrics(
                    emSize,
                    pixelsPerDip,
                    transform,
                    useGdiNatural ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    pGlyphIndices,
                    (uint)glyphIndices.Length,
                    pGlyphMetrics,
                    isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
            }
        }

        public HResult GetGdiCompatibleMetrics(float emSize, float pixelsPerDip, [Optional] DWRITE_MATRIX* transform, out DWRITE_FONT_METRICS fontFaceMetrics)
        {
            fixed (DWRITE_FONT_METRICS* pFontFaceMetrics = &fontFaceMetrics)
            {
                return Pointer->GetGdiCompatibleMetrics(emSize, pixelsPerDip, transform, pFontFaceMetrics);
            }
        }

        public HResult GetGlyphIndices(ReadOnlySpan<uint> codePoints, Span<ushort> glyphIndices)
        {
            if (glyphIndices.Length < codePoints.Length)
            {
                throw new ArgumentException($"Must have at least as many elements as {nameof(codePoints)}.", nameof(glyphIndices));
            }

            int hr;

            fixed (uint* pCodePoints = codePoints)
            fixed (ushort* pGlyphIndices = glyphIndices)
            {
                hr = Pointer->GetGlyphIndices(pCodePoints, (uint)codePoints.Length, pGlyphIndices);
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
                throw new ArgumentException(
                    $"{nameof(glyphIndices)}, {nameof(glyphAdvances)}, and {nameof(glyphOffsets)} must all have the same length, but they may be individually empty.");
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

        public DWRITE_FONT_METRICS GetMetrics()
        {
            DWRITE_FONT_METRICS fontFaceMetrics;

            Pointer->GetMetrics(&fontFaceMetrics);

            return fontFaceMetrics;
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