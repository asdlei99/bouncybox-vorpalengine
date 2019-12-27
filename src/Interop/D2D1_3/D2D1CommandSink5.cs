using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1CommandSink5" /> COM interface.</summary>
    public unsafe class D2D1CommandSink5 : D2D1CommandSink4
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1CommandSink5" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1CommandSink5(ID2D1CommandSink5* pointer) : base((ID2D1CommandSink4*)pointer)
        {
        }

        public new ID2D1CommandSink5* Pointer => (ID2D1CommandSink5*)base.Pointer;

        public HResult BlendImage(
            ID2D1Image* image,
            D2D1_BLEND_MODE blendMode,
            [Optional] D2D_POINT_2F* targetOffset,
            [Optional] D2D_RECT_F* imageRectangle,
            D2D1_INTERPOLATION_MODE interpolationMode)
        {
            return Pointer->BlendImage(image, blendMode, targetOffset, imageRectangle, interpolationMode);
        }

        public static implicit operator ID2D1CommandSink5*(D2D1CommandSink5 value)
        {
            return value.Pointer;
        }
    }
}