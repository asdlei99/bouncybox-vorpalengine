using System;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    public unsafe partial class SequentialStream
    {
        public HResult Read(Span<byte> buffer, out uint readCount)
        {
            fixed (byte* pBuffer = buffer)
            fixed (uint* pReadCount = &readCount)
            {
                return Pointer->Read(pBuffer, (uint)buffer.Length, pReadCount);
            }
        }

        public HResult Write(ReadOnlySpan<byte> buffer, out uint writtenCount)
        {
            fixed (byte* pBuffer = buffer)
            fixed (uint* pWrittenCount = &writtenCount)
            {
                return Pointer->Write(pBuffer, (uint)buffer.Length, pWrittenCount);
            }
        }
    }
}