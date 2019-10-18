using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Base class that contains COM object helper methods.
    /// </summary>
    public abstract class ComObject
    {
        /// <summary>
        ///     Throws a <see cref="ComObjectException" /> for failure HRESULTs.
        /// </summary>
        /// <param name="hr">An HRESULT.</param>
        /// <param name="exceptionMessage">The exception message to use if <see cref="ComObjectException" /> is thrown.</param>
        /// <param name="allowNoInterface">
        ///     A value that determines whether to allow an <see cref="Engine.Interop.Windows.E_NOINTERFACE" />
        ///     HRESULT.
        /// </param>
        public static void CheckResultHandle(int hr, string exceptionMessage, bool allowNoInterface = false)
        {
            if (Windows.SUCCEEDED(hr) || allowNoInterface && unchecked((uint)hr) == Interop.Windows.E_NOINTERFACE)
            {
                return;
            }

            throw new ComObjectException(exceptionMessage, Marshal.GetExceptionForHR(hr) ?? new Win32Exception(hr));
        }
    }

    /// <summary>
    ///     Base class for all COM interface proxies.
    /// </summary>
    public abstract unsafe class ComObject<T> : ComObject, IComObject<T>
        where T : unmanaged
    {
        private bool _isDisposed;

        /// <inheritdoc />
        public abstract T* Pointer { get; }

        /// <inheritdoc />
        public IUnknown* UnknownPointer => (IUnknown*)Pointer;

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(
                () =>
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                },
                ref _isDisposed);
        }

        /// <summary>
        ///     Casts the COM pointer to a pointer to <typeparamref name="T" />.
        /// </summary>
        public static implicit operator T*(ComObject<T> comObject)
        {
            return comObject.Pointer;
        }

        /// <summary>
        ///     Casts the COM pointer to a pointer to <see cref="IUnknown" />.
        /// </summary>
        public static implicit operator IUnknown*(ComObject<T> comObject)
        {
            return comObject.UnknownPointer;
        }

        /// <summary>
        ///     Disposes managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        ///     Destructor.
        /// </summary>
        ~ComObject()
        {
            if (!_isDisposed)
            {
                Dispose(false);
            }
        }
    }
}