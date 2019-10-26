using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class D2D1GdiInteropRenderTarget
    {
        public HResult GetDC(D2D1_DC_INITIALIZE_MODE mode, out IntPtr hdc)
        {
            fixed (IntPtr* pHdc = &hdc)
            {
                return Pointer->GetDC(mode, pHdc);
            }
        }
    }
}