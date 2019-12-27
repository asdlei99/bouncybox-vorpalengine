using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGISwapChain1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISwapChain1 : DXGISwapChain
    {
        /// <summary>Initializes a new instance of the <see cref="DXGISwapChain1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGISwapChain1(IDXGISwapChain1* pointer) : base((IDXGISwapChain*)pointer)
        {
        }

        public new IDXGISwapChain1* Pointer => (IDXGISwapChain1*)base.Pointer;

        public HResult GetBackgroundColor(DXGI_RGBA* color)
        {
            return Pointer->GetBackgroundColor(color);
        }

        public HResult GetCoreWindow(Guid* iid, void** unk)
        {
            return Pointer->GetCoreWindow(iid, unk);
        }

        public HResult GetDesc1(DXGI_SWAP_CHAIN_DESC1* desc)
        {
            return Pointer->GetDesc1(desc);
        }

        public HResult GetFullscreenDesc(DXGI_SWAP_CHAIN_FULLSCREEN_DESC* desc)
        {
            return Pointer->GetFullscreenDesc(desc);
        }

        public HResult GetHwnd(IntPtr* hwnd)
        {
            return Pointer->GetHwnd(hwnd);
        }

        public HResult GetRestrictToOutput(IDXGIOutput** restrictToOutput)
        {
            return Pointer->GetRestrictToOutput(restrictToOutput);
        }

        public HResult GetRotation(DXGI_MODE_ROTATION* rotation)
        {
            return Pointer->GetRotation(rotation);
        }

        public bool IsTemporaryMonoSupported()
        {
            return Pointer->IsTemporaryMonoSupported() == TerraFX.Interop.Windows.TRUE;
        }

        public HResult Present1(uint syncInterval, uint presentFlags, DXGI_PRESENT_PARAMETERS* presentParameters)
        {
            return Pointer->Present1(syncInterval, presentFlags, presentParameters);
        }

        public HResult SetBackgroundColor(DXGI_RGBA* color)
        {
            return Pointer->SetBackgroundColor(color);
        }

        public HResult SetRotation(DXGI_MODE_ROTATION rotation)
        {
            return Pointer->SetRotation(rotation);
        }

        public static implicit operator IDXGISwapChain1*(DXGISwapChain1 value)
        {
            return value.Pointer;
        }
    }
}