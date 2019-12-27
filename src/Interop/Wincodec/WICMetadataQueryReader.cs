using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICMetadataQueryReader" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICMetadataQueryReader : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICMetadataQueryReader" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICMetadataQueryReader(IWICMetadataQueryReader* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICMetadataQueryReader* Pointer => (IWICMetadataQueryReader*)base.Pointer;

        public HResult GetContainerFormat(Guid* containerFormat)
        {
            return Pointer->GetContainerFormat(containerFormat);
        }

        public HResult GetEnumerator(IEnumString** enumString)
        {
            return Pointer->GetEnumerator(enumString);
        }

        public HResult GetLocation(uint maxLength, ushort* @namespace, uint* actualLength)
        {
            return Pointer->GetLocation(maxLength, @namespace, actualLength);
        }

        public HResult GetMetadataByName(ushort* name, PROPVARIANT* value)
        {
            return Pointer->GetMetadataByName(name, value);
        }

        public static implicit operator IWICMetadataQueryReader*(WICMetadataQueryReader value)
        {
            return value.Pointer;
        }
    }
}