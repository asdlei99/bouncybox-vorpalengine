using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1Device3
    {
        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, out D2D1DeviceContext3? deviceContext3)
        {
            ID2D1DeviceContext3* pDeviceContext3;
            int hr = Pointer->CreateDeviceContext(options, &pDeviceContext3);

            deviceContext3 = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DeviceContext3(pDeviceContext3) : null;

            return hr;
        }
    }
}