using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="ID3D11DeviceContext" /> COM interface.
    /// </summary>
    public unsafe class D3D11DeviceContext : ComObject<ID3D11DeviceContext>
    {
        private ComPtr<ID3D11DeviceContext> _d3d11DeviceContext;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="D3D11DeviceContext" /> type.
        /// </summary>
        public D3D11DeviceContext(ComPtr<ID3D11DeviceContext> d3d11DeviceContext)
        {
            _d3d11DeviceContext = d3d11DeviceContext;
        }

        /// <inheritdoc />
        public override ID3D11DeviceContext* Pointer => _d3d11DeviceContext;

        /// <summary>
        ///     Proxies <see cref="ID3D11DeviceContext.ClearState" />.
        /// </summary>
        public void ClearState()
        {
            Pointer->ClearState();
        }

        /// <summary>
        ///     Proxies <see cref="ID3D11DeviceContext.Flush" />.
        /// </summary>
        public void Flush()
        {
            Pointer->Flush();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d3d11DeviceContext.Dispose();

            base.Dispose(disposing);
        }
    }
}