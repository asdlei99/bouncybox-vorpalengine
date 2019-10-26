using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteGlyphRunAnalysis
    {
        public HResult CreateAlphaTexture(DWRITE_TEXTURE_TYPE textureType, RECT* textureBounds, Span<byte> alphaValues)
        {
            fixed (byte* pAlphaValues = alphaValues)
            {
                return Pointer->CreateAlphaTexture(textureType, textureBounds, pAlphaValues, (uint)alphaValues.Length);
            }
        }

        public HResult GetAlphaBlendParams(
            IDWriteRenderingParams* renderingParams,
            out float blendGamma,
            out float blendEnhancedContrast,
            out float blendClearTypeLevel)
        {
            fixed (float* pBlendGamma = &blendGamma)
            fixed (float* pBlendEnhancedContrast = &blendEnhancedContrast)
            fixed (float* pBlendClearTypeLevel = &blendClearTypeLevel)
            {
                return Pointer->GetAlphaBlendParams(renderingParams, pBlendGamma, pBlendEnhancedContrast, pBlendClearTypeLevel);
            }
        }

        public HResult GetAlphaTextureBounds(DWRITE_TEXTURE_TYPE textureType, out RECT textureBounds)
        {
            fixed (RECT* pTextureBounds = &textureBounds)
            {
                return Pointer->GetAlphaTextureBounds(textureType, pTextureBounds);
            }
        }
    }
}