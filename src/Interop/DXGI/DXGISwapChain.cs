using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGISwapChain" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISwapChain : DXGIDeviceSubObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGISwapChain" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGISwapChain(IDXGISwapChain* pointer) : base((IDXGIDeviceSubObject*)pointer)
        {
        }

        public new IDXGISwapChain* Pointer => (IDXGISwapChain*)base.Pointer;

        public HResult GetBuffer(uint Buffer, Guid* riid, void** ppSurface)
        {
            return Pointer->GetBuffer(Buffer, riid, ppSurface);
        }

        public HResult GetContainingOutput(IDXGIOutput** ppOutput)
        {
            return Pointer->GetContainingOutput(ppOutput);
        }

        public HResult GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc)
        {
            return Pointer->GetDesc(pDesc);
        }

        public HResult GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
        {
            return Pointer->GetFrameStatistics(pStats);
        }

        public HResult GetFullscreenState(out bool? fullscreen, IDXGIOutput** ppTarget)
        {
            int iFullscreen;
            int hr = Pointer->GetFullscreenState(&iFullscreen, ppTarget);

            fullscreen = TerraFX.Interop.Windows.SUCCEEDED(hr) ? iFullscreen == TerraFX.Interop.Windows.TRUE : (bool?)null;

            return hr;
        }

        public HResult GetLastPresentCount(uint* pLastPresentCount)
        {
            return Pointer->GetLastPresentCount(pLastPresentCount);
        }

        public HResult Present(uint SyncInterval, uint Flags)
        {
            return Pointer->Present(SyncInterval, Flags);
        }

        public HResult ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
        {
            return Pointer->ResizeBuffers(BufferCount, Width, Height, NewFormat, SwapChainFlags);
        }

        public HResult ResizeTarget(DXGI_MODE_DESC* pNewTargetParameters)
        {
            return Pointer->ResizeTarget(pNewTargetParameters);
        }

        public HResult SetFullscreenState(bool Fullscreen, [Optional] IDXGIOutput* pTarget)
        {
            return Pointer->SetFullscreenState(Fullscreen ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE, pTarget);
        }

        public static implicit operator IDXGISwapChain*(DXGISwapChain value)
        {
            return value.Pointer;
        }
    }
}