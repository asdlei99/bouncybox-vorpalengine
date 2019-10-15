using System;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="ID2D1DeviceContext" /> COM interface.
    /// </summary>
    public unsafe class D2D1DeviceContext : ComObject<ID2D1DeviceContext>
    {
        private ComPtr<ID2D1DeviceContext> _d2d1DeviceContext;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="D2D1DeviceContext" /> type.
        /// </summary>
        public D2D1DeviceContext(ComPtr<ID2D1DeviceContext> d2d1DeviceContext)
        {
            _d2d1DeviceContext = d2d1DeviceContext;
        }

        /// <inheritdoc />
        public override ID2D1DeviceContext* Pointer => _d2d1DeviceContext;

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.BeginDraw" />.
        /// </summary>
        public void BeginDraw()
        {
            Pointer->BeginDraw();
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.Clear" />.
        /// </summary>
        public void Clear(DXGI_RGBA* color = null)
        {
            Pointer->Clear(color);
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.CreateBitmapFromDxgiSurface" />.
        /// </summary>
        public D2D1Bitmap1 CreateBitmapFromDxgiSurface(DXGISurface surface, ref D2D1_BITMAP_PROPERTIES1 bitmapProperties)
        {
            var d2d1Bitmap1 = new ComPtr<ID2D1Bitmap1>();

            fixed (D2D1_BITMAP_PROPERTIES1* pBitmapProperties = &bitmapProperties)
            {
                CheckResultHandle(
                    Pointer->CreateBitmapFromDxgiSurface(surface, pBitmapProperties, d2d1Bitmap1.GetAddressOf()),
                    $"Failed to create {nameof(ID2D1Bitmap1)}");
            }

            return new D2D1Bitmap1(d2d1Bitmap1);
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.CreateSolidColorBrush" />.
        /// </summary>
        public D2D1SolidColorBrush CreateSolidColorBrush(DXGI_RGBA color, D2D1_BRUSH_PROPERTIES* brushProperties = null)
        {
            var d2d1SolidColorBrush = new ComPtr<ID2D1SolidColorBrush>();

            CheckResultHandle(
                Pointer->CreateSolidColorBrush(&color, brushProperties, d2d1SolidColorBrush.GetAddressOf()),
                "Failed to create {nameof(ID2D1SolidColorBrush)}.");

            return new D2D1SolidColorBrush(d2d1SolidColorBrush);
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.DrawText" />.
        /// </summary>
        public void DrawText(
            ReadOnlySpan<char> text,
            DWriteTextFormat textFormat,
            D2D_RECT_F layoutRect,
            D2D1Brush d2d1Brush,
            D2D1_DRAW_TEXT_OPTIONS options = D2D1_DRAW_TEXT_OPTIONS.D2D1_DRAW_TEXT_OPTIONS_NONE,
            DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE.DWRITE_MEASURING_MODE_NATURAL)
        {
            fixed (char* pText = text)
            {
                Pointer->DrawText((ushort*)pText, (uint)text.Length, textFormat, &layoutRect, d2d1Brush, options, measuringMode);
            }
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.EndDraw" />.
        /// </summary>
        public int EndDraw()
        {
            return Pointer->EndDraw(null, null);
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.SetTarget" />.
        /// </summary>
        public void SetTarget(D2D1Bitmap1? d2d1Bitmap1)
        {
            Pointer->SetTarget(d2d1Bitmap1 != null ? (ID2D1Image*)d2d1Bitmap1.Pointer : null);
        }

        /// <summary>
        ///     Proxies <see cref="ID2D1DeviceContext.SetTextAntialiasMode" />.
        /// </summary>
        public void SetTextAntialiasMode(D2D1_TEXT_ANTIALIAS_MODE textAntialiasMode)
        {
            Pointer->SetTextAntialiasMode(textAntialiasMode);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d2d1DeviceContext.Dispose();

            base.Dispose(disposing);
        }
    }
}