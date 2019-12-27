using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapScaler" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICBitmapScaler : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapScaler" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapScaler(IWICBitmapScaler* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICBitmapScaler* Pointer => (IWICBitmapScaler*)base.Pointer;

        public HResult Initialize(IWICBitmapSource* source, uint width, uint height, WICBitmapInterpolationMode mode)
        {
            return Pointer->Initialize(source, width, height, mode);
        }

        public static implicit operator IWICBitmapScaler*(WICBitmapScaler value)
        {
            return value.Pointer;
        }
    }
}