using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGIFactory2" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIFactory2 : DXGIFactory1
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIFactory2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIFactory2(IDXGIFactory2* pointer) : base((IDXGIFactory1*)pointer)
        {
        }

        public new IDXGIFactory2* Pointer => (IDXGIFactory2*)base.Pointer;

        public HResult CreateSwapChainForComposition(
            IUnknown* device,
            DXGI_SWAP_CHAIN_DESC1* desc,
            [Optional] IDXGIOutput* restrictToOutput,
            IDXGISwapChain1** swapChain)
        {
            return Pointer->CreateSwapChainForComposition(device, desc, restrictToOutput, swapChain);
        }

        public HResult CreateSwapChainForCoreWindow(
            IUnknown* device,
            IUnknown* window,
            DXGI_SWAP_CHAIN_DESC1* desc,
            [Optional] IDXGIOutput* restrictToOutput,
            IDXGISwapChain1** swapChain)
        {
            return Pointer->CreateSwapChainForCoreWindow(device, window, desc, restrictToOutput, swapChain);
        }

        public HResult CreateSwapChainForHwnd(
            IUnknown* device,
            IntPtr hWnd,
            DXGI_SWAP_CHAIN_DESC1* desc,
            [Optional] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* fullscreenDesc,
            [Optional] IDXGIOutput* restrictToOutput,
            IDXGISwapChain1** swapChain)
        {
            return Pointer->CreateSwapChainForHwnd(device, hWnd, desc, fullscreenDesc, restrictToOutput, swapChain);
        }

        public HResult GetSharedResourceAdapterLuid(IntPtr hResource, LUID* luid)
        {
            return Pointer->GetSharedResourceAdapterLuid(hResource, luid);
        }

        public bool IsWindowedStereoEnabled()
        {
            return Pointer->IsWindowedStereoEnabled() == TerraFX.Interop.Windows.TRUE;
        }

        public HResult RegisterOcclusionStatusEvent(IntPtr hEvent, uint* cookie)
        {
            return Pointer->RegisterOcclusionStatusEvent(hEvent, cookie);
        }

        public HResult RegisterOcclusionStatusWindow(IntPtr windowHandle, uint msg, uint* cookie)
        {
            return Pointer->RegisterOcclusionStatusWindow(windowHandle, msg, cookie);
        }

        public HResult RegisterStereoStatusEvent(IntPtr hEvent, uint* cookie)
        {
            return Pointer->RegisterStereoStatusEvent(hEvent, cookie);
        }

        public HResult RegisterStereoStatusWindow(IntPtr windowHandle, uint msg, uint* cookie)
        {
            return Pointer->RegisterStereoStatusWindow(windowHandle, msg, cookie);
        }

        public void UnregisterOcclusionStatus(uint cookie)
        {
            Pointer->UnregisterOcclusionStatus(cookie);
        }

        public void UnregisterStereoStatus(uint cookie)
        {
            Pointer->UnregisterStereoStatus(cookie);
        }

        public static implicit operator IDXGIFactory2*(DXGIFactory2 value)
        {
            return value.Pointer;
        }
    }
}