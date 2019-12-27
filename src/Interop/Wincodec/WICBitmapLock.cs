using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapLock" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapLock : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapLock" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapLock(IWICBitmapLock* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICBitmapLock* Pointer => (IWICBitmapLock*)base.Pointer;

        public HResult GetDataPointer(uint* bufferSize, byte** data)
        {
            return Pointer->GetDataPointer(bufferSize, data);
        }

        public HResult GetPixelFormat(Guid* pixelFormat)
        {
            return Pointer->GetPixelFormat(pixelFormat);
        }

        public HResult GetSize(uint* width, uint* height)
        {
            return Pointer->GetSize(width, height);
        }

        public HResult GetStride(uint* stride)
        {
            return Pointer->GetStride(stride);
        }

        public static implicit operator IWICBitmapLock*(WICBitmapLock value)
        {
            return value.Pointer;
        }
    }
}