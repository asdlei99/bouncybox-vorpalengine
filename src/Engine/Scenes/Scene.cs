using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>A collection of entities that form one logical game unit.</summary>
    public abstract class Scene<TGameState, TSceneKey> : IScene<TSceneKey>
        where TGameState : class
        where TSceneKey : struct, Enum
    {
        private readonly HashSet<IEntity> _entities = new HashSet<IEntity>();
        private readonly IEntityManager<TGameState> _entityManager;
        private readonly ConcurrentMessagePublisherSubscriber<IGlobalMessage> _globalMessagePublisherSubscriber;
        private readonly IInterfaces _interfaces;
        private bool _isDisposed;

        /// <summary>Initializes a new instance of the <see cref="Scene{TGameState,TSceneKey}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        /// <param name="key">The scene's key.</param>
        /// <param name="context">A nested context.</param>
        protected Scene(IInterfaces interfaces, IEntityManager<TGameState> entityManager, TSceneKey key, NestedContext context)
        {
            context = context.Push(nameof(Scene<TGameState, TSceneKey>));

            _interfaces = interfaces;
            _entityManager = entityManager;
            Key = key;

            _globalMessagePublisherSubscriber = ConcurrentMessagePublisherSubscriber<IGlobalMessage>.Create(interfaces, context);
        }

        /// <summary>Initializes a new instance of the <see cref="Scene{TGameState,TSceneKey}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState}" /> implementation.</param>
        /// <param name="key">The scene's key.</param>
        protected Scene(IInterfaces interfaces, IEntityManager<TGameState> entityManager, TSceneKey key) : this(
            interfaces,
            entityManager,
            key,
            NestedContext.None())
        {
        }

        /// <inheritdoc />
        public TSceneKey Key { get; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Load()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnLoad();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Unload()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            ClearEntities();
            OnUnload();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _interfaces.ThreadManager.DisposeHelper(() => { _globalMessagePublisherSubscriber?.Dispose(); }, ref _isDisposed, ProcessThread.Main);
        }

        /// <inheritdoc cref="IScene{TSceneKey}.Load" />
        protected virtual void OnLoad()
        {
        }

        /// <inheritdoc cref="IScene{TSceneKey}.Unload" />
        protected virtual void OnUnload()
        {
        }

        /// <summary>Adds entities to the scene.</summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        protected IScene<TSceneKey> AddEntities(IEnumerable<IEntity> entities)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            entities = entities.ToImmutableArray();

            _entities.UnionWith(entities);
            _entityManager.Add(entities);

            return this;
        }

        /// <summary>Adds entities to the scene.</summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        protected IScene<TSceneKey> AddEntities(params IEntity[] entities)
        {
            return AddEntities((IEnumerable<IEntity>)entities);
        }

        /// <summary>Removes entities from the scene.</summary>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        protected IScene<TSceneKey> RemoveEntities(IEnumerable<IEntity> entities)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            ImmutableArray<IEntity> entitiesToRemove = entities.ToImmutableArray();

            _entities.ExceptWith(entitiesToRemove);
            _entityManager.Remove(entitiesToRemove);

            return this;
        }

        /// <summary>Removes entities from the scene.</summary>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        protected IScene<TSceneKey> RemoveEntities(params IEntity[] entities)
        {
            return RemoveEntities((IEnumerable<IEntity>)entities);
        }

        /// <summary>Removes all entities from the scene.</summary>
        protected void ClearEntities()
        {
            RemoveEntities(_entities);
        }
    }
}