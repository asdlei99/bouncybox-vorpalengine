using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="IDXGIDevice" /> COM interface.</summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGIDevice : ComObject<IDXGIDevice>
    {
        private ComPtr<IDXGIDevice> _dxgiDevice;

        /// <summary>Initializes a new instance of the <see cref="DXGIDevice" /> type.</summary>
        /// <param name="dxgiDevice">A COM pointer to an <see cref="IDXGIDevice" />.</param>
        public DXGIDevice(ComPtr<IDXGIDevice> dxgiDevice)
        {
            _dxgiDevice = dxgiDevice;
        }

        /// <inheritdoc />
        public override IDXGIDevice* Pointer => _dxgiDevice;

        /// <summary>Proxies <see cref="IDXGIDevice.GetAdapter" />.</summary>
        public DXGIAdapter GetAdapter()
        {
            var dxgiAdapter = new ComPtr<IDXGIAdapter>();

            CheckResultHandle(Pointer->GetAdapter(dxgiAdapter.GetAddressOf()), $"Failed to retrieve {nameof(IDXGIAdapter)}.");

            return new DXGIAdapter(dxgiAdapter);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiDevice.Dispose();

            base.Dispose(disposing);
        }
    }
}