using System;

#pragma warning disable 1591
namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteLocalizedStrings
    {
        public HResult FindLocaleName(ReadOnlySpan<char> localeName, out uint index, out bool exists)
        {
            int iExists;
            int hr;

            fixed (char* pLocaleName = localeName)
            fixed (uint* pIndex = &index)
            {
                hr = Pointer->FindLocaleName((ushort*)pLocaleName, pIndex, &iExists);
            }

            exists = iExists == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetLocaleName(uint index, Span<char> localeName)
        {
            int hr;

            fixed (char* pLocaleName = localeName)
            {
                hr = Pointer->GetLocaleName(index, (ushort*)pLocaleName, (uint)localeName.Length);
            }

            return hr;
        }

        public HResult GetLocaleNameLength(uint index, out uint length)
        {
            fixed (uint* pLength = &length)
            {
                return Pointer->GetLocaleNameLength(index, pLength);
            }
        }

        public HResult GetString(uint index, Span<char> stringBuffer)
        {
            fixed (char* pStringBuffer = stringBuffer)
            {
                return Pointer->GetString(index, (ushort*)pStringBuffer, (uint)stringBuffer.Length);
            }
        }

        public HResult GetStringLength(uint index, out uint length)
        {
            fixed (uint* pLength = &length)
            {
                return Pointer->GetStringLength(index, pLength);
            }
        }
    }
}