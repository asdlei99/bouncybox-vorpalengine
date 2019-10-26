using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIFactory2
    {
        public HResult CreateSwapChainForComposition(
            IUnknown* pDevice,
            DXGI_SWAP_CHAIN_DESC1* pDesc,
            [Optional] IDXGIOutput* pRestrictToOutput,
            out DXGISwapChain1? swapChain)
        {
            IDXGISwapChain1* pSwapChain;
            int hr = Pointer->CreateSwapChainForComposition(pDevice, pDesc, pRestrictToOutput, &pSwapChain);

            swapChain = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISwapChain1(pSwapChain) : null;

            return hr;
        }

        public HResult CreateSwapChainForCoreWindow(
            IUnknown* pDevice,
            IUnknown* pWindow,
            DXGI_SWAP_CHAIN_DESC1* pDesc,
            [Optional] IDXGIOutput* pRestrictToOutput,
            out DXGISwapChain1? swapChain)
        {
            IDXGISwapChain1* pSwapChain;
            int hr = Pointer->CreateSwapChainForCoreWindow(pDevice, pWindow, pDesc, pRestrictToOutput, &pSwapChain);

            swapChain = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISwapChain1(pSwapChain) : null;

            return hr;
        }

        public HResult CreateSwapChainForHwnd(
            IUnknown* pDevice,
            IntPtr hWnd,
            DXGI_SWAP_CHAIN_DESC1* pDesc,
            [Optional] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc,
            [Optional] IDXGIOutput* pRestrictToOutput,
            out DXGISwapChain1? swapChain)
        {
            IDXGISwapChain1* pSwapChain;
            int hr = Pointer->CreateSwapChainForHwnd(pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, &pSwapChain);

            swapChain = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISwapChain1(pSwapChain) : null;

            return hr;
        }

        public HResult GetSharedResourceAdapterLuid(IntPtr hResource, out LUID luid)
        {
            fixed (LUID* pLuid = &luid)
            {
                return Pointer->GetSharedResourceAdapterLuid(hResource, pLuid);
            }
        }

        public HResult RegisterOcclusionStatusEvent(IntPtr hEvent, out uint dwCookie)
        {
            fixed (uint* pDwCookie = &dwCookie)
            {
                return Pointer->RegisterOcclusionStatusEvent(hEvent, pDwCookie);
            }
        }

        public HResult RegisterOcclusionStatusWindow(IntPtr WindowHandle, uint wMsg, out uint dwCookie)
        {
            fixed (uint* pDwCookie = &dwCookie)
            {
                return Pointer->RegisterOcclusionStatusWindow(WindowHandle, wMsg, pDwCookie);
            }
        }

        public HResult RegisterStereoStatusEvent(IntPtr hEvent, out uint dwCookie)
        {
            fixed (uint* pDwCookie = &dwCookie)
            {
                return Pointer->RegisterStereoStatusEvent(hEvent, pDwCookie);
            }
        }

        public HResult RegisterStereoStatusWindow(IntPtr WindowHandle, uint wMsg, out uint dwCookie)
        {
            fixed (uint* pDwCookie = &dwCookie)
            {
                return Pointer->RegisterStereoStatusWindow(WindowHandle, wMsg, pDwCookie);
            }
        }
    }
}