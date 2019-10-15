using System;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="IDXGIDebug1" /> COM interface.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGIDebug1 : ComObject<IDXGIDebug1>
    {
        private ComPtr<IDXGIDebug1> _dxgiDebug1;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DXGIDebug1" /> type.
        /// </summary>
        /// <exception cref="DirectXException">Thrown when a <see cref="IDXGIDebug1" /> could not be created.</exception>
        public DXGIDebug1()
        {
            Guid iid = DXGIDebug.IID_IDXGIDebug1;

            fixed (ComPtr<IDXGIDebug1>* _ = &_dxgiDebug1)
            {
                CheckResultHandle(DXGIDebug.DXGIGetDebugInterface(&iid, (void**)_dxgiDebug1.GetAddressOf()), $"Failed to create {nameof(IDXGIDebug1)}.");
            }
        }

        /// <inheritdoc />
        public override IDXGIDebug1* Pointer => _dxgiDebug1;

        /// <summary>
        ///     Proxies <see cref="IDXGIDebug1.ReportLiveObjects" />.
        /// </summary>
        public void ReportLiveObjects(Guid apiid, DXGI_DEBUG_RLO_FLAGS flags)
        {
            CheckResultHandle(Pointer->ReportLiveObjects(apiid, flags), "Failed to report live objects.");
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiDebug1.Dispose();

            base.Dispose(disposing);
        }
    }
}