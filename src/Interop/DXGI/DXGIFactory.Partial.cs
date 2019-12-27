using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIFactory
    {
        public HResult CreateSoftwareAdapter(IntPtr module, out DXGIAdapter? adapter)
        {
            IDXGIAdapter* pAdapter;
            int hr = Pointer->CreateSoftwareAdapter(module, &pAdapter);

            adapter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIAdapter(pAdapter) : null;

            return hr;
        }

        public HResult CreateSwapChain(IUnknown* device, DXGI_SWAP_CHAIN_DESC* desc, out DXGISwapChain? swapChain)
        {
            IDXGISwapChain* pSwapChain;
            int hr = Pointer->CreateSwapChain(device, desc, &pSwapChain);

            swapChain = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISwapChain(pSwapChain) : null;

            return hr;
        }

        public HResult EnumAdapters(uint adapterIndex, out DXGIAdapter? adapter)
        {
            IDXGIAdapter* pAdapter;
            int hr = Pointer->EnumAdapters(adapterIndex, &pAdapter);

            adapter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIAdapter(pAdapter) : null;

            return hr;
        }

        public HResult GetWindowAssociation(out IntPtr windowHandle)
        {
            fixed (IntPtr* pWindowHandle = &windowHandle)
            {
                return Pointer->GetWindowAssociation(pWindowHandle);
            }
        }
    }
}