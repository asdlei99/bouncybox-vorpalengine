using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIOutput" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIOutput : DXGIObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIOutput" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIOutput(IDXGIOutput* pointer) : base((IDXGIObject*)pointer)
        {
        }

        public new IDXGIOutput* Pointer => (IDXGIOutput*)base.Pointer;

        public HResult GetDesc(DXGI_OUTPUT_DESC* pDesc)
        {
            return Pointer->GetDesc(pDesc);
        }

        public HResult GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
        {
            return Pointer->GetDisplayModeList(EnumFormat, Flags, pNumModes, pDesc);
        }

        public HResult GetDisplaySurfaceData(IDXGISurface* pDestination)
        {
            return Pointer->GetDisplaySurfaceData(pDestination);
        }

        public HResult GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
        {
            return Pointer->GetFrameStatistics(pStats);
        }

        public HResult GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
        {
            return Pointer->GetGammaControl(pArray);
        }

        public HResult GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
        {
            return Pointer->GetGammaControlCapabilities(pGammaCaps);
        }

        public void ReleaseOwnership()
        {
            Pointer->ReleaseOwnership();
        }

        public HResult SetDisplaySurface(IDXGISurface* pScanoutSurface)
        {
            return Pointer->SetDisplaySurface(pScanoutSurface);
        }

        public HResult SetGammaControl(DXGI_GAMMA_CONTROL* pArray)
        {
            return Pointer->SetGammaControl(pArray);
        }

        public HResult TakeOwnership(IUnknown* pDevice, bool Exclusive)
        {
            return Pointer->TakeOwnership(pDevice, Exclusive ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public HResult WaitForVBlank()
        {
            return Pointer->WaitForVBlank();
        }

        public static implicit operator IDXGIOutput*(DXGIOutput value)
        {
            return value.Pointer;
        }
    }
}