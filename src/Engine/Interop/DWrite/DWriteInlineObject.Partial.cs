using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteInlineObject
    {
        public HResult GetBreakConditions(out DWRITE_BREAK_CONDITION breakConditionBefore, out DWRITE_BREAK_CONDITION breakConditionAfter)
        {
            fixed (DWRITE_BREAK_CONDITION* pBreakConditionBefore = &breakConditionBefore)
            fixed (DWRITE_BREAK_CONDITION* pBreakConditionAfter = &breakConditionAfter)
            {
                return Pointer->GetBreakConditions(pBreakConditionBefore, pBreakConditionAfter);
            }
        }

        public HResult GetMetrics(out DWRITE_INLINE_OBJECT_METRICS metrics)
        {
            fixed (DWRITE_INLINE_OBJECT_METRICS* pMetrics = &metrics)
            {
                return Pointer->GetMetrics(pMetrics);
            }
        }

        public HResult GetOverhangMetrics(out DWRITE_OVERHANG_METRICS overhangs)
        {
            fixed (DWRITE_OVERHANG_METRICS* pOverhangs = &overhangs)
            {
                return Pointer->GetOverhangMetrics(pOverhangs);
            }
        }
    }
}