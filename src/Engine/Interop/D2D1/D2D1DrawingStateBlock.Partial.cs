using BouncyBox.VorpalEngine.Engine.Interop.DWrite;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1DrawingStateBlock
    {
        public D2D1_DRAWING_STATE_DESCRIPTION GetDescription()
        {
            D2D1_DRAWING_STATE_DESCRIPTION stateDescription;

            Pointer->GetDescription(&stateDescription);

            return stateDescription;
        }

        public DWriteRenderingParams? GetTextRenderingParams()
        {
            IDWriteRenderingParams* textRenderingParams;

            Pointer->GetTextRenderingParams(&textRenderingParams);

            return textRenderingParams is null ? null : new DWriteRenderingParams(textRenderingParams);
        }
    }
}