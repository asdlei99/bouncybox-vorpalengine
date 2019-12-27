using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIFactory" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIFactory : DXGIObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIFactory" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIFactory(IDXGIFactory* pointer) : base((IDXGIObject*)pointer)
        {
        }

        public new IDXGIFactory* Pointer => (IDXGIFactory*)base.Pointer;

        public HResult CreateSoftwareAdapter(IntPtr module, IDXGIAdapter** adapter)
        {
            return Pointer->CreateSoftwareAdapter(module, adapter);
        }

        public HResult CreateSwapChain(IUnknown* device, DXGI_SWAP_CHAIN_DESC* desc, IDXGISwapChain** swapChain)
        {
            return Pointer->CreateSwapChain(device, desc, swapChain);
        }

        public HResult EnumAdapters(uint adapterIndex, IDXGIAdapter** adapter)
        {
            return Pointer->EnumAdapters(adapterIndex, adapter);
        }

        public HResult GetWindowAssociation(IntPtr* windowHandle)
        {
            return Pointer->GetWindowAssociation(windowHandle);
        }

        public HResult MakeWindowAssociation(IntPtr windowHandle, uint flags)
        {
            return Pointer->MakeWindowAssociation(windowHandle, flags);
        }

        public static implicit operator IDXGIFactory*(DXGIFactory value)
        {
            return value.Pointer;
        }
    }
}