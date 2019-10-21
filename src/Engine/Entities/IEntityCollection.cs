using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Represents a collection of entities.</summary>
    public interface IEntityCollection : IReadOnlyCollection<IEntity>
    {
        /// <summary>
        ///     Gets a read-only collection of the entities in the collection ordered by <see cref="IUpdatingEntity.UpdateOrder" /> in
        ///     ascending order.
        /// </summary>
        IReadOnlyCollection<IUpdatingEntity> OrderedByUpdateOrder { get; }

        /// <summary>
        ///     Gets a read-only collection of the entities in the collection ordered by <see cref="IRenderingEntity.RenderOrder" /> in
        ///     ascending order.
        /// </summary>
        IReadOnlyCollection<IRenderingEntity> OrderedByRenderOrder { get; }

        /// <summary>Adds entities to the collection.</summary>
        /// <param name="entities">The entities to add.</param>
        void Add(IEnumerable<IEntity> entities);

        /// <summary>Adds entities to the collection.</summary>
        /// <param name="entities">The entities to add.</param>
        void Add(params IEntity[] entities);

        /// <summary>Removes entities from the collection.</summary>
        /// <param name="entities">The entities to remove.</param>
        void Remove(IEnumerable<IEntity> entities);

        /// <summary>Removes entities from the collection.</summary>
        /// <param name="entities">The entities to remove.</param>
        void Remove(params IEntity[] entities);

        /// <summary>Removes all entities from the collection.</summary>
        void Clear();
    }
}