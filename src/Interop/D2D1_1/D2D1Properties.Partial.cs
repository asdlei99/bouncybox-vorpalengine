using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class D2D1Properties
    {
        public uint GetPropertyIndex(ReadOnlySpan<char> name)
        {
            fixed (char* pName = name)
            {
                return Pointer->GetPropertyIndex((ushort*)pName);
            }
        }

        public HResult GetPropertyName(uint index, Span<char> name)
        {
            fixed (char* pName = name)
            {
                return Pointer->GetPropertyName(index, (ushort*)pName, (uint)name.Length);
            }
        }

        public HResult GetPropertyName<U>(U index, Span<char> name)
            where U : unmanaged
        {
            fixed (char* pName = name)
            {
                return Pointer->GetPropertyName(index, (ushort*)pName, (uint)name.Length);
            }
        }

        public HResult GetSubProperties<U>(U index, out D2D1Properties? subProperties)
            where U : unmanaged
        {
            ID2D1Properties* pSubProperties;
            int hr = Pointer->GetSubProperties(index, &pSubProperties);

            subProperties = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Properties(pSubProperties) : null;

            return hr;
        }

        public HResult GetSubProperties(uint index, out D2D1Properties? subProperties)
        {
            ID2D1Properties* pSubProperties;
            int hr = Pointer->GetSubProperties(index, &pSubProperties);

            subProperties = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Properties(pSubProperties) : null;

            return hr;
        }

        public HResult GetValue<U>(U index, Span<byte> data)
            where U : unmanaged
        {
            fixed (byte* pData = data)
            {
                return Pointer->GetValue(index, pData, (uint)data.Length);
            }
        }

        public HResult GetValue(uint index, Span<byte> data)
        {
            fixed (byte* pData = data)
            {
                return Pointer->GetValue(index, pData, (uint)data.Length);
            }
        }

        public HResult GetValue(uint index, D2D1_PROPERTY_TYPE type, Span<byte> data)
        {
            fixed (byte* pData = data)
            {
                return Pointer->GetValue(index, type, pData, (uint)data.Length);
            }
        }

        public HResult GetValueByName(ReadOnlySpan<char> name, D2D1_PROPERTY_TYPE type, Span<byte> data)
        {
            fixed (char* pName = name)
            fixed (byte* pData = data)
            {
                return Pointer->GetValueByName((ushort*)pName, type, pData, (uint)data.Length);
            }
        }

        public T GetValueByName<T>(ReadOnlySpan<char> propertyName)
            where T : unmanaged
        {
            fixed (char* pPropertyName = propertyName)
            {
                return Pointer->GetValueByName<T>((ushort*)pPropertyName);
            }
        }

        public HResult GetValueByName<T>(ReadOnlySpan<char> propertyName, T* value)
            where T : unmanaged
        {
            fixed (char* pPropertyName = propertyName)
            {
                return Pointer->GetValueByName((ushort*)pPropertyName, value);
            }
        }

        public HResult GetValueByName(ReadOnlySpan<char> name, Span<byte> data)
        {
            fixed (char* pName = name)
            fixed (byte* pData = data)
            {
                return Pointer->GetValueByName((ushort*)pName, pData, (uint)data.Length);
            }
        }

        public HResult SetValue<U>(U index, ReadOnlySpan<byte> data)
            where U : unmanaged
        {
            fixed (byte* pData = data)
            {
                return Pointer->SetValue(index, pData, (uint)data.Length);
            }
        }

        public HResult SetValue(uint index, D2D1_PROPERTY_TYPE type, ReadOnlySpan<byte> data)
        {
            fixed (byte* pData = data)
            {
                return Pointer->SetValue(index, type, pData, (uint)data.Length);
            }
        }

        public HResult SetValue(uint index, ReadOnlySpan<byte> data)
        {
            fixed (byte* pData = data)
            {
                return Pointer->SetValue(index, pData, (uint)data.Length);
            }
        }

        public HResult SetValueByName(ReadOnlySpan<char> name, D2D1_PROPERTY_TYPE type, ReadOnlySpan<byte> data)
        {
            fixed (char* pName = name)
            fixed (byte* pData = data)
            {
                return Pointer->SetValueByName((ushort*)pName, type, pData, (uint)data.Length);
            }
        }

        public HResult SetValueByName<T>(ReadOnlySpan<char> propertyName, T* value)
            where T : unmanaged
        {
            fixed (char* pPropertyName = propertyName)
            {
                return Pointer->SetValueByName((ushort*)pPropertyName, value);
            }
        }

        public HResult SetValueByName(ReadOnlySpan<char> name, ReadOnlySpan<byte> data)
        {
            fixed (char* pName = name)
            fixed (byte* pData = data)
            {
                return Pointer->SetValueByName((ushort*)pName, pData, (uint)data.Length);
            }
        }
    }
}