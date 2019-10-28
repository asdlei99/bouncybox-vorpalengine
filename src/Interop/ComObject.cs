using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Common;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop
{
    /// <summary>Base class that contains COM object helper methods.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public abstract unsafe class ComObject : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="ComObject" /> type.</para>
        ///     <para>
        ///         This constructor is intended to wrap factory interface pointers whose reference counts are already 1.
        ///         It does not increment the reference count.
        ///     </para>
        /// </summary>
        /// <param name="pointer">A COM object pointer.</param>
        protected ComObject(IUnknown* pointer)
        {
            if (pointer is null)
            {
                throw new ArgumentNullException(nameof(pointer));
            }

            Pointer = pointer;
        }

        /// <summary>Gets a pointer to <see cref="IUnknown" />.</summary>
        protected IUnknown* Pointer { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    ReleaseUnmanagedResources();
                    GC.SuppressFinalize(this);
                },
                ref _isDisposed);
        }

        /// <summary>Queries for the specified COM interface.</summary>
        /// <returns>Returns a pointer to the COM interface.</returns>
        /// <exception>Thrown when the interface query failed.</exception>
        public T* QueryInterface<T>()
            where T : unmanaged
        {
            Type interfaceType = typeof(T);
            Guid iid = interfaceType.GUID;
            T* pvObject;
            ((HResult)Pointer->QueryInterface(&iid, (void**)&pvObject))
                .ThrowIfFailed($"Interface query from {Pointer->GetType().Name} to {interfaceType.Name} failed.");

            return pvObject;
        }

        /// <summary>Queries for the specified COM interface.</summary>
        /// <returns>Returns true if the query succeeded; otherwise, returns false.</returns>
        /// <param name="result">A pointer to the target interface, if the query succeeded; otherwise, null.</param>
        /// <exception cref="COMException">
        ///     Thrown when the interface query failed for a reason other than
        ///     <see cref="Windows.E_NOINTERFACE" />.
        /// </exception>
        public bool TryQueryInterface<T>(out T* result)
            where T : unmanaged
        {
            Type interfaceType = typeof(T);
            Guid iid = interfaceType.GUID;
            T* pvObject;
            HResult hr = Pointer->QueryInterface(&iid, (void**)&pvObject);
            bool succeeded = TerraFX.Interop.Windows.SUCCEEDED(hr);

            if (!succeeded && hr.HR != unchecked((int)Windows.E_NOINTERFACE))
            {
                hr.ThrowIfFailed($"Interface query from {Pointer->GetType().Name} to {interfaceType.Name} failed.");
            }

            result = pvObject;

            return succeeded;
        }

        /// <summary>Returns the object's underlying pointer.</summary>
        /// <param name="comObject"></param>
        public static implicit operator IUnknown*(ComObject comObject)
        {
            return comObject.Pointer;
        }

        /// <inheritdoc />
        ~ComObject()
        {
            ReleaseUnmanagedResources();
        }

        /// <summary>Releases unmanaged resources.</summary>
        private void ReleaseUnmanagedResources()
        {
            Pointer->Release();
        }
    }
}