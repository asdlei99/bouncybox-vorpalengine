using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1SolidColorBrush" /> COM interface.</summary>
    public unsafe class D2D1SolidColorBrush : D2D1Brush
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SolidColorBrush" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SolidColorBrush(ID2D1SolidColorBrush* pointer) : base((ID2D1Brush*)pointer)
        {
        }

        public new ID2D1SolidColorBrush* Pointer => (ID2D1SolidColorBrush*)base.Pointer;

        public DXGI_RGBA GetColor()
        {
            return Pointer->GetColor();
        }

        public void SetColor(DXGI_RGBA* color)
        {
            Pointer->SetColor(color);
        }

        public static implicit operator ID2D1SolidColorBrush*(D2D1SolidColorBrush value)
        {
            return value.Pointer;
        }
    }
}