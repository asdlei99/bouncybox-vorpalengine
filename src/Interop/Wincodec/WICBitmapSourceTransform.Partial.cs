using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapSourceTransform
    {
        public HResult CopyPixels(
            [Optional] WICRect* rect,
            uint width,
            uint height,
            [Optional] Guid* format,
            WICBitmapTransformOptions transform,
            uint stride,
            Span<byte> buffer)
        {
            fixed (byte* pBuffer = buffer)
            {
                return Pointer->CopyPixels(rect, width, height, format, transform, stride, (uint)buffer.Length, pBuffer);
            }
        }

        public HResult GetClosestPixelFormat(out Guid format)
        {
            fixed (Guid* pFormat = &format)
            {
                return Pointer->GetClosestPixelFormat(pFormat);
            }
        }

        public HResult GetClosestSize(out uint width, out uint height)
        {
            fixed (uint* pWidth = &width)
            fixed (uint* pHeight = &height)
            {
                return Pointer->GetClosestSize(pWidth, pHeight);
            }
        }
    }
}