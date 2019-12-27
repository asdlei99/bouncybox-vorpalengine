using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    public unsafe partial class D2D1SvgStrokeDashArray
    {
        public HResult GetDashes(Span<float> dashes, uint startIndex)
        {
            fixed (float* pDashes = dashes)
            {
                return Pointer->GetDashes(pDashes, (uint)dashes.Length, startIndex);
            }
        }

        public HResult GetDashes(Span<D2D1_SVG_LENGTH> dashes, uint startIndex = 0)
        {
            fixed (D2D1_SVG_LENGTH* pDashes = dashes)
            {
                return Pointer->GetDashes(pDashes, (uint)dashes.Length, startIndex);
            }
        }

        public HResult UpdateDashes(ReadOnlySpan<float> dashes, uint startIndex = 0)
        {
            fixed (float* pDashes = dashes)
            {
                return Pointer->UpdateDashes(pDashes, (uint)dashes.Length, startIndex);
            }
        }

        public HResult UpdateDashes(ReadOnlySpan<D2D1_SVG_LENGTH> dashes, uint startIndex = 0)
        {
            fixed (D2D1_SVG_LENGTH* pDashes = dashes)
            {
                return Pointer->UpdateDashes(pDashes, (uint)dashes.Length, startIndex);
            }
        }
    }
}