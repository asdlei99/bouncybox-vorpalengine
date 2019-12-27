using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICImageEncoder" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICImageEncoder : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICImageEncoder" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICImageEncoder(IWICImageEncoder* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICImageEncoder* Pointer => (IWICImageEncoder*)base.Pointer;

        public HResult WriteFrame(IUnknown* image, IWICBitmapFrameEncode* frameEncode, WICImageParameters* imageParameters)
        {
            return Pointer->WriteFrame(image, frameEncode, imageParameters);
        }

        public HResult WriteFrameThumbnail(IUnknown* image, IWICBitmapFrameEncode* frameEncode, WICImageParameters* imageParameters)
        {
            return Pointer->WriteFrameThumbnail(image, frameEncode, imageParameters);
        }

        public HResult WriteThumbnail(IUnknown* image, IWICBitmapEncoder* encoder, WICImageParameters* imageParameters)
        {
            return Pointer->WriteThumbnail(image, encoder, imageParameters);
        }

        public static implicit operator IWICImageEncoder*(WICImageEncoder value)
        {
            return value.Pointer;
        }
    }
}