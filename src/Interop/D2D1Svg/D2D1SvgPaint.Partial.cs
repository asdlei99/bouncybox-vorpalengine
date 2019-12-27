using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    public unsafe partial class D2D1SvgPaint
    {
        public void GetColor(out DXGI_RGBA color)
        {
            fixed (DXGI_RGBA* pColor = &color)
            {
                Pointer->GetColor(pColor);
            }
        }

        public HResult GetId(Span<char> id)
        {
            fixed (char* pId = id)
            {
                return Pointer->GetId((ushort*)pId, (uint)id.Length);
            }
        }

        public HResult SetId(ReadOnlySpan<char> id)
        {
            fixed (char* pId = id)
            {
                return Pointer->SetId((ushort*)pId);
            }
        }
    }
}