using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGIOutput1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIOutput1 : DXGIOutput
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIOutput1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIOutput1(IDXGIOutput1* pointer) : base((IDXGIOutput*)pointer)
        {
        }

        public new IDXGIOutput1* Pointer => (IDXGIOutput1*)base.Pointer;

        public HResult DuplicateOutput(IUnknown* device, IDXGIOutputDuplication** outputDuplication)
        {
            return Pointer->DuplicateOutput(device, outputDuplication);
        }

        public HResult FindClosestMatchingMode1(DXGI_MODE_DESC1* modeToMatch, DXGI_MODE_DESC1* closestMatch, IUnknown* concernedDevice = null)
        {
            return Pointer->FindClosestMatchingMode1(modeToMatch, closestMatch, concernedDevice);
        }

        public HResult GetDisplayModeList1(DXGI_FORMAT enumFormat, uint flags, uint* numModes, DXGI_MODE_DESC1* desc)
        {
            return Pointer->GetDisplayModeList1(enumFormat, flags, numModes, desc);
        }

        public HResult GetDisplaySurfaceData1(IDXGIResource* destination)
        {
            return Pointer->GetDisplaySurfaceData1(destination);
        }

        public static implicit operator IDXGIOutput1*(DXGIOutput1 value)
        {
            return value.Pointer;
        }
    }
}