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

        public HResult GetDesc(DXGI_OUTPUT_DESC* desc)
        {
            return Pointer->GetDesc(desc);
        }

        public HResult GetDisplayModeList(DXGI_FORMAT enumFormat, uint flags, uint* numModes, DXGI_MODE_DESC* desc)
        {
            return Pointer->GetDisplayModeList(enumFormat, flags, numModes, desc);
        }

        public HResult GetDisplaySurfaceData(IDXGISurface* destination)
        {
            return Pointer->GetDisplaySurfaceData(destination);
        }

        public HResult GetFrameStatistics(DXGI_FRAME_STATISTICS* stats)
        {
            return Pointer->GetFrameStatistics(stats);
        }

        public HResult GetGammaControl(DXGI_GAMMA_CONTROL* array)
        {
            return Pointer->GetGammaControl(array);
        }

        public HResult GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* gammaCaps)
        {
            return Pointer->GetGammaControlCapabilities(gammaCaps);
        }

        public void ReleaseOwnership()
        {
            Pointer->ReleaseOwnership();
        }

        public HResult SetDisplaySurface(IDXGISurface* scanoutSurface)
        {
            return Pointer->SetDisplaySurface(scanoutSurface);
        }

        public HResult SetGammaControl(DXGI_GAMMA_CONTROL* array)
        {
            return Pointer->SetGammaControl(array);
        }

        public HResult TakeOwnership(IUnknown* device, bool exclusive)
        {
            return Pointer->TakeOwnership(device, exclusive ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
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