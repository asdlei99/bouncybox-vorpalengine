using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDevice1
    {
        public HResult GetMaximumFrameLatency(out uint maxLatency)
        {
            fixed (uint* pMaxLatency = &maxLatency)
            {
                return Pointer->GetMaximumFrameLatency(pMaxLatency);
            }
        }
    }
}