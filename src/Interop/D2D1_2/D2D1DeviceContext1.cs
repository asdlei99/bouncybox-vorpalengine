using BouncyBox.VorpalEngine.Interop.D2D1_1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_2
{
    /// <summary>Proxies the <see cref="ID2D1DeviceContext1" /> COM interface.</summary>
    public unsafe class D2D1DeviceContext1 : D2D1DeviceContext
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1DeviceContext1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1DeviceContext1(ID2D1DeviceContext1* pointer) : base((ID2D1DeviceContext*)pointer)
        {
        }

        public new ID2D1DeviceContext1* Pointer => (ID2D1DeviceContext1*)base.Pointer;

        public HResult CreateFilledGeometryRealization(ID2D1Geometry* geometry, float flatteningTolerance, out D2D1GeometryRealization? geometryRealization)
        {
            ID2D1GeometryRealization* pGeometryRealization;
            int hr = Pointer->CreateFilledGeometryRealization(geometry, flatteningTolerance, &pGeometryRealization);

            geometryRealization = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GeometryRealization(pGeometryRealization) : null;

            return hr;
        }

        public HResult CreateStrokedGeometryRealization(
            ID2D1Geometry* geometry,
            float flatteningTolerance,
            float strokeWidth,
            ID2D1StrokeStyle* strokeStyle,
            out D2D1GeometryRealization? geometryRealization)
        {
            ID2D1GeometryRealization* pGeometryRealization;
            int hr = Pointer->CreateStrokedGeometryRealization(geometry, flatteningTolerance, strokeWidth, strokeStyle, &pGeometryRealization);

            geometryRealization = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GeometryRealization(pGeometryRealization) : null;

            return hr;
        }

        public void DrawGeometryRealization(ID2D1GeometryRealization* geometryRealization, ID2D1Brush* brush)
        {
            Pointer->DrawGeometryRealization(geometryRealization, brush);
        }

        public static implicit operator ID2D1DeviceContext1*(D2D1DeviceContext1 value)
        {
            return value.Pointer;
        }
    }
}