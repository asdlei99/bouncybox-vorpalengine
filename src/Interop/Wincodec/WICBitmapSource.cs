using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapSource" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapSource : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapSource" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapSource(IWICBitmapSource* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICBitmapSource* Pointer => (IWICBitmapSource*)base.Pointer;

        public HResult CopyPalette(IWICPalette* palette = null)
        {
            return Pointer->CopyPalette(palette);
        }

        public HResult CopyPixels([Optional] WICRect* rect, uint stride, uint bufferSize, byte* buffer)
        {
            return Pointer->CopyPixels(rect, stride, bufferSize, buffer);
        }

        public HResult GetPixelFormat(Guid* pixelFormat)
        {
            return Pointer->GetPixelFormat(pixelFormat);
        }

        public HResult GetResolution(double* dpiX, double* dpiY)
        {
            return Pointer->GetResolution(dpiX, dpiY);
        }

        public HResult GetSize(uint* width, uint* height)
        {
            return Pointer->GetSize(width, height);
        }

        public static implicit operator IWICBitmapSource*(WICBitmapSource value)
        {
            return value.Pointer;
        }
    }
}