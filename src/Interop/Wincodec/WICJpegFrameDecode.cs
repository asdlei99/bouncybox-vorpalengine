using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICJpegFrameDecode" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICJpegFrameDecode : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICJpegFrameDecode" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICJpegFrameDecode(IWICJpegFrameDecode* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICJpegFrameDecode* Pointer => (IWICJpegFrameDecode*)base.Pointer;

        public HResult ClearIndexing()
        {
            return Pointer->ClearIndexing();
        }

        public HResult CopyMinimalStream(uint streamOffset, uint streamDataCount, byte* streamData, uint* actualStreamDataCount)
        {
            return Pointer->CopyMinimalStream(streamOffset, streamDataCount, streamData, actualStreamDataCount);
        }

        public HResult CopyScan(uint scanIndex, uint scanOffset, uint scanDataCount, byte* scanData, uint* actualScanDataCount)
        {
            return Pointer->CopyScan(scanIndex, scanOffset, scanDataCount, scanData, actualScanDataCount);
        }

        public HResult DoesSupportIndexing(out bool indexingSupported)
        {
            int iIndexingSupported;
            int hr = Pointer->DoesSupportIndexing(&iIndexingSupported);

            indexingSupported = iIndexingSupported == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetAcHuffmanTable(uint scanIndex, uint tableIndex, DXGI_JPEG_AC_HUFFMAN_TABLE* acHuffmanTable)
        {
            return Pointer->GetAcHuffmanTable(scanIndex, tableIndex, acHuffmanTable);
        }

        public HResult GetDcHuffmanTable(uint scanIndex, uint tableIndex, DXGI_JPEG_DC_HUFFMAN_TABLE* dcHuffmanTable)
        {
            return Pointer->GetDcHuffmanTable(scanIndex, tableIndex, dcHuffmanTable);
        }

        public HResult GetFrameHeader(WICJpegFrameHeader* frameHeader)
        {
            return Pointer->GetFrameHeader(frameHeader);
        }

        public HResult GetQuantizationTable(uint scanIndex, uint tableIndex, DXGI_JPEG_QUANTIZATION_TABLE* quantizationTable)
        {
            return Pointer->GetQuantizationTable(scanIndex, tableIndex, quantizationTable);
        }

        public HResult GetScanHeader(uint scanIndex, WICJpegScanHeader* scanHeader)
        {
            return Pointer->GetScanHeader(scanIndex, scanHeader);
        }

        public HResult SetIndexing(WICJpegIndexingOptions options, uint horizontalIntervalSize)
        {
            return Pointer->SetIndexing(options, horizontalIntervalSize);
        }

        public static implicit operator IWICJpegFrameDecode*(WICJpegFrameDecode value)
        {
            return value.Pointer;
        }
    }
}