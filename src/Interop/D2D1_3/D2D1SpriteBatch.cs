using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1SpriteBatch" /> COM interface.</summary>
    public unsafe partial class D2D1SpriteBatch : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SpriteBatch" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SpriteBatch(ID2D1SpriteBatch* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1SpriteBatch* Pointer => (ID2D1SpriteBatch*)base.Pointer;

        public HResult AddSprites(
            uint spriteCount,
            D2D_RECT_F* destinationRectangles,
            [Optional] D2D_RECT_U* sourceRectangles,
            [Optional] DXGI_RGBA* colors,
            [Optional] D2D_MATRIX_3X2_F* transforms,
            uint destinationRectanglesStride,
            uint sourceRectanglesStride,
            uint colorsStride,
            uint transformsStride)
        {
            return Pointer->AddSprites(
                spriteCount,
                destinationRectangles,
                sourceRectangles,
                colors,
                transforms,
                destinationRectanglesStride,
                sourceRectanglesStride,
                colorsStride,
                transformsStride);
        }

        public void Clear()
        {
            Pointer->Clear();
        }

        public uint GetSpriteCount()
        {
            return Pointer->GetSpriteCount();
        }

        public HResult GetSprites(
            uint startIndex,
            uint spriteCount,
            D2D_RECT_F* destinationRectangles,
            D2D_RECT_U* sourceRectangles,
            DXGI_RGBA* colors,
            D2D_MATRIX_3X2_F* transforms)
        {
            return Pointer->GetSprites(startIndex, spriteCount, destinationRectangles, sourceRectangles, colors, transforms);
        }

        public HResult SetSprites(
            uint startIndex,
            uint spriteCount,
            [Optional] D2D_RECT_F* destinationRectangles,
            [Optional] D2D_RECT_U* sourceRectangles,
            [Optional] DXGI_RGBA* colors,
            [Optional] D2D_MATRIX_3X2_F* transforms,
            uint destinationRectanglesStride,
            uint sourceRectanglesStride,
            uint colorsStride,
            uint transformsStride)
        {
            return Pointer->SetSprites(
                startIndex,
                spriteCount,
                destinationRectangles,
                sourceRectangles,
                colors,
                transforms,
                destinationRectanglesStride,
                sourceRectanglesStride,
                colorsStride,
                transformsStride);
        }

        public static implicit operator ID2D1SpriteBatch*(D2D1SpriteBatch value)
        {
            return value.Pointer;
        }
    }
}