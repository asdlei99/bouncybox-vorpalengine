using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICDdsDecoder" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDdsDecoder : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICDdsDecoder" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICDdsDecoder(IWICDdsDecoder* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICDdsDecoder* Pointer => (IWICDdsDecoder*)base.Pointer;

        public HResult GetFrame(uint arrayIndex, uint mipLevel, uint sliceIndex, IWICBitmapFrameDecode** bitmapFrame)
        {
            return Pointer->GetFrame(arrayIndex, mipLevel, sliceIndex, bitmapFrame);
        }

        public HResult GetParameters(WICDdsParameters* parameters)
        {
            return Pointer->GetParameters(parameters);
        }

        public static implicit operator IWICDdsDecoder*(WICDdsDecoder value)
        {
            return value.Pointer;
        }
    }
}