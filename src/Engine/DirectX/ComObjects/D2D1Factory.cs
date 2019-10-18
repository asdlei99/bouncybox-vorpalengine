using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="ID2D1Factory" /> COM interface.</summary>
    public unsafe class D2D1Factory : ComObject<ID2D1Factory>
    {
        private ComPtr<ID2D1Factory> _d2d1Factory;

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="D2D1Factory" /> type.</summary>
        public D2D1Factory(ComPtr<ID2D1Factory> d2d1Factory)
        {
            _d2d1Factory = d2d1Factory;
        }

        /// <inheritdoc />
        public override ID2D1Factory* Pointer => _d2d1Factory;

        /// <summary>Queries the <see cref="ID2D1Factory" /> for <see cref="ID2D1Multithread" />.</summary>
        /// <param name="allowNoInterface">
        ///     A value that determines whether to allow an <see cref="Engine.Interop.Windows.E_NOINTERFACE" />
        ///     HRESULT.
        /// </param>
        public D2D1Multithread? QueryD2D1Multithread(bool allowNoInterface = false)
        {
            ComPtr<ID2D1Multithread> d2d1Multithread;

            _d2d1Factory.CheckedAs(&d2d1Multithread, allowNoInterface);

            return d2d1Multithread.Get() != null ? new D2D1Multithread(d2d1Multithread) : null;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d2d1Factory.Dispose();

            base.Dispose(disposing);
        }
    }
}