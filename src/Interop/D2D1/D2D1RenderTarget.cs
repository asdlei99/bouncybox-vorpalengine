using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    /// <summary>Proxies the <see cref="ID2D1RenderTarget" /> COM interface.</summary>
    public unsafe partial class D2D1RenderTarget : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1RenderTarget" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1RenderTarget(ID2D1RenderTarget* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1RenderTarget* Pointer => (ID2D1RenderTarget*)base.Pointer;

        public void BeginDraw()
        {
            Pointer->BeginDraw();
        }

        public void Clear([Optional] DXGI_RGBA* clearColor)
        {
            Pointer->Clear(clearColor);
        }

        public HResult CreateBitmap(D2D_SIZE_U size, D2D1_BITMAP_PROPERTIES* bitmapProperties, ID2D1Bitmap** bitmap)
        {
            return Pointer->CreateBitmap(size, bitmapProperties, bitmap);
        }

        public HResult CreateBitmap(D2D_SIZE_U size, [Optional] void* srcData, uint pitch, D2D1_BITMAP_PROPERTIES* bitmapProperties, ID2D1Bitmap** bitmap)
        {
            return Pointer->CreateBitmap(size, srcData, pitch, bitmapProperties, bitmap);
        }

        public HResult CreateBitmapBrush(
            [Optional] ID2D1Bitmap* bitmap,
            [Optional] D2D1_BITMAP_BRUSH_PROPERTIES* bitmapBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            ID2D1BitmapBrush** bitmapBrush)
        {
            return Pointer->CreateBitmapBrush(bitmap, bitmapBrushProperties, brushProperties, bitmapBrush);
        }

        public HResult CreateBitmapBrush([Optional] ID2D1Bitmap* bitmap, ID2D1BitmapBrush** bitmapBrush)
        {
            return Pointer->CreateBitmapBrush(bitmap, bitmapBrush);
        }

        public HResult CreateBitmapFromWicBitmap(IWICBitmapSource* wicBitmapSource, ID2D1Bitmap** bitmap)
        {
            return Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, bitmap);
        }

        public HResult CreateBitmapFromWicBitmap(IWICBitmapSource* wicBitmapSource, [Optional] D2D1_BITMAP_PROPERTIES* bitmapProperties, ID2D1Bitmap** bitmap)
        {
            return Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, bitmapProperties, bitmap);
        }

        public HResult CreateCompatibleRenderTarget(ID2D1BitmapRenderTarget** bitmapRenderTarget)
        {
            return Pointer->CreateCompatibleRenderTarget(bitmapRenderTarget);
        }

        public HResult CreateCompatibleRenderTarget(D2D_SIZE_F desiredSize, ID2D1BitmapRenderTarget** bitmapRenderTarget)
        {
            return Pointer->CreateCompatibleRenderTarget(desiredSize, bitmapRenderTarget);
        }

        public HResult CreateCompatibleRenderTarget(D2D_SIZE_F desiredSize, D2D_SIZE_U desiredPixelSize, ID2D1BitmapRenderTarget** bitmapRenderTarget)
        {
            return Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, bitmapRenderTarget);
        }

        public HResult CreateCompatibleRenderTarget(
            D2D_SIZE_F desiredSize,
            D2D_SIZE_U desiredPixelSize,
            D2D1_PIXEL_FORMAT desiredFormat,
            ID2D1BitmapRenderTarget** bitmapRenderTarget)
        {
            return Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, desiredFormat, bitmapRenderTarget);
        }

        public HResult CreateCompatibleRenderTarget(
            D2D_SIZE_F desiredSize,
            D2D_SIZE_U desiredPixelSize,
            D2D1_PIXEL_FORMAT desiredFormat,
            D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS options,
            ID2D1BitmapRenderTarget** bitmapRenderTarget)
        {
            return Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, desiredFormat, options, bitmapRenderTarget);
        }

        public HResult CreateCompatibleRenderTarget(
            [Optional] D2D_SIZE_F* desiredSize,
            [Optional] D2D_SIZE_U* desiredPixelSize,
            [Optional] D2D1_PIXEL_FORMAT* desiredFormat,
            D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS options,
            ID2D1BitmapRenderTarget** bitmapRenderTarget)
        {
            return Pointer->CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, desiredFormat, options, bitmapRenderTarget);
        }

        public HResult CreateGradientStopCollection(
            D2D1_GRADIENT_STOP* gradientStops,
            uint gradientStopsCount,
            D2D1_GAMMA colorInterpolationGamma,
            D2D1_EXTEND_MODE extendMode,
            ID2D1GradientStopCollection** gradientStopCollection)
        {
            return Pointer->CreateGradientStopCollection(gradientStops, gradientStopsCount, colorInterpolationGamma, extendMode, gradientStopCollection);
        }

        public HResult CreateGradientStopCollection(
            D2D1_GRADIENT_STOP* gradientStops,
            uint gradientStopsCount,
            ID2D1GradientStopCollection** gradientStopCollection)
        {
            return Pointer->CreateGradientStopCollection(gradientStops, gradientStopsCount, gradientStopCollection);
        }

        public HResult CreateLayer([Optional] D2D_SIZE_F* size, ID2D1Layer** layer)
        {
            return Pointer->CreateLayer(size, layer);
        }

        public HResult CreateLayer(D2D_SIZE_F size, ID2D1Layer** layer)
        {
            return Pointer->CreateLayer(size, layer);
        }

        public HResult CreateLayer(ID2D1Layer** layer)
        {
            return Pointer->CreateLayer(layer);
        }

        public HResult CreateLinearGradientBrush(
            D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES* linearGradientBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            ID2D1LinearGradientBrush** linearGradientBrush)
        {
            return Pointer->CreateLinearGradientBrush(linearGradientBrushProperties, brushProperties, gradientStopCollection, linearGradientBrush);
        }

        public HResult CreateLinearGradientBrush(
            D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES* linearGradientBrushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            ID2D1LinearGradientBrush** linearGradientBrush)
        {
            return Pointer->CreateLinearGradientBrush(linearGradientBrushProperties, gradientStopCollection, linearGradientBrush);
        }

        public HResult CreateMesh(ID2D1Mesh** mesh)
        {
            return Pointer->CreateMesh(mesh);
        }

        public HResult CreateRadialGradientBrush(
            D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES* radialGradientBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            ID2D1RadialGradientBrush** radialGradientBrush)
        {
            return Pointer->CreateRadialGradientBrush(radialGradientBrushProperties, brushProperties, gradientStopCollection, radialGradientBrush);
        }

        public HResult CreateRadialGradientBrush(
            D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES* radialGradientBrushProperties,
            ID2D1GradientStopCollection* gradientStopCollection,
            ID2D1RadialGradientBrush** radialGradientBrush)
        {
            return Pointer->CreateRadialGradientBrush(radialGradientBrushProperties, gradientStopCollection, radialGradientBrush);
        }

        public HResult CreateSharedBitmap(Guid* riid, void* data, [Optional] D2D1_BITMAP_PROPERTIES* bitmapProperties, ID2D1Bitmap** bitmap)
        {
            return Pointer->CreateSharedBitmap(riid, data, bitmapProperties, bitmap);
        }

        public HResult CreateSolidColorBrush(DXGI_RGBA* color, [Optional] D2D1_BRUSH_PROPERTIES* brushProperties, ID2D1SolidColorBrush** solidColorBrush)
        {
            return Pointer->CreateSolidColorBrush(color, brushProperties, solidColorBrush);
        }

        public HResult CreateSolidColorBrush(DXGI_RGBA* color, ID2D1SolidColorBrush** solidColorBrush)
        {
            return Pointer->CreateSolidColorBrush(color, solidColorBrush);
        }

        public void DrawBitmap(
            ID2D1Bitmap* bitmap,
            [Optional] D2D_RECT_F* destinationRectangle,
            float opacity = 1f,
            D2D1_BITMAP_INTERPOLATION_MODE interpolationMode = D2D1_BITMAP_INTERPOLATION_MODE.D2D1_BITMAP_INTERPOLATION_MODE_LINEAR,
            D2D_RECT_F* sourceRectangle = default)
        {
            Pointer->DrawBitmap(bitmap, destinationRectangle, opacity, interpolationMode, sourceRectangle);
        }

        public void DrawEllipse(D2D1_ELLIPSE* ellipse, ID2D1Brush* brush, float strokeWidth = 1f, ID2D1StrokeStyle* strokeStyle = default)
        {
            Pointer->DrawEllipse(ellipse, brush, strokeWidth, strokeStyle);
        }

        public void DrawGeometry(ID2D1Geometry* geometry, ID2D1Brush* brush, float strokeWidth = 1f, ID2D1StrokeStyle* strokeStyle = default)
        {
            Pointer->DrawGeometry(geometry, brush, strokeWidth, strokeStyle);
        }

        public void DrawGlyphRun(D2D_POINT_2F baselineOrigin, DWRITE_GLYPH_RUN* glyphRun, ID2D1Brush* foregroundBrush, DWRITE_MEASURING_MODE measuringMode)
        {
            Pointer->DrawGlyphRun(baselineOrigin, glyphRun, foregroundBrush, measuringMode);
        }

        public void DrawLine(D2D_POINT_2F point0, D2D_POINT_2F point1, ID2D1Brush* brush, float strokeWidth = 1f, ID2D1StrokeStyle* strokeStyle = default)
        {
            Pointer->DrawLine(point0, point1, brush, strokeWidth, strokeStyle);
        }

        public void DrawRectangle(D2D_RECT_F* rect, ID2D1Brush* brush, float strokeWidth = 1f, ID2D1StrokeStyle* strokeStyle = default)
        {
            Pointer->DrawRectangle(rect, brush, strokeWidth, strokeStyle);
        }

        public void DrawRoundedRectangle(D2D1_ROUNDED_RECT* roundedRect, ID2D1Brush* brush, float strokeWidth = 1f, ID2D1StrokeStyle* strokeStyle = default)
        {
            Pointer->DrawRoundedRectangle(roundedRect, brush, strokeWidth, strokeStyle);
        }

        public void DrawText(
            ushort* @string,
            uint stringLength,
            IDWriteTextFormat* textFormat,
            D2D_RECT_F* layoutRect,
            ID2D1Brush* defaultFillBrush,
            D2D1_DRAW_TEXT_OPTIONS options,
            DWRITE_MEASURING_MODE measuringMode)
        {
            Pointer->DrawText(@string, stringLength, textFormat, layoutRect, defaultFillBrush, options, measuringMode);
        }

        public void DrawTextLayout(D2D_POINT_2F origin, IDWriteTextLayout* textLayout, ID2D1Brush* defaultFillBrush, D2D1_DRAW_TEXT_OPTIONS options)
        {
            Pointer->DrawTextLayout(origin, textLayout, defaultFillBrush, options);
        }

        public HResult EndDraw([Optional] ulong* tag1, [Optional] ulong* tag2)
        {
            return Pointer->EndDraw(tag1, tag2);
        }

        public void FillEllipse(D2D1_ELLIPSE* ellipse, ID2D1Brush* brush)
        {
            Pointer->FillEllipse(ellipse, brush);
        }

        public void FillGeometry(ID2D1Geometry* geometry, ID2D1Brush* brush, [Optional] ID2D1Brush* opacityBrush)
        {
            Pointer->FillGeometry(geometry, brush, opacityBrush);
        }

        public void FillMesh(ID2D1Mesh* mesh, ID2D1Brush* brush)
        {
            Pointer->FillMesh(mesh, brush);
        }

        public void FillOpacityMask(
            ID2D1Bitmap* opacityMask,
            ID2D1Brush* brush,
            D2D1_OPACITY_MASK_CONTENT content,
            [Optional] D2D_RECT_F* destinationRectangle,
            [Optional] D2D_RECT_F* sourceRectangle)
        {
            Pointer->FillOpacityMask(opacityMask, brush, content, destinationRectangle, sourceRectangle);
        }

        public void FillRectangle(D2D_RECT_F* rect, ID2D1Brush* brush)
        {
            Pointer->FillRectangle(rect, brush);
        }

        public void FillRoundedRectangle(D2D1_ROUNDED_RECT* roundedRect, ID2D1Brush* brush)
        {
            Pointer->FillRoundedRectangle(roundedRect, brush);
        }

        public HResult Flush([Optional] ulong* tag1, [Optional] ulong* tag2)
        {
            return Pointer->Flush(tag1, tag2);
        }

        public D2D1_ANTIALIAS_MODE GetAntialiasMode()
        {
            return Pointer->GetAntialiasMode();
        }

        public void GetDpi(float* dpiX, float* dpiY)
        {
            Pointer->GetDpi(dpiX, dpiY);
        }

        public uint GetMaximumBitmapSize()
        {
            return Pointer->GetMaximumBitmapSize();
        }

        public D2D1_PIXEL_FORMAT GetPixelFormat()
        {
            return Pointer->GetPixelFormat();
        }

        public D2D_SIZE_U GetPixelSize()
        {
            return Pointer->GetPixelSize();
        }

        public D2D_SIZE_F GetSize()
        {
            return Pointer->GetSize();
        }

        public void GetTags([Optional] ulong* tag1, [Optional] ulong* tag2)
        {
            Pointer->GetTags(tag1, tag2);
        }

        public D2D1_TEXT_ANTIALIAS_MODE GetTextAntialiasMode()
        {
            return Pointer->GetTextAntialiasMode();
        }

        public void GetTextRenderingParams(IDWriteRenderingParams** textRenderingParams)
        {
            Pointer->GetTextRenderingParams(textRenderingParams);
        }

        public void GetTransform(D2D_MATRIX_3X2_F* transform)
        {
            Pointer->GetTransform(transform);
        }

        public bool IsSupported(D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties)
        {
            return Pointer->IsSupported(renderTargetProperties) == TerraFX.Interop.Windows.TRUE;
        }

        public void PopAxisAlignedClip()
        {
            Pointer->PopAxisAlignedClip();
        }

        public void PopLayer()
        {
            Pointer->PopLayer();
        }

        public void PushAxisAlignedClip(D2D_RECT_F* clipRect, D2D1_ANTIALIAS_MODE antialiasMode)
        {
            Pointer->PushAxisAlignedClip(clipRect, antialiasMode);
        }

        public void PushLayer(D2D1_LAYER_PARAMETERS* layerParameters, [Optional] ID2D1Layer* layer)
        {
            Pointer->PushLayer(layerParameters, layer);
        }

        public void RestoreDrawingState(ID2D1DrawingStateBlock* drawingStateBlock)
        {
            Pointer->RestoreDrawingState(drawingStateBlock);
        }

        public void SaveDrawingState(ID2D1DrawingStateBlock* drawingStateBlock)
        {
            Pointer->SaveDrawingState(drawingStateBlock);
        }

        public void SetAntialiasMode(D2D1_ANTIALIAS_MODE antialiasMode)
        {
            Pointer->SetAntialiasMode(antialiasMode);
        }

        public void SetDpi(float dpiX, float dpiY)
        {
            Pointer->SetDpi(dpiX, dpiY);
        }

        public void SetTags(ulong tag1, ulong tag2)
        {
            Pointer->SetTags(tag1, tag2);
        }

        public void SetTextAntialiasMode(D2D1_TEXT_ANTIALIAS_MODE textAntialiasMode)
        {
            Pointer->SetTextAntialiasMode(textAntialiasMode);
        }

        public void SetTextRenderingParams([Optional] IDWriteRenderingParams* textRenderingParams)
        {
            Pointer->SetTextRenderingParams(textRenderingParams);
        }

        public void SetTransform(D2D_MATRIX_3X2_F* transform)
        {
            Pointer->SetTransform(transform);
        }

        public static implicit operator ID2D1RenderTarget*(D2D1RenderTarget value)
        {
            return value.Pointer;
        }
    }
}