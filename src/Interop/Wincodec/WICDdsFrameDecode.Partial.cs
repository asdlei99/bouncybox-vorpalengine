using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDdsFrameDecode
    {
        public HResult CopyBlocks([Optional] WICRect* boundsInBlocks, uint stride, Span<byte> buffer)
        {
            fixed (byte* pBuffer = buffer)
            {
                return Pointer->CopyBlocks(boundsInBlocks, stride, (uint)buffer.Length, pBuffer);
            }
        }

        public HResult GetFormatInfo(out WICDdsFormatInfo formatInfo)
        {
            fixed (WICDdsFormatInfo* pFormatInfo = &formatInfo)
            {
                return Pointer->GetFormatInfo(pFormatInfo);
            }
        }

        public HResult GetSizeInBlocks(out uint widthInBlocks, out uint heightInBlocks)
        {
            fixed (uint* pWidthInBlocks = &widthInBlocks)
            fixed (uint* pHeightInBlocks = &heightInBlocks)
            {
                return Pointer->GetSizeInBlocks(pWidthInBlocks, pHeightInBlocks);
            }
        }
    }
}