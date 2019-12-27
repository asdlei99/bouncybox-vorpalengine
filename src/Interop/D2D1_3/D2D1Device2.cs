using BouncyBox.VorpalEngine.Interop.D2D1_2;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1Device2" /> COM interface.</summary>
    public unsafe partial class D2D1Device2 : D2D1Device1
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Device2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Device2(ID2D1Device2* pointer) : base((ID2D1Device1*)pointer)
        {
        }

        public new ID2D1Device2* Pointer => (ID2D1Device2*)base.Pointer;

        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, ID2D1DeviceContext2** deviceContext2)
        {
            return Pointer->CreateDeviceContext(options, deviceContext2);
        }

        public void FlushDeviceContexts(ID2D1Bitmap* bitmap)
        {
            Pointer->FlushDeviceContexts(bitmap);
        }

        public HResult GetDxgiDevice(IDXGIDevice** dxgiDevice)
        {
            return Pointer->GetDxgiDevice(dxgiDevice);
        }

        public static implicit operator ID2D1Device2*(D2D1Device2 value)
        {
            return value.Pointer;
        }
    }
}