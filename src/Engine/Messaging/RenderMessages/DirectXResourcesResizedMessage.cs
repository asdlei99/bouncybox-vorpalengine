using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Messaging.RenderMessages
{
    /// <summary>
    ///     A global message indicating that core DirectX resources were resized.
    /// </summary>
    public struct DirectXResourcesResizedMessage : IRenderMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectXResourcesResizedMessage" /> type.
        /// </summary>
        /// <param name="clientSize">The new size of the render window's client area.</param>
        public DirectXResourcesResizedMessage(D2D_SIZE_U clientSize)
        {
            ClientSize = clientSize;
        }

        /// <summary>
        ///     Gets the new size of the render window's client area.
        /// </summary>
        public D2D_SIZE_U ClientSize { get; }
    }
}