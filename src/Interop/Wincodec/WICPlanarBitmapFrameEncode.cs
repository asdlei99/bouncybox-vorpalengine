using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICPlanarBitmapFrameEncode" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPlanarBitmapFrameEncode : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICPlanarBitmapFrameEncode" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICPlanarBitmapFrameEncode(IWICPlanarBitmapFrameEncode* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICPlanarBitmapFrameEncode* Pointer => (IWICPlanarBitmapFrameEncode*)base.Pointer;

        public HResult WritePixels(uint lineCount, WICBitmapPlane* planes, uint planeCount)
        {
            return Pointer->WritePixels(lineCount, planes, planeCount);
        }

        public HResult WriteSource(IWICBitmapSource** planes, uint planeCount, WICRect* source = null)
        {
            return Pointer->WriteSource(planes, planeCount, source);
        }

        public static implicit operator IWICPlanarBitmapFrameEncode*(WICPlanarBitmapFrameEncode value)
        {
            return value.Pointer;
        }
    }
}