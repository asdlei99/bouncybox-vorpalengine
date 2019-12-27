using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICComponentInfo" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICComponentInfo : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICComponentInfo" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICComponentInfo(IWICComponentInfo* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICComponentInfo* Pointer => (IWICComponentInfo*)base.Pointer;

        public HResult GetAuthor(uint authorSize, ushort* author, uint* actual)
        {
            return Pointer->GetAuthor(authorSize, author, actual);
        }

        public HResult GetCLSID(Guid* clsid)
        {
            return Pointer->GetCLSID(clsid);
        }

        public HResult GetComponentType(WICComponentType* type)
        {
            return Pointer->GetComponentType(type);
        }

        public HResult GetFriendlyName(uint friendlyNameSize, ushort* friendlyName, uint* actual)
        {
            return Pointer->GetFriendlyName(friendlyNameSize, friendlyName, actual);
        }

        public HResult GetSigningStatus(uint* status)
        {
            return Pointer->GetSigningStatus(status);
        }

        public HResult GetSpecVersion(uint specVersionSize, ushort* specVersion, uint* actual)
        {
            return Pointer->GetSpecVersion(specVersionSize, specVersion, actual);
        }

        public HResult GetVendorGUID(Guid* vendor)
        {
            return Pointer->GetVendorGUID(vendor);
        }

        public HResult GetVersion(uint versionSize, ushort* version, uint* actual)
        {
            return Pointer->GetVersion(versionSize, version, actual);
        }

        public static implicit operator IWICComponentInfo*(WICComponentInfo value)
        {
            return value.Pointer;
        }
    }
}