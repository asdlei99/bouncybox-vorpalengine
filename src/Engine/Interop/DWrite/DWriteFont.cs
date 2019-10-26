using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFont" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DWriteFont : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFont" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFont(IDWriteFont* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFont* Pointer => (IDWriteFont*)base.Pointer;

        public HResult CreateFontFace(IDWriteFontFace** fontFace)
        {
            return Pointer->CreateFontFace(fontFace);
        }

        public HResult GetFaceNames(IDWriteLocalizedStrings** names)
        {
            return Pointer->GetFaceNames(names);
        }

        public HResult GetFontFamily(IDWriteFontFamily** fontFamily)
        {
            return Pointer->GetFontFamily(fontFamily);
        }

        public HResult GetInformationalStrings(
            DWRITE_INFORMATIONAL_STRING_ID informationalStringID,
            IDWriteLocalizedStrings** informationalStrings,
            out bool exists)
        {
            int iExists;
            int hr = Pointer->GetInformationalStrings(informationalStringID, informationalStrings, &iExists);

            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public void GetMetrics(DWRITE_FONT_METRICS* fontMetrics)
        {
            Pointer->GetMetrics(fontMetrics);
        }

        public DWRITE_FONT_SIMULATIONS GetSimulations()
        {
            return Pointer->GetSimulations();
        }

        public DWRITE_FONT_STRETCH GetStretch()
        {
            return Pointer->GetStretch();
        }

        public DWRITE_FONT_STYLE GetStyle()
        {
            return Pointer->GetStyle();
        }

        public DWRITE_FONT_WEIGHT GetWeight()
        {
            return Pointer->GetWeight();
        }

        public HResult HasCharacter(uint unicodeValue, out bool exists)
        {
            int iExists;
            int hr = Pointer->HasCharacter(unicodeValue, &iExists);

            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public bool IsSymbolFont()
        {
            return Pointer->IsSymbolFont() == TerraFX.Interop.Windows.TRUE;
        }

        public static implicit operator IDWriteFont*(DWriteFont value)
        {
            return value.Pointer;
        }
    }
}