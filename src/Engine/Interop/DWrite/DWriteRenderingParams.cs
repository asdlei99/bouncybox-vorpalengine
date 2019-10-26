using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteRenderingParams" /> COM interface.</summary>
    public unsafe class DWriteRenderingParams : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteRenderingParams" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteRenderingParams(IDWriteRenderingParams* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteRenderingParams* Pointer => (IDWriteRenderingParams*)base.Pointer;

        public float GetClearTypeLevel()
        {
            return Pointer->GetClearTypeLevel();
        }

        public float GetEnhancedContrast()
        {
            return Pointer->GetEnhancedContrast();
        }

        public float GetGamma()
        {
            return Pointer->GetGamma();
        }

        public DWRITE_PIXEL_GEOMETRY GetPixelGeometry()
        {
            return Pointer->GetPixelGeometry();
        }

        public DWRITE_RENDERING_MODE GetRenderingMode()
        {
            return Pointer->GetRenderingMode();
        }

        public static implicit operator IDWriteRenderingParams*(DWriteRenderingParams value)
        {
            return value.Pointer;
        }
    }
}