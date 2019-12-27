using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICEnumMetadataItem" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICEnumMetadataItem : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICEnumMetadataItem" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICEnumMetadataItem(IWICEnumMetadataItem* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICEnumMetadataItem* Pointer => (IWICEnumMetadataItem*)base.Pointer;

        public HResult Clone(IWICEnumMetadataItem** enumMetadataItem)
        {
            return Pointer->Clone(enumMetadataItem);
        }

        public HResult Next(uint count, PROPVARIANT* schema = null, PROPVARIANT* id = null, PROPVARIANT* value = null, uint* fetched = null)
        {
            return Pointer->Next(count, schema, id, value, fetched);
        }

        public HResult Reset()
        {
            return Pointer->Reset();
        }

        public HResult Skip(uint count)
        {
            return Pointer->Skip(count);
        }

        public static implicit operator IWICEnumMetadataItem*(WICEnumMetadataItem value)
        {
            return value.Pointer;
        }
    }
}