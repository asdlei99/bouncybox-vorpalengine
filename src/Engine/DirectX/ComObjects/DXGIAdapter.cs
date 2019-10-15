using System;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="IDXGIAdapter" /> COM interface.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGIAdapter : ComObject<IDXGIAdapter>
    {
        private ComPtr<IDXGIAdapter> _dxgiAdapter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DXGIAdapter" /> type.
        /// </summary>
        /// <param name="dxgiAdapter">A COM pointer to an <see cref="IDXGIAdapter" />.</param>
        public DXGIAdapter(ComPtr<IDXGIAdapter> dxgiAdapter)
        {
            _dxgiAdapter = dxgiAdapter;
        }

        /// <inheritdoc />
        public override IDXGIAdapter* Pointer => _dxgiAdapter;

        /// <summary>
        ///     Proxies <see cref="IDXGIAdapter.GetDesc" />.
        /// </summary>
        public DXGI_ADAPTER_DESC GetDesc()
        {
            DXGI_ADAPTER_DESC desc;

            CheckResultHandle(Pointer->GetDesc(&desc), "Failed to get adapter description.");

            return desc;
        }

        /// <summary>
        ///     Proxies <see cref="IDXGIAdapter.GetParent" />.
        /// </summary>
        /// <param name="allowNoInterface">
        ///     A value that determines whether to allow an <see cref="Engine.Interop.Windows.E_NOINTERFACE" /> HRESULT.
        /// </param>
        // ReSharper disable once InconsistentNaming
        public DXGIFactory2? GetParentDXGIFactory2(bool allowNoInterface = false)
        {
            var dxgiFactory2 = new ComPtr<IDXGIFactory2>();
            Guid iid = DXGI.IID_IDXGIFactory2;

            CheckResultHandle(
                Pointer->GetParent(&iid, (void**)dxgiFactory2.GetAddressOf()),
                $"Failed to retrieve parent {nameof(IDXGIFactory2)}.",
                allowNoInterface);

            return new DXGIFactory2(dxgiFactory2);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiAdapter.Dispose();

            base.Dispose(disposing);
        }
    }
}