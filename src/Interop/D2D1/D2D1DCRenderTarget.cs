using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1DCRenderTarget" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class D2D1DCRenderTarget : D2D1RenderTarget
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DCRenderTarget" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DCRenderTarget(ID2D1DCRenderTarget* pointer) : base((ID2D1RenderTarget*)pointer)
        {
        }

        public new ID2D1DCRenderTarget* Pointer => (ID2D1DCRenderTarget*)base.Pointer;

        public HResult BindDC(IntPtr hDC, RECT* pSubRect)
        {
            return Pointer->BindDC(hDC, pSubRect);
        }

        public static implicit operator ID2D1DCRenderTarget*(D2D1DCRenderTarget value)
        {
            return value.Pointer;
        }
    }
}