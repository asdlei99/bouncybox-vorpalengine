using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIFactory2
    {
        public HResult CreateSwapChainForComposition(
            IUnknown* device,
            DXGI_SWAP_CHAIN_DESC1* desc,
            [Optional] IDXGIOutput* restrictToOutput,
            out DXGISwapChain1? swapChain)
        {
            IDXGISwapChain1* pSwapChain;
            int hr = Pointer->CreateSwapChainForComposition(device, desc, restrictToOutput, &pSwapChain);

            swapChain = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISwapChain1(pSwapChain) : null;

            return hr;
        }

        public HResult CreateSwapChainForCoreWindow(
            IUnknown* device,
            IUnknown* window,
            DXGI_SWAP_CHAIN_DESC1* desc,
            [Optional] IDXGIOutput* restrictToOutput,
            out DXGISwapChain1? swapChain)
        {
            IDXGISwapChain1* pSwapChain;
            int hr = Pointer->CreateSwapChainForCoreWindow(device, window, desc, restrictToOutput, &pSwapChain);

            swapChain = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISwapChain1(pSwapChain) : null;

            return hr;
        }

        public HResult CreateSwapChainForHwnd(
            IUnknown* device,
            IntPtr hWnd,
            DXGI_SWAP_CHAIN_DESC1* desc,
            [Optional] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* fullscreenDesc,
            [Optional] IDXGIOutput* restrictToOutput,
            out DXGISwapChain1? swapChain)
        {
            IDXGISwapChain1* pSwapChain;
            int hr = Pointer->CreateSwapChainForHwnd(device, hWnd, desc, fullscreenDesc, restrictToOutput, &pSwapChain);

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

        public HResult RegisterOcclusionStatusEvent(IntPtr hEvent, out uint cookie)
        {
            fixed (uint* pDwCookie = &cookie)
            {
                return Pointer->RegisterOcclusionStatusEvent(hEvent, pDwCookie);
            }
        }

        public HResult RegisterOcclusionStatusWindow(IntPtr windowHandle, uint msg, out uint cookie)
        {
            fixed (uint* pDwCookie = &cookie)
            {
                return Pointer->RegisterOcclusionStatusWindow(windowHandle, msg, pDwCookie);
            }
        }

        public HResult RegisterStereoStatusEvent(IntPtr hEvent, out uint cookie)
        {
            fixed (uint* pDwCookie = &cookie)
            {
                return Pointer->RegisterStereoStatusEvent(hEvent, pDwCookie);
            }
        }

        public HResult RegisterStereoStatusWindow(IntPtr windowHandle, uint msg, out uint dwCookie)
        {
            fixed (uint* pDwCookie = &dwCookie)
            {
                return Pointer->RegisterStereoStatusWindow(windowHandle, msg, pDwCookie);
            }
        }
    }
}