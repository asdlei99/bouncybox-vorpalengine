using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontFileLoader" /> COM interface.</summary>
    public unsafe partial class DWriteFontFileLoader : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontFileLoader" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontFileLoader(IDWriteFontFileLoader* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontFileLoader* Pointer => (IDWriteFontFileLoader*)base.Pointer;

        public HResult CreateStreamFromKey(void* fontFileReferenceKey, uint fontFileReferenceKeySize, IDWriteFontFileStream** fontFileStream)
        {
            return Pointer->CreateStreamFromKey(fontFileReferenceKey, fontFileReferenceKeySize, fontFileStream);
        }

        public static implicit operator IDWriteFontFileLoader*(DWriteFontFileLoader value)
        {
            return value.Pointer;
        }
    }
}