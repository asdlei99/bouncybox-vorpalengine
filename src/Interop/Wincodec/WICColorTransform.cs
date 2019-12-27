using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICColorTransform" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICColorTransform : WICBitmapSource
    {
        /// <summary>Initializes a new instance of the <see cref="WICColorTransform" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICColorTransform(IWICColorTransform* pointer) : base((IWICBitmapSource*)pointer)
        {
        }

        public new IWICColorTransform* Pointer => (IWICColorTransform*)base.Pointer;

        public HResult Initialize(
            [Optional] IWICBitmapSource* bitmapSource,
            [Optional] IWICColorContext* contextSource,
            [Optional] IWICColorContext* contextDest,
            Guid* pixelFmtDest)
        {
            return Pointer->Initialize(bitmapSource, contextSource, contextDest, pixelFmtDest);
        }

        public static implicit operator IWICColorTransform*(WICColorTransform value)
        {
            return value.Pointer;
        }
    }
}