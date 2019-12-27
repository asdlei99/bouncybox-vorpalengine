using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPalette
    {
        public HResult GetColorCount(out uint count)
        {
            fixed (uint* pCount = &count)
            {
                return Pointer->GetColorCount(pCount);
            }
        }

        public HResult GetColors(ReadOnlySpan<uint> colors, out uint actualColorCount)
        {
            fixed (uint* pColors = colors)
            fixed (uint* pActualColorCount = &actualColorCount)
            {
                return Pointer->GetColors((uint)colors.Length, pColors, pActualColorCount);
            }
        }

        public HResult GetType(out WICBitmapPaletteType paletteType)
        {
            fixed (WICBitmapPaletteType* pPaletteType = &paletteType)
            {
                return Pointer->GetType(pPaletteType);
            }
        }

        public HResult InitializeCustom(ReadOnlySpan<uint> colors)
        {
            fixed (uint* pColors = colors)
            {
                return Pointer->InitializeCustom(pColors, (uint)colors.Length);
            }
        }
    }
}