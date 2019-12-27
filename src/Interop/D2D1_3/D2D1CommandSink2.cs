using BouncyBox.VorpalEngine.Interop.D2D1_2;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1CommandSink2" /> COM interface.</summary>
    public unsafe class D2D1CommandSink2 : D2D1CommandSink1
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1CommandSink2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1CommandSink2(ID2D1CommandSink2* pointer) : base((ID2D1CommandSink1*)pointer)
        {
        }

        public new ID2D1CommandSink2* Pointer => (ID2D1CommandSink2*)base.Pointer;

        public HResult DrawGdiMetafile(ID2D1GdiMetafile* gdiMetafile, D2D_RECT_F* destinationRectangle = null, D2D_RECT_F* sourceRectangle = null)
        {
            return Pointer->DrawGdiMetafile(gdiMetafile, destinationRectangle, sourceRectangle);
        }

        public HResult DrawGradientMesh(ID2D1GradientMesh* gradientMesh)
        {
            return Pointer->DrawGradientMesh(gradientMesh);
        }

        public HResult DrawInk(ID2D1Ink* ink, ID2D1Brush* brush, ID2D1InkStyle* inkStyle = null)
        {
            return Pointer->DrawInk(ink, brush, inkStyle);
        }

        public static implicit operator ID2D1CommandSink2*(D2D1CommandSink2 value)
        {
            return value.Pointer;
        }
    }
}