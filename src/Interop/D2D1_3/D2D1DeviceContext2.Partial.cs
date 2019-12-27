using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1DeviceContext2
    {
        public HResult CreateGradientMesh(ReadOnlySpan<D2D1_GRADIENT_MESH_PATCH> patches, out D2D1GradientMesh? gradientMesh)
        {
            ID2D1GradientMesh* pGradientMesh;
            int hr;

            fixed (D2D1_GRADIENT_MESH_PATCH* pPatches = patches)
            {
                hr = Pointer->CreateGradientMesh(pPatches, (uint)patches.Length, &pGradientMesh);
            }

            gradientMesh = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GradientMesh(pGradientMesh) : null;

            return hr;
        }

        public HResult CreateImageSourceFromDxgi(
            ReadOnlySpan<Pointer<IDXGISurface>> surfaces,
            DXGI_COLOR_SPACE_TYPE colorSpace,
            D2D1_IMAGE_SOURCE_FROM_DXGI_OPTIONS options,
            out D2D1ImageSource? imageSource)
        {
            ID2D1ImageSource* pImageSource;
            int hr;

            fixed (Pointer<IDXGISurface>* pSurfaces = surfaces)
            {
                hr = Pointer->CreateImageSourceFromDxgi((IDXGISurface**)pSurfaces, (uint)surfaces.Length, colorSpace, options, &pImageSource);
            }

            imageSource = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ImageSource(pImageSource) : null;

            return hr;
        }

        public HResult CreateImageSourceFromWic(
            IWICBitmapSource* wicBitmapSource,
            D2D1_IMAGE_SOURCE_LOADING_OPTIONS loadingOptions,
            D2D1_ALPHA_MODE alphaMode,
            out D2D1ImageSourceFromWic? imageSource)
        {
            ID2D1ImageSourceFromWic* pImageSource;
            int hr = Pointer->CreateImageSourceFromWic(wicBitmapSource, loadingOptions, alphaMode, &pImageSource);

            imageSource = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ImageSourceFromWic(pImageSource) : null;

            return hr;
        }

        public HResult CreateImageSourceFromWic(IWICBitmapSource* wicBitmapSource, out D2D1ImageSourceFromWic? imageSource)
        {
            ID2D1ImageSourceFromWic* pImageSource;
            int hr = Pointer->CreateImageSourceFromWic(wicBitmapSource, &pImageSource);

            imageSource = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ImageSourceFromWic(pImageSource) : null;

            return hr;
        }

        public HResult CreateImageSourceFromWic(
            IWICBitmapSource* wicBitmapSource,
            D2D1_IMAGE_SOURCE_LOADING_OPTIONS loadingOptions,
            out D2D1ImageSourceFromWic? imageSource)
        {
            ID2D1ImageSourceFromWic* pImageSource;
            int hr = Pointer->CreateImageSourceFromWic(wicBitmapSource, loadingOptions, &pImageSource);

            imageSource = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ImageSourceFromWic(pImageSource) : null;

            return hr;
        }

        public HResult CreateInk(D2D1_INK_POINT* startPoint, out D2D1Ink? ink)
        {
            ID2D1Ink* pInk;
            int hr = Pointer->CreateInk(startPoint, &pInk);

            ink = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Ink(pInk) : null;

            return hr;
        }

        public HResult CreateInkStyle([Optional] D2D1_INK_STYLE_PROPERTIES* inkStyleProperties, out D2D1InkStyle? inkStyle)
        {
            ID2D1InkStyle* pInkStyle;
            int hr = Pointer->CreateInkStyle(inkStyleProperties, &pInkStyle);

            inkStyle = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1InkStyle(pInkStyle) : null;

            return hr;
        }

        public HResult CreateLookupTable3D(
            D2D1_BUFFER_PRECISION precision,
            uint* extents,
            ReadOnlySpan<byte> data,
            ReadOnlySpan<uint> strides,
            out D2D1LookupTable3D? lookupTable)
        {
            ID2D1LookupTable3D* pLookupTable;
            int hr;

            fixed (byte* pData = data)
            fixed (uint* pStrides = strides)
            {
                hr = Pointer->CreateLookupTable3D(precision, extents, pData, (uint)data.Length, pStrides, &pLookupTable);
            }

            lookupTable = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1LookupTable3D(pLookupTable) : null;

            return hr;
        }

        public HResult GetGradientMeshWorldBounds(ID2D1GradientMesh* gradientMesh, out D2D_RECT_F bounds)
        {
            fixed (D2D_RECT_F* pBounds = &bounds)
            {
                return Pointer->GetGradientMeshWorldBounds(gradientMesh, pBounds);
            }
        }
    }
}