using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    /// <summary>Proxies the <see cref="ID2D1SvgAttribute" /> COM interface.</summary>
    public unsafe partial class D2D1SvgAttribute : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgAttribute" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgAttribute(ID2D1SvgAttribute* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1SvgAttribute* Pointer => (ID2D1SvgAttribute*)base.Pointer;

        public HResult Clone(ID2D1SvgAttribute** attribute)
        {
            return Pointer->Clone(attribute);
        }

        public D2D1SvgElement? GetElement()
        {
            ID2D1SvgElement* pElement;

            Pointer->GetElement(&pElement);

            return pElement != null ? new D2D1SvgElement(pElement) : null;
        }

        public static implicit operator ID2D1SvgAttribute*(D2D1SvgAttribute value)
        {
            return value.Pointer;
        }
    }
}