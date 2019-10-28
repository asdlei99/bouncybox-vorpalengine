using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1DrawingStateBlock1" /> COM interface.</summary>
    public unsafe partial class D2D1DrawingStateBlock1 : D2D1DrawingStateBlock
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DrawingStateBlock1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DrawingStateBlock1(ID2D1DrawingStateBlock1* pointer) : base((ID2D1DrawingStateBlock*)pointer)
        {
        }

        public new ID2D1DrawingStateBlock1* Pointer => (ID2D1DrawingStateBlock1*)base.Pointer;

        public void GetDescription1(D2D1_DRAWING_STATE_DESCRIPTION1* stateDescription)
        {
            Pointer->GetDescription(stateDescription);
        }

        public void SetDescription1(D2D1_DRAWING_STATE_DESCRIPTION1* stateDescription)
        {
            Pointer->SetDescription(stateDescription);
        }

        public static implicit operator ID2D1DrawingStateBlock1*(D2D1DrawingStateBlock1 value)
        {
            return value.Pointer;
        }
    }
}