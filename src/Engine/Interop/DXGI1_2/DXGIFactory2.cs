using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
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
            IUnknown* pDevice,
            DXGI_SWAP_CHAIN_DESC1* pDesc,
            [Optional] IDXGIOutput* pRestrictToOutput,
            IDXGISwapChain1** ppSwapChain)
        {
            return Pointer->CreateSwapChainForComposition(pDevice, pDesc, pRestrictToOutput, ppSwapChain);
        }

        public HResult CreateSwapChainForCoreWindow(
            IUnknown* pDevice,
            IUnknown* pWindow,
            DXGI_SWAP_CHAIN_DESC1* pDesc,
            [Optional] IDXGIOutput* pRestrictToOutput,
            IDXGISwapChain1** ppSwapChain)
        {
            return Pointer->CreateSwapChainForCoreWindow(pDevice, pWindow, pDesc, pRestrictToOutput, ppSwapChain);
        }

        public HResult CreateSwapChainForHwnd(
            IUnknown* pDevice,
            IntPtr hWnd,
            DXGI_SWAP_CHAIN_DESC1* pDesc,
            [Optional] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc,
            [Optional] IDXGIOutput* pRestrictToOutput,
            IDXGISwapChain1** ppSwapChain)
        {
            return Pointer->CreateSwapChainForHwnd(pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, ppSwapChain);
        }

        public HResult GetSharedResourceAdapterLuid(IntPtr hResource, LUID* pLuid)
        {
            return Pointer->GetSharedResourceAdapterLuid(hResource, pLuid);
        }

        public bool IsWindowedStereoEnabled()
        {
            return Pointer->IsWindowedStereoEnabled() == TerraFX.Interop.Windows.TRUE;
        }

        public HResult RegisterOcclusionStatusEvent(IntPtr hEvent, uint* pdwCookie)
        {
            return Pointer->RegisterOcclusionStatusEvent(hEvent, pdwCookie);
        }

        public HResult RegisterOcclusionStatusWindow(IntPtr WindowHandle, uint wMsg, uint* pdwCookie)
        {
            return Pointer->RegisterOcclusionStatusWindow(WindowHandle, wMsg, pdwCookie);
        }

        public HResult RegisterStereoStatusEvent(IntPtr hEvent, uint* pdwCookie)
        {
            return Pointer->RegisterStereoStatusEvent(hEvent, pdwCookie);
        }

        public HResult RegisterStereoStatusWindow(IntPtr WindowHandle, uint wMsg, uint* pdwCookie)
        {
            return Pointer->RegisterStereoStatusWindow(WindowHandle, wMsg, pdwCookie);
        }

        public void UnregisterOcclusionStatus(uint dwCookie)
        {
            Pointer->UnregisterOcclusionStatus(dwCookie);
        }

        public void UnregisterStereoStatus(uint dwCookie)
        {
            Pointer->UnregisterStereoStatus(dwCookie);
        }

        public static implicit operator IDXGIFactory2*(DXGIFactory2 value)
        {
            return value.Pointer;
        }
    }
}