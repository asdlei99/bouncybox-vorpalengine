using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D3D11
{
    /// <summary>Proxies the <see cref="ID3D11DeviceContext" /> COM interface.</summary>
    public unsafe class D3D11DeviceContext : D3D11DeviceChild
    {
        /// <summary>Initializes a new instance of the <see cref="D3D11DeviceContext" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D3D11DeviceContext(ID3D11DeviceContext* pointer) : base((ID3D11DeviceChild*)pointer)
        {
        }

        public new ID3D11DeviceContext* Pointer => (ID3D11DeviceContext*)base.Pointer;

        public void ClearState()
        {
            Pointer->ClearState();
        }

        public void Flush()
        {
            Pointer->Flush();
        }

        public static implicit operator ID3D11DeviceContext*(D3D11DeviceContext value)
        {
            return value.Pointer;
        }
    }
}