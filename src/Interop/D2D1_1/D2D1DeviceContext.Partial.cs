using System;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    public unsafe partial class D2D1DeviceContext
    {
        public HResult CreateBitmap(
            D2D_SIZE_U size,
            [Optional] ReadOnlySpan<byte> sourceData,
            uint pitch,
            D2D1_BITMAP_PROPERTIES1* bitmapProperties,
            out D2D1Bitmap1? bitmap)
        {
            ID2D1Bitmap1* pBitmap;
            int hr;

            fixed (byte* pSourceData = sourceData)
            {
                hr = Pointer->CreateBitmap(size, pSourceData, pitch, bitmapProperties, &pBitmap);
            }

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap1(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapBrush(
            [Optional] ID2D1Bitmap* bitmap,
            D2D1_BITMAP_BRUSH_PROPERTIES1* bitmapBrushProperties,
            out D2D1BitmapBrush1? bitmapBrush)
        {
            ID2D1BitmapBrush1* pBitmapBrush;
            int hr = Pointer->CreateBitmapBrush(bitmap, bitmapBrushProperties, &pBitmapBrush);

            bitmapBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapBrush1(pBitmapBrush) : null;

            return hr;
        }

        public HResult CreateBitmapBrush(
            [Optional] ID2D1Bitmap* bitmap,
            [Optional] D2D1_BITMAP_BRUSH_PROPERTIES1* bitmapBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            out D2D1BitmapBrush1? bitmapBrush)
        {
            ID2D1BitmapBrush1* pBitmapBrush;
            int hr = Pointer->CreateBitmapBrush(bitmap, bitmapBrushProperties, brushProperties, &pBitmapBrush);

            bitmapBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapBrush1(pBitmapBrush) : null;

            return hr;
        }

        public HResult CreateBitmapBrush([Optional] ID2D1Bitmap* bitmap, out D2D1BitmapBrush1? bitmapBrush)
        {
            ID2D1BitmapBrush1* pBitmapBrush;
            int hr = Pointer->CreateBitmapBrush(bitmap, &pBitmapBrush);

            bitmapBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1BitmapBrush1(pBitmapBrush) : null;

            return hr;
        }

        public HResult CreateBitmapFromDxgiSurface(IDXGISurface* surface, [Optional] D2D1_BITMAP_PROPERTIES1* bitmapProperties, out D2D1Bitmap1? bitmap)
        {
            ID2D1Bitmap1* pBitmap;
            int hr = Pointer->CreateBitmapFromDxgiSurface(surface, bitmapProperties, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap1(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapFromWicBitmap(
            IWICBitmapSource* wicBitmapSource,
            [Optional] D2D1_BITMAP_PROPERTIES1* bitmapProperties,
            out D2D1Bitmap1? bitmap)
        {
            ID2D1Bitmap1* pBitmap;
            int hr = Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, bitmapProperties, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap1(pBitmap) : null;

            return hr;
        }

        public HResult CreateBitmapFromWicBitmap(IWICBitmapSource* wicBitmapSource, out D2D1Bitmap1? bitmap)
        {
            ID2D1Bitmap1* pBitmap;
            int hr = Pointer->CreateBitmapFromWicBitmap(wicBitmapSource, &pBitmap);

            bitmap = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Bitmap1(pBitmap) : null;

            return hr;
        }

        public HResult CreateColorContext(D2D1_COLOR_SPACE space, [Optional] ReadOnlySpan<byte> profile, out D2D1ColorContext? colorContext)
        {
            ID2D1ColorContext* pColorContext;
            int hr;

            fixed (byte* pProfile = profile)
            {
                hr = Pointer->CreateColorContext(space, pProfile, (uint)profile.Length, &pColorContext);
            }

            colorContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ColorContext(pColorContext) : null;

            return hr;
        }

        public HResult CreateColorContextFromFilename(ReadOnlySpan<char> filename, out D2D1ColorContext? colorContext)
        {
            ID2D1ColorContext* pColorContext;
            int hr;

            fixed (char* pFilename = filename)
            {
                hr = Pointer->CreateColorContextFromFilename((ushort*)pFilename, &pColorContext);
            }

            colorContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ColorContext(pColorContext) : null;

            return hr;
        }

        public HResult CreateColorContextFromWicColorContext(IWICColorContext* wicColorContext, out D2D1ColorContext? colorContext)
        {
            ID2D1ColorContext* pColorContext;
            int hr = Pointer->CreateColorContextFromWicColorContext(wicColorContext, &pColorContext);

            colorContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ColorContext(pColorContext) : null;

            return hr;
        }

        public HResult CreateCommandList(out D2D1CommandList? commandList)
        {
            ID2D1CommandList* pCommandList;
            int hr = Pointer->CreateCommandList(&pCommandList);

            commandList = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1CommandList(pCommandList) : null;

            return hr;
        }

        public HResult CreateEffect(Guid* effectId, out D2D1Effect? effect)
        {
            ID2D1Effect* pEffect;
            int hr = Pointer->CreateEffect(effectId, &pEffect);

            effect = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Effect(pEffect) : null;

            return hr;
        }

        public HResult CreateGradientStopCollection(
            ReadOnlySpan<D2D1_GRADIENT_STOP> straightAlphaGradientStops,
            D2D1_COLOR_SPACE preInterpolationSpace,
            D2D1_COLOR_SPACE postInterpolationSpace,
            D2D1_BUFFER_PRECISION bufferPrecision,
            D2D1_EXTEND_MODE extendMode,
            D2D1_COLOR_INTERPOLATION_MODE colorInterpolationMode,
            out D2D1GradientStopCollection1? gradientStopCollection1)
        {
            ID2D1GradientStopCollection1* pGradientStopCollection1;
            int hr;

            fixed (D2D1_GRADIENT_STOP* pStraightAlphaGradientStops = straightAlphaGradientStops)
            {
                hr = Pointer->CreateGradientStopCollection(
                    pStraightAlphaGradientStops,
                    (uint)straightAlphaGradientStops.Length,
                    preInterpolationSpace,
                    postInterpolationSpace,
                    bufferPrecision,
                    extendMode,
                    colorInterpolationMode,
                    &pGradientStopCollection1);
            }

            gradientStopCollection1 = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GradientStopCollection1(pGradientStopCollection1) : null;

            return hr;
        }

        public HResult CreateImageBrush([Optional] ID2D1Image* image, D2D1_IMAGE_BRUSH_PROPERTIES* imageBrushProperties, out D2D1ImageBrush? imageBrush)
        {
            ID2D1ImageBrush* pImageBrush;
            int hr = Pointer->CreateImageBrush(image, imageBrushProperties, &pImageBrush);

            imageBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ImageBrush(pImageBrush) : null;

            return hr;
        }

        public HResult CreateImageBrush(
            [Optional] ID2D1Image* image,
            [Optional] D2D1_IMAGE_BRUSH_PROPERTIES* imageBrushProperties,
            [Optional] D2D1_BRUSH_PROPERTIES* brushProperties,
            out D2D1ImageBrush? imageBrush)
        {
            ID2D1ImageBrush* pImageBrush;
            int hr = Pointer->CreateImageBrush(image, imageBrushProperties, brushProperties, &pImageBrush);

            imageBrush = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ImageBrush(pImageBrush) : null;

            return hr;
        }

        public D2D1Device GetDevice()
        {
            ID2D1Device* device;

            Pointer->GetDevice(&device);

            return new D2D1Device(device);
        }

        public HResult GetEffectInvalidRectangleCount(ID2D1Effect* effect, out uint rectangleCount)
        {
            fixed (uint* pRectangleCount = &rectangleCount)
            {
                return Pointer->GetEffectInvalidRectangleCount(effect, pRectangleCount);
            }
        }

        public HResult GetEffectInvalidRectangles(ID2D1Effect* effect, Span<D2D_RECT_F> rectangles)
        {
            int hr;

            fixed (D2D_RECT_F* pRectangles = rectangles)
            {
                hr = Pointer->GetEffectInvalidRectangles(effect, pRectangles, (uint)rectangles.Length);
            }

            return hr;
        }

        public HResult GetEffectRequiredInputRectangles(
            ID2D1Effect* renderEffect,
            [Optional] D2D_RECT_F* renderImageRectangle,
            D2D1_EFFECT_INPUT_DESCRIPTION* inputDescriptions,
            Span<D2D_RECT_F> requiredInputRects)
        {
            fixed (D2D_RECT_F* pRequiredInputRects = requiredInputRects)
            {
                return Pointer->GetEffectRequiredInputRectangles(
                    renderEffect,
                    renderImageRectangle,
                    inputDescriptions,
                    pRequiredInputRects,
                    (uint)requiredInputRects.Length);
            }
        }

        public HResult GetGlyphRunWorldBounds(
            D2D_POINT_2F baselineOrigin,
            DWRITE_GLYPH_RUN* glyphRun,
            DWRITE_MEASURING_MODE measuringMode,
            out D2D_RECT_F bounds)
        {
            fixed (D2D_RECT_F* pBounds = &bounds)
            {
                return Pointer->GetGlyphRunWorldBounds(baselineOrigin, glyphRun, measuringMode, pBounds);
            }
        }

        public HResult GetImageLocalBounds(ID2D1Image* image, out D2D_RECT_F localBounds)
        {
            fixed (D2D_RECT_F* pLocalBounds = &localBounds)
            {
                return Pointer->GetImageLocalBounds(image, pLocalBounds);
            }
        }

        public HResult GetImageWorldBounds(ID2D1Image* image, out D2D_RECT_F worldBounds)
        {
            fixed (D2D_RECT_F* pWorldBounds = &worldBounds)
            {
                return Pointer->GetImageWorldBounds(image, pWorldBounds);
            }
        }

        public D2D1_RENDERING_CONTROLS GetRenderingControls()
        {
            D2D1_RENDERING_CONTROLS renderingControls;

            Pointer->GetRenderingControls(&renderingControls);

            return renderingControls;
        }

        public D2D1Image? GetTarget()
        {
            ID2D1Image* pImage;

            Pointer->GetTarget(&pImage);

            return pImage is null ? null : new D2D1Image(pImage);
        }

    }
}