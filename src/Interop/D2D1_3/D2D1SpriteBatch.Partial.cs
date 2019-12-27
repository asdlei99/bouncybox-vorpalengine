using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1SpriteBatch
    {
        public HResult AddSprites(
            uint spriteCount,
            ReadOnlySpan<D2D_RECT_F> destinationRectangles,
            [Optional] ReadOnlySpan<D2D_RECT_U> sourceRectangles,
            [Optional] ReadOnlySpan<DXGI_RGBA> colors,
            [Optional] ReadOnlySpan<D2D_MATRIX_3X2_F> transforms,
            uint? destinationRectanglesStride = null,
            uint? sourceRectanglesStride = null,
            uint? colorsStride = null,
            uint? transformsStride = null)
        {
            fixed (D2D_RECT_F* pDestinationRectangles = destinationRectangles)
            fixed (D2D_RECT_U* pSourceRectangles = sourceRectangles)
            fixed (DXGI_RGBA* pColors = colors)
            fixed (D2D_MATRIX_3X2_F* pTransforms = transforms)
            {
                return Pointer->AddSprites(
                    spriteCount,
                    pDestinationRectangles,
                    pSourceRectangles,
                    pColors,
                    pTransforms,
                    destinationRectanglesStride ?? (uint)sizeof(D2D_RECT_F),
                    sourceRectanglesStride ?? (uint)sizeof(D2D_RECT_U),
                    colorsStride ?? (uint)sizeof(DXGI_RGBA),
                    transformsStride ?? (uint)sizeof(D2D_MATRIX_3X2_F));
            }
        }

        public HResult GetSprites(
            uint startIndex,
            Span<D2D_RECT_F> destinationRectangles,
            Span<D2D_RECT_U> sourceRectangles,
            Span<DXGI_RGBA> colors,
            Span<D2D_MATRIX_3X2_F> transforms)
        {
            if (destinationRectangles.Length != sourceRectangles.Length ||
                destinationRectangles.Length != colors.Length ||
                destinationRectangles.Length != transforms.Length)
            {
                throw new ArgumentException(
                    $"{nameof(destinationRectangles)}, {nameof(sourceRectangles)}, {nameof(colors)}, and {nameof(transforms)} must all have the same length.");
            }

            fixed (D2D_RECT_F* pDestinationRectangles = destinationRectangles)
            fixed (D2D_RECT_U* pSourceRectangles = sourceRectangles)
            fixed (DXGI_RGBA* pColors = colors)
            fixed (D2D_MATRIX_3X2_F* pTransforms = transforms)
            {
                return Pointer->GetSprites(startIndex, (uint)destinationRectangles.Length, pDestinationRectangles, pSourceRectangles, pColors, pTransforms);
            }
        }

        public HResult SetSprites(
            uint startIndex,
            uint spriteCount,
            [Optional] ReadOnlySpan<D2D_RECT_F> destinationRectangles,
            [Optional] ReadOnlySpan<D2D_RECT_U> sourceRectangles,
            [Optional] ReadOnlySpan<DXGI_RGBA> colors,
            [Optional] ReadOnlySpan<D2D_MATRIX_3X2_F> transforms,
            uint? destinationRectanglesStride = null,
            uint? sourceRectanglesStride = null,
            uint? colorsStride = null,
            uint? transformsStride = null)
        {
            fixed (D2D_RECT_F* pDestinationRectangles = destinationRectangles)
            fixed (D2D_RECT_U* pSourceRectangles = sourceRectangles)
            fixed (DXGI_RGBA* pColors = colors)
            fixed (D2D_MATRIX_3X2_F* pTransforms = transforms)
            {
                return Pointer->SetSprites(
                    startIndex,
                    spriteCount,
                    pDestinationRectangles,
                    pSourceRectangles,
                    pColors,
                    pTransforms,
                    destinationRectanglesStride ?? (uint)Marshal.SizeOf<D2D_RECT_F>(),
                    sourceRectanglesStride ?? (uint)Marshal.SizeOf<D2D_RECT_U>(),
                    colorsStride ?? (uint)Marshal.SizeOf<DXGI_RGBA>(),
                    transformsStride ?? (uint)Marshal.SizeOf<D2D_MATRIX_3X2_F>());
            }
        }
    }
}