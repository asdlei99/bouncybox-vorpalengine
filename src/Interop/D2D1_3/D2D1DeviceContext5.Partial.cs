using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1Svg;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1DeviceContext5
    {
        public HResult CreateColorContextFromDxgiColorSpace(DXGI_COLOR_SPACE_TYPE colorSpace, out D2D1ColorContext1? colorContext)
        {
            ID2D1ColorContext1* pColorContext;
            int hr = Pointer->CreateColorContextFromDxgiColorSpace(colorSpace, &pColorContext);

            colorContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ColorContext1(pColorContext) : null;

            return hr;
        }

        public HResult CreateColorContextFromSimpleColorProfile(D2D1_SIMPLE_COLOR_PROFILE* simpleProfile, out D2D1ColorContext1? colorContext)
        {
            ID2D1ColorContext1* pColorContext;
            int hr = Pointer->CreateColorContextFromSimpleColorProfile(simpleProfile, &pColorContext);

            colorContext = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1ColorContext1(pColorContext) : null;

            return hr;
        }

        public HResult CreateSvgDocument([Optional] IStream* inputXmlStream, D2D_SIZE_F viewportSize, out D2D1SvgDocument? svgDocument)
        {
            ID2D1SvgDocument* pSvgDocument;
            int hr = Pointer->CreateSvgDocument(inputXmlStream, viewportSize, &pSvgDocument);

            svgDocument = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgDocument(pSvgDocument) : null;

            return hr;
        }
    }
}