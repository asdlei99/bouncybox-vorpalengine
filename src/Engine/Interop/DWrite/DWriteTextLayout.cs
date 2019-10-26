using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteTextLayout" /> COM interface.</summary>
    public unsafe partial class DWriteTextLayout : DWriteTextFormat
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteTextLayout" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteTextLayout(IDWriteTextLayout* pointer) : base((IDWriteTextFormat*)pointer)
        {
        }

        public new IDWriteTextLayout* Pointer => (IDWriteTextLayout*)base.Pointer;

        public HResult DetermineMinWidth(float* minWidth)
        {
            return Pointer->DetermineMinWidth(minWidth);
        }

        public HResult Draw([Optional] void* clientDrawingContext, IDWriteTextRenderer* renderer, float originX, float originY)
        {
            return Pointer->Draw(clientDrawingContext, renderer, originX, originY);
        }

        public HResult GetClusterMetrics(DWRITE_CLUSTER_METRICS* clusterMetrics, uint maxClusterCount, uint* actualClusterCount)
        {
            return Pointer->GetClusterMetrics(clusterMetrics, maxClusterCount, actualClusterCount);
        }

        public HResult GetDrawingEffect(uint currentPosition, IUnknown** drawingEffect, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetDrawingEffect(currentPosition, drawingEffect, textRange);
        }

        public HResult GetFontCollection(uint currentPosition, IDWriteFontCollection** fontCollection, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetFontCollection(currentPosition, fontCollection, textRange);
        }

        public HResult GetFontFamilyName(uint currentPosition, ushort* fontFamilyName, uint nameSize, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetFontFamilyName(currentPosition, fontFamilyName, nameSize, textRange);
        }

        public HResult GetFontFamilyNameLength(uint currentPosition, uint* nameLength, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetFontFamilyNameLength(currentPosition, nameLength, textRange);
        }

        public HResult GetFontSize(uint currentPosition, float* fontSize, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetFontSize(currentPosition, fontSize, textRange);
        }

        public HResult GetFontStretch(uint currentPosition, DWRITE_FONT_STRETCH* fontStretch, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetFontStretch(currentPosition, fontStretch, textRange);
        }

        public HResult GetFontStyle(uint currentPosition, DWRITE_FONT_STYLE* fontStyle, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetFontStyle(currentPosition, fontStyle, textRange);
        }

        public HResult GetFontWeight(uint currentPosition, DWRITE_FONT_WEIGHT* fontWeight, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetFontWeight(currentPosition, fontWeight, textRange);
        }

        public HResult GetInlineObject(uint currentPosition, IDWriteInlineObject** inlineObject, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetInlineObject(currentPosition, inlineObject, textRange);
        }

        public HResult GetLineMetrics(DWRITE_LINE_METRICS* lineMetrics, uint maxLineCount, uint* actualLineCount)
        {
            return Pointer->GetLineMetrics(lineMetrics, maxLineCount, actualLineCount);
        }

        public HResult GetLocaleName(uint currentPosition, ushort* localeName, uint nameSize, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetLocaleName(currentPosition, localeName, nameSize, textRange);
        }

        public HResult GetLocaleNameLength(uint currentPosition, uint* nameLength, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetLocaleNameLength(currentPosition, nameLength, textRange);
        }

        public float GetMaxHeight()
        {
            return Pointer->GetMaxHeight();
        }

        public float GetMaxWidth()
        {
            return Pointer->GetMaxWidth();
        }

        public HResult GetMetrics(DWRITE_TEXT_METRICS* textMetrics)
        {
            return Pointer->GetMetrics(textMetrics);
        }

        public HResult GetOverhangMetrics(DWRITE_OVERHANG_METRICS* overhangs)
        {
            return Pointer->GetOverhangMetrics(overhangs);
        }

        public HResult GetStrikethrough(uint currentPosition, out bool hasStrikethrough, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            int iHasStrikethrough;
            int hr = Pointer->GetStrikethrough(currentPosition, &iHasStrikethrough, textRange);

            hasStrikethrough = iHasStrikethrough == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetTypography(uint currentPosition, IDWriteTypography** typography, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            return Pointer->GetTypography(currentPosition, typography, textRange);
        }

        public HResult GetUnderline(uint currentPosition, out bool hasUnderline, [Optional] DWRITE_TEXT_RANGE* textRange)
        {
            int iHasUnderline;
            int hr = Pointer->GetUnderline(currentPosition, &iHasUnderline, textRange);

            hasUnderline = iHasUnderline == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult HitTestPoint(float pointX, float pointY, out bool isTrailingHit, out bool isInside, DWRITE_HIT_TEST_METRICS* hitTestMetrics)
        {
            int iIsTrailingHit;
            int iIsInside;
            int hr = Pointer->HitTestPoint(pointX, pointY, &iIsTrailingHit, &iIsInside, hitTestMetrics);

            isTrailingHit = iIsTrailingHit == TerraFX.Interop.Windows.TRUE;
            isInside = iIsInside == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult HitTestTextPosition(uint textPosition, bool isTrailingHit, float* pointX, float* pointY, DWRITE_HIT_TEST_METRICS* hitTestMetrics)
        {
            return Pointer->HitTestTextPosition(
                textPosition,
                isTrailingHit ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                pointX,
                pointY,
                hitTestMetrics);
        }

        public HResult HitTestTextRange(
            uint textPosition,
            uint textLength,
            float originX,
            float originY,
            DWRITE_HIT_TEST_METRICS* hitTestMetrics,
            uint maxHitTestMetricsCount,
            uint* actualHitTestMetricsCount)
        {
            return Pointer->HitTestTextRange(textPosition, textLength, originX, originY, hitTestMetrics, maxHitTestMetricsCount, actualHitTestMetricsCount);
        }

        public HResult SetDrawingEffect(IUnknown* drawingEffect, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetDrawingEffect(drawingEffect, textRange);
        }

        public HResult SetFontCollection(IDWriteFontCollection* fontCollection, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetFontCollection(fontCollection, textRange);
        }

        public HResult SetFontFamilyName(ushort* fontFamilyName, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetFontFamilyName(fontFamilyName, textRange);
        }

        public HResult SetFontSize(float fontSize, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetFontSize(fontSize, textRange);
        }

        public HResult SetFontStretch(DWRITE_FONT_STRETCH fontStretch, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetFontStretch(fontStretch, textRange);
        }

        public HResult SetFontStyle(DWRITE_FONT_STYLE fontStyle, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetFontStyle(fontStyle, textRange);
        }

        public HResult SetFontWeight(DWRITE_FONT_WEIGHT fontWeight, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetFontWeight(fontWeight, textRange);
        }

        public HResult SetInlineObject(IDWriteInlineObject* inlineObject, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetInlineObject(inlineObject, textRange);
        }

        public HResult SetLocaleName(ushort* localeName, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetLocaleName(localeName, textRange);
        }

        public HResult SetMaxHeight(float maxHeight)
        {
            return Pointer->SetMaxHeight(maxHeight);
        }

        public HResult SetMaxWidth(float maxWidth)
        {
            return Pointer->SetMaxWidth(maxWidth);
        }

        public HResult SetStrikethrough(bool hasStrikethrough, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetStrikethrough(hasStrikethrough ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE, textRange);
        }

        public HResult SetTypography(IDWriteTypography* typography, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetTypography(typography, textRange);
        }

        public HResult SetUnderline(bool hasUnderline, DWRITE_TEXT_RANGE textRange)
        {
            return Pointer->SetUnderline(hasUnderline ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE, textRange);
        }

        public static implicit operator IDWriteTextLayout*(DWriteTextLayout value)
        {
            return value.Pointer;
        }
    }
}