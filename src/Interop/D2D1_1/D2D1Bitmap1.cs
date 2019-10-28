using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1Bitmap1" /> COM interface.</summary>
    public unsafe partial class D2D1Bitmap1 : D2D1Bitmap
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Bitmap1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Bitmap1(ID2D1Bitmap1* pointer) : base((ID2D1Bitmap*)pointer)
        {
        }

        public new ID2D1Bitmap1* Pointer => (ID2D1Bitmap1*)base.Pointer;

        public void GetColorContext(ID2D1ColorContext** colorContext)
        {
            Pointer->GetColorContext(colorContext);
        }

        public D2D1_BITMAP_OPTIONS GetOptions()
        {
            return Pointer->GetOptions();
        }

        public HResult GetSurface(IDXGISurface** dxgiSurface)
        {
            return Pointer->GetSurface(dxgiSurface);
        }

        public HResult Map(D2D1_MAP_OPTIONS options, D2D1_MAPPED_RECT* mappedRect)
        {
            return Pointer->Map(options, mappedRect);
        }

        public HResult Unmap()
        {
            return Pointer->Unmap();
        }

        public static implicit operator ID2D1Bitmap1*(D2D1Bitmap1 value)
        {
            return value.Pointer;
        }
    }
}