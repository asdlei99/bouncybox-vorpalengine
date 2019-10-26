using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISwapChain1
    {
        public HResult GetBackgroundColor(out DXGI_RGBA color)
        {
            fixed (DXGI_RGBA* pColor = &color)
            {
                return Pointer->GetBackgroundColor(pColor);
            }
        }

        public HResult GetCoreWindow<T>(out T* pUnk)
            where T : unmanaged
        {
            Guid iid = typeof(T).GUID;

            fixed (T** ppUnk = &pUnk)
            {
                return Pointer->GetCoreWindow(&iid, (void**)ppUnk);
            }
        }

        public HResult GetDesc1(out DXGI_SWAP_CHAIN_DESC1 desc)
        {
            fixed (DXGI_SWAP_CHAIN_DESC1* pDesc = &desc)
            {
                return Pointer->GetDesc1(pDesc);
            }
        }

        public HResult GetFullscreenDesc(out DXGI_SWAP_CHAIN_FULLSCREEN_DESC desc)
        {
            fixed (DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc = &desc)
            {
                return Pointer->GetFullscreenDesc(pDesc);
            }
        }

        public HResult GetHwnd(out IntPtr hwnd)
        {
            fixed (IntPtr* pHwnd = &hwnd)
            {
                return Pointer->GetHwnd(pHwnd);
            }
        }

        public HResult GetRestrictToOutput(out DXGIOutput? restrictToOutput)
        {
            IDXGIOutput* pRestrictToOutput;
            int hr = Pointer->GetRestrictToOutput(&pRestrictToOutput);

            restrictToOutput = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIOutput(pRestrictToOutput) : null;

            return hr;
        }

        public HResult GetRotation(out DXGI_MODE_ROTATION rotation)
        {
            fixed (DXGI_MODE_ROTATION* pRotation = &rotation)
            {
                return Pointer->GetRotation(pRotation);
            }
        }
    }
}