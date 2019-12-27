using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    public unsafe partial class EnumString
    {
        public HResult Clone(out EnumString? @enum)
        {
            IEnumString* pEnumString;
            int hr = Pointer->Clone(&pEnumString);

            @enum = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new EnumString(pEnumString) : null;

            return hr;
        }
    }
}