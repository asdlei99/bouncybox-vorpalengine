using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1DeviceContext5" /> COM interface.</summary>
    public unsafe partial class D2D1DeviceContext5 : D2D1DeviceContext4
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DeviceContext5" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DeviceContext5(ID2D1DeviceContext5* pointer) : base((ID2D1DeviceContext4*)pointer)
        {
        }

        public new ID2D1DeviceContext5* Pointer => (ID2D1DeviceContext5*)base.Pointer;

        public HResult CreateColorContextFromDxgiColorSpace(DXGI_COLOR_SPACE_TYPE colorSpace, ID2D1ColorContext1** colorContext)
        {
            return Pointer->CreateColorContextFromDxgiColorSpace(colorSpace, colorContext);
        }

        public HResult CreateColorContextFromSimpleColorProfile(D2D1_SIMPLE_COLOR_PROFILE* simpleProfile, ID2D1ColorContext1** colorContext)
        {
            return Pointer->CreateColorContextFromSimpleColorProfile(simpleProfile, colorContext);
        }

        public HResult CreateSvgDocument([Optional] IStream* inputXmlStream, D2D_SIZE_F viewportSize, ID2D1SvgDocument** svgDocument)
        {
            return Pointer->CreateSvgDocument(inputXmlStream, viewportSize, svgDocument);
        }

        public void DrawSvgDocument(ID2D1SvgDocument* svgDocument)
        {
            Pointer->DrawSvgDocument(svgDocument);
        }

        public static implicit operator ID2D1DeviceContext5*(D2D1DeviceContext5 value)
        {
            return value.Pointer;
        }
    }
}