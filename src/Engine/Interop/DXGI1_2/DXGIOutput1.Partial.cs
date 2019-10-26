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

        public HResult GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, Span<DXGI_MODE_DESC1> desc)
        {
            var numModes = (uint)desc.Length;

            fixed (DXGI_MODE_DESC1* pDesc = desc)
            {
                return Pointer->GetDisplayModeList1(EnumFormat, Flags, &numModes, pDesc);
            }
        }

        public HResult GetDisplayModeList1Count(DXGI_FORMAT EnumFormat, uint Flags, out uint count)
        {
            fixed (uint* pCount = &count)
            {
                return Pointer->GetDisplayModeList1(EnumFormat, Flags, pCount, null);
            }
        }
    }
}