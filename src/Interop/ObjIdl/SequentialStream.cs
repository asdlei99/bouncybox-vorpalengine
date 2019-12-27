using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    /// <summary>Proxies the <see cref="ISequentialStream" /> COM interface.</summary>
    public unsafe partial class SequentialStream : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="SequentialStream" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public SequentialStream(ISequentialStream* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ISequentialStream* Pointer => (ISequentialStream*)base.Pointer;

        public HResult Read(void* buffer, uint count, uint* readCount = null)
        {
            return Pointer->Read(buffer, count, readCount);
        }

        public HResult Write(void* buffer, uint count, uint* writtenCount = null)
        {
            return Pointer->Write(buffer, count, writtenCount);
        }

        public static implicit operator ISequentialStream*(SequentialStream value)
        {
            return value.Pointer;
        }
    }
}