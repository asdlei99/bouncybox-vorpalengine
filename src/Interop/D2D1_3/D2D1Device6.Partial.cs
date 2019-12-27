using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1Device6
    {
        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, out D2D1DeviceContext6? deviceContext6)
        {
            ID2D1DeviceContext6* pDeviceContext6;
            int hr = Pointer->CreateDeviceContext(options, &pDeviceContext6);

            deviceContext6 = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DeviceContext6(pDeviceContext6) : null;

            return hr;
        }
    }
}