using System;
using System.Collections;
using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>
    ///     <para>Represents a collection of entities.</para>
    ///     <para>
    ///         Entities added to the collection must have unique <see cref="IEntity.UpdateOrder" /> and
    ///         <see cref="IEntity.RenderOrder" /> values.
    ///     </para>
    /// </summary>
    public class EntityCollection<TEntity> : IEntityCollection<TEntity>
        where TEntity : IEntity
    {
        private readonly HashSet<TEntity> _entities = new HashSet<TEntity>();
        private readonly SortedSet<TEntity> _entitiesOrderedByRenderOrder = new SortedSet<TEntity>(new EntityRenderOrderComparer());
        private readonly SortedSet<TEntity> _entitiesOrderedByUpdateOrder = new SortedSet<TEntity>(new EntityUpdateOrderComparer());

        /// <inheritdoc />
        public int Count => _entities.Count;

        /// <inheritdoc />
        public IReadOnlyCollection<TEntity> OrderedByUpdateOrder => _entitiesOrderedByUpdateOrder;

        /// <inheritdoc />
        public IReadOnlyCollection<TEntity> OrderedByRenderOrder => _entitiesOrderedByRenderOrder;

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
        /// <exception cref="InvalidOperationException">Thrown when an entity with a duplicate <see cref="IEntity.UpdateOrder" /> is added.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an entity with a duplicate <see cref="IEntity.RenderOrder" /> is added.</exception>
        public void Add(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (!_entities.Add(entity))
                {
                    return;
                }

                if (!_entitiesOrderedByUpdateOrder.Add(entity))
                {
                    throw new InvalidOperationException($"A {typeof(TEntity).FullName} with update order {entity.UpdateOrder} already exists.");
                }
                if (!_entitiesOrderedByRenderOrder.Add(entity))
                {
                    throw new InvalidOperationException($"A {typeof(TEntity).FullName} with render order {entity.RenderOrder} already exists.");
                }
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when an entity with a duplicate <see cref="IEntity.UpdateOrder" /> is added.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an entity with a duplicate <see cref="IEntity.RenderOrder" /> is added.</exception>
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

                _entitiesOrderedByUpdateOrder.Remove(entity);
                _entitiesOrderedByRenderOrder.Remove(entity);
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
            _entities.Clear();
            _entitiesOrderedByUpdateOrder.Clear();
            _entitiesOrderedByRenderOrder.Clear();
        }

        /// <summary>A comparer that orders entities by <see cref="IEntity.UpdateOrder" />.</summary>
        private class EntityUpdateOrderComparer : IComparer<TEntity>
        {
            /// <inheritdoc />
            public int Compare(TEntity x, TEntity y)
            {
                return Comparer<uint?>.Default.Compare(x?.UpdateOrder, y?.UpdateOrder);
            }
        }

        /// <summary>A comparer that orders entities by <see cref="IEntity.RenderOrder" />.</summary>
        private class EntityRenderOrderComparer : IComparer<TEntity>
        {
            /// <inheritdoc />
            public int Compare(TEntity x, TEntity y)
            {
                return Comparer<uint?>.Default.Compare(x?.RenderOrder, y?.RenderOrder);
            }
        }
    }
}