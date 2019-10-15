using BouncyBox.VorpalEngine.Engine.DirectX;

namespace BouncyBox.VorpalEngine.Engine.Messaging.RenderMessages
{
    /// <summary>
    ///     A global message indicating that core DirectX resources were initialized.
    /// </summary>
    public struct DirectXResourcesInitializedMessage : IRenderMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectXResourcesInitializedMessage" /> type.
        /// </summary>
        /// <param name="resources">The core DirectX resources.</param>
        public DirectXResourcesInitializedMessage(DirectXResources resources)
        {
            Resources = resources;
        }

        /// <summary>
        ///     Gets the core DirectX resources.
        /// </summary>
        public DirectXResources Resources { get; }
    }
}