using BouncyBox.VorpalEngine.Engine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1ColorContext" /> COM interface.</summary>
    public unsafe partial class D2D1ColorContext : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1ColorContext" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1ColorContext(ID2D1ColorContext* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1ColorContext* Pointer => (ID2D1ColorContext*)base.Pointer;

        public D2D1_COLOR_SPACE GetColorSpace()
        {
            return Pointer->GetColorSpace();
        }

        public HResult GetProfile(byte* profile, uint profileSize)
        {
            return Pointer->GetProfile(profile, profileSize);
        }

        public uint GetProfileSize()
        {
            return Pointer->GetProfileSize();
        }

        public static implicit operator ID2D1ColorContext*(D2D1ColorContext value)
        {
            return value.Pointer;
        }
    }
}