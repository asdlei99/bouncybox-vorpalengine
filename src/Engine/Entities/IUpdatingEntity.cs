using System.Drawing;
using System.Threading;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Represents an entity that can update the game state.</summary>
    public interface IUpdatingEntity : IEntity
    {
        /// <summary>
        ///     Gets the entity's position in update order, which is determined by sorting all entities' update orders in ascending
        ///     order.
        /// </summary>
        uint UpdateOrder { get; }

        /// <summary>Initializes update resources.</summary>
        void InitializeUpdateResources();

        /// <summary>Resizes update resources to account for the new render window client size.</summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeUpdateResources(Size clientSize);

        /// <summary>Releases update resources created by this entity.</summary>
        void ReleaseUpdateResources();

        /// <summary>Updates the game state.</summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        void UpdateGameState(in CancellationToken cancellationToken);
    }
}