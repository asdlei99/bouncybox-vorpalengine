using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteTextAnalysisSink" /> COM interface.</summary>
    public unsafe class DWriteTextAnalysisSink : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteTextAnalysisSink" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteTextAnalysisSink(IDWriteTextAnalysisSink* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteTextAnalysisSink* Pointer => (IDWriteTextAnalysisSink*)base.Pointer;

        public HResult SetBidiLevel(uint textPosition, uint textLength, byte explicitLevel, byte resolvedLevel)
        {
            return Pointer->SetBidiLevel(textPosition, textLength, explicitLevel, resolvedLevel);
        }

        public HResult SetLineBreakpoints(uint textPosition, uint textLength, DWRITE_LINE_BREAKPOINT* lineBreakpoints)
        {
            return Pointer->SetLineBreakpoints(textPosition, textLength, lineBreakpoints);
        }

        public HResult SetNumberSubstitution(uint textPosition, uint textLength, IDWriteNumberSubstitution* numberSubstitution)
        {
            return Pointer->SetNumberSubstitution(textPosition, textLength, numberSubstitution);
        }

        public HResult SetScriptAnalysis(uint textPosition, uint textLength, DWRITE_SCRIPT_ANALYSIS* scriptAnalysis)
        {
            return Pointer->SetScriptAnalysis(textPosition, textLength, scriptAnalysis);
        }

        public static implicit operator IDWriteTextAnalysisSink*(DWriteTextAnalysisSink value)
        {
            return value.Pointer;
        }
    }
}