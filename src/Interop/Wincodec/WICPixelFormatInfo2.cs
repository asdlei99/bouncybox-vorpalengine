using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICPixelFormatInfo2" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPixelFormatInfo2 : WICPixelFormatInfo
    {
        /// <summary>Initializes a new instance of the <see cref="WICPixelFormatInfo2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICPixelFormatInfo2(IWICPixelFormatInfo2* pointer) : base((IWICPixelFormatInfo*)pointer)
        {
        }

        public new IWICPixelFormatInfo2* Pointer => (IWICPixelFormatInfo2*)base.Pointer;

        public HResult GetNumericRepresentation(WICPixelFormatNumericRepresentation* numericRepresentation)
        {
            return Pointer->GetNumericRepresentation(numericRepresentation);
        }

        public HResult SupportsTransparency(out bool supportsTransparency)
        {
            int iSupportsTransparency;
            int hr = Pointer->SupportsTransparency(&iSupportsTransparency);

            supportsTransparency = iSupportsTransparency == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IWICPixelFormatInfo2*(WICPixelFormatInfo2 value)
        {
            return value.Pointer;
        }
    }
}