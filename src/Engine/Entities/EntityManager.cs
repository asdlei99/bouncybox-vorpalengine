using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Threads;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Entities
{
    /// <summary>Manages all entities and their interactions with the engine.</summary>
    public class EntityManager<TGameState> : IEntityManager<TGameState>
        where TGameState : class
    {
        private readonly EntityCollection _entities = new EntityCollection();
        private readonly object _entitiesLockObject = new object();
        private readonly IGameExecutionStateManager _gameExecutionStateManager;
        private readonly GlobalMessageQueueHelper _globalMessageQueue;
        private readonly IInterfaces _interfaces;
        private readonly object _renderLockObject = new object();
        private bool _isDisposed;
        private bool _shouldPause;
        private bool _shouldResume;
        private bool _shouldSuspend;
        private bool _shouldUnpause;

        /// <summary>Initializes a new instance of the <see cref="EntityManager{TGameState}" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="GamePausedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameUnpausedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameSuspendedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameResumedMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        /// <param name="context">A nested context.</param>
        public EntityManager(IInterfaces interfaces, IGameExecutionStateManager gameExecutionStateManager, NestedContext context)
        {
            _globalMessageQueue =
                new GlobalMessageQueueHelper(interfaces.GlobalMessageQueue, context.Push(nameof(EntityManager<TGameState>)))
                    .WithThread(ProcessThread.Update)
                    .Subscribe<GamePausedMessage>(HandleGamePausedMessage)
                    .Subscribe<GameUnpausedMessage>(HandleGameUnpausedMessage)
                    .Subscribe<GameSuspendedMessage>(HandleGameSuspendedMessage)
                    .Subscribe<GameResumedMessage>(HandleGameResumedMessage);
            _interfaces = interfaces;
            _gameExecutionStateManager = gameExecutionStateManager;
        }

        /// <summary>Initializes a new instance of the <see cref="EntityManager{TGameState}" /> type.</summary>
        /// <remarks>
        ///     <para>Subscribes to the <see cref="GamePausedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameUnpausedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameSuspendedMessage" /> global message.</para>
        ///     <para>Subscribes to the <see cref="GameResumedMessage" /> global message.</para>
        /// </remarks>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="gameExecutionStateManager">An <see cref="IGameExecutionStateManager" /> implementation.</param>
        public EntityManager(IInterfaces interfaces, IGameExecutionStateManager gameExecutionStateManager)
            : this(interfaces, gameExecutionStateManager, NestedContext.None())
        {
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Add(IEnumerable<IEntity> entities)
        {
            entities = entities.ToImmutableArray();

            PrepareForAdd(entities);

            lock (_entitiesLockObject)
            {
                _entities.Add(entities);
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Add(params IEntity[] entities)
        {
            return Add((IEnumerable<IEntity>)entities);
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Remove(IEnumerable<IEntity> entities)
        {
            entities = entities.ToImmutableArray();

            lock (_entitiesLockObject)
            {
                _entities.Remove(entities);
            }

            foreach (IEntity entity in entities)
            {
                entity.Dispose();
            }

            return this;
        }

        /// <inheritdoc />
        public IEntityManager<TGameState> Remove(params IEntity[] entities)
        {
            return Remove((IEnumerable<IEntity>)entities);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Update" /> thread.
        /// </exception>
        public void Update(CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            // Allow entities to update the game state

            ImmutableArray<IUpdatingEntity> entities;

            lock (_entitiesLockObject)
            {
                entities = _entities.OrderedByUpdateOrder.ToImmutableArray();
            }

            foreach (IUpdatingEntity entity in entities)
            {
                // Inform entity if pause/suspend state was changed

                if (_shouldPause)
                {
                    entity.Pause();
                }
                else if (_shouldUnpause)
                {
                    entity.Unpause();
                }
                if (_shouldSuspend)
                {
                    entity.Suspend();
                }
                else if (_shouldResume)
                {
                    entity.Resume();
                }

                // Update game state
                entity.UpdateGameState(cancellationToken);
            }

            // Reset pause/suspend state

            _shouldPause = false;
            _shouldUnpause = false;
            _shouldSuspend = false;
            _shouldResume = false;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Render" /> thread.
        /// </exception>
        public void InitializeRenderResources(in DirectXResources resources)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            ImmutableArray<IRenderingEntity> entities;

            lock (_entitiesLockObject)
            {
                entities = _entities.OrderedByRenderOrder.ToImmutableArray();
            }

            foreach (IRenderingEntity entity in entities)
            {
                entity.InitializeRenderResources(resources);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Render" /> thread.
        /// </exception>
        public void ResizeRenderResources(in DirectXResources resources, D2D_SIZE_U clientSize)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            ImmutableArray<IRenderingEntity> entities;

            lock (_entitiesLockObject)
            {
                entities = _entities.OrderedByRenderOrder.ToImmutableArray();
            }

            foreach (IRenderingEntity entity in entities)
            {
                entity.ResizeRenderResources(resources, clientSize);
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Render" /> thread.
        /// </exception>
        public void ReleaseRenderResources()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            ImmutableArray<IRenderingEntity> entities;

            lock (_entitiesLockObject)
            {
                entities = _entities.OrderedByRenderOrder.ToImmutableArray();
            }

            foreach (IRenderingEntity entity in entities)
            {
                entity.ReleaseRenderResources();
            }
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the thread executing this method is not the
        ///     <see cref="ProcessThread.Render" /> thread.
        /// </exception>
        public int Render(in DirectXResources resources, CancellationToken cancellationToken)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Render);

            var entityRenderCount = 0;
            ImmutableArray<IRenderingEntity> entities;

            lock (_entitiesLockObject)
            {
                entities = _entities.OrderedByRenderOrder.ToImmutableArray();
            }

            lock (_renderLockObject)
            {
                // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
                foreach (IRenderingEntity entity in entities)
                {
                    entityRenderCount += entity.Render(resources, cancellationToken) == EntityRenderResult.Rendered ? 1 : 0;
                }
            }

            return entityRenderCount;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _interfaces.ThreadManager.DisposeHelper(() => _globalMessageQueue.Dispose(), ref _isDisposed, ProcessThread.Main);
        }

        /// <summary>Prepares entities for adding by informing them of game execution state changes.</summary>
        /// <param name="entities">The entities to prepare.</param>
        private void PrepareForAdd(IEnumerable<IEntity> entities)
        {
            GameExecutionState gameExecutionState = _gameExecutionStateManager.GameExecutionState;

            foreach (IEntity entity in entities)
            {
                if (gameExecutionState.IsPaused)
                {
                    entity.Pause();
                }
                if (gameExecutionState.IsSuspended)
                {
                    entity.Suspend();
                }
            }
        }

        /// <summary>Handles the <see cref="GamePausedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGamePausedMessage(GamePausedMessage message)
        {
            _shouldPause = true;
        }

        /// <summary>Handles the <see cref="GameUnpausedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGameUnpausedMessage(GameUnpausedMessage message)
        {
            _shouldUnpause = true;
        }

        /// <summary>Handles the <see cref="GameSuspendedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGameSuspendedMessage(GameSuspendedMessage message)
        {
            _shouldSuspend = true;
        }

        /// <summary>Handles the <see cref="GameResumedMessage" /> global message.</summary>
        /// <param name="message">The message being handled.</param>
        private void HandleGameResumedMessage(GameResumedMessage message)
        {
            _shouldResume = true;
        }
    }
}