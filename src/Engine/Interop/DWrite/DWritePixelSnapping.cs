using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWritePixelSnapping" /> COM interface.</summary>
    public unsafe partial class DWritePixelSnapping : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWritePixelSnapping" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWritePixelSnapping(IDWritePixelSnapping* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWritePixelSnapping* Pointer => (IDWritePixelSnapping*)base.Pointer;

        public HResult GetCurrentTransform([Optional] void* clientDrawingContext, DWRITE_MATRIX* transform)
        {
            return Pointer->GetCurrentTransform(clientDrawingContext, transform);
        }

        public HResult GetPixelsPerDip([Optional] void* clientDrawingContext, float* pixelsPerDip)
        {
            return Pointer->GetPixelsPerDip(clientDrawingContext, pixelsPerDip);
        }

        public HResult IsPixelSnappingDisabled([Optional] void* clientDrawingContext, out bool isDisabled)
        {
            int iIsDisabled;
            int hr = Pointer->IsPixelSnappingDisabled(clientDrawingContext, &iIsDisabled);

            isDisabled = iIsDisabled == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IDWritePixelSnapping*(DWritePixelSnapping value)
        {
            return value.Pointer;
        }
    }
}