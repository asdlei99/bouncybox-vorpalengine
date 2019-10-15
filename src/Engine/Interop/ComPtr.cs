// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from winrt/wrl/client.h in the Windows SDK for Windows 10.0.18362.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial struct ComPtr<T> : IDisposable
        where T : unmanaged
    {
        private T* ptr_;

        public ComPtr(T* ptr)
        {
            ptr_ = ptr;
            InternalAddRef();
        }

        public ComPtr(void* other) : this((T*)other)
        {
        }

        public static implicit operator ComPtr<T>(T* ptr)
        {
            return new ComPtr<T>(ptr);
        }

        public static implicit operator ComPtr<T>(void* other)
        {
            return (T*)other;
        }

        public static implicit operator T*(ComPtr<T> value)
        {
            return value.ptr_;
        }

        public int As<U>(ComPtr<U>* p)
            where U : unmanaged
        {
            Guid iid = typeof(U).GUID;
            return CopyTo(&iid, p->ReleaseAndGetAddressOf());
        }

        public int AsIID<U>(Guid* riid, ComPtr<IUnknown>* p)
            where U : unmanaged
        {
            return CopyTo(riid, p->ReleaseAndGetAddressOf());
        }

        public void Attach(T* other)
        {
            if (ptr_ != null)
            {
                uint refCount = ((IUnknown*)ptr_)->Release();
                Debug.Assert(refCount != 0 || ptr_ != other);
            }

            ptr_ = other;
        }

        public int CopyTo(T** ptr)
        {
            InternalAddRef();
            *ptr = ptr_;
            return Windows.S_OK;
        }

        public int CopyTo(Guid* riid, void** ptr)
        {
            return ((IUnknown*)ptr_)->QueryInterface(riid, ptr);
        }

        public int CopyTo<U>(Guid* riid, U** ptr)
            where U : unmanaged
        {
            return CopyTo(riid, (void**)ptr);
        }

        public T* Detatch()
        {
            T* ptr = ptr_;
            ptr_ = null;
            return ptr;
        }

        public void Dispose()
        {
            InternalRelease();
        }

        public T* Get()
        {
            return ptr_;
        }

        public T** GetAddressOf()
        {
            return (T**)Unsafe.AsPointer(ref this);
        }

        public T** ReleaseAndGetAddressOf()
        {
            InternalRelease();
            return GetAddressOf();
        }

        public uint Reset()
        {
            return InternalRelease();
        }

        private void InternalAddRef()
        {
            if (ptr_ != null)
            {
                ((IUnknown*)ptr_)->AddRef();
            }
        }

        private uint InternalRelease()
        {
            var refCount = 0u;
            T* temp = ptr_;

            if (temp != null)
            {
                ptr_ = null;
                refCount = ((IUnknown*)temp)->Release();
            }

            return refCount;
        }
    }
}