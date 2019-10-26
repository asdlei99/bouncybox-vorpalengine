using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIOutput1
    {
        public HResult FindClosestMatchingMode1(DXGI_MODE_DESC1* pModeToMatch, out DXGI_MODE_DESC1 closestMatch, [Optional] IUnknown* pConcernedDevice)
        {
            fixed (DXGI_MODE_DESC1* pClosestMatch = &closestMatch)
            {
                return Pointer->FindClosestMatchingMode1(pModeToMatch, pClosestMatch, pConcernedDevice);
            }
        }

        public HResult GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, out ReadOnlySpan<DXGI_MODE_DESC1> desc)
        {
            uint numModes;
            int hr = Pointer->GetDisplayModeList1(EnumFormat, Flags, &numModes, null);

            desc = default;

            // ReSharper disable once InvertIf
            if (TerraFX.Interop.Windows.SUCCEEDED(hr))
            {
                var descArray = new DXGI_MODE_DESC1[checked((int)numModes)];

                fixed (DXGI_MODE_DESC1* pDescs = descArray)
                {
                    hr = Pointer->GetDisplayModeList1(EnumFormat, Flags, &numModes, pDescs);
                }

                if (TerraFX.Interop.Windows.SUCCEEDED(hr))
                {
                    desc = descArray;
                }
            }

            return hr;
        }
    }
}