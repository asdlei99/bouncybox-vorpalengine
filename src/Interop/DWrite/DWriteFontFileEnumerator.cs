using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontFileEnumerator" /> COM interface.</summary>
    public unsafe partial class DWriteFontFileEnumerator : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontFileEnumerator" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontFileEnumerator(IDWriteFontFileEnumerator* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontFileEnumerator* Pointer => (IDWriteFontFileEnumerator*)base.Pointer;

        public HResult GetCurrentFontFile(IDWriteFontFile** fontFile)
        {
            return Pointer->GetCurrentFontFile(fontFile);
        }

        public HResult MoveNext(out bool hasCurrentFile)
        {
            int iHasCurrentFile;
            int hr = Pointer->MoveNext(&iHasCurrentFile);

            hasCurrentFile = iHasCurrentFile == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IDWriteFontFileEnumerator*(DWriteFontFileEnumerator value)
        {
            return value.Pointer;
        }
    }
}