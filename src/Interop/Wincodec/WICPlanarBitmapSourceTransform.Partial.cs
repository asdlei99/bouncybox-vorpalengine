using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPlanarBitmapSourceTransform
    {
        public HResult CopyPixels(
            [Optional] WICRect* source,
            uint width,
            uint height,
            WICBitmapTransformOptions dstTransform,
            WICPlanarOptions dstPlanarOptions,
            ReadOnlySpan<WICBitmapPlane> dstPlanes)
        {
            fixed (WICBitmapPlane* pDstPlanes = dstPlanes)
            {
                return Pointer->CopyPixels(source, width, height, dstTransform, dstPlanarOptions, pDstPlanes, (uint)dstPlanes.Length);
            }
        }

        public HResult DoesSupportTransform(
            [Optional] uint* width,
            [Optional] uint* height,
            WICBitmapTransformOptions dstTransform,
            WICPlanarOptions dstPlanarOptions,
            ReadOnlySpan<Guid> dstFormats,
            ReadOnlySpan<WICBitmapPlaneDescription> planeDescriptions,
            out bool isSupported)
        {
            if (dstFormats.Length != planeDescriptions.Length)
            {
                throw new ArgumentException($"{nameof(dstFormats)} and {nameof(planeDescriptions)} must have the same length.");
            }

            int iIsSupported;
            int hr;

            fixed (Guid* pDstFormats = dstFormats)
            fixed (WICBitmapPlaneDescription* pPlaneDescriptions = planeDescriptions)
            {
                hr = Pointer->DoesSupportTransform(
                    width,
                    height,
                    dstTransform,
                    dstPlanarOptions,
                    pDstFormats,
                    pPlaneDescriptions,
                    (uint)dstFormats.Length,
                    &iIsSupported);
            }

            isSupported = iIsSupported == TerraFX.Interop.Windows.TRUE;

            return hr;
        }
    }
}