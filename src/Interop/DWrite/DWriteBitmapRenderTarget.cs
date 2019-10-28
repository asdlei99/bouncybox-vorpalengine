using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteBitmapRenderTarget" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DWriteBitmapRenderTarget : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteBitmapRenderTarget" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteBitmapRenderTarget(IDWriteBitmapRenderTarget* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteBitmapRenderTarget* Pointer => (IDWriteBitmapRenderTarget*)base.Pointer;

        public HResult DrawGlyphRun(
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_MEASURING_MODE measuringMode,
            DWRITE_GLYPH_RUN* glyphRun,
            IDWriteRenderingParams* renderingParams,
            uint textColor,
            [Optional] RECT* blackBoxRect)
        {
            return Pointer->DrawGlyphRun(baselineOriginX, baselineOriginY, measuringMode, glyphRun, renderingParams, textColor, blackBoxRect);
        }

        public HResult GetCurrentTransform(DWRITE_MATRIX* transform)
        {
            return Pointer->GetCurrentTransform(transform);
        }

        public IntPtr GetMemoryDC()
        {
            return Pointer->GetMemoryDC();
        }

        public float GetPixelsPerDip()
        {
            return Pointer->GetPixelsPerDip();
        }

        public HResult GetSize(SIZE* size)
        {
            return Pointer->GetSize(size);
        }

        public HResult Resize(uint width, uint height)
        {
            return Pointer->Resize(width, height);
        }

        public HResult SetCurrentTransform([Optional] DWRITE_MATRIX* transform)
        {
            return Pointer->SetCurrentTransform(transform);
        }

        public HResult SetPixelsPerDip(float pixelsPerDip)
        {
            return Pointer->SetPixelsPerDip(pixelsPerDip);
        }

        public static implicit operator IDWriteBitmapRenderTarget*(DWriteBitmapRenderTarget value)
        {
            return value.Pointer;
        }
    }
}