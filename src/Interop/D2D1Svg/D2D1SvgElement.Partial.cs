using System;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1Svg
{
    public unsafe partial class D2D1SvgElement
    {
        public HResult CreateChild(ushort* tagName, out D2D1SvgElement? newChild)
        {
            ID2D1SvgElement* pNewChild;
            int hr = Pointer->CreateChild(tagName, &pNewChild);

            newChild = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgElement(pNewChild) : null;

            return hr;
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1SvgPathData? value)
        {
            ID2D1SvgPathData* pValue;
            int hr;

            fixed (char* pName = name)
            {
                hr = Pointer->GetAttributeValue((ushort*)pName, &pValue);
            }

            value = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgPathData(pValue) : null;

            return hr;
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1SvgPointCollection? value)
        {
            ID2D1SvgPointCollection* pValue;
            int hr;

            fixed (char* pName = name)
            {
                hr = Pointer->GetAttributeValue((ushort*)pName, &pValue);
            }

            value = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgPointCollection(pValue) : null;

            return hr;
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1SvgStrokeDashArray? value)
        {
            ID2D1SvgStrokeDashArray* pValue;
            int hr;

            fixed (char* pName = name)
            {
                hr = Pointer->GetAttributeValue((ushort*)pName, &pValue);
            }

            value = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgStrokeDashArray(pValue) : null;

            return hr;
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1SvgPaint? value)
        {
            ID2D1SvgPaint* pValue;
            int hr;

            fixed (char* pName = name)
            {
                hr = Pointer->GetAttributeValue((ushort*)pName, &pValue);
            }

            value = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgPaint(pValue) : null;

            return hr;
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1SvgAttribute? value)
        {
            ID2D1SvgAttribute* pValue;
            int hr;

            fixed (char* pName = name)
            {
                hr = Pointer->GetAttributeValue((ushort*)pName, &pValue);
            }

            value = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgAttribute(pValue) : null;

            return hr;
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_LENGTH value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_LENGTH* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_EXTEND_MODE value)
        {
            fixed (char* pName = name)
            fixed (D2D1_EXTEND_MODE* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_UNIT_TYPE value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_UNIT_TYPE* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D_MATRIX_3X2_F value)
        {
            fixed (char* pName = name)
            fixed (D2D_MATRIX_3X2_F* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_VISIBILITY value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_VISIBILITY* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_PRESERVE_ASPECT_RATIO value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_PRESERVE_ASPECT_RATIO* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue<T>(ReadOnlySpan<char> name, out T* value)
            where T : unmanaged
        {
            Guid iid = typeof(T).GUID;

            fixed (char* pName = name)
            fixed (T** pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, &iid, (void**)pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out float value)
        {
            fixed (char* pName = name)
            fixed (float* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_ATTRIBUTE_POD_TYPE type, Span<byte> value)
        {
            fixed (char* pName = name)
            fixed (byte* pValue = value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, type, pValue, (uint)value.Length);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_LINE_CAP value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_LINE_CAP* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out DXGI_RGBA value)
        {
            fixed (char* pName = name)
            fixed (DXGI_RGBA* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_FILL_MODE value)
        {
            fixed (char* pName = name)
            fixed (D2D1_FILL_MODE* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_ATTRIBUTE_STRING_TYPE type, Span<char> value)
        {
            fixed (char* pName = name)
            fixed (char* pValue = value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, type, (ushort*)pValue, (uint)value.Length);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_DISPLAY value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_DISPLAY* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_OVERFLOW value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_OVERFLOW* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValue(ReadOnlySpan<char> name, out D2D1_SVG_LINE_JOIN value)
        {
            fixed (char* pName = name)
            fixed (D2D1_SVG_LINE_JOIN* pValue = &value)
            {
                return Pointer->GetAttributeValue((ushort*)pName, pValue);
            }
        }

        public HResult GetAttributeValueLength(ReadOnlySpan<char> name, D2D1_SVG_ATTRIBUTE_STRING_TYPE type, out uint valueLength)
        {
            fixed (char* pName = name)
            fixed (uint* pValueLength = &valueLength)
            {
                return Pointer->GetAttributeValueLength((ushort*)pName, type, pValueLength);
            }
        }

        public D2D1SvgDocument? GetDocument()
        {
            ID2D1SvgDocument* pDocument;

            Pointer->GetDocument(&pDocument);

            return pDocument != null ? new D2D1SvgDocument(pDocument) : null;
        }

        public D2D1SvgElement? GetFirstChild()
        {
            ID2D1SvgElement* pChild;

            Pointer->GetFirstChild(&pChild);

            return pChild != null ? new D2D1SvgElement(pChild) : null;
        }

        public D2D1SvgElement? GetLastChild()
        {
            ID2D1SvgElement* pChild;

            Pointer->GetLastChild(&pChild);

            return pChild != null ? new D2D1SvgElement(pChild) : null;
        }

        public HResult GetNextChild(ID2D1SvgElement* referenceChild, out D2D1SvgElement? nextChild)
        {
            ID2D1SvgElement* pNextChild;
            int hr = Pointer->GetNextChild(referenceChild, &pNextChild);

            nextChild = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgElement(pNextChild) : null;

            return hr;
        }

        public D2D1SvgElement? GetParent()
        {
            ID2D1SvgElement* pParent;

            Pointer->GetParent(&pParent);

            return pParent != null ? new D2D1SvgElement(pParent) : null;
        }

        public HResult GetPreviousChild(ID2D1SvgElement* referenceChild, out D2D1SvgElement? previousChild)
        {
            ID2D1SvgElement* pPreviousChild;
            int hr = Pointer->GetPreviousChild(referenceChild, &pPreviousChild);

            previousChild = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SvgElement(pPreviousChild) : null;

            return hr;
        }

        public HResult GetSpecifiedAttributeName(uint index, Span<char> name, out bool inherited)
        {
            int iInherited;
            int hr;

            fixed (char* pName = name)
            {
                hr = Pointer->GetSpecifiedAttributeName(index, (ushort*)pName, (uint)name.Length, &iInherited);
            }

            inherited = iInherited == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetSpecifiedAttributeNameLength(uint index, out uint nameLength, out bool inherited)
        {
            int iInherited;
            int hr;

            fixed (uint* pNameLength = &nameLength)
            {
                hr = Pointer->GetSpecifiedAttributeNameLength(index, pNameLength, &iInherited);
            }

            inherited = iInherited == TerraFX.Interop.Windows.TRUE;

            return hr;
        }

        public HResult GetTagName(Span<char> name)
        {
            fixed (char* pName = name)
            {
                return Pointer->GetTagName((ushort*)pName, (uint)name.Length);
            }
        }

        public HResult GetTextValue(Span<char> name)
        {
            fixed (char* pName = name)
            {
                return Pointer->GetTextValue((ushort*)pName, (uint)name.Length);
            }
        }

        public bool IsAttributeSpecified(ReadOnlySpan<char> name, out bool inherited)
        {
            int iInherited;
            bool result;

            fixed (char* pName = name)
            {
                result = Pointer->IsAttributeSpecified((ushort*)pName, &iInherited) == TerraFX.Interop.Windows.TRUE;
            }

            inherited = iInherited == TerraFX.Interop.Windows.TRUE;

            return result;
        }

        public HResult RemoveAttribute(ReadOnlySpan<char> name)
        {
            fixed (char* pName = name)
            {
                return Pointer->RemoveAttribute((ushort*)pName);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, DXGI_RGBA* value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_ATTRIBUTE_POD_TYPE type, ReadOnlySpan<byte> value)
        {
            fixed (char* pName = name)
            fixed (byte* pValue = value)
            {
                return Pointer->SetAttributeValue((ushort*)pName, type, pValue, (uint)value.Length);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_FILL_MODE value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_DISPLAY value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_ATTRIBUTE_STRING_TYPE type, ReadOnlySpan<char> value)
        {
            fixed (char* pName = name)
            fixed (char* pValue = value)
            {
                return Pointer->SetAttributeValue((ushort*)pName, type, (ushort*)pValue);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_OVERFLOW value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_LINE_JOIN value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_LINE_CAP value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_VISIBILITY value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_SVG_UNIT_TYPE value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, D2D1_EXTEND_MODE value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, ID2D1SvgAttribute* value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetAttributeValue(ReadOnlySpan<char> name, float value)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetAttributeValue((ushort*)pName, value);
            }
        }

        public HResult SetTextValue(ReadOnlySpan<char> name)
        {
            fixed (char* pName = name)
            {
                return Pointer->SetTextValue((ushort*)pName, (uint)name.Length);
            }
        }
    }
}