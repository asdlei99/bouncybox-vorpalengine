using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICDevelopRawNotificationCallback" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class WICDevelopRawNotificationCallback : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="WICDevelopRawNotificationCallback" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICDevelopRawNotificationCallback(IWICDevelopRawNotificationCallback* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IWICDevelopRawNotificationCallback* Pointer => (IWICDevelopRawNotificationCallback*)base.Pointer;

        public HResult Notify(uint notificationMask)
        {
            return Pointer->Notify(notificationMask);
        }

        public static implicit operator IWICDevelopRawNotificationCallback*(WICDevelopRawNotificationCallback value)
        {
            return value.Pointer;
        }
    }
}