using BouncyBox.VorpalEngine.Interop.D2D1_1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_2
{
    /// <summary>Proxies the <see cref="ID2D1Factory2" /> COM interface.</summary>
    public unsafe class D2D1Factory2 : D2D1Factory1
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Factory2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Factory2(ID2D1Factory2* pointer) : base((ID2D1Factory1*)pointer)
        {
        }

        public new ID2D1Factory2* Pointer => (ID2D1Factory2*)base.Pointer;

        public HResult CreateDevice(IDXGIDevice* dxgiDevice, out D2D1Device1? d2dDevice1)
        {
            ID2D1Device1* pD2DDevice1;
            int hr = Pointer->CreateDevice(dxgiDevice, &pD2DDevice1);

            d2dDevice1 = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Device1(pD2DDevice1) : null;

            return hr;
        }

        public static implicit operator ID2D1Factory2*(D2D1Factory2 value)
        {
            return value.Pointer;
        }
    }
}