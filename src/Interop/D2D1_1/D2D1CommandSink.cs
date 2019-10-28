using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1CommandSink" /> COM interface.</summary>
    public unsafe class D2D1CommandSink : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1CommandSink" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1CommandSink(ID2D1CommandSink* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1CommandSink* Pointer => (ID2D1CommandSink*)base.Pointer;

        public HResult BeginDraw()
        {
            return Pointer->BeginDraw();
        }

        public HResult Clear([Optional] DXGI_RGBA* color)
        {
            return Pointer->Clear(color);
        }

        public HResult DrawBitmap(
            ID2D1Bitmap* bitmap,
            [Optional] D2D_RECT_F* destinationRectangle,
            float opacity,
            D2D1_INTERPOLATION_MODE interpolationMode,
            [Optional] D2D_RECT_F* sourceRectangle,
            [Optional] D2D_MATRIX_4X4_F* perspectiveTransform)
        {
            return Pointer->DrawBitmap(bitmap, destinationRectangle, opacity, interpolationMode, sourceRectangle, perspectiveTransform);
        }

        public HResult DrawGdiMetafile(ID2D1GdiMetafile* gdiMetafile, [Optional] D2D_POINT_2F* targetOffset)
        {
            return Pointer->DrawGdiMetafile(gdiMetafile, targetOffset);
        }

        public HResult DrawGeometry(ID2D1Geometry* geometry, ID2D1Brush* brush, float strokeWidth, [Optional] ID2D1StrokeStyle* strokeStyle)
        {
            return Pointer->DrawGeometry(geometry, brush, strokeWidth, strokeStyle);
        }

        public HResult DrawGlyphRun(
            D2D_POINT_2F baselineOrigin,
            DWRITE_GLYPH_RUN* glyphRun,
            [Optional] DWRITE_GLYPH_RUN_DESCRIPTION* glyphRunDescription,
            ID2D1Brush* foregroundBrush,
            DWRITE_MEASURING_MODE measuringMode)
        {
            return Pointer->DrawGlyphRun(baselineOrigin, glyphRun, glyphRunDescription, foregroundBrush, measuringMode);
        }

        public HResult DrawImage(
            ID2D1Image* image,
            [Optional] D2D_POINT_2F* targetOffset,
            [Optional] D2D_RECT_F* imageRectangle,
            D2D1_INTERPOLATION_MODE interpolationMode,
            D2D1_COMPOSITE_MODE compositeMode)
        {
            return Pointer->DrawImage(image, targetOffset, imageRectangle, interpolationMode, compositeMode);
        }

        public HResult DrawLine(D2D_POINT_2F point0, D2D_POINT_2F point1, ID2D1Brush* brush, float strokeWidth, [Optional] ID2D1StrokeStyle* strokeStyle)
        {
            return Pointer->DrawLine(point0, point1, brush, strokeWidth, strokeStyle);
        }

        public HResult DrawRectangle(D2D_RECT_F* rect, ID2D1Brush* brush, float strokeWidth, [Optional] ID2D1StrokeStyle* strokeStyle)
        {
            return Pointer->DrawRectangle(rect, brush, strokeWidth, strokeStyle);
        }

        public HResult EndDraw()
        {
            return Pointer->EndDraw();
        }

        public HResult FillGeometry(ID2D1Geometry* geometry, ID2D1Brush* brush, [Optional] ID2D1Brush* opacityBrush)
        {
            return Pointer->FillGeometry(geometry, brush, opacityBrush);
        }

        public HResult FillMesh(ID2D1Mesh* mesh, ID2D1Brush* brush)
        {
            return Pointer->FillMesh(mesh, brush);
        }

        public HResult FillOpacityMask(
            ID2D1Bitmap* opacityMask,
            ID2D1Brush* brush,
            [Optional] D2D_RECT_F* destinationRectangle,
            [Optional] D2D_RECT_F* sourceRectangle)
        {
            return Pointer->FillOpacityMask(opacityMask, brush, destinationRectangle, sourceRectangle);
        }

        public HResult FillRectangle(D2D_RECT_F* rect, ID2D1Brush* brush)
        {
            return Pointer->FillRectangle(rect, brush);
        }

        public HResult PopAxisAlignedClip()
        {
            return Pointer->PopAxisAlignedClip();
        }

        public HResult PopLayer()
        {
            return Pointer->PopLayer();
        }

        public HResult PushAxisAlignedClip(D2D_RECT_F* clipRect, D2D1_ANTIALIAS_MODE antialiasMode)
        {
            return Pointer->PushAxisAlignedClip(clipRect, antialiasMode);
        }

        public HResult PushLayer(D2D1_LAYER_PARAMETERS1* layerParameters1, [Optional] ID2D1Layer* layer)
        {
            return Pointer->PushLayer(layerParameters1, layer);
        }

        public HResult SetAntialiasMode(D2D1_ANTIALIAS_MODE antialiasMode)
        {
            return Pointer->SetAntialiasMode(antialiasMode);
        }

        public HResult SetPrimitiveBlend(D2D1_PRIMITIVE_BLEND primitiveBlend)
        {
            return Pointer->SetPrimitiveBlend(primitiveBlend);
        }

        public HResult SetTags(ulong tag1, ulong tag2)
        {
            return Pointer->SetTags(tag1, tag2);
        }

        public HResult SetTextAntialiasMode(D2D1_TEXT_ANTIALIAS_MODE textAntialiasMode)
        {
            return Pointer->SetTextAntialiasMode(textAntialiasMode);
        }

        public HResult SetTextRenderingParams([Optional] IDWriteRenderingParams* textRenderingParams)
        {
            return Pointer->SetTextRenderingParams(textRenderingParams);
        }

        public HResult SetTransform(D2D_MATRIX_3X2_F* transform)
        {
            return Pointer->SetTransform(transform);
        }

        public HResult SetUnitMode(D2D1_UNIT_MODE unitMode)
        {
            return Pointer->SetUnitMode(unitMode);
        }

        public static implicit operator ID2D1CommandSink*(D2D1CommandSink value)
        {
            return value.Pointer;
        }
    }
}