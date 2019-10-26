using BouncyBox.VorpalEngine.Engine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    public unsafe partial class D2D1Bitmap1
    {
        public D2D1ColorContext? GetColorContext()
        {
            ID2D1ColorContext* colorContext;

            Pointer->GetColorContext(&colorContext);

            return colorContext is null ? null : new D2D1ColorContext(colorContext);
        }

        public HResult GetSurface(out DXGISurface? dxgiSurface)
        {
            IDXGISurface* pDxgiSurface;
            int hr = Pointer->GetSurface(&pDxgiSurface);

            dxgiSurface = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGISurface(pDxgiSurface) : null;

            return hr;
        }
    }
}