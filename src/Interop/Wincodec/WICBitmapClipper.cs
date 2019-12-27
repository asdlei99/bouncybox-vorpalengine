using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapClipper" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICBitmapClipper : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapClipper" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapClipper(IWICBitmapClipper* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICBitmapClipper* Pointer => (IWICBitmapClipper*)base.Pointer;

        public HResult Initialize([Optional] IWICBitmapSource* source, WICRect* rect)
        {
            return Pointer->Initialize(source, rect);
        }

        public static implicit operator IWICBitmapClipper*(WICBitmapClipper value)
        {
            return value.Pointer;
        }
    }
}