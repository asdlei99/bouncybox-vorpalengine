using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapCodecProgressNotification" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICBitmapCodecProgressNotification : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapCodecProgressNotification" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapCodecProgressNotification(IWICBitmapCodecProgressNotification* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICBitmapCodecProgressNotification* Pointer => (IWICBitmapCodecProgressNotification*)base.Pointer;

        public HResult RegisterProgressNotification([Optional] IntPtr progressNotification, [Optional] void* data, uint progressFlags)
        {
            return Pointer->RegisterProgressNotification(progressNotification, data, progressFlags);
        }

        public static implicit operator IWICBitmapCodecProgressNotification*(WICBitmapCodecProgressNotification value)
        {
            return value.Pointer;
        }
    }
}