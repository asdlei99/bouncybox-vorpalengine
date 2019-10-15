using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="ID2D1Bitmap1" /> COM interface.
    /// </summary>
    public unsafe class D2D1Bitmap1 : ComObject<ID2D1Bitmap1>
    {
        private ComPtr<ID2D1Bitmap1> _d2d1Bitmap1;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="D2D1Bitmap1" /> type.
        /// </summary>
        public D2D1Bitmap1(ComPtr<ID2D1Bitmap1> d2d1Bitmap1)
        {
            _d2d1Bitmap1 = d2d1Bitmap1;
        }

        /// <inheritdoc />
        public override ID2D1Bitmap1* Pointer => _d2d1Bitmap1;

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d2d1Bitmap1.Dispose();

            base.Dispose(disposing);
        }
    }
}