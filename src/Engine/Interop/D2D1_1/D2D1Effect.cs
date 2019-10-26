using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1Effect" /> COM interface.</summary>
    public unsafe partial class D2D1Effect : D2D1Properties
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Effect" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Effect(ID2D1Effect* pointer) : base((ID2D1Properties*)pointer)
        {
        }

        public new ID2D1Effect* Pointer => (ID2D1Effect*)base.Pointer;

        public void GetInput(uint index, ID2D1Image** input)
        {
            Pointer->GetInput(index, input);
        }

        public uint GetInputCount()
        {
            return Pointer->GetInputCount();
        }

        public void GetOutput(ID2D1Image** outputImage)
        {
            Pointer->GetOutput(outputImage);
        }

        public void SetInput(uint index, [Optional] ID2D1Image* input, bool invalidate)
        {
            Pointer->SetInput(index, input, invalidate ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public HResult SetInputCount(uint inputCount)
        {
            return Pointer->SetInputCount(inputCount);
        }

        public void SetInputEffect(uint index, [Optional] ID2D1Effect* inputEffect, bool invalidate)
        {
            Pointer->SetInputEffect(index, inputEffect, invalidate ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public static implicit operator ID2D1Effect*(D2D1Effect value)
        {
            return value.Pointer;
        }
    }
}