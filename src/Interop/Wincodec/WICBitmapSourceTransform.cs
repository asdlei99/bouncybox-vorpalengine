using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapSourceTransform" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapSourceTransform : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapSourceTransform" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapSourceTransform(IWICBitmapSourceTransform* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICBitmapSourceTransform* Pointer => (IWICBitmapSourceTransform*)base.Pointer;

        public HResult CopyPixels(
            [Optional] WICRect* rect,
            uint width,
            uint height,
            [Optional] Guid* format,
            WICBitmapTransformOptions transform,
            uint stride,
            uint bufferSize,
            byte* buffer)
        {
            return Pointer->CopyPixels(rect, width, height, format, transform, stride, bufferSize, buffer);
        }

        public HResult DoesSupportTransform(WICBitmapTransformOptions transform, out bool isSupported)
        {
            int iIsSupported;
            int hr = Pointer->DoesSupportTransform(transform, &iIsSupported);

            isSupported = iIsSupported == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetClosestPixelFormat(Guid* format)
        {
            return Pointer->GetClosestPixelFormat(format);
        }

        public HResult GetClosestSize(uint* width, uint* height)
        {
            return Pointer->GetClosestSize(width, height);
        }

        public static implicit operator IWICBitmapSourceTransform*(WICBitmapSourceTransform value)
        {
            return value.Pointer;
        }
    }
}