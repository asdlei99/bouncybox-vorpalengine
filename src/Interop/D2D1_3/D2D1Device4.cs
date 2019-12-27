using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1Device4" /> COM interface.</summary>
    public unsafe partial class D2D1Device4 : D2D1Device3
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Device4" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Device4(ID2D1Device4* pointer) : base((ID2D1Device3*)pointer)
        {
        }

        public new ID2D1Device4* Pointer => (ID2D1Device4*)base.Pointer;

        public HResult CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options, ID2D1DeviceContext4** deviceContext4)
        {
            return Pointer->CreateDeviceContext(options, deviceContext4);
        }

        public ulong GetMaximumColorGlyphCacheMemory()
        {
            return Pointer->GetMaximumColorGlyphCacheMemory();
        }

        public void SetMaximumColorGlyphCacheMemory(ulong maximumInBytes)
        {
            Pointer->SetMaximumColorGlyphCacheMemory(maximumInBytes);
        }

        public static implicit operator ID2D1Device4*(D2D1Device4 value)
        {
            return value.Pointer;
        }
    }
}