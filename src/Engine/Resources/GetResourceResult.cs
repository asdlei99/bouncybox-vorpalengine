namespace BouncyBox.VorpalEngine.Engine.Resources
{
    /// <summary>
    ///     The result of a call to <see cref="IResourceSource.GetResource" />.
    /// </summary>
    public enum GetResourceResult
    {
        /// <summary>
        ///     A valid resource was returned.
        /// </summary>
        Valid,

        /// <summary>
        ///     A resource with the given descriptor was not found.
        /// </summary>
        ResourceNotFound
    }
}