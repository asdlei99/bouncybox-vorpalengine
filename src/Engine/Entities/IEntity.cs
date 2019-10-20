using System;
using System.Drawing;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>An entity.</summary>
    public interface IEntity : IDisposable
    {
        /// <summary>
        ///     Gets the entity's position in update order, which is determined by sorting all entities' update orders in ascending
        ///     order.
        /// </summary>
        uint UpdateOrder { get; }

        /// <summary>
        ///     Gets the entity's position in render order, which is determined by sorting all entities' render orders in ascending
        ///     order.
        /// </summary>
        uint RenderOrder { get; }

        /// <summary>Allows the entity to respond to the game execution state being paused.</summary>
        void Pause();

        /// <summary>Allows the entity to respond to the game execution state being unpaused.</summary>
        void Unpause();

        /// <summary>Allows the entity to respond to the game execution state being suspended.</summary>
        void Suspend();

        /// <summary>Allows the entity to respond to the game execution state being resumed.</summary>
        void Resume();

        /// <summary>Initializes update resources.</summary>
        void InitializeUpdateResources();

        /// <summary>Resizes update resources to account for the new render window client size.</summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeUpdateResources(Size clientSize);

        /// <summary>Releases update resources created by this entity.</summary>
        void ReleaseUpdateResources();

        /// <summary>Updates the game state.</summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A render request if the entity is requesting a render.</returns>
        RenderRequest? UpdateGameState(CancellationToken cancellationToken = default);

        /// <summary>Initializes render resources.</summary>
        /// <param name="resources">DirectX resources.</param>
        void InitializeRenderResources(DirectXResources resources);

        /// <summary>Resizes render resources to account for the new render window client size.</summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeRenderResources(D2D_SIZE_U clientSize);

        /// <summary>Releases render resources created by this entity.</summary>
        void ReleaseRenderResources();
    }
}