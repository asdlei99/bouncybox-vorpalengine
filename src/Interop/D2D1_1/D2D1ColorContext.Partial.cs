using System;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    public unsafe partial class D2D1ColorContext
    {
        public HResult GetProfile(Span<byte> profile)
        {
            fixed (byte* pProfile = profile)
            {
                return Pointer->GetProfile(pProfile, (uint)profile.Length);
            }
        }
    }
}