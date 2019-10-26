using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    public unsafe partial class DWriteTypography
    {
        public HResult GetFontFeature(uint fontFeatureIndex, out DWRITE_FONT_FEATURE fontFeature)
        {
            fixed (DWRITE_FONT_FEATURE* pFontFeature = &fontFeature)
            {
                return Pointer->GetFontFeature(fontFeatureIndex, pFontFeature);
            }
        }
    }
}