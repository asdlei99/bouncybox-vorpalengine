using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1_2;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1DeviceContext2" /> COM interface.</summary>
    public unsafe partial class D2D1DeviceContext2 : D2D1DeviceContext1
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DeviceContext2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DeviceContext2(ID2D1DeviceContext2* pointer) : base((ID2D1DeviceContext1*)pointer)
        {
        }

        public new ID2D1DeviceContext2* Pointer => (ID2D1DeviceContext2*)base.Pointer;

        public HResult CreateGradientMesh(D2D1_GRADIENT_MESH_PATCH* patches, uint patchesCount, ID2D1GradientMesh** gradientMesh)
        {
            return Pointer->CreateGradientMesh(patches, patchesCount, gradientMesh);
        }

        public HResult CreateImageSourceFromDxgi(
            IDXGISurface** surfaces,
            uint surfaceCount,
            DXGI_COLOR_SPACE_TYPE colorSpace,
            D2D1_IMAGE_SOURCE_FROM_DXGI_OPTIONS options,
            ID2D1ImageSource** imageSource)
        {
            return Pointer->CreateImageSourceFromDxgi(surfaces, surfaceCount, colorSpace, options, imageSource);
        }

        public HResult CreateImageSourceFromWic(
            IWICBitmapSource* wicBitmapSource,
            D2D1_IMAGE_SOURCE_LOADING_OPTIONS loadingOptions,
            D2D1_ALPHA_MODE alphaMode,
            ID2D1ImageSourceFromWic** imageSource)
        {
            return Pointer->CreateImageSourceFromWic(wicBitmapSource, loadingOptions, alphaMode, imageSource);
        }

        public HResult CreateImageSourceFromWic(IWICBitmapSource* wicBitmapSource, ID2D1ImageSourceFromWic** imageSource)
        {
            return Pointer->CreateImageSourceFromWic(wicBitmapSource, imageSource);
        }

        public HResult CreateImageSourceFromWic(
            IWICBitmapSource* wicBitmapSource,
            D2D1_IMAGE_SOURCE_LOADING_OPTIONS loadingOptions,
            ID2D1ImageSourceFromWic** imageSource)
        {
            return Pointer->CreateImageSourceFromWic(wicBitmapSource, loadingOptions, imageSource);
        }

        public HResult CreateInk(D2D1_INK_POINT* startPoint, ID2D1Ink** ink)
        {
            return Pointer->CreateInk(startPoint, ink);
        }

        public HResult CreateInkStyle([Optional] D2D1_INK_STYLE_PROPERTIES* inkStyleProperties, ID2D1InkStyle** inkStyle)
        {
            return Pointer->CreateInkStyle(inkStyleProperties, inkStyle);
        }

        public HResult CreateLookupTable3D(
            D2D1_BUFFER_PRECISION precision,
            uint* extents,
            byte* data,
            uint dataCount,
            uint* strides,
            ID2D1LookupTable3D** lookupTable)
        {
            return Pointer->CreateLookupTable3D(precision, extents, data, dataCount, strides, lookupTable);
        }

        public HResult CreateTransformedImageSource(
            ID2D1ImageSource* imageSource,
            D2D1_TRANSFORMED_IMAGE_SOURCE_PROPERTIES* properties,
            ID2D1TransformedImageSource** transformedImageSource)
        {
            return Pointer->CreateTransformedImageSource(imageSource, properties, transformedImageSource);
        }

        public void DrawGdiMetafile(ID2D1GdiMetafile* gdiMetafile, D2D_RECT_F* destinationRectangle = null, D2D_RECT_F* sourceRectangle = null)
        {
            Pointer->DrawGdiMetafile(gdiMetafile, destinationRectangle, sourceRectangle);
        }

        public void DrawGradientMesh(ID2D1GradientMesh* gradientMesh)
        {
            Pointer->DrawGradientMesh(gradientMesh);
        }

        public void DrawInk(ID2D1Ink* ink, ID2D1Brush* brush, ID2D1InkStyle* inkStyle = null)
        {
            Pointer->DrawInk(ink, brush, inkStyle);
        }

        public HResult GetGradientMeshWorldBounds(ID2D1GradientMesh* gradientMesh, D2D_RECT_F* pBounds)
        {
            return Pointer->GetGradientMeshWorldBounds(gradientMesh, pBounds);
        }

        public static implicit operator ID2D1DeviceContext2*(D2D1DeviceContext2 value)
        {
            return value.Pointer;
        }
    }
}