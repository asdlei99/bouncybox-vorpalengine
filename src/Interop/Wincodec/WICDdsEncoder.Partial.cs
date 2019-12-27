using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDdsEncoder
    {
        public HResult CreateNewFrame(out WICBitmapFrameEncode? frameEncode, out uint arrayIndex, out uint mipLevel, out uint sliceIndex)
        {
            IWICBitmapFrameEncode* pIFrameEncode;
            int hr;

            fixed (uint* pArrayIndex = &arrayIndex)
            fixed (uint* pMipLevel = &mipLevel)
            fixed (uint* pSliceIndex = &sliceIndex)
            {
                hr = Pointer->CreateNewFrame(&pIFrameEncode, pArrayIndex, pMipLevel, pSliceIndex);
            }

            frameEncode = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapFrameEncode(pIFrameEncode) : null;

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