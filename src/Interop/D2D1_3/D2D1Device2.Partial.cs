using BouncyBox.VorpalEngine.Interop.DXGI;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1Device2
    {
        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, out D2D1DeviceContext2? deviceContext2)
        {
            ID2D1DeviceContext2* pDeviceContext2;
            int hr = Pointer->CreateDeviceContext(options, &pDeviceContext2);

            deviceContext2 = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DeviceContext2(pDeviceContext2) : null;

            return hr;
        }

        public HResult GetDxgiDevice(out DXGIDevice? dxgiDevice)
        {
            IDXGIDevice* pDxgiDevice;
            int hr = Pointer->GetDxgiDevice(&pDxgiDevice);

            dxgiDevice = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIDevice(pDxgiDevice) : null;

            return hr;
        }
    }
}