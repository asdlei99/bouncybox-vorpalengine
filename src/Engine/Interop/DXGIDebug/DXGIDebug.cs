using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGIDebug
{
    /// <summary>Proxies the <see cref="IDXGIDebug" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDebug : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIDebug" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIDebug(IDXGIDebug* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDXGIDebug* Pointer => (IDXGIDebug*)base.Pointer;

        public HResult ReportLiveObjects(Guid apiid, DXGI_DEBUG_RLO_FLAGS flags)
        {
            return Pointer->ReportLiveObjects(apiid, flags);
        }

        public static implicit operator IDXGIDebug*(DXGIDebug value)
        {
            return value.Pointer;
        }
    }
}