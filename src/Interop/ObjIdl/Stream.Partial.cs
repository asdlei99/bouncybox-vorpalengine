using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.ObjIdl
{
    public unsafe partial class Stream
    {
        public HResult Clone(out Stream? stream)
        {
            IStream* pStream;
            int hr = Pointer->Clone(&pStream);

            stream = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new Stream(pStream) : null;

            return hr;
        }

        public HResult CopyTo(IStream* stream, ULARGE_INTEGER count, out ULARGE_INTEGER readCount, out ULARGE_INTEGER writtenCount)
        {
            fixed (ULARGE_INTEGER* pReadCount = &readCount)
            fixed (ULARGE_INTEGER* pWrittenCount = &writtenCount)
            {
                return Pointer->CopyTo(stream, count, pReadCount, pWrittenCount);
            }
        }

        public HResult Seek(LARGE_INTEGER move, uint origin, out ULARGE_INTEGER newPosition)
        {
            fixed (ULARGE_INTEGER* pNewPosition = &newPosition)
            {
                return Pointer->Seek(move, origin, pNewPosition);
            }
        }

        public HResult Stat(out STATSTG statstg, uint statFlag)
        {
            fixed (STATSTG* pStatstg = &statstg)
            {
                return Pointer->Stat(pStatstg, statFlag);
            }
        }
    }
}