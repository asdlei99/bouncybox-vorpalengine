using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.OCIdl
{
    public unsafe partial class PropertyBag2
    {
        public HResult CountProperties(out uint properties)
        {
            fixed (uint* pProperties = &properties)
            {
                return Pointer->CountProperties(pProperties);
            }
        }

        public HResult GetPropertyInfo(uint property, Span<PROPBAG2> propBags, out uint actualProperties)
        {
            fixed (PROPBAG2* pPropBags = propBags)
            fixed (uint* pActualProperties = &actualProperties)
            {
                return Pointer->GetPropertyInfo(property, (uint)propBags.Length, pPropBags, pActualProperties);
            }
        }

        public HResult LoadObject(ReadOnlySpan<char> name, uint hint, IUnknown* unkObject = null, IErrorLog* errLog = null)
        {
            fixed (char* pName = name)
            {
                return Pointer->LoadObject((ushort*)pName, hint, unkObject, errLog);
            }
        }

        public HResult Read(Span<PROPBAG2> propBags, [Optional] IErrorLog* errLog, VARIANT* value, int* error = null)
        {
            fixed (PROPBAG2* pPropBags = propBags)
            {
                return Pointer->Read((uint)propBags.Length, pPropBags, errLog, value, error);
            }
        }

        public HResult Write(ReadOnlySpan<PROPBAG2> propBags, VARIANT* value)
        {
            fixed (PROPBAG2* pPropBags = propBags)
            {
                return Pointer->Write((uint)propBags.Length, pPropBags, value);
            }
        }
    }
}