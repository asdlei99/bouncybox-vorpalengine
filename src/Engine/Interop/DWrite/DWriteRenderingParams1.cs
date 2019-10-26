using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteRenderingParams1" /> COM interface.</summary>
    public unsafe class DWriteRenderingParams1 : DWriteRenderingParams
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteRenderingParams1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteRenderingParams1(IDWriteRenderingParams1* pointer) : base((IDWriteRenderingParams*)pointer)
        {
        }

        public new IDWriteRenderingParams1* Pointer => (IDWriteRenderingParams1*)base.Pointer;

        public float GetGrayscaleEnhancedContrast()
        {
            return Pointer->GetGrayscaleEnhancedContrast();
        }

        public static implicit operator IDWriteRenderingParams1*(DWriteRenderingParams1 value)
        {
            return value.Pointer;
        }
    }
}