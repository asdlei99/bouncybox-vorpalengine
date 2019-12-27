using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    /// <summary>Proxies the <see cref="ID2D1SvgPaint" /> COM interface.</summary>
    public unsafe partial class D2D1SvgPaint : D2D1SvgAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgPaint" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgPaint(ID2D1SvgPaint* pointer) : base((ID2D1SvgAttribute*)pointer)
        {
        }

        public new ID2D1SvgPaint* Pointer => (ID2D1SvgPaint*)base.Pointer;

        public void GetColor(DXGI_RGBA* color)
        {
            Pointer->GetColor(color);
        }

        public HResult GetId(ushort* id, uint idCount)
        {
            return Pointer->GetId(id, idCount);
        }

        public uint GetIdLength()
        {
            return Pointer->GetIdLength();
        }

        public D2D1_SVG_PAINT_TYPE GetPaintType()
        {
            return Pointer->GetPaintType();
        }

        public HResult SetColor(DXGI_RGBA* color)
        {
            return Pointer->SetColor(color);
        }

        public HResult SetId(ushort* id)
        {
            return Pointer->SetId(id);
        }

        public HResult SetPaintType(D2D1_SVG_PAINT_TYPE paintType)
        {
            return Pointer->SetPaintType(paintType);
        }

        public static implicit operator ID2D1SvgPaint*(D2D1SvgPaint value)
        {
            return value.Pointer;
        }
    }
}