using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICBitmapCodecInfo" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapCodecInfo : WICComponentInfo
    {
        /// <summary>Initializes a new instance of the <see cref="WICBitmapCodecInfo" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICBitmapCodecInfo(IWICBitmapCodecInfo* pointer) : base((IWICComponentInfo*)pointer)
        {
        }

        public new IWICBitmapCodecInfo* Pointer => (IWICBitmapCodecInfo*)base.Pointer;

        public HResult DoesSupportAnimation(out bool supportAnimation)
        {
            int iSupportAnimation;
            int hr = Pointer->DoesSupportAnimation(&iSupportAnimation);

            supportAnimation = iSupportAnimation == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult DoesSupportChromakey(out bool supportChromakey)
        {
            int iSupportChromakey;
            int hr = Pointer->DoesSupportChromakey(&iSupportChromakey);

            supportChromakey = iSupportChromakey == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult DoesSupportLossless(out bool supportLossless)
        {
            int iSupportLossless;
            int hr = Pointer->DoesSupportLossless(&iSupportLossless);

            supportLossless = iSupportLossless == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult DoesSupportMultiframe(out bool supportMultiframe)
        {
            int iSupportMultiframe;
            int hr = Pointer->DoesSupportMultiframe(&iSupportMultiframe);

            supportMultiframe = iSupportMultiframe == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetColorManagementVersion(uint colorManagementVersionSize, ushort* colorManagementVersion, uint* actual)
        {
            return Pointer->GetColorManagementVersion(colorManagementVersionSize, colorManagementVersion, actual);
        }

        public HResult GetContainerFormat(Guid* containerFormat)
        {
            return Pointer->GetContainerFormat(containerFormat);
        }

        public HResult GetDeviceManufacturer(uint deviceManufacturerSize, ushort* deviceManufacturer, uint* actual)
        {
            return Pointer->GetDeviceManufacturer(deviceManufacturerSize, deviceManufacturer, actual);
        }

        public HResult GetDeviceModels(uint deviceModelsSize, ushort* deviceModels, uint* actual)
        {
            return Pointer->GetDeviceModels(deviceModelsSize, deviceModels, actual);
        }

        public HResult GetFileExtensions(uint fileExtensionsSize, ushort* fileExtensions, uint* actual)
        {
            return Pointer->GetFileExtensions(fileExtensionsSize, fileExtensions, actual);
        }

        public HResult GetMimeTypes(uint mimeTypesSize, ushort* mimeTypes, uint* actual)
        {
            return Pointer->GetMimeTypes(mimeTypesSize, mimeTypes, actual);
        }

        public HResult GetPixelFormats(uint formats, Guid* pixelFormats, uint* actual)
        {
            return Pointer->GetPixelFormats(formats, pixelFormats, actual);
        }

        public HResult MatchesMimeType(ushort* mimeType, out bool matches)
        {
            int iMatches;
            int hr = Pointer->MatchesMimeType(mimeType, &iMatches);

            matches = iMatches == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public static implicit operator IWICBitmapCodecInfo*(WICBitmapCodecInfo value)
        {
            return value.Pointer;
        }
    }
}