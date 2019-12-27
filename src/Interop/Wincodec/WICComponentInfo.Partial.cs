using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICComponentInfo
    {
        public HResult GetAuthor(Span<char> author, out uint actual)
        {
            fixed (char* pAuthor = author)
            fixed (uint* pCchActual = &actual)
            {
                return Pointer->GetAuthor((uint)author.Length, (ushort*)pAuthor, pCchActual);
            }
        }

        public HResult GetCLSID(out Guid clsid)
        {
            fixed (Guid* pClsid = &clsid)
            {
                return Pointer->GetCLSID(pClsid);
            }
        }

        public HResult GetComponentType(out WICComponentType type)
        {
            fixed (WICComponentType* pType = &type)
            {
                return Pointer->GetComponentType(pType);
            }
        }

        public HResult GetFriendlyName(Span<char> friendlyName, out uint actual)
        {
            fixed (char* pFriendlyName = friendlyName)
            fixed (uint* pCchActual = &actual)
            {
                return Pointer->GetFriendlyName((uint)friendlyName.Length, (ushort*)pFriendlyName, pCchActual);
            }
        }

        public HResult GetSigningStatus(out uint status)
        {
            fixed (uint* pStatus = &status)
            {
                return Pointer->GetSigningStatus(pStatus);
            }
        }

        public HResult GetSpecVersion(Span<char> specVersion, out uint actual)
        {
            fixed (char* pSpecVersion = specVersion)
            fixed (uint* pCchActual = &actual)
            {
                return Pointer->GetSpecVersion((uint)specVersion.Length, (ushort*)pSpecVersion, pCchActual);
            }
        }

        public HResult GetVendorGUID(out Guid vendor)
        {
            fixed (Guid* pGuidVendor = &vendor)
            {
                return Pointer->GetVendorGUID(pGuidVendor);
            }
        }

        public HResult GetVersion(Span<char> version, out uint actual)
        {
            fixed (char* pVersion = version)
            fixed (uint* pCchActual = &actual)
            {
                return Pointer->GetVersion((uint)version.Length, (ushort*)pVersion, pCchActual);
            }
        }
    }
}