using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1Mesh
    {
        public HResult Open(out D2D1TessellationSink? tessellationSink)
        {
            ID2D1TessellationSink* pTessellationSink;
            int hr = Pointer->Open(&pTessellationSink);

            tessellationSink = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1TessellationSink(pTessellationSink) : null;

            return hr;
        }
    }
}