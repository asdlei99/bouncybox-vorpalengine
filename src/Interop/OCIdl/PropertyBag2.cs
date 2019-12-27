using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.OCIdl
{
    /// <summary>Proxies the <see cref="IPropertyBag2" /> COM interface.</summary>
    public unsafe partial class PropertyBag2 : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="PropertyBag2" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public PropertyBag2(IPropertyBag2* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IPropertyBag2* Pointer => (IPropertyBag2*)base.Pointer;

        public HResult CountProperties(uint* properties)
        {
            return Pointer->CountProperties(properties);
        }

        public HResult GetPropertyInfo(uint property, uint propertySize, PROPBAG2* propBag, uint* actualProperties)
        {
            return Pointer->GetPropertyInfo(property, propertySize, propBag, actualProperties);
        }

        public HResult LoadObject(ushort* name, uint hint, IUnknown* unkObject = null, IErrorLog* errLog = null)
        {
            return Pointer->LoadObject(name, hint, unkObject, errLog);
        }

        public HResult Read(uint propertySize, PROPBAG2* propBag, [Optional] IErrorLog* errLog, VARIANT* value, int* error = null)
        {
            return Pointer->Read(propertySize, propBag, errLog, value, error);
        }

        public HResult Write(uint propertySize, PROPBAG2* propBag, VARIANT* value)
        {
            return Pointer->Write(propertySize, propBag, value);
        }

        public static implicit operator IPropertyBag2*(PropertyBag2 value)
        {
            return value.Pointer;
        }
    }
}