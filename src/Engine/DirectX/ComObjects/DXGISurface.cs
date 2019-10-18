using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="IDXGISurface" /> COM interface.</summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGISurface : ComObject<IDXGISurface>
    {
        private ComPtr<IDXGISurface> _dxgiSurface;

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="DXGISurface" /> type.</summary>
        public DXGISurface(ComPtr<IDXGISurface> dxgiSurface)
        {
            _dxgiSurface = dxgiSurface;
        }

        /// <inheritdoc />
        public override IDXGISurface* Pointer => _dxgiSurface;

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiSurface.Dispose();

            base.Dispose(disposing);
        }
    }
}