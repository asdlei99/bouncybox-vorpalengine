using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <inheritdoc />
    public unsafe class D2D1Brush : ComObject<ID2D1Brush>
    {
        private ComPtr<ID2D1Brush> _d2d1Brush;

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="D2D1Brush" /> type.</summary>
        public D2D1Brush(ComPtr<ID2D1Brush> d2d1Brush)
        {
            _d2d1Brush = d2d1Brush;
        }

        /// <inheritdoc />
        public override ID2D1Brush* Pointer => _d2d1Brush;

        /// <summary>Proxies <see cref="ID2D1Brush.GetOpacity" /> and <see cref="ID2D1Brush.SetOpacity" />.</summary>
        public float Opacity
        {
            get => Pointer->GetOpacity();
            set => Pointer->SetOpacity(value);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d2d1Brush.Dispose();

            base.Dispose(disposing);
        }
    }
}