using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Game;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities.Renderers
{
    /// <summary>
    ///     Represents an entity that renders a render state.
    /// </summary>
    public interface IRenderer<in TRenderState> : IEntity
        where TRenderState : class
    {
        /// <summary>
        ///     Initializes DirectX resources.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to initialize other resources.</param>
        void InitializeResources(DirectXResources resources);

        /// <summary>
        ///     Resizes DirectX resources to account for the new render window client size.
        /// </summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeResources(D2D_SIZE_U clientSize);

        /// <summary>
        ///     Releases DirectX resources created by this renderer.
        /// </summary>
        void ReleaseResources();

        /// <summary>
        ///     Renders a render state.
        /// </summary>
        /// <param name="renderState">The render state to render.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        /// <returns>Returns the result of the render.</returns>
        void Render(TRenderState renderState, IEngineStats engineStats);

        /// <summary>
        ///     Allows the entity to respond to the game execution state being paused.
        /// </summary>
        void Pause();

        /// <summary>
        ///     Allows the entity to respond to the game execution state being unpaused.
        /// </summary>
        void Unpause();

        /// <summary>
        ///     Allows the entity to respond to the game execution state being suspended.
        /// </summary>
        void Suspend();

        /// <summary>
        ///     Allows the entity to respond to the game execution state being resumed.
        /// </summary>
        void Resume();
    }
}