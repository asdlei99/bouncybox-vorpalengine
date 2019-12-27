using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    /// <summary>Proxies the <see cref="IStream" /> COM interface.</summary>
    public unsafe partial class Stream : SequentialStream
    {
        /// <summary>Initializes a new instance of the <see cref="Stream" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public Stream(IStream* pointer) : base((ISequentialStream*)pointer)
        {
        }

        public new IStream* Pointer => (IStream*)base.Pointer;

        public HResult Clone(IStream** stream = null)
        {
            return Pointer->Clone(stream);
        }

        public HResult Commit(uint commitFlags)
        {
            return Pointer->Commit(commitFlags);
        }

        public HResult CopyTo(IStream* stream, ULARGE_INTEGER count, ULARGE_INTEGER* readCount = null, ULARGE_INTEGER* writtenCount = null)
        {
            return Pointer->CopyTo(stream, count, readCount, writtenCount);
        }

        public HResult LockRegion(ULARGE_INTEGER offset, ULARGE_INTEGER count, uint lockType)
        {
            return Pointer->LockRegion(offset, count, lockType);
        }

        public HResult Revert()
        {
            return Pointer->Revert();
        }

        public HResult Seek(LARGE_INTEGER move, uint origin, ULARGE_INTEGER* newPosition = null)
        {
            return Pointer->Seek(move, origin, newPosition);
        }

        public HResult SetSize(ULARGE_INTEGER newSize)
        {
            return Pointer->SetSize(newSize);
        }

        public HResult Stat(STATSTG* statstg, uint statFlag)
        {
            return Pointer->Stat(statstg, statFlag);
        }

        public HResult UnlockRegion(ULARGE_INTEGER offset, ULARGE_INTEGER count, uint lockType)
        {
            return Pointer->UnlockRegion(offset, count, lockType);
        }

        public static implicit operator IStream*(Stream value)
        {
            return value.Pointer;
        }
    }
}