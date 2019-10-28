using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteTextAnalysisSource" /> COM interface.</summary>
    public unsafe partial class DWriteTextAnalysisSource : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteTextAnalysisSource" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteTextAnalysisSource(IDWriteTextAnalysisSource* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteTextAnalysisSource* Pointer => (IDWriteTextAnalysisSource*)base.Pointer;

        public HResult GetLocaleName(uint textPosition, uint* textLength, ushort** localeName)
        {
            return Pointer->GetLocaleName(textPosition, textLength, localeName);
        }

        public HResult GetNumberSubstitution(uint textPosition, uint* textLength, IDWriteNumberSubstitution** numberSubstitution)
        {
            return Pointer->GetNumberSubstitution(textPosition, textLength, numberSubstitution);
        }

        public DWRITE_READING_DIRECTION GetParagraphReadingDirection()
        {
            return Pointer->GetParagraphReadingDirection();
        }

        public HResult GetTextAtPosition(uint textPosition, ushort** textString, uint* textLength)
        {
            return Pointer->GetTextAtPosition(textPosition, textString, textLength);
        }

        public HResult GetTextBeforePosition(uint textPosition, ushort** textString, uint* textLength)
        {
            return Pointer->GetTextBeforePosition(textPosition, textString, textLength);
        }

        public static implicit operator IDWriteTextAnalysisSource*(DWriteTextAnalysisSource value)
        {
            return value.Pointer;
        }
    }
}