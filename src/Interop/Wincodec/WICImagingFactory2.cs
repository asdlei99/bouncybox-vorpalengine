using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICImagingFactory2" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICImagingFactory2 : WICImagingFactory
    {
        /// <summary>Initializes a new instance of the <see cref="WICImagingFactory2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICImagingFactory2(IWICImagingFactory2* pointer) : base((IWICImagingFactory*)pointer)
        {
        }

        public new IWICImagingFactory2* Pointer => (IWICImagingFactory2*)base.Pointer;

        public HResult CreateImageEncoder(IUnknown* d2dDevice, IWICImageEncoder** imageEncoder)
        {
            return Pointer->CreateImageEncoder(d2dDevice, imageEncoder);
        }

        public static implicit operator IWICImagingFactory2*(WICImagingFactory2 value)
        {
            return value.Pointer;
        }
    }
}