using System;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    /// <summary>Proxies the <see cref="ID2D1SvgElement" /> COM interface.</summary>
    public unsafe partial class D2D1SvgElement : D2D1Resource
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1SvgElement" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1SvgElement(ID2D1SvgElement* pointer) : base((ID2D1Resource*)pointer)
        {
        }

        public new ID2D1SvgElement* Pointer => (ID2D1SvgElement*)base.Pointer;

        public HResult AppendChild(ID2D1SvgElement* newChild)
        {
            return Pointer->AppendChild(newChild);
        }

        public HResult CreateChild(ushort* tagName, ID2D1SvgElement** newChild)
        {
            return Pointer->CreateChild(tagName, newChild);
        }

        public HResult GetAttributeValue(ushort* name, ID2D1SvgPathData** value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, ID2D1SvgPointCollection** value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, ID2D1SvgStrokeDashArray** value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, ID2D1SvgPaint** value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, ID2D1SvgAttribute** value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_LENGTH* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_EXTEND_MODE* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_UNIT_TYPE* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D_MATRIX_3X2_F* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_VISIBILITY* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_PRESERVE_ASPECT_RATIO* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, Guid* riid, void** value)
        {
            return Pointer->GetAttributeValue(name, riid, value);
        }

        public HResult GetAttributeValue(ushort* name, float* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_ATTRIBUTE_POD_TYPE type, void* value, uint valueSizeInBytes)
        {
            return Pointer->GetAttributeValue(name, type, value, valueSizeInBytes);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_LINE_CAP* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, DXGI_RGBA* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_FILL_MODE* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_ATTRIBUTE_STRING_TYPE type, ushort* value, uint valueCount)
        {
            return Pointer->GetAttributeValue(name, type, value, valueCount);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_DISPLAY* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_OVERFLOW* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValue(ushort* name, D2D1_SVG_LINE_JOIN* value)
        {
            return Pointer->GetAttributeValue(name, value);
        }

        public HResult GetAttributeValueLength(ushort* name, D2D1_SVG_ATTRIBUTE_STRING_TYPE type, uint* valueLength)
        {
            return Pointer->GetAttributeValueLength(name, type, valueLength);
        }

        public void GetDocument(ID2D1SvgDocument** document)
        {
            Pointer->GetDocument(document);
        }

        public void GetFirstChild(ID2D1SvgElement** child)
        {
            Pointer->GetFirstChild(child);
        }

        public void GetLastChild(ID2D1SvgElement** child)
        {
            Pointer->GetLastChild(child);
        }

        public HResult GetNextChild(ID2D1SvgElement* referenceChild, ID2D1SvgElement** nextChild)
        {
            return Pointer->GetNextChild(referenceChild, nextChild);
        }

        public void GetParent(ID2D1SvgElement** parent)
        {
            Pointer->GetParent(parent);
        }

        public HResult GetPreviousChild(ID2D1SvgElement* referenceChild, ID2D1SvgElement** previousChild)
        {
            return Pointer->GetPreviousChild(referenceChild, previousChild);
        }

        public uint GetSpecifiedAttributeCount()
        {
            return Pointer->GetSpecifiedAttributeCount();
        }

        public HResult GetSpecifiedAttributeName(uint index, ushort* name, uint nameCount, out bool inherited)
        {
            int iInherited;
            int hr = Pointer->GetSpecifiedAttributeName(index, name, nameCount, &iInherited);

            inherited = iInherited == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetSpecifiedAttributeNameLength(uint index, uint* nameLength, out bool inherited)
        {
            int iInherited;
            int hr = Pointer->GetSpecifiedAttributeNameLength(index, nameLength, &iInherited);

            inherited = iInherited == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetTagName(ushort* name, uint nameCount)
        {
            return Pointer->GetTagName(name, nameCount);
        }

        public uint GetTagNameLength()
        {
            return Pointer->GetTagNameLength();
        }

        public HResult GetTextValue(ushort* name, uint nameCount)
        {
            return Pointer->GetTextValue(name, nameCount);
        }

        public uint GetTextValueLength()
        {
            return Pointer->GetTextValueLength();
        }

        public bool HasChildren()
        {
            return Pointer->HasChildren() == TerraFX.Interop.Windows.TRUE;
        }

        public HResult InsertChildBefore(ID2D1SvgElement* newChild, ID2D1SvgElement* referenceChild = null)
        {
            return Pointer->InsertChildBefore(newChild, referenceChild);
        }

        public bool IsAttributeSpecified(ushort* name, out bool inherited)
        {
            int iInherited;
            bool result = Pointer->IsAttributeSpecified(name, &iInherited) == TerraFX.Interop.Windows.TRUE;

            inherited = iInherited == TerraFX.Interop.Windows.TRUE;

            return result;
        }

        public bool IsTextContent()
        {
            return Pointer->IsTextContent() == TerraFX.Interop.Windows.TRUE;
        }

        public HResult RemoveAttribute(ushort* name)
        {
            return Pointer->RemoveAttribute(name);
        }

        public HResult RemoveChild(ID2D1SvgElement* oldChild)
        {
            return Pointer->RemoveChild(oldChild);
        }

        public HResult ReplaceChild(ID2D1SvgElement* newChild, ID2D1SvgElement* oldChild)
        {
            return Pointer->ReplaceChild(newChild, oldChild);
        }

        public HResult SetAttributeValue(ushort* name, DXGI_RGBA* value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_ATTRIBUTE_POD_TYPE type, void* value, uint valueSizeInBytes)
        {
            return Pointer->SetAttributeValue(name, type, value, valueSizeInBytes);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_FILL_MODE value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_DISPLAY value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_ATTRIBUTE_STRING_TYPE type, ushort* value)
        {
            return Pointer->SetAttributeValue(name, type, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_OVERFLOW value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_LINE_JOIN value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_LINE_CAP value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_VISIBILITY value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_SVG_UNIT_TYPE value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, D2D1_EXTEND_MODE value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, ID2D1SvgAttribute* value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetAttributeValue(ushort* name, float value)
        {
            return Pointer->SetAttributeValue(name, value);
        }

        public HResult SetTextValue(ushort* name, uint nameCount)
        {
            return Pointer->SetTextValue(name, nameCount);
        }

        public static implicit operator ID2D1SvgElement*(D2D1SvgElement value)
        {
            return value.Pointer;
        }
    }
}