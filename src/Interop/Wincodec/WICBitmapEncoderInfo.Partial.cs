using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmapEncoderInfo
    {
        public HResult CreateInstance(out WICBitmapEncoder? bitmapEncoder)
        {
            IWICBitmapEncoder* pBitmapEncoder;
            int hr = Pointer->CreateInstance(&pBitmapEncoder);

            bitmapEncoder = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapEncoder(pBitmapEncoder) : null;

            return hr;
        }
    }
}