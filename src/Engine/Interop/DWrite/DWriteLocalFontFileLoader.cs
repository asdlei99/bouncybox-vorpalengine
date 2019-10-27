using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteLocalFontFileLoader" /> COM interface.</summary>
    public unsafe partial class DWriteLocalFontFileLoader : DWriteFontFileLoader
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteLocalFontFileLoader" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteLocalFontFileLoader(IDWriteLocalFontFileLoader* pointer) : base((IDWriteFontFileLoader*)pointer)
        {
        }

        public new IDWriteLocalFontFileLoader* Pointer => (IDWriteLocalFontFileLoader*)base.Pointer;

        public HResult GetFilePathFromKey(void* fontFileReferenceKey, uint fontFileReferenceKeySize, ushort* filePath, uint filePathSize)
        {
            return Pointer->GetFilePathFromKey(fontFileReferenceKey, fontFileReferenceKeySize, filePath, filePathSize);
        }

        public HResult GetFilePathLengthFromKey(void* fontFileReferenceKey, uint fontFileReferenceKeySize, uint* filePathLength)
        {
            return Pointer->GetFilePathLengthFromKey(fontFileReferenceKey, fontFileReferenceKeySize, filePathLength);
        }

        public HResult GetLastWriteTimeFromKey(void* fontFileReferenceKey, uint fontFileReferenceKeySize, FILETIME* lastWriteTime)
        {
            return Pointer->GetLastWriteTimeFromKey(fontFileReferenceKey, fontFileReferenceKeySize, lastWriteTime);
        }

        public static implicit operator IDWriteLocalFontFileLoader*(DWriteLocalFontFileLoader value)
        {
            return value.Pointer;
        }
    }
}