using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFontFileStream" /> COM interface.</summary>
    public unsafe partial class DWriteFontFileStream : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFontFileStream" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFontFileStream(IDWriteFontFileStream* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFontFileStream* Pointer => (IDWriteFontFileStream*)base.Pointer;

        public HResult GetFileSize(ulong* fileSize)
        {
            return Pointer->GetFileSize(fileSize);
        }

        public HResult GetLastWriteTime(ulong* lastWriteTime)
        {
            return Pointer->GetLastWriteTime(lastWriteTime);
        }

        public HResult ReadFileFragment(void** fragmentStart, ulong fileOffset, ulong fragmentSize, void** fragmentContext)
        {
            return Pointer->ReadFileFragment(fragmentStart, fileOffset, fragmentSize, fragmentContext);
        }

        public void ReleaseFileFragment(void* fragmentContext)
        {
            Pointer->ReleaseFileFragment(fragmentContext);
        }

        public static implicit operator IDWriteFontFileStream*(DWriteFontFileStream value)
        {
            return value.Pointer;
        }
    }
}