using System;
using System.Collections.Generic;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Entities.Renderers;
using BouncyBox.VorpalEngine.Engine.Entities.Updaters;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>
    ///     Represents an object that manages entities.
    /// </summary>
    public interface IEntityManager<out TGameState, out TRenderState> : IDisposable
        where TGameState : class
        where TRenderState : class
    {
        /// <summary>
        ///     Adds updaters to the collection.
        /// </summary>
        /// <param name="updaters">The updaters to add.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Add(IEnumerable<IUpdater<TRenderState>> updaters);

        /// <summary>
        ///     Adds updaters to the collection.
        /// </summary>
        /// <param name="updaters">The updaters to add.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Add(params IUpdater<TRenderState>[] updaters);

        /// <summary>
        ///     Adds renderers to the collection.
        /// </summary>
        /// <param name="renderers">The renderers to add.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Add(IEnumerable<IRenderer<TRenderState>> renderers);

        /// <summary>
        ///     Adds renderers to the collection.
        /// </summary>
        /// <param name="renderers">The renderers to add.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Add(params IRenderer<TRenderState>[] renderers);

        /// <summary>
        ///     Removes updaters from the collection.
        /// </summary>
        /// <param name="updaters">The updaters to remove.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Remove(IEnumerable<IUpdater<TRenderState>> updaters);

        /// <summary>
        ///     Removes updaters from the collection.
        /// </summary>
        /// <param name="updaters">The updaters to remove.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Remove(params IUpdater<TRenderState>[] updaters);

        /// <summary>
        ///     Removes renderers from the collection.
        /// </summary>
        /// <param name="renderers">The renderers to remove.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Remove(IEnumerable<IRenderer<TRenderState>> renderers);

        /// <summary>
        ///     Removes renderers from the collection.
        /// </summary>
        /// <param name="renderers">The renderers to remove.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState, TRenderState> Remove(params IRenderer<TRenderState>[] renderers);

        /// <summary>
        ///     Updates the game state.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        void Update(CancellationToken cancellationToken);

        /// <summary>
        ///     Initializes renderer resources.
        /// </summary>
        /// <param name="resources">DirectX resources.</param>
        void InitializeRendererResources(DirectXResources resources);

        /// <summary>
        ///     Resizes renderer resources to account for the new render window client size.
        /// </summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeRendererResources(D2D_SIZE_U clientSize);

        /// <summary>
        ///     Releases renderer resources.
        /// </summary>
        void ReleaseRendererResources();

        /// <summary>
        ///     Renders a render state.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns a tuple containing the result of the rendering attempt and a frametime if a frame was rendered.</returns>
        (RenderResult result, TimeSpan frametime) Render(CancellationToken cancellationToken);
    }
}