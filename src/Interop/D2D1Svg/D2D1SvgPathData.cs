using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    /// <summary>Proxies the <see cref="ID2D1SvgPathData" /> COM interface.</summary>
    public unsafe partial class D2D1SvgPathData : D2D1SvgAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgPathData" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgPathData(ID2D1SvgPathData* pointer) : base((ID2D1SvgAttribute*)pointer)
        {
        }

        public new ID2D1SvgPathData* Pointer => (ID2D1SvgPathData*)base.Pointer;

        public HResult CreatePathGeometry(D2D1_FILL_MODE fillMode, ID2D1PathGeometry1** pathGeometry)
        {
            return Pointer->CreatePathGeometry(fillMode, pathGeometry);
        }

        public HResult GetCommands(D2D1_SVG_PATH_COMMAND* commands, uint commandsCount, uint startIndex = 0)
        {
            return Pointer->GetCommands(commands, commandsCount, startIndex);
        }

        public uint GetCommandsCount()
        {
            return Pointer->GetCommandsCount();
        }

        public HResult GetSegmentData(float* data, uint dataCount, uint startIndex = 0)
        {
            return Pointer->GetSegmentData(data, dataCount, startIndex);
        }

        public uint GetSegmentDataCount()
        {
            return Pointer->GetSegmentDataCount();
        }

        public HResult RemoveCommandsAtEnd(uint commandsCount)
        {
            return Pointer->RemoveCommandsAtEnd(commandsCount);
        }

        public HResult RemoveSegmentDataAtEnd(uint dataCount)
        {
            return Pointer->RemoveSegmentDataAtEnd(dataCount);
        }

        public HResult UpdateCommands(D2D1_SVG_PATH_COMMAND* commands, uint commandsCount, uint startIndex = 0)
        {
            return Pointer->UpdateCommands(commands, commandsCount, startIndex);
        }

        public HResult UpdateSegmentData(float* data, uint dataCount, uint startIndex = 0)
        {
            return Pointer->UpdateSegmentData(data, dataCount, startIndex);
        }

        public static implicit operator ID2D1SvgPathData*(D2D1SvgPathData value)
        {
            return value.Pointer;
        }
    }
}