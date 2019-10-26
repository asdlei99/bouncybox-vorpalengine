using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1
{
    public unsafe partial class D2D1PathGeometry
    {
        public HResult GetFigureCount(out uint count)
        {
            fixed (uint* pCount = &count)
            {
                return Pointer->GetFigureCount(pCount);
            }
        }

        public HResult GetSegmentCount(out uint count)
        {
            fixed (uint* pCount = &count)
            {
                return Pointer->GetSegmentCount(pCount);
            }
        }

        public HResult Open(out D2D1GeometrySink? geometrySink)
        {
            ID2D1GeometrySink* pGeometrySink;
            int hr = Pointer->Open(&pGeometrySink);

            geometrySink = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GeometrySink(pGeometrySink) : null;

            return hr;
        }
    }
}