using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    /// <summary>Proxies the <see cref="ID2D1SvgDocument" /> COM interface.</summary>
    public unsafe partial class D2D1SvgDocument : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgDocument" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgDocument(ID2D1SvgDocument* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1SvgDocument* Pointer => (ID2D1SvgDocument*)base.Pointer;

        public HResult CreatePaint(D2D1_SVG_PAINT_TYPE paintType, [Optional] DXGI_RGBA* color, [Optional] ushort* id, ID2D1SvgPaint** paint)
        {
            return Pointer->CreatePaint(paintType, color, id, paint);
        }

        public HResult CreatePathData(
            [Optional] float* segmentData,
            uint segmentDataCount,
            [Optional] D2D1_SVG_PATH_COMMAND* commands,
            uint commandsCount,
            ID2D1SvgPathData** pathData)
        {
            return Pointer->CreatePathData(segmentData, segmentDataCount, commands, commandsCount, pathData);
        }

        public HResult CreatePointCollection([Optional] D2D_POINT_2F* points, uint pointsCount, ID2D1SvgPointCollection** pointCollection)
        {
            return Pointer->CreatePointCollection(points, pointsCount, pointCollection);
        }

        public HResult CreateStrokeDashArray([Optional] D2D1_SVG_LENGTH* dashes, uint dashesCount, ID2D1SvgStrokeDashArray** strokeDashArray)
        {
            return Pointer->CreateStrokeDashArray(dashes, dashesCount, strokeDashArray);
        }

        public HResult Deserialize(IStream* inputXmlStream, ID2D1SvgElement** subtree)
        {
            return Pointer->Deserialize(inputXmlStream, subtree);
        }

        public HResult FindElementById(ushort* id, ID2D1SvgElement** svgElement)
        {
            return Pointer->FindElementById(id, svgElement);
        }

        public void GetRoot(ID2D1SvgElement** root)
        {
            Pointer->GetRoot(root);
        }

        public D2D_SIZE_F GetViewportSize()
        {
            return Pointer->GetViewportSize();
        }

        public HResult Serialize(IStream* outputXmlStream, ID2D1SvgElement* subtree = null)
        {
            return Pointer->Serialize(outputXmlStream, subtree);
        }

        public HResult SetRoot(ID2D1SvgElement* root = null)
        {
            return Pointer->SetRoot(root);
        }

        public HResult SetViewportSize(D2D_SIZE_F viewportSize)
        {
            return Pointer->SetViewportSize(viewportSize);
        }

        public static implicit operator ID2D1SvgDocument*(D2D1SvgDocument value)
        {
            return value.Pointer;
        }
    }
}