using System;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1DeviceContext" /> COM interface.</summary>
    public unsafe partial class D2D1DeviceContext : D2D1RenderTarget
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DeviceContext" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DeviceContext(ID2D1DeviceContext* pointer) : base((ID2D1RenderTarget*)pointer)
        {
        }

        public new ID2D1DeviceContext* Pointer => (ID2D1DeviceContext*)base.Pointer;

        public HResult CreateBitmap(D2D_SIZE_U size, [Optional] void* sourceData, uint pitch, D2D1_BITMAP_PROPERTIES1* bitmapProperties, ID2D1Bitmap1** bitmap)
        {
            return Pointer->CreateBitmap(size, sourceData, pitch, bitmapProperties, bitmap);
        }

        public HResult CreateBitmapBrush([Optional] ID2D1Bitmap* bitmap, D2D1_BITMAP_BRUSH_PROPERTIES1* bitmapBrushProperties, ID2D1BitmapBrush1** bitmapBrush)
        {
            return Pointer->CreateBitmapBrush(bitmap, bitmapBrushProperties, bitmapBrush);
        }

        public HResult CreateBitmapBrush(
            [Optional] ID2D1Bitmap* bitmap,
            [Optional] D2D1_BITMAP_BRUSH_PROPERTIES1* bitmapBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            ID2D1BitmapBrush1** bitmapBrush)
        {
            return Pointer->CreateBitmapBrush(bitmap, bitmapBrushProperties, brushProperties, bitmapBrush);
        }

        public HResult CreateBitmapBrush([Optional] ID2D1Bitmap* bitmap, ID2D1BitmapBrush1** bitmapBrush)
        {
            return Pointer->CreateBitmapBrush(bitmap, bitmapBrush);
        }

        public HResult CreateBitmapFromDxgiSurface(IDXGISurface* surface, [Optional] D2D1_BITMAP_PROPERTIES1* bitmapProperties, ID2D1Bitmap1** bitmap)
        {
            return Pointer->CreateBitmapFromDxgiSurface(surface, bitmapProperties, bitmap);
        }

        public HResult CreateBitmapFromWicBitmap(IWICBitmapSource* wicBitmapSource, [Optional] D2D1_BITMAP_PROPERTIES1* bitmapProperties, ID2D1Bitmap1** bitmap)
        {
            return Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, bitmapProperties, bitmap);
        }

        public HResult CreateBitmapFromWicBitmap(IWICBitmapSource* wicBitmapSource, ID2D1Bitmap1** bitmap)
        {
            return Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, bitmap);
        }

        public HResult CreateColorContext(D2D1_COLOR_SPACE space, [Optional] byte* profile, uint profileSize, ID2D1ColorContext** colorContext)
        {
            return Pointer->CreateColorContext(space, profile, profileSize, colorContext);
        }

        public HResult CreateColorContextFromFilename(ushort* filename, ID2D1ColorContext** colorContext)
        {
            return Pointer->CreateColorContextFromFilename(filename, colorContext);
        }

        public HResult CreateColorContextFromWicColorContext(IWICColorContext* wicColorContext, ID2D1ColorContext** colorContext)
        {
            return Pointer->CreateColorContextFromWicColorContext(wicColorContext, colorContext);
        }

        public HResult CreateCommandList(ID2D1CommandList** commandList)
        {
            return Pointer->CreateCommandList(commandList);
        }

        public HResult CreateEffect(Guid* effectId, ID2D1Effect** effect)
        {
            return Pointer->CreateEffect(effectId, effect);
        }

        public HResult CreateGradientStopCollection(
            D2D1_GRADIENT_STOP* straightAlphaGradientStops,
            uint straightAlphaGradientStopsCount,
            D2D1_COLOR_SPACE preInterpolationSpace,
            D2D1_COLOR_SPACE postInterpolationSpace,
            D2D1_BUFFER_PRECISION bufferPrecision,
            D2D1_EXTEND_MODE extendMode,
            D2D1_COLOR_INTERPOLATION_MODE colorInterpolationMode,
            ID2D1GradientStopCollection1** gradientStopCollection1)
        {
            return Pointer->CreateGradientStopCollection(
                straightAlphaGradientStops,
                straightAlphaGradientStopsCount,
                preInterpolationSpace,
                postInterpolationSpace,
                bufferPrecision,
                extendMode,
                colorInterpolationMode,
                gradientStopCollection1);
        }

        public HResult CreateImageBrush([Optional] ID2D1Image* image, D2D1_IMAGE_BRUSH_PROPERTIES* imageBrushProperties, ID2D1ImageBrush** imageBrush)
        {
            return Pointer->CreateImageBrush(image, imageBrushProperties, imageBrush);
        }

        public HResult CreateImageBrush(
            [Optional] ID2D1Image* image,
            [Optional] D2D1_IMAGE_BRUSH_PROPERTIES* imageBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            ID2D1ImageBrush** imageBrush)
        {
            return Pointer->CreateImageBrush(image, imageBrushProperties, brushProperties, imageBrush);
        }

        public void DrawBitmap(
            ID2D1Bitmap* bitmap,
            [Optional] D2D_RECT_F* destinationRectangle,
            float opacity,
            D2D1_INTERPOLATION_MODE interpolationMode,
            D2D_RECT_F* sourceRectangle = null,
            D2D_MATRIX_4X4_F* perspectiveTransform = null)
        {
            Pointer->DrawBitmap(bitmap, destinationRectangle, opacity, interpolationMode, sourceRectangle, perspectiveTransform);
        }

        public void DrawGdiMetafile(ID2D1GdiMetafile* gdiMetafile, D2D_POINT_2F targetOffset)
        {
            Pointer->DrawGdiMetafile(gdiMetafile, targetOffset);
        }

        public void DrawGdiMetafile(ID2D1GdiMetafile* gdiMetafile, D2D_POINT_2F* targetOffset = null)
        {
            Pointer->DrawGdiMetafile(gdiMetafile, targetOffset);
        }

        public void DrawGlyphRun(
            D2D_POINT_2F baselineOrigin,
            DWRITE_GLYPH_RUN* glyphRun,
            [Optional] DWRITE_GLYPH_RUN_DESCRIPTION* glyphRunDescription,
            ID2D1Brush* foregroundBrush,
            DWRITE_MEASURING_MODE measuringMode = DWRITE_MEASURING_MODE.DWRITE_MEASURING_MODE_NATURAL)
        {
            Pointer->DrawGlyphRun(baselineOrigin, glyphRun, glyphRunDescription, foregroundBrush, measuringMode);
        }

        public void DrawImage(
            ID2D1Effect* effect,
            D2D_POINT_2F targetOffset,
            D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_LINEAR,
            D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_OVER)
        {
            Pointer->DrawImage(effect, targetOffset, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Image* image,
            D2D_POINT_2F targetOffset,
            D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_LINEAR,
            D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_OVER)
        {
            Pointer->DrawImage(image, targetOffset, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Effect* effect,
            D2D1_INTERPOLATION_MODE interpolationMode,
            D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_OVER)
        {
            Pointer->DrawImage(effect, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Image* image,
            D2D1_INTERPOLATION_MODE interpolationMode,
            D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_OVER)
        {
            Pointer->DrawImage(image, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Effect* effect,
            [Optional] D2D_POINT_2F* targetOffset,
            [Optional] D2D_RECT_F* imageRectangle,
            D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_LINEAR,
            D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_OVER)
        {
            Pointer->DrawImage(effect, targetOffset, imageRectangle, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Image* image,
            [Optional] D2D_POINT_2F* targetOffset,
            [Optional] D2D_RECT_F* imageRectangle,
            D2D1_INTERPOLATION_MODE interpolationMode = D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_LINEAR,
            D2D1_COMPOSITE_MODE compositeMode = D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_OVER)
        {
            Pointer->DrawImage(image, targetOffset, imageRectangle, interpolationMode, compositeMode);
        }

        public void FillOpacityMask(
            ID2D1Bitmap* opacityMask,
            ID2D1Brush* brush,
            D2D_RECT_F* destinationRectangle = null,
            D2D_RECT_F* sourceRectangle = null)
        {
            Pointer->FillOpacityMask(opacityMask, brush, destinationRectangle, sourceRectangle);
        }

        public void GetDevice(ID2D1Device** device)
        {
            Pointer->GetDevice(device);
        }

        public HResult GetEffectInvalidRectangleCount(ID2D1Effect* effect, uint* rectangleCount)
        {
            return Pointer->GetEffectInvalidRectangleCount(effect, rectangleCount);
        }

        public HResult GetEffectInvalidRectangles(ID2D1Effect* effect, D2D_RECT_F* rectangles, uint rectanglesCount)
        {
            return Pointer->GetEffectInvalidRectangles(effect, rectangles, rectanglesCount);
        }

        public HResult GetEffectRequiredInputRectangles(
            ID2D1Effect* renderEffect,
            [Optional] D2D_RECT_F* renderImageRectangle,
            D2D1_EFFECT_INPUT_DESCRIPTION* inputDescriptions,
            D2D_RECT_F* requiredInputRects,
            uint inputCount)
        {
            return Pointer->GetEffectRequiredInputRectangles(renderEffect, renderImageRectangle, inputDescriptions, requiredInputRects, inputCount);
        }

        public HResult GetGlyphRunWorldBounds(D2D_POINT_2F baselineOrigin, DWRITE_GLYPH_RUN* glyphRun, DWRITE_MEASURING_MODE measuringMode, D2D_RECT_F* bounds)
        {
            return Pointer->GetGlyphRunWorldBounds(baselineOrigin, glyphRun, measuringMode, bounds);
        }

        public HResult GetImageLocalBounds(ID2D1Image* image, D2D_RECT_F* localBounds)
        {
            return Pointer->GetImageLocalBounds(image, localBounds);
        }

        public HResult GetImageWorldBounds(ID2D1Image* image, D2D_RECT_F* worldBounds)
        {
            return Pointer->GetImageWorldBounds(image, worldBounds);
        }

        public D2D1_PRIMITIVE_BLEND GetPrimitiveBlend()
        {
            return Pointer->GetPrimitiveBlend();
        }

        public void GetRenderingControls(D2D1_RENDERING_CONTROLS* renderingControls)
        {
            Pointer->GetRenderingControls(renderingControls);
        }

        public void GetTarget(ID2D1Image** image)
        {
            Pointer->GetTarget(image);
        }

        public D2D1_UNIT_MODE GetUnitMode()
        {
            return Pointer->GetUnitMode();
        }

        public HResult InvalidateEffectInputRectangle(ID2D1Effect* effect, uint input, D2D_RECT_F* inputRectangle)
        {
            return Pointer->InvalidateEffectInputRectangle(effect, input, inputRectangle);
        }

        public bool IsBufferPrecisionSupported(D2D1_BUFFER_PRECISION bufferPrecision)
        {
            return Pointer->IsBufferPrecisionSupported(bufferPrecision) == TerraFX.Interop.Windows.TRUE;
        }

        public bool IsDxgiFormatSupported(DXGI_FORMAT format)
        {
            return Pointer->IsDxgiFormatSupported(format) == TerraFX.Interop.Windows.TRUE;
        }

        public void PushLayer(D2D1_LAYER_PARAMETERS1* layerParameters, ID2D1Layer* layer = null)
        {
            Pointer->PushLayer(layerParameters, layer);
        }

        public void SetPrimitiveBlend(D2D1_PRIMITIVE_BLEND primitiveBlend)
        {
            Pointer->SetPrimitiveBlend(primitiveBlend);
        }

        public void SetRenderingControls(D2D1_RENDERING_CONTROLS* renderingControls)
        {
            Pointer->SetRenderingControls(renderingControls);
        }

        public void SetTarget(ID2D1Image* image = null)
        {
            Pointer->SetTarget(image);
        }

        public void SetUnitMode(D2D1_UNIT_MODE unitMode)
        {
            Pointer->SetUnitMode(unitMode);
        }

        public static implicit operator ID2D1DeviceContext*(D2D1DeviceContext value)
        {
            return value.Pointer;
        }
    }
}