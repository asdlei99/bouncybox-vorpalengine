using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.D2D1_1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    /// <summary>Proxies the <see cref="ID2D1ColorContext1" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe class D2D1ColorContext1 : D2D1ColorContext
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1ColorContext1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1ColorContext1(ID2D1ColorContext1* pointer) : base((ID2D1ColorContext*)pointer)
        {
        }

        public new ID2D1ColorContext1* Pointer => (ID2D1ColorContext1*)base.Pointer;

        public D2D1_COLOR_CONTEXT_TYPE GetColorContextType()
        {
            return Pointer->GetColorContextType();
        }

        public DXGI_COLOR_SPACE_TYPE GetDXGIColorSpace()
        {
            return Pointer->GetDXGIColorSpace();
        }

        public HResult GetSimpleColorProfile(out D2D1_SIMPLE_COLOR_PROFILE simpleProfile)
        {
            fixed (D2D1_SIMPLE_COLOR_PROFILE* pSimpleProfile = &simpleProfile)
            {
                return Pointer->GetSimpleColorProfile(pSimpleProfile);
            }
        }

        public static implicit operator ID2D1ColorContext1*(D2D1ColorContext1 value)
        {
            return value.Pointer;
        }
    }
}