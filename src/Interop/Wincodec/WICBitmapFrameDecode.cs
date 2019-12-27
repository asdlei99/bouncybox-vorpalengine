using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapFrameDecode" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapFrameDecode : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapFrameDecode" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapFrameDecode(IWICBitmapFrameDecode* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICBitmapFrameDecode* Pointer => (IWICBitmapFrameDecode*)base.Pointer;

        public HResult GetColorContexts(uint count, IWICColorContext** colorContexts, uint* actualCount)
        {
            return Pointer->GetColorContexts(count, colorContexts, actualCount);
        }

        public HResult GetMetadataQueryReader(IWICMetadataQueryReader** metadataQueryReader)
        {
            return Pointer->GetMetadataQueryReader(metadataQueryReader);
        }

        public HResult GetThumbnail(IWICBitmapSource** thumbnail)
        {
            return Pointer->GetThumbnail(thumbnail);
        }

        public static implicit operator IWICBitmapFrameDecode*(WICBitmapFrameDecode value)
        {
            return value.Pointer;
        }
    }
}