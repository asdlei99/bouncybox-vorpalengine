#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop
{
    /// <summary>Wraps a pointer in a struct for compatibility with generics.</summary>
    public unsafe struct Pointer<T>
        where T : unmanaged
    {
        /// <summary>The pointer.</summary>
        public T* Ptr;

        public static implicit operator Pointer<T>(T* value)
        {
            return
                new Pointer<T>
                {
                    Ptr = value
                };
        }
    }
}