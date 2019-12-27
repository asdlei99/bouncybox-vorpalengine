using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapEncoderInfo" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapEncoderInfo : WICBitmapCodecInfo
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapEncoderInfo" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapEncoderInfo(IWICBitmapEncoderInfo* pointer) : base((IWICBitmapCodecInfo*)pointer)
        {
        }

        public new IWICBitmapEncoderInfo* Pointer => (IWICBitmapEncoderInfo*)base.Pointer;

        public HResult CreateInstance(IWICBitmapEncoder** bitmapEncoder)
        {
            return Pointer->CreateInstance(bitmapEncoder);
        }

        public static implicit operator IWICBitmapEncoderInfo*(WICBitmapEncoderInfo value)
        {
            return value.Pointer;
        }
    }
}