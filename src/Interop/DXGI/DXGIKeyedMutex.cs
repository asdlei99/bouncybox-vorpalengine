using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGI
{
    /// <summary>Proxies the <see cref="IDXGIKeyedMutex" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class DXGIKeyedMutex : DXGIDeviceSubObject
    {
        /// <summary>Initializes a new instance of the <see cref="DXGIKeyedMutex" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DXGIKeyedMutex(IDXGIKeyedMutex* pointer) : base((IDXGIDeviceSubObject*)pointer)
        {
        }

        public new IDXGIKeyedMutex* Pointer => (IDXGIKeyedMutex*)base.Pointer;

        public HResult AcquireSync(ulong Key, uint dwMilliseconds)
        {
            return Pointer->AcquireSync(Key, dwMilliseconds);
        }

        public HResult ReleaseSync(ulong Key)
        {
            return Pointer->ReleaseSync(Key);
        }

        public static implicit operator IDXGIKeyedMutex*(DXGIKeyedMutex value)
        {
            return value.Pointer;
        }
    }
}