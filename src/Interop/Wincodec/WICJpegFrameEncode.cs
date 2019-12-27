using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICJpegFrameEncode" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICJpegFrameEncode : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICJpegFrameEncode" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICJpegFrameEncode(IWICJpegFrameEncode* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICJpegFrameEncode* Pointer => (IWICJpegFrameEncode*)base.Pointer;

        public HResult GetAcHuffmanTable(uint scanIndex, uint tableIndex, DXGI_JPEG_AC_HUFFMAN_TABLE* acHuffmanTable)
        {
            return Pointer->GetAcHuffmanTable(scanIndex, tableIndex, acHuffmanTable);
        }

        public HResult GetDcHuffmanTable(uint scanIndex, uint tableIndex, DXGI_JPEG_DC_HUFFMAN_TABLE* dcHuffmanTable)
        {
            return Pointer->GetDcHuffmanTable(scanIndex, tableIndex, dcHuffmanTable);
        }

        public HResult GetQuantizationTable(uint scanIndex, uint tableIndex, DXGI_JPEG_QUANTIZATION_TABLE* quantizationTable)
        {
            return Pointer->GetQuantizationTable(scanIndex, tableIndex, quantizationTable);
        }

        public HResult WriteScan(uint scanDataCount, byte* scanData)
        {
            return Pointer->WriteScan(scanDataCount, scanData);
        }

        public static implicit operator IWICJpegFrameEncode*(WICJpegFrameEncode value)
        {
            return value.Pointer;
        }
    }
}