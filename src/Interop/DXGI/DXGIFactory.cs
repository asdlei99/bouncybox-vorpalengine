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

        public HResult CreateSoftwareAdapter(IntPtr Module, IDXGIAdapter** ppAdapter)
        {
            return Pointer->CreateSoftwareAdapter(Module, ppAdapter);
        }

        public HResult CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
        {
            return Pointer->CreateSwapChain(pDevice, pDesc, ppSwapChain);
        }

        public HResult EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
        {
            return Pointer->EnumAdapters(Adapter, ppAdapter);
        }

        public HResult GetWindowAssociation(IntPtr* pWindowHandle)
        {
            return Pointer->GetWindowAssociation(pWindowHandle);
        }

        public HResult MakeWindowAssociation(IntPtr WindowHandle, uint Flags)
        {
            return Pointer->MakeWindowAssociation(WindowHandle, Flags);
        }

        public static implicit operator IDXGIFactory*(DXGIFactory value)
        {
            return value.Pointer;
        }
    }
}