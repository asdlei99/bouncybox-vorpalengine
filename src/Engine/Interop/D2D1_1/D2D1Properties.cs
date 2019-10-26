using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1Properties" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class D2D1Properties : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Properties" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Properties(ID2D1Properties* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1Properties* Pointer => (ID2D1Properties*)base.Pointer;

        public uint GetPropertyCount()
        {
            return Pointer->GetPropertyCount();
        }

        public uint GetPropertyIndex(ushort* name)
        {
            return Pointer->GetPropertyIndex(name);
        }

        public HResult GetPropertyName(uint index, ushort* name, uint nameCount)
        {
            return Pointer->GetPropertyName(index, name, nameCount);
        }

        public HResult GetPropertyName<U>(U index, ushort* name, uint nameCount)
            where U : unmanaged
        {
            return Pointer->GetPropertyName(index, name, nameCount);
        }

        public uint GetPropertyNameLength(uint index)
        {
            return Pointer->GetPropertyNameLength(index);
        }

        public uint GetPropertyNameLength<U>(U index)
            where U : unmanaged
        {
            return Pointer->GetPropertyNameLength(index);
        }

        public HResult GetSubProperties<U>(U index, ID2D1Properties** subProperties)
            where U : unmanaged
        {
            return Pointer->GetSubProperties(index, subProperties);
        }

        public HResult GetSubProperties(uint index, ID2D1Properties** subProperties)
        {
            return Pointer->GetSubProperties(index, subProperties);
        }

        public D2D1_PROPERTY_TYPE GetType(uint index)
        {
            return Pointer->GetType(index);
        }

        public D2D1_PROPERTY_TYPE GetType<U>(U index)
            where U : unmanaged
        {
            return Pointer->GetType(index);
        }

        public HResult GetValue<U>(U index, byte* data, uint dataSize)
            where U : unmanaged
        {
            return Pointer->GetValue(index, data, dataSize);
        }

        public HResult GetValue(uint index, byte* data, uint dataSize)
        {
            return Pointer->GetValue(index, data, dataSize);
        }

        public HResult GetValue(uint index, D2D1_PROPERTY_TYPE type, byte* data, uint dataSize)
        {
            return Pointer->GetValue(index, type, data, dataSize);
        }

        public HResult GetValue<T, U>(U index, T* value)
            where T : unmanaged
            where U : unmanaged
        {
            return Pointer->GetValue(index, value);
        }

        public T GetValue<T, U>(U index)
            where T : unmanaged
            where U : unmanaged
        {
            return Pointer->GetValue<T, U>(index);
        }

        public HResult GetValueByName(ushort* name, D2D1_PROPERTY_TYPE type, byte* data, uint dataSize)
        {
            return Pointer->GetValueByName(name, type, data, dataSize);
        }

        public T GetValueByName<T>(ushort* propertyName)
            where T : unmanaged
        {
            return Pointer->GetValueByName<T>(propertyName);
        }

        public HResult GetValueByName<T>(ushort* propertyName, T* value)
            where T : unmanaged
        {
            return Pointer->GetValueByName(propertyName, value);
        }

        public HResult GetValueByName(ushort* name, byte* data, uint dataSize)
        {
            return Pointer->GetValueByName(name, data, dataSize);
        }

        public uint GetValueSize<U>(U index)
            where U : unmanaged
        {
            return Pointer->GetValueSize(index);
        }

        public uint GetValueSize(uint index)
        {
            return Pointer->GetValueSize(index);
        }

        public HResult SetValue<T, U>(U index, T* value)
            where T : unmanaged
            where U : unmanaged
        {
            return Pointer->SetValue(index, value);
        }

        public HResult SetValue<U>(U index, byte* data, uint dataSize)
            where U : unmanaged
        {
            return Pointer->SetValue(index, data, dataSize);
        }

        public HResult SetValue(uint index, D2D1_PROPERTY_TYPE type, byte* data, uint dataSize)
        {
            return Pointer->SetValue(index, type, data, dataSize);
        }

        public HResult SetValue(uint index, byte* data, uint dataSize)
        {
            return Pointer->SetValue(index, data, dataSize);
        }

        public HResult SetValueByName(ushort* name, D2D1_PROPERTY_TYPE type, byte* data, uint dataSize)
        {
            return Pointer->SetValueByName(name, type, data, dataSize);
        }

        public HResult SetValueByName<T>(ushort* propertyName, T* value)
            where T : unmanaged
        {
            return Pointer->SetValueByName(propertyName, value);
        }

        public HResult SetValueByName(ushort* name, byte* data, uint dataSize)
        {
            return Pointer->SetValueByName(name, data, dataSize);
        }

        public static implicit operator ID2D1Properties*(D2D1Properties value)
        {
            return value.Pointer;
        }
    }
}