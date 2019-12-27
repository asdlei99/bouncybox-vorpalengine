using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDdsDecoder
    {
        public HResult GetFrame(uint arrayIndex, uint mipLevel, uint sliceIndex, out WICBitmapFrameDecode? bitmapFrame)
        {
            IWICBitmapFrameDecode* pIBitmapFrame;
            int hr = Pointer->GetFrame(arrayIndex, mipLevel, sliceIndex, &pIBitmapFrame);

            bitmapFrame = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapFrameDecode(pIBitmapFrame) : null;

            return hr;
        }

        public HResult GetParameters(out WICDdsParameters parameters)
        {
            fixed (WICDdsParameters* pParameters = &parameters)
            {
                return Pointer->GetParameters(pParameters);
            }
        }
    }
}