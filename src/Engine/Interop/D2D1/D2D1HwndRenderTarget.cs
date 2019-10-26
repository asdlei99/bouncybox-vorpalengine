using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1HwndRenderTarget" /> COM interface.</summary>
    public unsafe class D2D1HwndRenderTarget : D2D1RenderTarget
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1HwndRenderTarget" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1HwndRenderTarget(ID2D1HwndRenderTarget* pointer) : base((ID2D1RenderTarget*)pointer)
        {
        }

        public new ID2D1HwndRenderTarget* Pointer => (ID2D1HwndRenderTarget*)base.Pointer;

        public D2D1_WINDOW_STATE CheckWindowState()
        {
            return Pointer->CheckWindowState();
        }

        public IntPtr GetHwnd()
        {
            return Pointer->GetHwnd();
        }

        public HResult Resize(D2D_SIZE_U* pixelSize)
        {
            return Pointer->Resize(pixelSize);
        }

        public static implicit operator ID2D1HwndRenderTarget*(D2D1HwndRenderTarget value)
        {
            return value.Pointer;
        }
    }
}