using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICDdsFrameDecode" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDdsFrameDecode : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICDdsFrameDecode" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICDdsFrameDecode(IWICDdsFrameDecode* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICDdsFrameDecode* Pointer => (IWICDdsFrameDecode*)base.Pointer;

        public HResult CopyBlocks([Optional] WICRect* boundsInBlocks, uint stride, uint bufferSize, byte* buffer)
        {
            return Pointer->CopyBlocks(boundsInBlocks, stride, bufferSize, buffer);
        }

        public HResult GetFormatInfo(WICDdsFormatInfo* formatInfo)
        {
            return Pointer->GetFormatInfo(formatInfo);
        }

        public HResult GetSizeInBlocks(uint* widthInBlocks, uint* heightInBlocks)
        {
            return Pointer->GetSizeInBlocks(widthInBlocks, heightInBlocks);
        }

        public static implicit operator IWICDdsFrameDecode*(WICDdsFrameDecode value)
        {
            return value.Pointer;
        }
    }
}