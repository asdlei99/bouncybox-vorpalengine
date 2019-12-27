using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICFormatConverterInfo
    {
        public HResult CreateInstance(out WICFormatConverter? converter)
        {
            IWICFormatConverter* pConverter;
            int hr = Pointer->CreateInstance(&pConverter);

            converter = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICFormatConverter(pConverter) : null;

            return hr;
        }

        public HResult GetPixelFormats(ref Span<Guid> pixelFormatGUIDs)
        {
            uint actual;
            int hr;

            fixed (Guid* pPixelFormatGUIDs = pixelFormatGUIDs)
            {
                hr = Pointer->GetPixelFormats((uint)pixelFormatGUIDs.Length, pPixelFormatGUIDs, &actual);
            }

            pixelFormatGUIDs = pixelFormatGUIDs.Slice(0, checked((int)actual));

            return hr;
        }
    }
}