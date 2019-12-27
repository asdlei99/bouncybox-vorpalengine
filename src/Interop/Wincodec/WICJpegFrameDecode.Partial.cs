using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICJpegFrameDecode
    {
        public HResult CopyMinimalStream(uint streamOffset, ReadOnlySpan<byte> streamData, out uint actualStreamDataCount)
        {
            fixed (byte* pStreamData = streamData)
            fixed (uint* pActualStreamDataCount = &actualStreamDataCount)
            {
                return Pointer->CopyMinimalStream(streamOffset, (uint)streamData.Length, pStreamData, pActualStreamDataCount);
            }
        }

        public HResult CopyScan(uint scanIndex, uint scanOffset, ReadOnlySpan<byte> scanData, out uint actualScanDataCount)
        {
            fixed (byte* pScanData = scanData)
            fixed (uint* pActualScanDataCount = &actualScanDataCount)
            {
                return Pointer->CopyScan(scanIndex, scanOffset, (uint)scanData.Length, pScanData, pActualScanDataCount);
            }
        }

        public HResult GetAcHuffmanTable(uint scanIndex, uint tableIndex, out DXGI_JPEG_AC_HUFFMAN_TABLE acHuffmanTable)
        {
            fixed (DXGI_JPEG_AC_HUFFMAN_TABLE* pAcHuffmanTable = &acHuffmanTable)
            {
                return Pointer->GetAcHuffmanTable(scanIndex, tableIndex, pAcHuffmanTable);
            }
        }

        public HResult GetDcHuffmanTable(uint scanIndex, uint tableIndex, out DXGI_JPEG_DC_HUFFMAN_TABLE dcHuffmanTable)
        {
            fixed (DXGI_JPEG_DC_HUFFMAN_TABLE* pDcHuffmanTable = &dcHuffmanTable)
            {
                return Pointer->GetDcHuffmanTable(scanIndex, tableIndex, pDcHuffmanTable);
            }
        }

        public HResult GetFrameHeader(out WICJpegFrameHeader frameHeader)
        {
            fixed (WICJpegFrameHeader* pFrameHeader = &frameHeader)
            {
                return Pointer->GetFrameHeader(pFrameHeader);
            }
        }

        public HResult GetQuantizationTable(uint scanIndex, uint tableIndex, out DXGI_JPEG_QUANTIZATION_TABLE quantizationTable)
        {
            fixed (DXGI_JPEG_QUANTIZATION_TABLE* pQuantizationTable = &quantizationTable)
            {
                return Pointer->GetQuantizationTable(scanIndex, tableIndex, pQuantizationTable);
            }
        }

        public HResult GetScanHeader(uint scanIndex, out WICJpegScanHeader scanHeader)
        {
            fixed (WICJpegScanHeader* pScanHeader = &scanHeader)
            {
                return Pointer->GetScanHeader(scanIndex, pScanHeader);
            }
        }
    }
}