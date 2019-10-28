using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D3D11
{
    /// <summary>Proxies the <see cref="ID3D11Device" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class D3D11Device : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D3D11Device" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D3D11Device(ID3D11Device* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID3D11Device* Pointer => (ID3D11Device*)base.Pointer;

        public static implicit operator ID3D11Device*(D3D11Device value)
        {
            return value.Pointer;
        }
    }
}