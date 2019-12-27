using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISwapChain
    {
        public HResult GetBuffer<T>(uint buffer, out T* surface)
            where T : unmanaged
        {
            Guid iid = typeof(T).GUID;

            fixed (T** ppSurface = &surface)
            {
                return Pointer->GetBuffer(buffer, &iid, (void**)ppSurface);
            }
        }

        public HResult GetContainingOutput(out DXGIOutput? output)
        {
            IDXGIOutput* pOutput;
            int hr = Pointer->GetContainingOutput(&pOutput);

            output = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIOutput(pOutput) : null;

            return hr;
        }

        public HResult GetDesc(out DXGI_SWAP_CHAIN_DESC desc)
        {
            fixed (DXGI_SWAP_CHAIN_DESC* pDesc = &desc)
            {
                return Pointer->GetDesc(pDesc);
            }
        }

        public HResult GetFrameStatistics(out DXGI_FRAME_STATISTICS stats)
        {
            fixed (DXGI_FRAME_STATISTICS* pStats = &stats)
            {
                return Pointer->GetFrameStatistics(pStats);
            }
        }

        public HResult GetFullscreenState(out bool? fullscreen, out DXGIOutput? target)
        {
            int iFullscreen;
            IDXGIOutput* pTarget;
            int hr = Pointer->GetFullscreenState(&iFullscreen, &pTarget);
            bool succeeded = TerraFX.Interop.Windows.SUCCEEDED(hr);

            fullscreen = succeeded ? iFullscreen == TerraFX.Interop.Windows.TRUE : (bool?)null;
            target = succeeded ? new DXGIOutput(pTarget) : null;

            return hr;
        }

        public HResult GetLastPresentCount(out uint lastPresentCount)
        {
            fixed (uint* pLastPresentCount = &lastPresentCount)
            {
                return Pointer->GetLastPresentCount(pLastPresentCount);
            }
        }
    }
}