using System;
using System.Collections;
using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>
    ///     <para>Represents a collection of entities.</para>
    ///     <para>Entities added to the collection must have unique <see cref="IEntity.Order" /> values.</para>
    /// </summary>
    public class EntityCollection<TEntity> : IEntityCollection<TEntity>
        where TEntity : IEntity
    {
        private readonly HashSet<TEntity> _entities = new HashSet<TEntity>();
        private readonly SortedSet<TEntity> _sortedEntities = new SortedSet<TEntity>(new EntityComparer());
        private bool _isDisposed;

        /// <inheritdoc />
        public int Count => _entities.Count;

        /// <inheritdoc />
        public IReadOnlyCollection<TEntity> SortedByOrder => _sortedEntities;

        /// <inheritdoc />
        public IEnumerator<TEntity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when an entity with a duplicate <see cref="IEntity.Order" /> is added.</exception>
        public void Add(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (!_entities.Add(entity))
                {
                    return;
                }

                if (!_sortedEntities.Add(entity))
                {
                    throw new InvalidOperationException($"A {typeof(TEntity).FullName} with order {entity.Order} already exists.");
                }
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when an entity with a duplicate <see cref="IEntity.Order" /> is added.</exception>
        public void Add(params TEntity[] entities)
        {
            Add((IEnumerable<TEntity>)entities);
        }

        /// <inheritdoc />
        public void Remove(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (!_entities.Remove(entity))
                {
                    return;
                }

                entity.Dispose();

                _sortedEntities.Remove(entity);
            }
        }

        /// <inheritdoc />
        public void Remove(params TEntity[] entities)
        {
            Remove((IEnumerable<TEntity>)entities);
        }

        /// <inheritdoc />
        public void Clear()
        {
            DisposeEntities();
            _entities.Clear();
            _sortedEntities.Clear();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeHelper.Dispose(DisposeEntities, ref _isDisposed);
        }

        /// <summary>
        ///     Disposes all entities in the collection.
        /// </summary>
        private void DisposeEntities()
        {
            foreach (TEntity entity in _entities)
            {
                entity.Dispose();
            }
        }

        /// <summary>
        ///     A comparer that orders entities by <see cref="IEntity.Order" />.
        /// </summary>
        private class EntityComparer : IComparer<TEntity>
        {
            /// <inheritdoc />
            public int Compare(TEntity x, TEntity y)
            {
                return Comparer<uint?>.Default.Compare(x?.Order, y?.Order);
            }
        }
    }
}