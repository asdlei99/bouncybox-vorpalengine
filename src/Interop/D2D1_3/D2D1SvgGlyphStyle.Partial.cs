using System;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1SvgGlyphStyle
    {
        public D2D1Brush? GetFill()
        {
            ID2D1Brush* pBrush;

            Pointer->GetFill(&pBrush);

            return pBrush != null ? new D2D1Brush(pBrush) : null;
        }

        public void GetStroke(out D2D1Brush? brush, float* strokeWidth = null, Span<float> dashes = default, float* dashOffset = null)
        {
            ID2D1Brush* pBrush;

            fixed (float* pDashes = dashes)
            {
                Pointer->GetStroke(&pBrush, strokeWidth, pDashes, (uint)dashes.Length, dashOffset);
            }

            brush = pBrush != null ? new D2D1Brush(pBrush) : null;
        }

        public HResult SetStroke(ID2D1Brush* brush = null, float strokeWidth = 1.0f, ReadOnlySpan<float> dashes = default, float dashOffset = 1.0f)
        {
            fixed (float* pDashes = dashes)
            {
                return Pointer->SetStroke(brush, strokeWidth, pDashes, (uint)dashes.Length, dashOffset);
            }
        }
    }
}