using System;
using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Represents a collection of entities.</summary>
    public interface IEntityCollection<TEntity> : IReadOnlyCollection<TEntity>, IDisposable
        where TEntity : IEntity
    {
        /// <summary>Gets a read-only collection of the entities in the collection, sorted by <see cref="IEntity.Order" />.</summary>
        IReadOnlyCollection<TEntity> SortedByOrder { get; }

        /// <summary>Adds entities to the collection.</summary>
        /// <param name="entities">The entities to add.</param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>Adds entities to the collection.</summary>
        /// <param name="entities">The entities to add.</param>
        void Add(params TEntity[] entities);

        /// <summary>Removes entities from the collection.</summary>
        /// <param name="entities">The entities to remove.</param>
        void Remove(IEnumerable<TEntity> entities);

        /// <summary>Removes entities from the collection.</summary>
        /// <param name="entities">The entities to remove.</param>
        void Remove(params TEntity[] entities);

        /// <summary>Removes all entities from the collection.</summary>
        void Clear();
    }
}