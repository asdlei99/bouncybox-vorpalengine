using System;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;
using Windows = TerraFX.Interop.Windows;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="IDXGIFactory2" /> COM interface.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGIFactory2 : ComObject<IDXGIFactory2>
    {
        private ComPtr<IDXGIFactory2> _dxgiFactory2;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="DXGIFactory2" /> type.
        /// </summary>
        public DXGIFactory2(ComPtr<IDXGIFactory2> dxgiFactory2)
        {
            _dxgiFactory2 = dxgiFactory2;
        }

        /// <inheritdoc />
        public override IDXGIFactory2* Pointer => _dxgiFactory2.Get();

        /// <summary>
        ///     Proxies <see cref="IDXGIFactory2.CreateSwapChainForHwnd" />.
        /// </summary>
        public DXGISwapChain1 CreateSwapChainForHwnd(D3D11Device d3d11Device, IntPtr windowHandle, DXGI_FORMAT format = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM)
        {
            var dxgiSwapChain1 = new ComPtr<IDXGISwapChain1>();
            var dxgiSwapChainDesc1 =
                new DXGI_SWAP_CHAIN_DESC1
                {
                    Width = 0,
                    Height = 0,
                    Format = format,
                    Stereo = Windows.FALSE,
                    SampleDesc =
                        new DXGI_SAMPLE_DESC
                        {
                            Count = 1,
                            Quality = 0
                        },
                    BufferUsage = DXGI.DXGI_USAGE_RENDER_TARGET_OUTPUT,
                    // Jesse Natalie on DirectX Discord: "Using 3 buffers prevents quantizing to 30hz if you drop below 60"
                    BufferCount = 2, // Triple-buffering
                    Scaling = DXGI_SCALING.DXGI_SCALING_NONE,
                    SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_DISCARD,
                    AlphaMode = DXGI_ALPHA_MODE.DXGI_ALPHA_MODE_IGNORE,
                    Flags = 0
                };

            CheckResultHandle(
                Pointer->CreateSwapChainForHwnd(
                    d3d11Device,
                    windowHandle,
                    &dxgiSwapChainDesc1,
                    null,
                    null,
                    dxgiSwapChain1.GetAddressOf()),
                $"Failed to create {nameof(IDXGISwapChain1)}.");

            return new DXGISwapChain1(dxgiSwapChain1);
        }

        /// <summary>
        ///     Proxies <see cref="IDXGIFactory2.MakeWindowAssociation" />.
        /// </summary>
        public void MakeWindowAssociation(IntPtr windowHandle, uint flags)
        {
            CheckResultHandle(Pointer->MakeWindowAssociation(windowHandle, flags), "Failed to make window association.");
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiFactory2.Dispose();

            base.Dispose(disposing);
        }
    }
}