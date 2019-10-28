using System;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.DWrite;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1RenderTarget
    {
        public HResult CreateBitmap(D2D_SIZE_U size, D2D1_BITMAP_PROPERTIES* bitmapProperties, out D2D1Bitmap? bitmap)
        {
            ID2D1Bitmap* pBitmap;
            int hr = Pointer->CreateBitmap(size, bitmapProperties, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmap(
            D2D_SIZE_U size,
            [Optional] ReadOnlySpan<byte> srcData,
            uint pitch,
            D2D1_BITMAP_PROPERTIES* bitmapProperties,
            out D2D1Bitmap? bitmap)
        {
            ID2D1Bitmap* pBitmap;
            int hr;

            fixed (byte* pSrcData = srcData)
            {
                hr = Pointer->CreateBitmap(size, pSrcData, pitch, bitmapProperties, &pBitmap);
            }

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapBrush(
            [Optional] ID2D1Bitmap* bitmap,
            [Optional] D2D1_BITMAP_BRUSH_PROPERTIES* bitmapBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            out D2D1BitmapBrush? bitmapBrush)
        {
            ID2D1BitmapBrush* pBitmapBrush;
            int hr = Pointer->CreateBitmapBrush(bitmap, bitmapBrushProperties, brushProperties, &pBitmapBrush);

            bitmapBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapBrush(pBitmapBrush) : null;

            return hr;
        }

        public HResult CreateBitmapBrush([Optional] ID2D1Bitmap* bitmap, out D2D1BitmapBrush? bitmapBrush)
        {
            ID2D1BitmapBrush* pBitmapBrush;
            int hr = Pointer->CreateBitmapBrush(bitmap, &pBitmapBrush);

            bitmapBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapBrush(pBitmapBrush) : null;

            return hr;
        }

        public HResult CreateBitmapFromWicBitmap(IWICBitmapSource* wicBitmapSource, out D2D1Bitmap? bitmap)
        {
            ID2D1Bitmap* pBitmap;
            int hr = Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapFromWicBitmap(IWICBitmapSource* wicBitmapSource, [Optional] D2D1_BITMAP_PROPERTIES* bitmapProperties, out D2D1Bitmap? bitmap)
        {
            ID2D1Bitmap* pBitmap;
            int hr = Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, bitmapProperties, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateCompatibleRenderTarget(out D2D1BitmapRenderTarget? bitmapRenderTarget)
        {
            ID2D1BitmapRenderTarget* pBitmapRenderTarget;
            int hr = Pointer->CreateCompatibleRenderTarget(&pBitmapRenderTarget);

            bitmapRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapRenderTarget(pBitmapRenderTarget) : null;

            return hr;
        }

        public HResult CreateCompatibleRenderTarget(D2D_SIZE_F desiredSize, out D2D1BitmapRenderTarget? bitmapRenderTarget)
        {
            ID2D1BitmapRenderTarget* pBitmapRenderTarget;
            int hr = Pointer->CreateCompatibleRenderTarget(desiredSize, &pBitmapRenderTarget);

            bitmapRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapRenderTarget(pBitmapRenderTarget) : null;

            return hr;
        }

        public HResult CreateCompatibleRenderTarget(D2D_SIZE_F desiredSize, D2D_SIZE_U desiredPixelSize, out D2D1BitmapRenderTarget? bitmapRenderTarget)
        {
            ID2D1BitmapRenderTarget* pBitmapRenderTarget;
            int hr = Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, &pBitmapRenderTarget);

            bitmapRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapRenderTarget(pBitmapRenderTarget) : null;

            return hr;
        }

        public HResult CreateCompatibleRenderTarget(
            D2D_SIZE_F desiredSize,
            D2D_SIZE_U desiredPixelSize,
            D2D1_PIXEL_FORMAT desiredFormat,
            out D2D1BitmapRenderTarget? bitmapRenderTarget)
        {
            ID2D1BitmapRenderTarget* pBitmapRenderTarget;
            int hr = Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, desiredFormat, &pBitmapRenderTarget);

            bitmapRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapRenderTarget(pBitmapRenderTarget) : null;

            return hr;
        }

        public HResult CreateCompatibleRenderTarget(
            D2D_SIZE_F desiredSize,
            D2D_SIZE_U desiredPixelSize,
            D2D1_PIXEL_FORMAT desiredFormat,
            D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS options,
            out D2D1BitmapRenderTarget? bitmapRenderTarget)
        {
            ID2D1BitmapRenderTarget* pBitmapRenderTarget;
            int hr = Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, desiredFormat, options, &pBitmapRenderTarget);

            bitmapRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapRenderTarget(pBitmapRenderTarget) : null;

            return hr;
        }

        public HResult CreateCompatibleRenderTarget(
            [Optional] D2D_SIZE_F* desiredSize,
            [Optional] D2D_SIZE_U* desiredPixelSize,
            [Optional] D2D1_PIXEL_FORMAT* desiredFormat,
            D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS options,
            out D2D1BitmapRenderTarget? bitmapRenderTarget)
        {
            ID2D1BitmapRenderTarget* pBitmapRenderTarget;
            int hr = Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, desiredFormat, options, &pBitmapRenderTarget);

            bitmapRenderTarget = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapRenderTarget(pBitmapRenderTarget) : null;

            return hr;
        }

        public HResult CreateGradientStopCollection(
            ReadOnlySpan<D2D1_GRADIENT_STOP> gradientStops,
            D2D1_GAMMA colorInterpolationGamma,
            D2D1_EXTEND_MODE extendMode,
            out D2D1GradientStopCollection? gradientStopCollection)
        {
            ID2D1GradientStopCollection* pGradientStopCollection;
            int hr;

            fixed (D2D1_GRADIENT_STOP* pGradientStops = gradientStops)
            {
                hr = Pointer->CreateGradientStopCollection(
                    pGradientStops,
                    (uint)gradientStops.Length,
                    colorInterpolationGamma,
                    extendMode,
                    &pGradientStopCollection);
            }

            gradientStopCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GradientStopCollection(pGradientStopCollection) : null;

            return hr;
        }

        public HResult CreateGradientStopCollection(ReadOnlySpan<D2D1_GRADIENT_STOP> gradientStops, out D2D1GradientStopCollection? gradientStopCollection)
        {
            ID2D1GradientStopCollection* pGradientStopCollection;
            int hr;

            fixed (D2D1_GRADIENT_STOP* pGradientStops = gradientStops)
            {
                hr = Pointer->CreateGradientStopCollection(pGradientStops, (uint)gradientStops.Length, &pGradientStopCollection);
            }

            gradientStopCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GradientStopCollection(pGradientStopCollection) : null;

            return hr;
        }

        public HResult CreateLayer([Optional] D2D_SIZE_F* size, out D2D1Layer? layer)
        {
            ID2D1Layer* pLayer;
            int hr = Pointer->CreateLayer(size, &pLayer);

            layer = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Layer(pLayer) : null;

            return hr;
        }

        public HResult CreateLayer(D2D_SIZE_F size, out D2D1Layer? layer)
        {
            ID2D1Layer* pLayer;
            int hr = Pointer->CreateLayer(size, &pLayer);

            layer = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Layer(pLayer) : null;

            return hr;
        }

        public HResult CreateLayer(out D2D1Layer? layer)
        {
            ID2D1Layer* pLayer;
            int hr = Pointer->CreateLayer(&pLayer);

            layer = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Layer(pLayer) : null;

            return hr;
        }

        public HResult CreateLinearGradientBrush(
            D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES* linearGradientBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            out D2D1LinearGradientBrush? linearGradientBrush)
        {
            ID2D1LinearGradientBrush* pLinearGradientBrush;
            int hr = Pointer->CreateLinearGradientBrush(linearGradientBrushProperties, brushProperties, gradientStopCollection, &pLinearGradientBrush);

            linearGradientBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1LinearGradientBrush(pLinearGradientBrush) : null;

            return hr;
        }

        public HResult CreateLinearGradientBrush(
            D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES* linearGradientBrushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            out D2D1LinearGradientBrush? linearGradientBrush)
        {
            ID2D1LinearGradientBrush* pLinearGradientBrush;
            int hr = Pointer->CreateLinearGradientBrush(linearGradientBrushProperties, gradientStopCollection, &pLinearGradientBrush);

            linearGradientBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1LinearGradientBrush(pLinearGradientBrush) : null;

            return hr;
        }

        public HResult CreateMesh(out D2D1Mesh? mesh)
        {
            ID2D1Mesh* pMesh;
            int hr = Pointer->CreateMesh(&pMesh);

            mesh = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Mesh(pMesh) : null;

            return hr;
        }

        public HResult CreateRadialGradientBrush(
            D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES* radialGradientBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            out D2D1RadialGradientBrush? radialGradientBrush)
        {
            ID2D1RadialGradientBrush* pRadialGradientBrush;
            int hr = Pointer->CreateRadialGradientBrush(radialGradientBrushProperties, brushProperties, gradientStopCollection, &pRadialGradientBrush);

            radialGradientBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1RadialGradientBrush(pRadialGradientBrush) : null;

            return hr;
        }

        public HResult CreateRadialGradientBrush(
            D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES* radialGradientBrushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            out D2D1RadialGradientBrush? radialGradientBrush)
        {
            ID2D1RadialGradientBrush* pRadialGradientBrush;
            int hr = Pointer->CreateRadialGradientBrush(radialGradientBrushProperties, gradientStopCollection, &pRadialGradientBrush);

            radialGradientBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1RadialGradientBrush(pRadialGradientBrush) : null;

            return hr;
        }

        public HResult CreateSharedBitmap<T>(ReadOnlySpan<byte> data, [Optional] D2D1_BITMAP_PROPERTIES* bitmapProperties, out D2D1Bitmap? bitmap)
            where T : unmanaged
        {
            Guid iid = typeof(T).GUID;
            ID2D1Bitmap* pBitmap;
            int hr;

            fixed (byte* pData = data)
            {
                hr = Pointer->CreateSharedBitmap(&iid, pData, bitmapProperties, &pBitmap);
            }

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap(pBitmap) : null;

            return hr;
        }

        public HResult CreateSolidColorBrush(DXGI_RGBA* color, [Optional] D2D1_BRUSH_PROPERTIES* brushProperties, out D2D1SolidColorBrush? solidColorBrush)
        {
            ID2D1SolidColorBrush* pSolidColorBrush;
            int hr = Pointer->CreateSolidColorBrush(color, brushProperties, &pSolidColorBrush);

            solidColorBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SolidColorBrush(pSolidColorBrush) : null;

            return hr;
        }

        public HResult CreateSolidColorBrush(DXGI_RGBA* color, out D2D1SolidColorBrush? solidColorBrush)
        {
            ID2D1SolidColorBrush* pSolidColorBrush;
            int hr = Pointer->CreateSolidColorBrush(color, &pSolidColorBrush);

            solidColorBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SolidColorBrush(pSolidColorBrush) : null;

            return hr;
        }

        public void DrawText(
            ReadOnlySpan<char> @string,
            IDWriteTextFormat* textFormat,
            D2D_RECT_F* layoutRect,
            ID2D1Brush* defaultFillBrush,
            D2D1_DRAW_TEXT_OPTIONS options,
            DWRITE_MEASURING_MODE measuringMode)
        {
            fixed (char* pString = @string)
            {
                Pointer->DrawText((ushort*)pString, (uint)@string.Length, textFormat, layoutRect, defaultFillBrush, options, measuringMode);
            }
        }

        public (float dpiX, float dpiY) GetDpi()
        {
            float dpiX;
            float dpiY;

            Pointer->GetDpi(&dpiX, &dpiY);

            return (dpiX, dpiY);
        }

        public DWriteRenderingParams? GetTextRenderingParams()
        {
            IDWriteRenderingParams* textRenderingParams;

            Pointer->GetTextRenderingParams(&textRenderingParams);

            return textRenderingParams is null ? null : new DWriteRenderingParams(textRenderingParams);
        }

        public D2D_MATRIX_3X2_F GetTransform()
        {
            D2D_MATRIX_3X2_F transform;

            Pointer->GetTransform(&transform);

            return transform;
        }
    }
}