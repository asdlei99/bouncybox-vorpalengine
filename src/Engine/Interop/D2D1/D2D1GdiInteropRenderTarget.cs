using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1GdiInteropRenderTarget" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class D2D1GdiInteropRenderTarget : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GdiInteropRenderTarget" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GdiInteropRenderTarget(ID2D1GdiInteropRenderTarget* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1GdiInteropRenderTarget* Pointer => (ID2D1GdiInteropRenderTarget*)base.Pointer;

        public HResult GetDC(D2D1_DC_INITIALIZE_MODE mode, IntPtr* hdc)
        {
            return Pointer->GetDC(mode, hdc);
        }

        public HResult ReleaseDC([Optional] RECT* update)
        {
            return Pointer->ReleaseDC(update);
        }

        public static implicit operator ID2D1GdiInteropRenderTarget*(D2D1GdiInteropRenderTarget value)
        {
            return value.Pointer;
        }
    }
}