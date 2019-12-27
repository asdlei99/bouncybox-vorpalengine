using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    public unsafe partial class D2D1SvgAttribute
    {
        public HResult Clone(out D2D1SvgAttribute? attribute)
        {
            ID2D1SvgAttribute* pAttribute;
            int hr = Pointer->Clone(&pAttribute);

            attribute = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgAttribute(pAttribute) : null;

            return hr;
        }
    }
}