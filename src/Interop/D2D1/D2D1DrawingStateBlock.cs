using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1DrawingStateBlock" /> COM interface.</summary>
    public unsafe partial class D2D1DrawingStateBlock : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DrawingStateBlock" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DrawingStateBlock(ID2D1DrawingStateBlock* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1DrawingStateBlock* Pointer => (ID2D1DrawingStateBlock*)base.Pointer;

        public void GetDescription(D2D1_DRAWING_STATE_DESCRIPTION* stateDescription)
        {
            Pointer->GetDescription(stateDescription);
        }

        public void GetTextRenderingParams(IDWriteRenderingParams** textRenderingParams)
        {
            Pointer->GetTextRenderingParams(textRenderingParams);
        }

        public void SetDescription(D2D1_DRAWING_STATE_DESCRIPTION* stateDescription)
        {
            Pointer->SetDescription(stateDescription);
        }

        public void SetTextRenderingParams([Optional] IDWriteRenderingParams* textRenderingParams)
        {
            Pointer->SetTextRenderingParams(textRenderingParams);
        }

        public static implicit operator ID2D1DrawingStateBlock*(D2D1DrawingStateBlock value)
        {
            return value.Pointer;
        }
    }
}