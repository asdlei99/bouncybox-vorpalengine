using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICImagingFactory2
    {
        public HResult CreateImageEncoder(IUnknown* d2dDevice, out WICImageEncoder? imageEncoder)
        {
            IWICImageEncoder* pImageEncoder;
            int hr = Pointer->CreateImageEncoder(d2dDevice, &pImageEncoder);

            imageEncoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICImageEncoder(pImageEncoder) : null;

            return hr;
        }
    }
}