using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="ID2D1SolidColorBrush" /> COM interface.
    /// </summary>
    public unsafe class D2D1SolidColorBrush : ComObject<ID2D1SolidColorBrush>
    {
        private readonly D2D1Brush _d2d1Brush;
        private ComPtr<ID2D1SolidColorBrush> _d2d1SolidColorBrush;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="D2D1SolidColorBrush" /> type.
        /// </summary>
        public D2D1SolidColorBrush(ComPtr<ID2D1SolidColorBrush> d2d1SolidColorBrush)
        {
            _d2d1SolidColorBrush = d2d1SolidColorBrush;
            _d2d1Brush = new D2D1Brush(new ComPtr<ID2D1Brush>(_d2d1SolidColorBrush));
        }

        /// <inheritdoc />
        public override ID2D1SolidColorBrush* Pointer => _d2d1SolidColorBrush;

        /// <summary>
        ///     Proxies <see cref="ID2D1SolidColorBrush.GetColor" /> and <see cref="ID2D1SolidColorBrush.SetColor" />.
        /// </summary>
        public DXGI_RGBA Color
        {
            get => Pointer->GetColor();
            set => Pointer->SetColor(&value);
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1Brush.GetOpacity" /> and <see cref="ID2D1Brush.SetOpacity" />.
        /// </summary>
        public float Opacity
        {
            get => _d2d1Brush.Opacity;
            set => _d2d1Brush.Opacity = value;
        }

        /// <summary>
        ///     Casts the object to <see cref="D2D1Brush" />.
        /// </summary>
        public static implicit operator D2D1Brush(D2D1SolidColorBrush d2d1SolidColorBrush)
        {
            return d2d1SolidColorBrush._d2d1Brush;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d2d1SolidColorBrush.Dispose();
            _d2d1Brush.Dispose();

            base.Dispose(disposing);
        }
    }
}