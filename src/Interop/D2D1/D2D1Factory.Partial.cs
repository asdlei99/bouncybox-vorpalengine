using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class D2D1Factory
    {
        public HResult CreateDCRenderTarget(D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, out D2D1DCRenderTarget? dcRenderTarget)
        {
            ID2D1DCRenderTarget* pDcRenderTarget;
            int hr = Pointer->CreateDCRenderTarget(renderTargetProperties, &pDcRenderTarget);

            dcRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DCRenderTarget(pDcRenderTarget) : null;

            return hr;
        }

        public HResult CreateDrawingStateBlock(D2D1_DRAWING_STATE_DESCRIPTION* drawingStateDescription, out D2D1DrawingStateBlock? drawingStateBlock)
        {
            ID2D1DrawingStateBlock* pDrawingStateBlock;
            int hr = Pointer->CreateDrawingStateBlock(drawingStateDescription, &pDrawingStateBlock);

            drawingStateBlock = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DrawingStateBlock(pDrawingStateBlock) : null;

            return hr;
        }

        public HResult CreateDrawingStateBlock(out D2D1DrawingStateBlock? drawingStateBlock)
        {
            ID2D1DrawingStateBlock* pDrawingStateBlock;
            int hr = Pointer->CreateDrawingStateBlock(&pDrawingStateBlock);

            drawingStateBlock = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DrawingStateBlock(pDrawingStateBlock) : null;

            return hr;
        }

        public HResult CreateDxgiSurfaceRenderTarget(
            IDXGISurface* dxgiSurface,
            D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            out D2D1RenderTarget? renderTarget)
        {
            ID2D1RenderTarget* pRenderTarget;
            int hr = Pointer->CreateDxgiSurfaceRenderTarget(dxgiSurface, renderTargetProperties, &pRenderTarget);

            renderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1RenderTarget(pRenderTarget) : null;

            return hr;
        }

        public HResult CreateEllipseGeometry(D2D1_ELLIPSE* ellipse, out D2D1EllipseGeometry? ellipseGeometry)
        {
            ID2D1EllipseGeometry* pEllipseGeometry;
            int hr = Pointer->CreateEllipseGeometry(ellipse, &pEllipseGeometry);

            ellipseGeometry = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1EllipseGeometry(pEllipseGeometry) : null;

            return hr;
        }

        public HResult CreateGeometryGroup(D2D1_FILL_MODE fillMode, ReadOnlySpan<Pointer<ID2D1Geometry>> geometries, out D2D1GeometryGroup? geometryGroup)
        {
            ID2D1GeometryGroup* pGeometryGroup;
            int hr;

            fixed (Pointer<ID2D1Geometry>* pGeometries = geometries)
            {
                hr = Pointer->CreateGeometryGroup(fillMode, (ID2D1Geometry**)pGeometries, (uint)geometries.Length, &pGeometryGroup);
            }

            geometryGroup = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GeometryGroup(pGeometryGroup) : null;

            return hr;
        }

        public HResult CreateHwndRenderTarget(
            D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            D2D1_HWND_RENDER_TARGET_PROPERTIES* hwndRenderTargetProperties,
            out D2D1HwndRenderTarget? hwndRenderTarget)
        {
            ID2D1HwndRenderTarget* pHwndRenderTarget;
            int hr = Pointer->CreateHwndRenderTarget(renderTargetProperties, hwndRenderTargetProperties, &pHwndRenderTarget);

            hwndRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1HwndRenderTarget(pHwndRenderTarget) : null;

            return hr;
        }

        public HResult CreatePathGeometry(out D2D1PathGeometry? pathGeometry)
        {
            ID2D1PathGeometry* pPathGeometry;
            int hr = Pointer->CreatePathGeometry(&pPathGeometry);

            pathGeometry = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1PathGeometry(pPathGeometry) : null;

            return hr;
        }

        public HResult CreateRectangleGeometry(D2D_RECT_F* rectangle, out D2D1RectangleGeometry? rectangleGeometry)
        {
            ID2D1RectangleGeometry* pRectangleGeometry;
            int hr = Pointer->CreateRectangleGeometry(rectangle, &pRectangleGeometry);

            rectangleGeometry = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1RectangleGeometry(pRectangleGeometry) : null;

            return hr;
        }

        public HResult CreateRoundedRectangleGeometry(D2D1_ROUNDED_RECT* roundedRectangle, out D2D1RoundedRectangleGeometry? roundedRectangleGeometry)
        {
            ID2D1RoundedRectangleGeometry* pRoundedRectangleGeometry;
            int hr = Pointer->CreateRoundedRectangleGeometry(roundedRectangle, &pRoundedRectangleGeometry);

            roundedRectangleGeometry = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1RoundedRectangleGeometry(pRoundedRectangleGeometry) : null;

            return hr;
        }

        public HResult CreateStrokeStyle(
            D2D1_STROKE_STYLE_PROPERTIES* strokeStyleProperties,
            [Optional] ReadOnlySpan<float> dashes,
            out D2D1StrokeStyle? strokeStyle)
        {
            ID2D1StrokeStyle* pStrokeStyle;
            int hr;

            fixed (float* pDashes = dashes)
            {
                hr = Pointer->CreateStrokeStyle(strokeStyleProperties, pDashes, (uint)dashes.Length, &pStrokeStyle);
            }

            strokeStyle = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1StrokeStyle(pStrokeStyle) : null;

            return hr;
        }

        public HResult CreateTransformedGeometry(ID2D1Geometry* sourceGeometry, D2D_MATRIX_3X2_F* transform, out D2D1TransformedGeometry? transformedGeometry)
        {
            ID2D1TransformedGeometry* pTransformedGeometry;
            int hr = Pointer->CreateTransformedGeometry(sourceGeometry, transform, &pTransformedGeometry);

            transformedGeometry = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1TransformedGeometry(pTransformedGeometry) : null;

            return hr;
        }

        public HResult CreateWicBitmapRenderTarget(
            IWICBitmap* target,
            D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            out D2D1RenderTarget? renderTarget)
        {
            ID2D1RenderTarget* pRenderTarget;
            int hr = Pointer->CreateWicBitmapRenderTarget(target, renderTargetProperties, &pRenderTarget);

            renderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1RenderTarget(pRenderTarget) : null;

            return hr;
        }

        public (float dpiX, float dpiY) GetDesktopDpi()
        {
            float dpiX;
            float dpiY;

            Pointer->GetDesktopDpi(&dpiX, &dpiY);

            return (dpiX, dpiY);
        }
    }
}