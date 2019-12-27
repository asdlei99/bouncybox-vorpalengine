using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteTextLayout
    {
        public HResult DetermineMinWidth(out float minWidth)
        {
            fixed (float* pMinWidth = &minWidth)
            {
                return Pointer->DetermineMinWidth(pMinWidth);
            }
        }

        public HResult GetClusterMetrics(out DWRITE_CLUSTER_METRICS clusterMetrics, uint maxClusterCount, out uint actualClusterCount)
        {
            fixed (DWRITE_CLUSTER_METRICS* pClusterMetrics = &clusterMetrics)
            fixed (uint* pActualClusterCount = &actualClusterCount)
            {
                return Pointer->GetClusterMetrics(pClusterMetrics, maxClusterCount, pActualClusterCount);
            }
        }

        public HResult GetDrawingEffect(uint currentPosition, out IUnknown* drawingEffect, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (IUnknown** pDrawingEffect = &drawingEffect)
            {
                return Pointer->GetDrawingEffect(currentPosition, pDrawingEffect, textRange);
            }
        }

        public HResult GetFontCollection(uint currentPosition, out DWriteFontCollection? fontCollection, DWRITE_TEXT_RANGE* textRange = null)
        {
            IDWriteFontCollection* pFontCollection;
            int hr = Pointer->GetFontCollection(currentPosition, &pFontCollection, textRange);

            fontCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontCollection(pFontCollection) : null;

            return hr;
        }

        public HResult GetFontFamilyName(uint currentPosition, Span<char> fontFamilyName, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (char* pFontFamilyName = fontFamilyName)
            {
                return Pointer->GetFontFamilyName(currentPosition, (ushort*)pFontFamilyName, (uint)fontFamilyName.Length, textRange);
            }
        }

        public HResult GetFontFamilyNameLength(uint currentPosition, out uint nameLength, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (uint* pNameLength = &nameLength)
            {
                return Pointer->GetFontFamilyNameLength(currentPosition, pNameLength, textRange);
            }
        }

        public HResult GetFontSize(uint currentPosition, out float fontSize, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (float* pFontSize = &fontSize)
            {
                return Pointer->GetFontSize(currentPosition, pFontSize, textRange);
            }
        }

        public HResult GetFontStretch(uint currentPosition, out DWRITE_FONT_STRETCH fontStretch, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (DWRITE_FONT_STRETCH* pFontStretch = &fontStretch)
            {
                return Pointer->GetFontStretch(currentPosition, pFontStretch, textRange);
            }
        }

        public HResult GetFontStyle(uint currentPosition, out DWRITE_FONT_STYLE fontStyle, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (DWRITE_FONT_STYLE* pFontStyle = &fontStyle)
            {
                return Pointer->GetFontStyle(currentPosition, pFontStyle, textRange);
            }
        }

        public HResult GetFontWeight(uint currentPosition, out DWRITE_FONT_WEIGHT fontWeight, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (DWRITE_FONT_WEIGHT* pFontWeight = &fontWeight)
            {
                return Pointer->GetFontWeight(currentPosition, pFontWeight, textRange);
            }
        }

        public HResult GetInlineObject(uint currentPosition, out DWriteInlineObject? inlineObject, DWRITE_TEXT_RANGE* textRange = null)
        {
            IDWriteInlineObject* pInlineObject;
            int hr = Pointer->GetInlineObject(currentPosition, &pInlineObject, textRange);

            inlineObject = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteInlineObject(pInlineObject) : null;

            return hr;
        }

        public HResult GetLineMetrics(Span<DWRITE_LINE_METRICS> lineMetrics, out uint actualLineCount)
        {
            fixed (DWRITE_LINE_METRICS* pLineMetrics = lineMetrics)
            fixed (uint* pActualLineCount = &actualLineCount)
            {
                return Pointer->GetLineMetrics(pLineMetrics, (uint)lineMetrics.Length, pActualLineCount);
            }
        }

        public HResult GetLocaleName(uint currentPosition, Span<char> localeName, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (char* pLocaleName = localeName)
            {
                return Pointer->GetLocaleName(currentPosition, (ushort*)pLocaleName, (uint)localeName.Length, textRange);
            }
        }

        public HResult GetLocaleNameLength(uint currentPosition, out uint nameLength, DWRITE_TEXT_RANGE* textRange = null)
        {
            fixed (uint* pNameLength = &nameLength)
            {
                return Pointer->GetLocaleNameLength(currentPosition, pNameLength, textRange);
            }
        }

        public HResult GetMetrics(out DWRITE_TEXT_METRICS textMetrics)
        {
            fixed (DWRITE_TEXT_METRICS* pTextMetrics = &textMetrics)
            {
                return Pointer->GetMetrics(pTextMetrics);
            }
        }

        public HResult GetOverhangMetrics(out DWRITE_OVERHANG_METRICS overhangs)
        {
            fixed (DWRITE_OVERHANG_METRICS* pOverhangs = &overhangs)
            {
                return Pointer->GetOverhangMetrics(pOverhangs);
            }
        }

        public HResult GetTypography(uint currentPosition, out DWriteTypography? typography, DWRITE_TEXT_RANGE* textRange = null)
        {
            IDWriteTypography* pTypography;
            int hr = Pointer->GetTypography(currentPosition, &pTypography, textRange);

            typography = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteTypography(pTypography) : null;

            return hr;
        }

        public HResult HitTestTextPosition(
            uint textPosition,
            bool isTrailingHit,
            out float pointX,
            out float pointY,
            out DWRITE_HIT_TEST_METRICS hitTestMetrics)
        {
            fixed (float* pPointX = &pointX)
            fixed (float* pPointY = &pointY)
            fixed (DWRITE_HIT_TEST_METRICS* pHitTestMetrics = &hitTestMetrics)
            {
                return Pointer->HitTestTextPosition(
                    textPosition,
                    isTrailingHit ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    pPointX,
                    pPointY,
                    pHitTestMetrics);
            }
        }

        public HResult HitTestTextRange(
            uint textPosition,
            uint textLength,
            float originX,
            float originY,
            Span<DWRITE_HIT_TEST_METRICS> hitTestMetrics,
            out uint actualHitTestMetricsCount)
        {
            fixed (DWRITE_HIT_TEST_METRICS* pHitTestMetrics = hitTestMetrics)
            fixed (uint* pActualHitTestMetricsCount = &actualHitTestMetricsCount)
            {
                return Pointer->HitTestTextRange(
                    textPosition,
                    textLength,
                    originX,
                    originY,
                    pHitTestMetrics,
                    (uint)hitTestMetrics.Length,
                    pActualHitTestMetricsCount);
            }
        }

        public HResult SetFontFamilyName(ReadOnlySpan<char> fontFamilyName, DWRITE_TEXT_RANGE textRange)
        {
            fixed (char* pFontFamilyName = fontFamilyName)
            {
                return Pointer->SetFontFamilyName((ushort*)pFontFamilyName, textRange);
            }
        }

        public HResult SetLocaleName(ReadOnlySpan<char> localeName, DWRITE_TEXT_RANGE textRange)
        {
            fixed (char* pLocaleName = localeName)
            {
                return Pointer->SetLocaleName((ushort*)pLocaleName, textRange);
            }
        }
    }
}