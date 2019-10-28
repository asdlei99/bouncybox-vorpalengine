using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteLocalFontFileLoader
    {
        public HResult GetFilePathFromKey(ReadOnlySpan<byte> fontFileReferenceKey, Span<char> filePath)
        {
            fixed (byte* pFontFileReferenceKey = fontFileReferenceKey)
            fixed (char* pFilePath = filePath)
            {
                return Pointer->GetFilePathFromKey(pFontFileReferenceKey, (uint)fontFileReferenceKey.Length, (ushort*)pFilePath, (uint)filePath.Length);
            }
        }

        public HResult GetFilePathLengthFromKey(ReadOnlySpan<byte> fontFileReferenceKey, out uint filePathLength)
        {
            fixed (byte* pFontFileReferenceKey = fontFileReferenceKey)
            fixed (uint* pFilePathLength = &filePathLength)
            {
                return Pointer->GetFilePathLengthFromKey(pFontFileReferenceKey, (uint)fontFileReferenceKey.Length, pFilePathLength);
            }
        }

        public HResult GetLastWriteTimeFromKey(ReadOnlySpan<byte> fontFileReferenceKey, out FILETIME lastWriteTime)
        {
            fixed (byte* pFontFileReferenceKey = fontFileReferenceKey)
            fixed (FILETIME* pLastWriteTime = &lastWriteTime)
            {
                return Pointer->GetLastWriteTimeFromKey(pFontFileReferenceKey, (uint)fontFileReferenceKey.Length, pLastWriteTime);
            }
        }
    }
}