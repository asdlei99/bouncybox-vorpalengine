using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1Device4
    {
        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, out D2D1DeviceContext4? deviceContext4)
        {
            ID2D1DeviceContext4* pDeviceContext4;
            int hr = Pointer->CreateDeviceContext(options, &pDeviceContext4);

            deviceContext4 = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DeviceContext4(pDeviceContext4) : null;

            return hr;
        }
    }
}