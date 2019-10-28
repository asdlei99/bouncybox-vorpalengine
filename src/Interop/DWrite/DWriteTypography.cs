using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteTypography" /> COM interface.</summary>
    public unsafe partial class DWriteTypography : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteTypography" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteTypography(IDWriteTypography* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteTypography* Pointer => (IDWriteTypography*)base.Pointer;

        public HResult AddFontFeature(DWRITE_FONT_FEATURE fontFeature)
        {
            return Pointer->AddFontFeature(fontFeature);
        }

        public HResult GetFontFeature(uint fontFeatureIndex, DWRITE_FONT_FEATURE* fontFeature)
        {
            return Pointer->GetFontFeature(fontFeatureIndex, fontFeature);
        }

        public HResult GetFontFeatureCount()
        {
            return Pointer->GetFontFeatureCount();
        }

        public static implicit operator IDWriteTypography*(DWriteTypography value)
        {
            return value.Pointer;
        }
    }
}