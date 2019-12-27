using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapCodecInfo
    {
        public HResult GetColorManagementVersion(Span<char> colorManagementVersion, out uint actual)
        {
            fixed (char* pColorManagementVersion = colorManagementVersion)
            fixed (uint* pActual = &actual)
            {
                return Pointer->GetColorManagementVersion((uint)colorManagementVersion.Length, (ushort*)pColorManagementVersion, pActual);
            }
        }

        public HResult GetContainerFormat(out Guid containerFormat)
        {
            fixed (Guid* pContainerFormat = &containerFormat)
            {
                return Pointer->GetContainerFormat(pContainerFormat);
            }
        }

        public HResult GetDeviceManufacturer(Span<char> deviceManufacturer, out uint actual)
        {
            fixed (char* pDeviceManufacturer = deviceManufacturer)
            fixed (uint* pActual = &actual)
            {
                return Pointer->GetDeviceManufacturer((uint)deviceManufacturer.Length, (ushort*)pDeviceManufacturer, pActual);
            }
        }

        public HResult GetDeviceModels(Span<char> deviceModels, out uint actual)
        {
            fixed (char* pDeviceModels = deviceModels)
            fixed (uint* pActual = &actual)
            {
                return Pointer->GetDeviceModels((uint)deviceModels.Length, (ushort*)pDeviceModels, pActual);
            }
        }

        public HResult GetFileExtensions(Span<char> fileExtensions, out uint actual)
        {
            fixed (char* pFileExtensions = fileExtensions)
            fixed (uint* pActual = &actual)
            {
                return Pointer->GetFileExtensions((uint)fileExtensions.Length, (ushort*)pFileExtensions, pActual);
            }
        }

        public HResult GetMimeTypes(Span<char> mimeTypes, out uint actual)
        {
            fixed (char* pMimeTypes = mimeTypes)
            fixed (uint* pActual = &actual)
            {
                return Pointer->GetMimeTypes((uint)mimeTypes.Length, (ushort*)pMimeTypes, pActual);
            }
        }

        public HResult GetPixelFormats(Span<Guid> pixelFormats, out uint actual)
        {
            fixed (Guid* pPixelFormats = pixelFormats)
            fixed (uint* pActual = &actual)
            {
                return Pointer->GetPixelFormats((uint)pixelFormats.Length, pPixelFormats, pActual);
            }
        }

        public HResult MatchesMimeType(ReadOnlySpan<char> mimeType, out bool matches)
        {
            int iMatches;
            int hr;

            fixed (char* pMimeType = mimeType)
            {
                hr = Pointer->MatchesMimeType((ushort*)pMimeType, &iMatches);
            }

            matches = iMatches == TerraFX.Interop.Windows.TRUE;

            return hr;
        }
    }
}