using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGISurface1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class DXGISurface1 : DXGISurface
    {
        /// <summary>Initializes a new instance of the <see cref="DXGISurface1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGISurface1(IDXGISurface1* pointer) : base((IDXGISurface*)pointer)
        {
        }

        public new IDXGISurface1* Pointer => (IDXGISurface1*)base.Pointer;

        public HResult GetDC(bool discard, IntPtr* hdc)
        {
            return Pointer->GetDC(discard ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE, hdc);
        }

        public HResult ReleaseDC(RECT* dirtyRect = null)
        {
            return Pointer->ReleaseDC(dirtyRect);
        }

        public static implicit operator IDXGISurface1*(DXGISurface1 value)
        {
            return value.Pointer;
        }
    }
}