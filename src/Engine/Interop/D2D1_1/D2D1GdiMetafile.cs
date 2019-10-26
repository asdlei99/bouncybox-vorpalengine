using BouncyBox.VorpalEngine.Engine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1GdiMetafile" /> COM interface.</summary>
    public unsafe partial class D2D1GdiMetafile : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GdiMetafile" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GdiMetafile(ID2D1GdiMetafile* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1GdiMetafile* Pointer => (ID2D1GdiMetafile*)base.Pointer;

        public int GetBounds(D2D_RECT_F* bounds)
        {
            return Pointer->GetBounds(bounds);
        }

        public HResult Stream(ID2D1GdiMetafileSink* sink)
        {
            return Pointer->Stream(sink);
        }

        public static implicit operator ID2D1GdiMetafile*(D2D1GdiMetafile value)
        {
            return value.Pointer;
        }
    }
}