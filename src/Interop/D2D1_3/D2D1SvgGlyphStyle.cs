using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1SvgGlyphStyle" /> COM interface.</summary>
    public unsafe partial class D2D1SvgGlyphStyle : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgGlyphStyle" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgGlyphStyle(ID2D1SvgGlyphStyle* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1SvgGlyphStyle* Pointer => (ID2D1SvgGlyphStyle*)base.Pointer;

        public void GetFill(ID2D1Brush** brush)
        {
            Pointer->GetFill(brush);
        }

        public void GetStroke(ID2D1Brush** brush = null, float* strokeWidth = null, float* dashes = null, uint dashesCount = 0, float* dashOffset = null)
        {
            Pointer->GetStroke(brush, strokeWidth, dashes, dashesCount, dashOffset);
        }

        public uint GetStrokeDashesCount()
        {
            return Pointer->GetStrokeDashesCount();
        }

        public HResult SetFill(ID2D1Brush* brush = null)
        {
            return Pointer->SetFill(brush);
        }

        public HResult SetStroke(ID2D1Brush* brush = null, float strokeWidth = 1.0f, float* dashes = null, uint dashesCount = 0, float dashOffset = 1.0f)
        {
            return Pointer->SetStroke(brush, strokeWidth, dashes, dashesCount, dashOffset);
        }

        public static implicit operator ID2D1SvgGlyphStyle*(D2D1SvgGlyphStyle value)
        {
            return value.Pointer;
        }
    }
}