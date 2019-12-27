using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGISurface" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGISurface : DXGIDeviceSubObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGISurface" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGISurface(IDXGISurface* pointer) : base((IDXGIDeviceSubObject*)pointer)
        {
        }

        public new IDXGISurface* Pointer => (IDXGISurface*)base.Pointer;

        public HResult GetDesc(DXGI_SURFACE_DESC* desc)
        {
            return Pointer->GetDesc(desc);
        }

        public HResult Map(DXGI_MAPPED_RECT* lockedRect, uint mapFlags)
        {
            return Pointer->Map(lockedRect, mapFlags);
        }

        public HResult Unmap()
        {
            return Pointer->Unmap();
        }

        public static implicit operator IDXGISurface*(DXGISurface value)
        {
            return value.Pointer;
        }
    }
}