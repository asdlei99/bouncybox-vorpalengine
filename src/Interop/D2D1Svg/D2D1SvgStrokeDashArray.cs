using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    /// <summary>Proxies the <see cref="ID2D1SvgStrokeDashArray" /> COM interface.</summary>
    public unsafe partial class D2D1SvgStrokeDashArray : D2D1SvgAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgStrokeDashArray" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgStrokeDashArray(ID2D1SvgStrokeDashArray* pointer) : base((ID2D1SvgAttribute*)pointer)
        {
        }

        public new ID2D1SvgStrokeDashArray* Pointer => (ID2D1SvgStrokeDashArray*)base.Pointer;

        public HResult GetDashes(float* dashes, uint dashesCount, uint startIndex)
        {
            return Pointer->GetDashes(dashes, dashesCount, startIndex);
        }

        public HResult GetDashes(D2D1_SVG_LENGTH* dashes, uint dashesCount, uint startIndex = 0)
        {
            return Pointer->GetDashes(dashes, dashesCount, startIndex);
        }

        public uint GetDashesCount()
        {
            return Pointer->GetDashesCount();
        }

        public HResult RemoveDashesAtEnd(uint dashesCount)
        {
            return Pointer->RemoveDashesAtEnd(dashesCount);
        }

        public HResult UpdateDashes(float* dashes, uint dashesCount, uint startIndex = 0)
        {
            return Pointer->UpdateDashes(dashes, dashesCount, startIndex);
        }

        public HResult UpdateDashes(D2D1_SVG_LENGTH* dashes, uint dashesCount, uint startIndex = 0)
        {
            return Pointer->UpdateDashes(dashes, dashesCount, startIndex);
        }

        public static implicit operator ID2D1SvgStrokeDashArray*(D2D1SvgStrokeDashArray value)
        {
            return value.Pointer;
        }
    }
}