using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICStream
    {
        public HResult InitializeFromFilename(ReadOnlySpan<char> fileName, uint desiredAccess)
        {
            fixed (char* pFileName = fileName)
            {
                return Pointer->InitializeFromFilename((ushort*)pFileName, desiredAccess);
            }
        }

        public HResult InitializeFromMemory(ReadOnlySpan<byte> buffer)
        {
            fixed (byte* pBuffer = buffer)
            {
                return Pointer->InitializeFromMemory(pBuffer, (uint)buffer.Length);
            }
        }
    }
}