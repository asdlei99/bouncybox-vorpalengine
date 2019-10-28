using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D3D11
{
    public unsafe partial class D3D11Device
    {
        public static HResult Create(
            [Optional] IDXGIAdapter* pAdapter,
            D3D_DRIVER_TYPE driverType,
            ReadOnlySpan<D3D_FEATURE_LEVEL> featureLevels,
            [Optional] D3D_FEATURE_LEVEL* featureLevel,
            [Optional] [DefaultParameterValue(false)]
            bool debug,
            out D3D11Device? device,
            out D3D11DeviceContext? immediateContext)
        {
            var flags = D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT;

            if (debug)
            {
                flags |= D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_DEBUG;
            }

            ID3D11Device* ppDevice;
            ID3D11DeviceContext* pImmediateContext;
            int hr;

            fixed (D3D_FEATURE_LEVEL* pFeatureLevels = featureLevels)
            {
                hr = TerraFX.Interop.D3D11.D3D11CreateDevice(
                    pAdapter,
                    driverType,
                    IntPtr.Zero,
                    (uint)flags,
                    pFeatureLevels,
                    (uint)featureLevels.Length,
                    TerraFX.Interop.D3D11.D3D11_SDK_VERSION,
                    &ppDevice,
                    featureLevel,
                    &pImmediateContext);
            }

            bool succeeded = TerraFX.Interop.Windows.SUCCEEDED(hr);

            device = succeeded ? new D3D11Device(ppDevice) : null;
            immediateContext = succeeded ? new D3D11DeviceContext(pImmediateContext) : null;

            return hr;
        }
    }
}