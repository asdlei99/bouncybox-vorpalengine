using System;
using System.Diagnostics.CodeAnalysis;
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

        public HResult GetBuffer(uint buffer, Guid* iid, void** surface)
        {
            return Pointer->GetBuffer(buffer, iid, surface);
        }

        public HResult GetContainingOutput(IDXGIOutput** output)
        {
            return Pointer->GetContainingOutput(output);
        }

        public HResult GetDesc(DXGI_SWAP_CHAIN_DESC* desc)
        {
            return Pointer->GetDesc(desc);
        }

        public HResult GetFrameStatistics(DXGI_FRAME_STATISTICS* stats)
        {
            return Pointer->GetFrameStatistics(stats);
        }

        public HResult GetFullscreenState(out bool? fullscreen, IDXGIOutput** target)
        {
            int iFullscreen;
            int hr = Pointer->GetFullscreenState(&iFullscreen, target);

            fullscreen = TerraFX.Interop.Windows.SUCCEEDED(hr) ? iFullscreen == TerraFX.Interop.Windows.TRUE : (bool?)null;

            return hr;
        }

        public HResult GetLastPresentCount(uint* lastPresentCount)
        {
            return Pointer->GetLastPresentCount(lastPresentCount);
        }

        public HResult Present(uint syncInterval, uint flags)
        {
            return Pointer->Present(syncInterval, flags);
        }

        public HResult ResizeBuffers(uint bufferCount, uint width, uint height, DXGI_FORMAT newFormat, uint swapChainFlags)
        {
            return Pointer->ResizeBuffers(bufferCount, width, height, newFormat, swapChainFlags);
        }

        public HResult ResizeTarget(DXGI_MODE_DESC* newTargetParameters)
        {
            return Pointer->ResizeTarget(newTargetParameters);
        }

        public HResult SetFullscreenState(bool fullscreen, IDXGIOutput* target = null)
        {
            return Pointer->SetFullscreenState(fullscreen ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE, target);
        }

        public static implicit operator IDXGISwapChain*(DXGISwapChain value)
        {
            return value.Pointer;
        }
    }
}