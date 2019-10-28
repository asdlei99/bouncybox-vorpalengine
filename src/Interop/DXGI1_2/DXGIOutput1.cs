using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
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

        public HResult DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
        {
            return Pointer->DuplicateOutput(pDevice, ppOutputDuplication);
        }

        public HResult FindClosestMatchingMode1(DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, [Optional] IUnknown* pConcernedDevice)
        {
            return Pointer->FindClosestMatchingMode1(pModeToMatch, pClosestMatch, pConcernedDevice);
        }

        public HResult GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc)
        {
            return Pointer->GetDisplayModeList1(EnumFormat, Flags, pNumModes, pDesc);
        }

        public HResult GetDisplaySurfaceData1(IDXGIResource* pDestination)
        {
            return Pointer->GetDisplaySurfaceData1(pDestination);
        }

        public static implicit operator IDXGIOutput1*(DXGIOutput1 value)
        {
            return value.Pointer;
        }
    }
}