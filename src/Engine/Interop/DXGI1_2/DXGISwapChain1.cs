using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI1_2
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

        public HResult GetBackgroundColor(DXGI_RGBA* pColor)
        {
            return Pointer->GetBackgroundColor(pColor);
        }

        public HResult GetCoreWindow(Guid* refiid, void** ppUnk)
        {
            return Pointer->GetCoreWindow(refiid, ppUnk);
        }

        public HResult GetDesc1(DXGI_SWAP_CHAIN_DESC1* pDesc)
        {
            return Pointer->GetDesc1(pDesc);
        }

        public HResult GetFullscreenDesc(DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc)
        {
            return Pointer->GetFullscreenDesc(pDesc);
        }

        public HResult GetHwnd(IntPtr* pHwnd)
        {
            return Pointer->GetHwnd(pHwnd);
        }

        public HResult GetRestrictToOutput(IDXGIOutput** ppRestrictToOutput)
        {
            return Pointer->GetRestrictToOutput(ppRestrictToOutput);
        }

        public HResult GetRotation(DXGI_MODE_ROTATION* pRotation)
        {
            return Pointer->GetRotation(pRotation);
        }

        public bool IsTemporaryMonoSupported()
        {
            return Pointer->IsTemporaryMonoSupported() == TerraFX.Interop.Windows.TRUE;
        }

        public HResult Present1(uint SyncInterval, uint PresentFlags, DXGI_PRESENT_PARAMETERS* pPresentParameters)
        {
            return Pointer->Present1(SyncInterval, PresentFlags, pPresentParameters);
        }

        public HResult SetBackgroundColor(DXGI_RGBA* pColor)
        {
            return Pointer->SetBackgroundColor(pColor);
        }

        public HResult SetRotation(DXGI_MODE_ROTATION Rotation)
        {
            return Pointer->SetRotation(Rotation);
        }

        public static implicit operator IDXGISwapChain1*(DXGISwapChain1 value)
        {
            return value.Pointer;
        }
    }
}