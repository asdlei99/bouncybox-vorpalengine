using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteInlineObject" /> COM interface.</summary>
    public unsafe partial class DWriteInlineObject : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteInlineObject" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteInlineObject(IDWriteInlineObject* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteInlineObject* Pointer => (IDWriteInlineObject*)base.Pointer;

        public HResult Draw(
            [Optional] void* clientDrawingContext,
            IDWriteTextRenderer* renderer,
            float originX,
            float originY,
            bool isSideways,
            bool isRightToLeft,
            [Optional] IUnknown* clientDrawingEffect)
        {
            return Pointer->Draw(
                clientDrawingContext,
                renderer,
                originX,
                originY,
                isSideways ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                isRightToLeft ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                clientDrawingEffect);
        }

        public HResult GetBreakConditions(DWRITE_BREAK_CONDITION* breakConditionBefore, DWRITE_BREAK_CONDITION* breakConditionAfter)
        {
            return Pointer->GetBreakConditions(breakConditionBefore, breakConditionAfter);
        }

        public HResult GetMetrics(DWRITE_INLINE_OBJECT_METRICS* metrics)
        {
            return Pointer->GetMetrics(metrics);
        }

        public HResult GetOverhangMetrics(DWRITE_OVERHANG_METRICS* overhangs)
        {
            return Pointer->GetOverhangMetrics(overhangs);
        }

        public static implicit operator IDWriteInlineObject*(DWriteInlineObject value)
        {
            return value.Pointer;
        }
    }
}