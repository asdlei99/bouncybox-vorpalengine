using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    public unsafe partial class D2D1Effect
    {
        public void GetInput(uint index, out D2D1Image? input)
        {
            ID2D1Image* pInput;

            Pointer->GetInput(index, &pInput);

            input = pInput != null ? new D2D1Image(pInput) : null;
        }

        public D2D1Image GetOutput()
        {
            ID2D1Image* pOutputImage;

            Pointer->GetOutput(&pOutputImage);

            return new D2D1Image(pOutputImage);
        }
    }
}