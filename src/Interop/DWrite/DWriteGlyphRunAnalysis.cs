using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteGlyphRunAnalysis" /> COM interface.</summary>
    public unsafe partial class DWriteGlyphRunAnalysis : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteGlyphRunAnalysis" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteGlyphRunAnalysis(IDWriteGlyphRunAnalysis* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteGlyphRunAnalysis* Pointer => (IDWriteGlyphRunAnalysis*)base.Pointer;

        public HResult CreateAlphaTexture(DWRITE_TEXTURE_TYPE textureType, RECT* textureBounds, byte* alphaValues, uint bufferSize)
        {
            return Pointer->CreateAlphaTexture(textureType, textureBounds, alphaValues, bufferSize);
        }

        public HResult GetAlphaBlendParams(IDWriteRenderingParams* renderingParams, float* blendGamma, float* blendEnhancedContrast, float* blendClearTypeLevel)
        {
            return Pointer->GetAlphaBlendParams(renderingParams, blendGamma, blendEnhancedContrast, blendClearTypeLevel);
        }

        public HResult GetAlphaTextureBounds(DWRITE_TEXTURE_TYPE textureType, RECT* textureBounds)
        {
            return Pointer->GetAlphaTextureBounds(textureType, textureBounds);
        }

        public static implicit operator IDWriteGlyphRunAnalysis*(DWriteGlyphRunAnalysis value)
        {
            return value.Pointer;
        }
    }
}