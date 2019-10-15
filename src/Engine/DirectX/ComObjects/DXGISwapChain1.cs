using System;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="IDXGISwapChain1" /> COM interface.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGISwapChain1 : ComObject<IDXGISwapChain1>
    {
        private ComPtr<IDXGISwapChain1> _dxgiSwapChain1;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="DXGISwapChain1" /> type.
        /// </summary>
        public DXGISwapChain1(ComPtr<IDXGISwapChain1> dxgiSwapChain1)
        {
            _dxgiSwapChain1 = dxgiSwapChain1;
        }

        /// <inheritdoc />
        public override IDXGISwapChain1* Pointer => _dxgiSwapChain1;

        /// <summary>
        ///     Proxies <see cref="IDXGISwapChain1.GetBuffer" />.
        /// </summary>
        public DXGISurface GetBuffer(uint buffer)
        {
            var dxgiSurface = new ComPtr<IDXGISurface>();
            Guid iid = DXGI.IID_IDXGISurface;

            CheckResultHandle(Pointer->GetBuffer(buffer, &iid, (void**)dxgiSurface.GetAddressOf()), $"Failed to get {nameof(IDXGISurface)}.");

            return new DXGISurface(dxgiSurface);
        }

        /// <summary>
        ///     Proxies <see cref="IDXGISwapChain1.GetContainingOutput" />.
        /// </summary>
        public DXGIOutput GetContainingOutput()
        {
            var dxgiOutput = new ComPtr<IDXGIOutput>();

            CheckResultHandle(Pointer->GetContainingOutput(dxgiOutput.GetAddressOf()), $"Failed to get {nameof(IDXGIOutput1)}.");

            return new DXGIOutput(dxgiOutput);
        }

        /// <summary>
        ///     Proxies <see cref="IDXGISwapChain1.Present" />.
        /// </summary>
        public void Present(uint syncInterval)
        {
            CheckResultHandle(Pointer->Present(syncInterval, 0), "Failed to present.");
        }

        /// <summary>
        ///     Proxies <see cref="IDXGISwapChain1.ResizeBuffers" />.
        /// </summary>
        public void ResizeBuffers(DXGI_FORMAT format = DXGI_FORMAT.DXGI_FORMAT_UNKNOWN)
        {
            CheckResultHandle(Pointer->ResizeBuffers(0, 0, 0, format, 0), "Failed to resize buffers.");
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiSwapChain1.Dispose();

            base.Dispose(disposing);
        }
    }
}