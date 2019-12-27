using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICFastMetadataEncoder" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICFastMetadataEncoder : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICFastMetadataEncoder" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICFastMetadataEncoder(IWICFastMetadataEncoder* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICFastMetadataEncoder* Pointer => (IWICFastMetadataEncoder*)base.Pointer;

        public HResult Commit()
        {
            return Pointer->Commit();
        }

        public HResult GetMetadataQueryWriter(IWICMetadataQueryWriter** ppIMetadataQueryWriter)
        {
            return Pointer->GetMetadataQueryWriter(ppIMetadataQueryWriter);
        }

        public static implicit operator IWICFastMetadataEncoder*(WICFastMetadataEncoder value)
        {
            return value.Pointer;
        }
    }
}