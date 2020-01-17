using System;
using System.Collections.Generic;
using System.Threading;
using BouncyBox.VorpalEngine.Engine.DirectX;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Represents an object that manages entities.</summary>
    public interface IEntityManager<out TGameState> : IDisposable
        where TGameState : class
    {
        /// <summary>Adds entities to the collection.</summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState> Add(IEnumerable<IEntity> entities);

        /// <summary>Adds entities to the collection.</summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState> Add(params IEntity[] entities);

        /// <summary>Removes entities from the collection.</summary>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState> Remove(IEnumerable<IEntity> entities);

        /// <summary>Removes entities from the collection.</summary>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>Returns the entity manager.</returns>
        IEntityManager<TGameState> Remove(params IEntity[] entities);

        /// <summary>Updates the game state.</summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        void Update(CancellationToken cancellationToken);

        /// <summary>Initializes render resources.</summary>
        /// <param name="resources">DirectX resources.</param>
        void InitializeRenderResources(in DirectXResources resources);

        /// <summary>Resizes render resources to account for the new render window client size.</summary>
        /// <param name="resources">DirectX resources.</param>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeRenderResources(in DirectXResources resources, D2D_SIZE_U clientSize);

        /// <summary>Releases render resources.</summary>
        void ReleaseRenderResources();

        /// <summary>Renders a render state.</summary>
        /// <param name="resources">DirectX resources.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns the number of entities rendered.</returns>
        int Render(in DirectXResources resources, CancellationToken cancellationToken);
    }
}