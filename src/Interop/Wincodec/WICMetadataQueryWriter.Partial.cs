using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICMetadataQueryWriter
    {
        public HResult RemoveMetadataByName(ReadOnlySpan<char> name)
        {
            fixed (char* pName = name)
            {
                return Pointer->RemoveMetadataByName((ushort*)pName);
            }
        }

        public HResult SetMetadataByName(ReadOnlySpan<char> name, PROPVARIANT* value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetMetadataByName((ushort*)pName, value);
            }
        }
    }
}