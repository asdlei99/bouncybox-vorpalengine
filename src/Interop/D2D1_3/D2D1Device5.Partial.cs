using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1Device5
    {
        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, out D2D1DeviceContext5? deviceContext5)
        {
            ID2D1DeviceContext5* pDeviceContext5;
            int hr = Pointer->CreateDeviceContext(options, &pDeviceContext5);

            deviceContext5 = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DeviceContext5(pDeviceContext5) : null;

            return hr;
        }
    }
}