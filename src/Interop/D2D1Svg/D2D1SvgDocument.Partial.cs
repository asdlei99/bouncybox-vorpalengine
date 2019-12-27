using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    public unsafe partial class D2D1SvgDocument
    {
        public HResult CreatePaint(D2D1_SVG_PAINT_TYPE paintType, [Optional] DXGI_RGBA* color, [Optional] ushort* id, out D2D1SvgPaint? paint)
        {
            ID2D1SvgPaint* pPaint;
            int hr = Pointer->CreatePaint(paintType, color, id, &pPaint);

            paint = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgPaint(pPaint) : null;

            return hr;
        }

        public HResult CreatePathData(
            [Optional] ReadOnlySpan<float> segmentData,
            [Optional] ReadOnlySpan<D2D1_SVG_PATH_COMMAND> commands,
            out D2D1SvgPathData? pathData)
        {
            ID2D1SvgPathData* pPathData;
            int hr;

            fixed (float* pSegmentData = segmentData)
            fixed (D2D1_SVG_PATH_COMMAND* pCommands = commands)
            {
                hr = Pointer->CreatePathData(pSegmentData, (uint)segmentData.Length, pCommands, (uint)commands.Length, &pPathData);
            }

            pathData = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgPathData(pPathData) : null;

            return hr;
        }

        public HResult CreatePointCollection([Optional] ReadOnlySpan<D2D_POINT_2F> points, out D2D1SvgPointCollection? pointCollection)
        {
            ID2D1SvgPointCollection* pPointCollection;
            int hr;

            fixed (D2D_POINT_2F* pPoints = points)
            {
                hr = Pointer->CreatePointCollection(pPoints, (uint)points.Length, &pPointCollection);
            }

            pointCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgPointCollection(pPointCollection) : null;

            return hr;
        }

        public HResult CreateStrokeDashArray([Optional] ReadOnlySpan<D2D1_SVG_LENGTH> dashes, out D2D1SvgStrokeDashArray? strokeDashArray)
        {
            ID2D1SvgStrokeDashArray* pStrokeDashArray;
            int hr;

            fixed (D2D1_SVG_LENGTH* pDashes = dashes)
            {
                hr = Pointer->CreateStrokeDashArray(pDashes, (uint)dashes.Length, &pStrokeDashArray);
            }

            strokeDashArray = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgStrokeDashArray(pStrokeDashArray) : null;

            return hr;
        }

        public HResult Deserialize(IStream* inputXmlStream, out D2D1SvgElement? subtree)
        {
            ID2D1SvgElement* pSubtree;
            int hr = Pointer->Deserialize(inputXmlStream, &pSubtree);

            subtree = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgElement(pSubtree) : null;

            return hr;
        }

        public HResult FindElementById(ushort* id, out D2D1SvgElement? svgElement)
        {
            ID2D1SvgElement* pSvgElement;
            int hr = Pointer->FindElementById(id, &pSvgElement);

            svgElement = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgElement(pSvgElement) : null;

            return hr;
        }

        public D2D1SvgElement? GetRoot()
        {
            ID2D1SvgElement* pRoot;

            Pointer->GetRoot(&pRoot);

            return pRoot != null ? new D2D1SvgElement(pRoot) : null;
        }
    }
}