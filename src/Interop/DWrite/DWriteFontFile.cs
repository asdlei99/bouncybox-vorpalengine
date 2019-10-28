using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontFile" /> COM interface.</summary>
    public unsafe partial class DWriteFontFile : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontFile" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontFile(IDWriteFontFile* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontFile* Pointer => (IDWriteFontFile*)base.Pointer;

        public HResult Analyze(
            out bool isSupportedFontType,
            DWRITE_FONT_FILE_TYPE* fontFileType,
            [Optional] DWRITE_FONT_FACE_TYPE* fontFaceType,
            uint* numberOfFaces)
        {
            int iIsSupportedFontType;
            int hr = Pointer->Analyze(&iIsSupportedFontType, fontFileType, fontFaceType, numberOfFaces);

            isSupportedFontType = iIsSupportedFontType == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetLoader(IDWriteFontFileLoader** fontFileLoader)
        {
            return Pointer->GetLoader(fontFileLoader);
        }

        public HResult GetReferenceKey(void** fontFileReferenceKey, uint* fontFileReferenceKeySize)
        {
            return Pointer->GetReferenceKey(fontFileReferenceKey, fontFileReferenceKeySize);
        }

        public static implicit operator IDWriteFontFile*(DWriteFontFile value)
        {
            return value.Pointer;
        }
    }
}