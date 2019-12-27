using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapLock
    {
        public HResult GetDataPointer(out uint bufferSize, out byte* data)
        {
            fixed (uint* pBufferSize = &bufferSize)
            fixed (byte** ppData = &data)
            {
                return Pointer->GetDataPointer(pBufferSize, ppData);
            }
        }

        public HResult GetPixelFormat(out Guid pixelFormat)
        {
            fixed (Guid* pPixelFormat = &pixelFormat)
            {
                return Pointer->GetPixelFormat(pPixelFormat);
            }
        }

        public HResult GetSize(out uint width, out uint height)
        {
            fixed (uint* pWidth = &width)
            fixed (uint* pHeight = &height)
            {
                return Pointer->GetSize(pWidth, pHeight);
            }
        }

        public HResult GetStride(out uint stride)
        {
            fixed (uint* pStride = &stride)
            {
                return Pointer->GetStride(pStride);
            }
        }
    }
}