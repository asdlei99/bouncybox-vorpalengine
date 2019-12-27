using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICJpegFrameEncode
    {
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

        public HResult GetQuantizationTable(uint scanIndex, uint tableIndex, out DXGI_JPEG_QUANTIZATION_TABLE quantizationTable)
        {
            fixed (DXGI_JPEG_QUANTIZATION_TABLE* pQuantizationTable = &quantizationTable)
            {
                return Pointer->GetQuantizationTable(scanIndex, tableIndex, pQuantizationTable);
            }
        }

        public HResult WriteScan(ReadOnlySpan<byte> scanData)
        {
            fixed (byte* pScanData = scanData)
            {
                return Pointer->WriteScan((uint)scanData.Length, pScanData);
            }
        }
    }
}