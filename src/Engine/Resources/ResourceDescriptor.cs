namespace BouncyBox.VorpalEngine.Engine.Resources
{
    /// <summary>Uniquely describes a resource.</summary>
    public struct ResourceDescriptor
    {
        /// <summary>Gets the type of the resource.</summary>
        public ResourceType Type { get; }

        /// <summary>Gets the key of the resource.</summary>
        public string Key { get; }

        /// <summary>Initializes a new instance of the <see cref="ResourceDescriptor" /> type.</summary>
        /// <param name="type">The type of the resource.</param>
        /// <param name="key">The key of the resource.</param>
        public ResourceDescriptor(ResourceType type, string key)
        {
            Type = type;
            Key = key;
        }
    }
}