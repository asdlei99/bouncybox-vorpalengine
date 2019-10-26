using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D3D11
{
    /// <summary>Proxies the <see cref="ID3D11DeviceChild" /> COM interface.</summary>
    public unsafe class D3D11DeviceChild : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D3D11DeviceChild" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D3D11DeviceChild(ID3D11DeviceChild* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID3D11DeviceChild* Pointer => (ID3D11DeviceChild*)base.Pointer;

        public static implicit operator ID3D11DeviceChild*(D3D11DeviceChild value)
        {
            return value.Pointer;
        }
    }
}