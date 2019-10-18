using System;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="ID3D11Device" /> COM interface.</summary>
    public unsafe class D3D11Device : ComObject<ID3D11Device>
    {
        private ComPtr<ID3D11Device> _d3d11Device;

        /// <summary>Initializes a new instance of the <see cref="D3D11Device" /> type by proxying <see cref="D3D11.D3D11CreateDevice" />.</summary>
        /// <param name="driverType"></param>
        /// <param name="featureLevels"></param>
        /// <param name="debug">A value determining whether to enable Direct3D debugging.</param>
        public D3D11Device(D3D_DRIVER_TYPE driverType, Span<D3D_FEATURE_LEVEL> featureLevels, bool debug = false)
        {
            D3D11_CREATE_DEVICE_FLAG flags =
                D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT | (debug ? D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_DEBUG : 0);
            var d3d11DeviceContext = new ComPtr<ID3D11DeviceContext>();

            // ReSharper disable once UnusedVariable
            fixed (ComPtr<ID3D11Device>* a = &_d3d11Device)
            fixed (D3D_FEATURE_LEVEL* pFeatureLevels = featureLevels)
            {
                D3D_FEATURE_LEVEL featureLevel;

                CheckResultHandle(
                    D3D11
                        .D3D11CreateDevice(
                            null,
                            driverType,
                            IntPtr.Zero,
                            (uint)flags,
                            pFeatureLevels,
                            (uint)featureLevels.Length,
                            D3D11.D3D11_SDK_VERSION,
                            _d3d11Device.GetAddressOf(),
                            &featureLevel,
                            d3d11DeviceContext.GetAddressOf()),
                    $"Failed to create {nameof(ID3D11Device)}.");

                FeatureLevel = featureLevel;
            }

            ImmediateContext = new D3D11DeviceContext(d3d11DeviceContext);
        }

        /// <inheritdoc />
        public override ID3D11Device* Pointer => _d3d11Device;

        /// <summary>Gets the actual feature level chosen by Direct3D.</summary>
        public D3D_FEATURE_LEVEL FeatureLevel { get; }

        /// <summary>Gets the immediate context.</summary>
        public D3D11DeviceContext ImmediateContext { get; }

        /// <summary>Queries the <see cref="ID3D11Device" /> for <see cref="IDXGIDevice" />.</summary>
        /// <param name="allowNoInterface">
        ///     A value that determines whether to allow an <see cref="Engine.Interop.Windows.E_NOINTERFACE" />
        ///     HRESULT.
        /// </param>
        /// <returns>Returns a <see cref="DXGIDevice" /> if the query succeeded; otherwise, returns null.</returns>
        // ReSharper disable once InconsistentNaming
        public DXGIDevice? QueryDXGIDevice(bool allowNoInterface = false)
        {
            ComPtr<IDXGIDevice> dxgiDevice;

            _d3d11Device.CheckedAs(&dxgiDevice, allowNoInterface);

            return dxgiDevice.Get() != null ? new DXGIDevice(dxgiDevice) : null;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _d3d11Device.Dispose();
            ImmediateContext.Dispose();

            base.Dispose(disposing);
        }
    }
}