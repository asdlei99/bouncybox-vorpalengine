using System.Collections.Immutable;

namespace BouncyBox.VorpalEngine.Engine.Resources
{
    /// <summary>
    ///     Represents a source for binary resources.
    /// </summary>
    public interface IResourceSource
    {
        /// <summary>
        ///     Gets a resource for the given descriptor.
        /// </summary>
        /// <param name="descriptor">A resource descriptor.</param>
        /// <returns>Returns a tuple containing a <see cref="GetResourceResult" /> and the resource data.</returns>
        (GetResourceResult result, ImmutableArray<byte>? data) GetResource(ResourceDescriptor descriptor);
    }
}