using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    public unsafe partial class D2D1GdiMetafileSink
    {
        public HResult ProcessRecord(uint recordType, [Optional] ReadOnlySpan<byte> recordData)
        {
            fixed (byte* pRecordData = recordData)
            {
                return Pointer->ProcessRecord(recordType, pRecordData, (uint)recordData.Length);
            }
        }
    }
}