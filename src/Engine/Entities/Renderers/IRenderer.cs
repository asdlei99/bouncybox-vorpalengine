using BouncyBox.VorpalEngine.Engine.DirectX;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities.Renderers
{
    /// <summary>
    ///     Represents a renderer's capability to use resources.
    /// </summary>
    public interface IRenderer : IEntity
    {
        /// <summary>
        ///     Initializes resources.
        /// </summary>
        /// <param name="resources">DirectX resources.</param>
        void InitializeResources(DirectXResources resources);

        /// <summary>
        ///     Resizes resources to account for the new render window client size.
        /// </summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeResources(D2D_SIZE_U clientSize);

        /// <summary>
        ///     Releases resources created by this renderer.
        /// </summary>
        void ReleaseResources();
    }

    /// <summary>
    ///     Represents an entity that renders a render state.
    /// </summary>
    public interface IRenderer<in TRenderState> : IRenderer
        where TRenderState : class
    {
        /// <summary>
        ///     Renders a render state.
        /// </summary>
        /// <param name="renderState">The render state to render.</param>
        void Render(TRenderState renderState);
    }
}