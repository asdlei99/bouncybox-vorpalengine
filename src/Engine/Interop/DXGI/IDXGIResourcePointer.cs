using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe struct IDXGIResourcePointer
    {
        public IDXGIResource* Pointer;

        public static implicit operator IDXGIResourcePointer(IDXGIResource* value)
        {
            return
                new IDXGIResourcePointer
                {
                    Pointer = value
                };
        }
    }
}