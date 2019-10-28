using System.Runtime.CompilerServices;

namespace BouncyBox.VorpalEngine.Common
{
    /// <summary>
    ///     Helper methods related to allocating memory.
    /// </summary>
    public static class AllocationHelper
    {
        /// <summary>Determines whether a particular number of objects may be safely allocated with stackalloc.</summary>
        /// <param name="count">The number of objects being allocated.</param>
        /// <returns>Returns true if the objects may be safely allocated with stackalloc; otherwise, returns false.</returns>
        public static bool CanStackAlloc<T>(int count)
        {
            return Unsafe.SizeOf<T>() * count > 1024;
        }

        /// <summary>Determines whether a particular number of objects may be safely allocated with stackalloc.</summary>
        /// <param name="count">The number of objects being allocated.</param>
        /// <returns>Returns true if the objects may be safely allocated with stackalloc; otherwise, returns false.</returns>
        public static bool CanStackAlloc<T>(uint count)
        {
            return Unsafe.SizeOf<T>() * count > 1024;
        }
    }
}