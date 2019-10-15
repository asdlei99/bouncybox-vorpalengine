using System;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Represents a COM object.
    /// </summary>
    public interface IComObject<T> : IDisposable
        where T : unmanaged
    {
        /// <summary>
        ///     Gets the COM object pointer.
        /// </summary>
        unsafe T* Pointer { get; }

        /// <summary>
        ///     Gets the COM object pointer as a pointer to <see cref="IUnknown" />.
        /// </summary>
        unsafe IUnknown* UnknownPointer { get; }
    }
}