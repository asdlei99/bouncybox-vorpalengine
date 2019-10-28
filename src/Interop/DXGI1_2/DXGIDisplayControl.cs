using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI1_2
{
    /// <summary>Proxies the <see cref="IDXGIDisplayControl" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class DXGIDisplayControl : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIDisplayControl" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIDisplayControl(IDXGIDisplayControl* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDXGIDisplayControl* Pointer => (IDXGIDisplayControl*)base.Pointer;

        public bool IsStereoEnabled()
        {
            return Pointer->IsStereoEnabled() == TerraFX.Interop.Windows.TRUE;
        }

        public void SetStereoEnabled(bool enabled)
        {
            Pointer->SetStereoEnabled(enabled ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public static implicit operator IDXGIDisplayControl*(DXGIDisplayControl value)
        {
            return value.Pointer;
        }
    }
}