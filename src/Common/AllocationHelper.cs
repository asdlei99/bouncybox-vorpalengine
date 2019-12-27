using System.Runtime.CompilerServices;

namespace BouncyBox.VorpalEngine.Common
{
    /// <summary>Helper methods related to allocating memory.</summary>
    public static class AllocationHelper
    {
        /// <summary>Determines whether a particular number of objects may be safely allocated with stackalloc.</summary>
        /// <param name="count">The number of objects being allocated.</param>
        /// <param name="limit">The maximum number of bytes allowed.</param>
        /// <returns>Returns true if the objects may be safely allocated with stackalloc; otherwise, returns false.</returns>
        public static bool CanStackAlloc<T>(uint count, uint limit = 1024)
        {
            return Unsafe.SizeOf<T>() * count <= limit;
        }
    }
}