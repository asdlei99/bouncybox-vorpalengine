using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICPlanarBitmapFrameEncode
    {
        public HResult WritePixels(uint lineCount, ReadOnlySpan<WICBitmapPlane> planes)
        {
            fixed (WICBitmapPlane* pPlanes = planes)
            {
                return Pointer->WritePixels(lineCount, pPlanes, (uint)planes.Length);
            }
        }

        public HResult WriteSource(ReadOnlySpan<Pointer<IWICBitmapSource>> planes, WICRect* source)
        {
            fixed (Pointer<IWICBitmapSource>* pPlanes = planes)
            {
                return Pointer->WriteSource((IWICBitmapSource**)pPlanes, (uint)planes.Length, source);
            }
        }
    }
}