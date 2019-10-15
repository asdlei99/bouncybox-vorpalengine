using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="ID2D1Multithread" /> COM interface.
    /// </summary>
    public unsafe class D2D1Multithread : ComObject<ID2D1Multithread>
    {
        private ComPtr<ID2D1Multithread> _d2d1Multithread;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="D2D1Multithread" /> type.
        /// </summary>
        public D2D1Multithread(ComPtr<ID2D1Multithread> d2d1Multithread)
        {
            _d2d1Multithread = d2d1Multithread;
        }

        /// <inheritdoc />
        public override ID2D1Multithread* Pointer => _d2d1Multithread;

        /// <summary>
        ///     Proxies <see cref="ID2D1Multithread.Enter" />.
        /// </summary>
        /// <returns>Returns a <see cref="D2D1DeviceContext" />.</returns>
        public void Enter()
        {
            Pointer->Enter();
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1Multithread.Leave" />.
        /// </summary>
        /// <returns>Returns a <see cref="D2D1DeviceContext" />.</returns>
        public void Leave()
        {
            Pointer->Leave();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d2d1Multithread.Dispose();

            base.Dispose(disposing);
        }
    }
}