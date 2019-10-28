using System;

#pragma warning disable 1591
namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteFontFileStream
    {
        public HResult GetFileSize(out ulong fileSize)
        {
            fixed (ulong* pFileSize = &fileSize)
            {
                return Pointer->GetFileSize(pFileSize);
            }
        }

        public HResult GetLastWriteTime(out ulong lastWriteTime)
        {
            fixed (ulong* pLastWriteTime = &lastWriteTime)
            {
                return Pointer->GetLastWriteTime(pLastWriteTime);
            }
        }

        public HResult ReadFileFragment(out ReadOnlySpan<byte> fragmentStart, ulong fileOffset, int fragmentSize, out void* fragmentContext)
        {
            if (fragmentSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fragmentSize), fragmentSize, null);
            }

            void* pFragmentStart;
            int hr;

            fixed (void** pFragmentContext = &fragmentContext)
            {
                hr = Pointer->ReadFileFragment(&pFragmentStart, fileOffset, (ulong)fragmentSize, pFragmentContext);
            }

            fragmentStart = new ReadOnlySpan<byte>(pFragmentStart, fragmentSize);

            return hr;
        }
    }
}