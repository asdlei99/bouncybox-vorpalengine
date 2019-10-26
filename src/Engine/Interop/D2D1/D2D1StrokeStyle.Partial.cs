using System;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1StrokeStyle
    {
        public void GetDashes(Span<float> dashes)
        {
            fixed (float* pDashes = dashes)
            {
                Pointer->GetDashes(pDashes, (uint)dashes.Length);
            }
        }
    }
}