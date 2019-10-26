using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    public unsafe partial class D2D1DrawingStateBlock1
    {
        public D2D1_DRAWING_STATE_DESCRIPTION1 GetDescription1()
        {
            D2D1_DRAWING_STATE_DESCRIPTION1 stateDescription;

            Pointer->GetDescription(&stateDescription);

            return stateDescription;
        }
    }
}