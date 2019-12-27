using System;
using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.ObjIdl;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICMetadataQueryReader
    {
        public HResult GetContainerFormat(out Guid containerFormat)
        {
            fixed (Guid* pContainerFormat = &containerFormat)
            {
                return Pointer->GetContainerFormat(pContainerFormat);
            }
        }

        public HResult GetEnumerator(out EnumString? enumString)
        {
            IEnumString* pEnumString;
            int hr = Pointer->GetEnumerator(&pEnumString);

            enumString = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new EnumString(pEnumString) : null;

            return hr;
        }

        public HResult GetLocation(Span<char> @namespace, out uint actualLength)
        {
            fixed (char* pNamespace = @namespace)
            fixed (uint* pActualLength = &actualLength)
            {
                return Pointer->GetLocation((uint)@namespace.Length, (ushort*)pNamespace, pActualLength);
            }
        }

        public HResult GetMetadataByName(ReadOnlySpan<char> name, PROPVARIANT* value)
        {
            fixed (char* pName = name)
            {
                return Pointer->GetMetadataByName((ushort*)pName, value);
            }
        }
    }
}