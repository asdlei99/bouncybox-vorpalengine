using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICPlanarBitmapSourceTransform" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPlanarBitmapSourceTransform : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICPlanarBitmapSourceTransform" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICPlanarBitmapSourceTransform(IWICPlanarBitmapSourceTransform* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICPlanarBitmapSourceTransform* Pointer => (IWICPlanarBitmapSourceTransform*)base.Pointer;

        public HResult CopyPixels(
            [Optional] WICRect* source,
            uint width,
            uint height,
            WICBitmapTransformOptions dstTransform,
            WICPlanarOptions dstPlanarOptions,
            WICBitmapPlane* dstPlanes,
            uint planeCount)
        {
            return Pointer->CopyPixels(source, width, height, dstTransform, dstPlanarOptions, dstPlanes, planeCount);
        }

        public HResult DoesSupportTransform(
            [Optional] uint* width,
            [Optional] uint* height,
            WICBitmapTransformOptions dstTransform,
            WICPlanarOptions dstPlanarOptions,
            Guid* dstFormats,
            WICBitmapPlaneDescription* planeDescriptions,
            uint planeCount,
            out bool isSupported)
        {
            int iIsSupported;
            int hr = Pointer->DoesSupportTransform(width, height, dstTransform, dstPlanarOptions, dstFormats, planeDescriptions, planeCount, &iIsSupported);

            isSupported = iIsSupported == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IWICPlanarBitmapSourceTransform*(WICPlanarBitmapSourceTransform value)
        {
            return value.Pointer;
        }
    }
}