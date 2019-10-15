using System.Runtime.CompilerServices;

namespace BouncyBox.VorpalEngine.Engine
{
    /// <summary>
    ///     Methods that extend the <see cref="Unsafe" /> type's functionality.
    /// </summary>
    public static class UnsafeExtensions
    {
        /// <summary>
        ///     Writes to a readonly value.
        /// </summary>
        /// <param name="readonlyReference">A readonly reference.</param>
        /// <param name="value">The value to write.</param>
        public static void WriteReadonly<T>(in T readonlyReference, T value)
        {
            Unsafe.AsRef(in readonlyReference) = value;
        }
    }
}