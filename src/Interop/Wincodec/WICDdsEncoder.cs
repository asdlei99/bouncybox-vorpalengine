using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICDdsEncoder" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDdsEncoder : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICDdsEncoder" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICDdsEncoder(IWICDdsEncoder* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICDdsEncoder* Pointer => (IWICDdsEncoder*)base.Pointer;

        public HResult CreateNewFrame(IWICBitmapFrameEncode** frameEncode, uint* arrayIndex, uint* mipLevel, uint* sliceIndex)
        {
            return Pointer->CreateNewFrame(frameEncode, arrayIndex, mipLevel, sliceIndex);
        }

        public HResult GetParameters(WICDdsParameters* parameters)
        {
            return Pointer->GetParameters(parameters);
        }

        public HResult SetParameters(WICDdsParameters* parameters)
        {
            return Pointer->SetParameters(parameters);
        }

        public static implicit operator IWICDdsEncoder*(WICDdsEncoder value)
        {
            return value.Pointer;
        }
    }
}