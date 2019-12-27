using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapSource
    {
        public HResult CopyPixels([Optional] WICRect* rect, uint stride, Span<byte> buffer)
        {
            fixed (byte* pBuffer = buffer)
            {
                return Pointer->CopyPixels(rect, stride, (uint)buffer.Length, pBuffer);
            }
        }

        public HResult GetPixelFormat(out Guid pixelFormat)
        {
            fixed (Guid* pPixelFormat = &pixelFormat)
            {
                return Pointer->GetPixelFormat(pPixelFormat);
            }
        }

        public HResult GetResolution(out double dpiX, out double dpiY)
        {
            fixed (double* pDpiX = &dpiX)
            fixed (double* pDpiY = &dpiY)
            {
                return Pointer->GetResolution(pDpiX, pDpiY);
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
    }
}