using System;
using BouncyBox.VorpalEngine.Interop.D2D1_1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    public unsafe partial class D2D1SvgPathData
    {
        public HResult CreatePathGeometry(D2D1_FILL_MODE fillMode, out D2D1PathGeometry1? pathGeometry)
        {
            ID2D1PathGeometry1* pPathGeometry;
            int hr = Pointer->CreatePathGeometry(fillMode, &pPathGeometry);

            pathGeometry = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1PathGeometry1(pPathGeometry) : null;

            return hr;
        }

        public HResult GetCommands(Span<D2D1_SVG_PATH_COMMAND> commands, uint startIndex = 0)
        {
            fixed (D2D1_SVG_PATH_COMMAND* pCommands = commands)
            {
                return Pointer->GetCommands(pCommands, (uint)commands.Length, startIndex);
            }
        }

        public HResult GetSegmentData(Span<float> data, uint startIndex = 0)
        {
            fixed (float* pData = data)
            {
                return Pointer->GetSegmentData(pData, (uint)data.Length, startIndex);
            }
        }

        public HResult UpdateCommands(ReadOnlySpan<D2D1_SVG_PATH_COMMAND> commands, uint startIndex = 0)
        {
            fixed (D2D1_SVG_PATH_COMMAND* pCommands = commands)
            {
                return Pointer->UpdateCommands(pCommands, (uint)commands.Length, startIndex);
            }
        }

        public HResult UpdateSegmentData(ReadOnlySpan<float> data, uint startIndex = 0)
        {
            fixed (float* pData = data)
            {
                return Pointer->UpdateSegmentData(pData, (uint)data.Length, startIndex);
            }
        }
    }
}