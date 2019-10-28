using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1GdiMetafileSink" /> COM interface.</summary>
    public unsafe partial class D2D1GdiMetafileSink : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1GdiMetafileSink" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1GdiMetafileSink(ID2D1GdiMetafileSink* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1GdiMetafileSink* Pointer => (ID2D1GdiMetafileSink*)base.Pointer;

        public HResult ProcessRecord(uint recordType, [Optional] void* recordData, uint recordDataSize)
        {
            return Pointer->ProcessRecord(recordType, recordData, recordDataSize);
        }

        public static implicit operator ID2D1GdiMetafileSink*(D2D1GdiMetafileSink value)
        {
            return value.Pointer;
        }
    }
}