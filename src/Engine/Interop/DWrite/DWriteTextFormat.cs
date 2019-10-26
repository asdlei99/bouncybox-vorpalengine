using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteTextFormat" /> COM interface.</summary>
    public unsafe partial class DWriteTextFormat : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteTextFormat" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteTextFormat(IDWriteTextFormat* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteTextFormat* Pointer => (IDWriteTextFormat*)base.Pointer;

        public DWRITE_FLOW_DIRECTION GetFlowDirection()
        {
            return Pointer->GetFlowDirection();
        }

        public HResult GetFontCollection(IDWriteFontCollection** fontCollection)
        {
            return Pointer->GetFontCollection(fontCollection);
        }

        public HResult GetFontFamilyName(ushort* fontFamilyName, uint nameSize)
        {
            return Pointer->GetFontFamilyName(fontFamilyName, nameSize);
        }

        public uint GetFontFamilyNameLength()
        {
            return Pointer->GetFontFamilyNameLength();
        }

        public float GetFontSize()
        {
            return Pointer->GetFontSize();
        }

        public DWRITE_FONT_STRETCH GetFontStretch()
        {
            return Pointer->GetFontStretch();
        }

        public DWRITE_FONT_STYLE GetFontStyle()
        {
            return Pointer->GetFontStyle();
        }

        public DWRITE_FONT_WEIGHT GetFontWeight()
        {
            return Pointer->GetFontWeight();
        }

        public float GetIncrementalTabStop()
        {
            return Pointer->GetIncrementalTabStop();
        }

        public HResult GetLineSpacing(DWRITE_LINE_SPACING_METHOD* lineSpacingMethod, float* lineSpacing, float* baseline)
        {
            return Pointer->GetLineSpacing(lineSpacingMethod, lineSpacing, baseline);
        }

        public HResult GetLocaleName(ushort* localeName, uint nameSize)
        {
            return Pointer->GetLocaleName(localeName, nameSize);
        }

        public uint GetLocaleNameLength()
        {
            return Pointer->GetLocaleNameLength();
        }

        public DWRITE_PARAGRAPH_ALIGNMENT GetParagraphAlignment()
        {
            return Pointer->GetParagraphAlignment();
        }

        public DWRITE_READING_DIRECTION GetReadingDirection()
        {
            return Pointer->GetReadingDirection();
        }

        public DWRITE_TEXT_ALIGNMENT GetTextAlignment()
        {
            return Pointer->GetTextAlignment();
        }

        public HResult GetTrimming(DWRITE_TRIMMING* trimmingOptions, IDWriteInlineObject** trimmingSign)
        {
            return Pointer->GetTrimming(trimmingOptions, trimmingSign);
        }

        public DWRITE_WORD_WRAPPING GetWordWrapping()
        {
            return Pointer->GetWordWrapping();
        }

        public HResult SetFlowDirection(DWRITE_FLOW_DIRECTION flowDirection)
        {
            return Pointer->SetFlowDirection(flowDirection);
        }

        public HResult SetIncrementalTabStop(float incrementalTabStop)
        {
            return Pointer->SetIncrementalTabStop(incrementalTabStop);
        }

        public HResult SetLineSpacing(DWRITE_LINE_SPACING_METHOD lineSpacingMethod, float lineSpacing, float baseline)
        {
            return Pointer->SetLineSpacing(lineSpacingMethod, lineSpacing, baseline);
        }

        public HResult SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT paragraphAlignment)
        {
            return Pointer->SetParagraphAlignment(paragraphAlignment);
        }

        public HResult SetReadingDirection(DWRITE_READING_DIRECTION readingDirection)
        {
            return Pointer->SetReadingDirection(readingDirection);
        }

        public HResult SetTextAlignment(DWRITE_TEXT_ALIGNMENT textAlignment)
        {
            return Pointer->SetTextAlignment(textAlignment);
        }

        public HResult SetTrimming(DWRITE_TRIMMING* trimmingOptions, [Optional] IDWriteInlineObject* trimmingSign)
        {
            return Pointer->SetTrimming(trimmingOptions, trimmingSign);
        }

        public HResult SetWordWrapping(DWRITE_WORD_WRAPPING wordWrapping)
        {
            return Pointer->SetWordWrapping(wordWrapping);
        }

        public static implicit operator IDWriteTextFormat*(DWriteTextFormat value)
        {
            return value.Pointer;
        }
    }
}