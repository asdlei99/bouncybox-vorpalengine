using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapFlipRotator" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICBitmapFlipRotator : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapFlipRotator" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapFlipRotator(IWICBitmapFlipRotator* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICBitmapFlipRotator* Pointer => (IWICBitmapFlipRotator*)base.Pointer;

        public HResult Initialize([Optional] IWICBitmapSource* source, WICBitmapTransformOptions options)
        {
            return Pointer->Initialize(source, options);
        }

        public static implicit operator IWICBitmapFlipRotator*(WICBitmapFlipRotator value)
        {
            return value.Pointer;
        }
    }
}