using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591
namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICColorContext
    {
        public HResult GetExifColorSpace(out uint value)
        {
            fixed (uint* pValue = &value)
            {
                return Pointer->GetExifColorSpace(pValue);
            }
        }

        public HResult GetProfileBytes(Span<byte> buffer, out uint actual)
        {
            fixed (byte* pBuffer = buffer)
            fixed (uint* pActual = &actual)
            {
                return Pointer->GetProfileBytes((uint)buffer.Length, pBuffer, pActual);
            }
        }

        public HResult GetType(out WICColorContextType type)
        {
            fixed (WICColorContextType* pType = &type)
            {
                return Pointer->GetType(pType);
            }
        }

        public HResult InitializeFromFilename(ReadOnlySpan<char> filename)
        {
            fixed (char* pFilename = filename)
            {
                return Pointer->InitializeFromFilename((ushort*)pFilename);
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