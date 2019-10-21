using System;
using System.Collections;
using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>
    ///     <para>Represents a collection of entities.</para>
    ///     <para>
    ///         Entities added to the collection must have unique <see cref="IUpdatingEntity.UpdateOrder" /> and see
    ///         cref="IRenderingEntity.RenderOrder" /> values.
    ///     </para>
    /// </summary>
    public class EntityCollection : IEntityCollection
    {
        private readonly HashSet<IEntity> _entities = new HashSet<IEntity>();
        private readonly SortedSet<IRenderingEntity> _entitiesOrderedByRenderOrder = new SortedSet<IRenderingEntity>(new EntityRenderOrderComparer());
        private readonly SortedSet<IUpdatingEntity> _entitiesOrderedByUpdateOrder = new SortedSet<IUpdatingEntity>(new EntityUpdateOrderComparer());

        /// <inheritdoc />
        public int Count => _entities.Count;

        /// <inheritdoc />
        public IReadOnlyCollection<IUpdatingEntity> OrderedByUpdateOrder => _entitiesOrderedByUpdateOrder;

        /// <inheritdoc />
        public IReadOnlyCollection<IRenderingEntity> OrderedByRenderOrder => _entitiesOrderedByRenderOrder;

        /// <inheritdoc />
        public IEnumerator<IEntity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when an entity with a duplicate <see cref="IUpdatingEntity.UpdateOrder" /> is
        ///     added.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when an entity with a duplicate <see cref="IRenderingEntity.RenderOrder" /> is
        ///     added.
        /// </exception>
        public void Add(IEnumerable<IEntity> entities)
        {
            foreach (IEntity entity in entities)
            {
                if (!_entities.Add(entity))
                {
                    return;
                }

                switch (entity)
                {
                    case IUpdatingEntity updatingEntity when !_entitiesOrderedByUpdateOrder.Add(updatingEntity):
                        throw new InvalidOperationException($"An entity with update order {updatingEntity.UpdateOrder} already exists.");
                    case IRenderingEntity renderingEntity when !_entitiesOrderedByRenderOrder.Add(renderingEntity):
                        throw new InvalidOperationException($"An entity with render order {renderingEntity.RenderOrder} already exists.");
                }
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when an entity with a duplicate <see cref="IUpdatingEntity.UpdateOrder" /> is
        ///     added.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when an entity with a duplicate <see cref="IRenderingEntity.RenderOrder" /> is
        ///     added.
        /// </exception>
        public void Add(params IEntity[] entities)
        {
            Add((IEnumerable<IEntity>)entities);
        }

        /// <inheritdoc />
        public void Remove(IEnumerable<IEntity> entities)
        {
            foreach (IEntity entity in entities)
            {
                if (!_entities.Remove(entity))
                {
                    return;
                }

                switch (entity)
                {
                    case IUpdatingEntity updatingEntity:
                        _entitiesOrderedByUpdateOrder.Remove(updatingEntity);
                        break;
                    case IRenderingEntity renderingEntity:
                        _entitiesOrderedByRenderOrder.Remove(renderingEntity);
                        break;
                }
            }
        }

        /// <inheritdoc />
        public void Remove(params IEntity[] entities)
        {
            Remove((IEnumerable<IEntity>)entities);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _entities.Clear();
            _entitiesOrderedByUpdateOrder.Clear();
            _entitiesOrderedByRenderOrder.Clear();
        }

        /// <summary>A comparer that orders entities by <see cref="IUpdatingEntity.UpdateOrder" />.</summary>
        private class EntityUpdateOrderComparer : IComparer<IUpdatingEntity>
        {
            /// <inheritdoc />
            public int Compare(IUpdatingEntity x, IUpdatingEntity y)
            {
                return Comparer<uint?>.Default.Compare(x?.UpdateOrder, y?.UpdateOrder);
            }
        }

        /// <summary>A comparer that orders entities by <see cref="IRenderingEntity.RenderOrder" />.</summary>
        private class EntityRenderOrderComparer : IComparer<IRenderingEntity>
        {
            /// <inheritdoc />
            public int Compare(IRenderingEntity x, IRenderingEntity y)
            {
                return Comparer<uint?>.Default.Compare(x?.RenderOrder, y?.RenderOrder);
            }
        }
    }
}