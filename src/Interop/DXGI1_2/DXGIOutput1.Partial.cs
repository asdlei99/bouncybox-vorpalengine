using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIOutput1
    {
        public HResult FindClosestMatchingMode1(DXGI_MODE_DESC1* modeToMatch, out DXGI_MODE_DESC1 closestMatch, IUnknown* concernedDevice = null)
        {
            fixed (DXGI_MODE_DESC1* pClosestMatch = &closestMatch)
            {
                return Pointer->FindClosestMatchingMode1(modeToMatch, pClosestMatch, concernedDevice);
            }
        }

        public HResult GetDisplayModeList1(DXGI_FORMAT enumFormat, uint flags, Span<DXGI_MODE_DESC1> desc)
        {
            var numModes = (uint)desc.Length;

            fixed (DXGI_MODE_DESC1* pDesc = desc)
            {
                return Pointer->GetDisplayModeList1(enumFormat, flags, &numModes, pDesc);
            }
        }

        public HResult GetDisplayModeList1Count(DXGI_FORMAT enumFormat, uint flags, out uint count)
        {
            fixed (uint* pCount = &count)
            {
                return Pointer->GetDisplayModeList1(enumFormat, flags, pCount, null);
            }
        }
    }
}