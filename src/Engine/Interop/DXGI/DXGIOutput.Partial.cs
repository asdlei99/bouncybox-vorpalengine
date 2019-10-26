using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIOutput
    {
        public HResult GetDesc(out DXGI_OUTPUT_DESC desc)
        {
            fixed (DXGI_OUTPUT_DESC* pDesc = &desc)
            {
                return Pointer->GetDesc(pDesc);
            }
        }

        public HResult GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, Span<DXGI_MODE_DESC> desc)
        {
            var numModes = (uint)desc.Length;

            fixed (DXGI_MODE_DESC* pDesc = desc)
            {
                return Pointer->GetDisplayModeList(EnumFormat, Flags, &numModes, pDesc);
            }
        }

        public HResult GetDisplayModeListCount(DXGI_FORMAT EnumFormat, uint Flags, out uint count)
        {
            fixed (uint* pCount = &count)
            {
                return Pointer->GetDisplayModeList(EnumFormat, Flags, pCount, null);
            }
        }

        public HResult GetFrameStatistics(out DXGI_FRAME_STATISTICS stats)
        {
            fixed (DXGI_FRAME_STATISTICS* pStats = &stats)
            {
                return Pointer->GetFrameStatistics(pStats);
            }
        }

        public HResult GetGammaControl(out DXGI_GAMMA_CONTROL array)
        {
            fixed (DXGI_GAMMA_CONTROL* pArray = &array)
            {
                return Pointer->GetGammaControl(pArray);
            }
        }

        public HResult GetGammaControlCapabilities(out DXGI_GAMMA_CONTROL_CAPABILITIES gammaCaps)
        {
            fixed (DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps = &gammaCaps)
            {
                return Pointer->GetGammaControlCapabilities(pGammaCaps);
            }
        }
    }
}