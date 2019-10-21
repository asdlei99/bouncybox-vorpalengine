using System;
using System.Collections.Generic;
using System.Threading;

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
        void Update(in CancellationToken cancellationToken);

        /// <summary>Initialized render resources.</summary>
        void ReleaseRenderResources(in CancellationToken cancellationToken);

        /// <summary>Renders a render state.</summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>Returns a tuple containing the result of the render attempt and a frametime if a frame was rendered.</returns>
        (RenderResult result, TimeSpan frametime) Render(in CancellationToken cancellationToken);

        /// <summary>Handles dispatched update messages.</summary>
        void HandleDispatchedUpdateMessages();

        /// <summary>Handles dispatched render resources messages.</summary>
        void HandleDispatchedRenderResourcesMessages();
    }
}