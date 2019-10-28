using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1Factory" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class D2D1Factory : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Factory" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Factory(ID2D1Factory* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1Factory* Pointer => (ID2D1Factory*)base.Pointer;

        public HResult CreateDCRenderTarget(D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1DCRenderTarget** dcRenderTarget)
        {
            return Pointer->CreateDCRenderTarget(renderTargetProperties, dcRenderTarget);
        }

        public HResult CreateDrawingStateBlock(
            [Optional] D2D1_DRAWING_STATE_DESCRIPTION* drawingStateDescription,
            [Optional] IDWriteRenderingParams* textRenderingParams,
            ID2D1DrawingStateBlock** drawingStateBlock)
        {
            return Pointer->CreateDrawingStateBlock(drawingStateDescription, textRenderingParams, drawingStateBlock);
        }

        public HResult CreateDrawingStateBlock(D2D1_DRAWING_STATE_DESCRIPTION* drawingStateDescription, ID2D1DrawingStateBlock** drawingStateBlock)
        {
            return Pointer->CreateDrawingStateBlock(drawingStateDescription, drawingStateBlock);
        }

        public HResult CreateDrawingStateBlock(ID2D1DrawingStateBlock** drawingStateBlock)
        {
            return Pointer->CreateDrawingStateBlock(drawingStateBlock);
        }

        public HResult CreateDxgiSurfaceRenderTarget(
            IDXGISurface* dxgiSurface,
            D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            ID2D1RenderTarget** renderTarget)
        {
            return Pointer->CreateDxgiSurfaceRenderTarget(dxgiSurface, renderTargetProperties, renderTarget);
        }

        public HResult CreateEllipseGeometry(D2D1_ELLIPSE* ellipse, ID2D1EllipseGeometry** ellipseGeometry)
        {
            return Pointer->CreateEllipseGeometry(ellipse, ellipseGeometry);
        }

        public HResult CreateGeometryGroup(D2D1_FILL_MODE fillMode, ID2D1Geometry** geometries, uint geometriesCount, ID2D1GeometryGroup** geometryGroup)
        {
            return Pointer->CreateGeometryGroup(fillMode, geometries, geometriesCount, geometryGroup);
        }

        public HResult CreateHwndRenderTarget(
            D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            D2D1_HWND_RENDER_TARGET_PROPERTIES* hwndRenderTargetProperties,
            ID2D1HwndRenderTarget** hwndRenderTarget)
        {
            return Pointer->CreateHwndRenderTarget(renderTargetProperties, hwndRenderTargetProperties, hwndRenderTarget);
        }

        public HResult CreatePathGeometry(ID2D1PathGeometry** pathGeometry)
        {
            return Pointer->CreatePathGeometry(pathGeometry);
        }

        public HResult CreateRectangleGeometry(D2D_RECT_F* rectangle, ID2D1RectangleGeometry** rectangleGeometry)
        {
            return Pointer->CreateRectangleGeometry(rectangle, rectangleGeometry);
        }

        public HResult CreateRoundedRectangleGeometry(D2D1_ROUNDED_RECT* roundedRectangle, ID2D1RoundedRectangleGeometry** roundedRectangleGeometry)
        {
            return Pointer->CreateRoundedRectangleGeometry(roundedRectangle, roundedRectangleGeometry);
        }

        public HResult CreateStrokeStyle(
            D2D1_STROKE_STYLE_PROPERTIES* strokeStyleProperties,
            [Optional] float* dashes,
            uint dashesCount,
            ID2D1StrokeStyle** strokeStyle)
        {
            return Pointer->CreateStrokeStyle(strokeStyleProperties, dashes, dashesCount, strokeStyle);
        }

        public HResult CreateTransformedGeometry(ID2D1Geometry* sourceGeometry, D2D_MATRIX_3X2_F* transform, ID2D1TransformedGeometry** transformedGeometry)
        {
            return Pointer->CreateTransformedGeometry(sourceGeometry, transform, transformedGeometry);
        }

        public HResult CreateWicBitmapRenderTarget(IWICBitmap* target, D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1RenderTarget** renderTarget)
        {
            return Pointer->CreateWicBitmapRenderTarget(target, renderTargetProperties, renderTarget);
        }

        public void GetDesktopDpi(float* dpiX, float* dpiY)
        {
            Pointer->GetDesktopDpi(dpiX, dpiY);
        }

        public HResult ReloadSystemMetrics()
        {
            return Pointer->ReloadSystemMetrics();
        }

        public static implicit operator ID2D1Factory*(D2D1Factory value)
        {
            return value.Pointer;
        }
    }
}