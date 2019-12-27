using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    public unsafe partial class EnumUnknown
    {
        public HResult Clone(out EnumUnknown? @enum)
        {
            IEnumUnknown* pEnumUnknown;
            int hr = Pointer->Clone(&pEnumUnknown);

            @enum = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new EnumUnknown(pEnumUnknown) : null;

            return hr;
        }
    }
}