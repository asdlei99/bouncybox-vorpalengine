using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BouncyBox.Common.NetStandard21;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Entities.Renderers;
using BouncyBox.VorpalEngine.Engine.Entities.Updaters;
using BouncyBox.VorpalEngine.Engine.Threads;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>A collection of updaters and renderers that form one logical game unit.</summary>
    public abstract class Scene<TGameState, TRenderState, TSceneKey> : IScene<TSceneKey>
        where TGameState : class
        where TRenderState : class, new()
        where TSceneKey : struct, Enum
    {
        private readonly IEntityManager<TGameState, TRenderState> _entityManager;
        private readonly IInterfaces _interfaces;
        private readonly HashSet<IRenderer<TRenderState>> _renderers = new HashSet<IRenderer<TRenderState>>();
        private readonly HashSet<IUpdater<TRenderState>> _updaters = new HashSet<IUpdater<TRenderState>>();

        /// <summary>Initializes a new instance of the <see cref="Scene{TGameState,TRenderState,TSceneKey}" /> type.</summary>
        /// <param name="interfaces">An <see cref="IInterfaces" /> implementation.</param>
        /// <param name="entityManager">An <see cref="IEntityManager{TGameState,TRenderState}" /> implementation.</param>
        /// <param name="key">The scene's key.</param>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the render thread.</exception>
        protected Scene(IInterfaces interfaces, IEntityManager<TGameState, TRenderState> entityManager, TSceneKey key)
        {
            _interfaces = interfaces;
            _entityManager = entityManager;
            Key = key;
        }

        /// <inheritdoc />
        public TSceneKey Key { get; }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Load()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnLoad();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        public void Unload()
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            OnUnload();
            ClearEntities();
        }

        /// <inheritdoc cref="IScene{TSceneKey}.Load" />
        protected virtual void OnLoad()
        {
        }

        /// <inheritdoc cref="IScene{TSceneKey}.Unload" />
        protected virtual void OnUnload()
        {
        }

        /// <summary>Adds updaters to the scene.</summary>
        /// <param name="updaters">The updaters to add.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> AddUpdaters(IEnumerable<IUpdater<TRenderState>> updaters)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            updaters = updaters.ToImmutableArray();

            _updaters.AddRange(updaters);
            _entityManager.Add(updaters);

            return this;
        }

        /// <summary>Adds updaters to the scene.</summary>
        /// <param name="updaters">The updaters to add.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> AddUpdaters(params IUpdater<TRenderState>[] updaters)
        {
            return AddUpdaters((IEnumerable<IUpdater<TRenderState>>)updaters);
        }

        /// <summary>Adds renderers to the scene.</summary>
        /// <param name="renderers">The renderers to add.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> AddRenderers(IEnumerable<IRenderer<TRenderState>> renderers)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            renderers = renderers.ToImmutableArray();

            _renderers.AddRange(renderers);
            _entityManager.Add(renderers);

            return this;
        }

        /// <summary>Adds renderers to the scene.</summary>
        /// <param name="renderers">The renderers to add.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> AddRenderers(params IRenderer<TRenderState>[] renderers)
        {
            return AddRenderers((IEnumerable<IRenderer<TRenderState>>)renderers);
        }

        /// <summary>Removes updaters from the scene.</summary>
        /// <param name="updaters">The updaters to remove.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> RemoveUpdaters(IEnumerable<IUpdater<TRenderState>> updaters)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            updaters = updaters.ToImmutableArray();

            _updaters.RemoveRange(updaters);
            _entityManager.Remove(updaters);

            return this;
        }

        /// <summary>Removes updaters from the scene.</summary>
        /// <param name="updaters">The updaters to remove.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> RemoveUpdaters(params IUpdater<TRenderState>[] updaters)
        {
            return RemoveUpdaters((IEnumerable<IUpdater<TRenderState>>)updaters);
        }

        /// <summary>Removes renderers from the scene.</summary>
        /// <param name="renderers">The renderers to remove.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> RemoveRenderers(IEnumerable<IRenderer<TRenderState>> renderers)
        {
            _interfaces.ThreadManager.VerifyProcessThread(ProcessThread.Update);

            renderers = renderers.ToImmutableArray();

            _renderers.RemoveRange(renderers);
            _entityManager.Remove(renderers);

            return this;
        }

        /// <summary>Removes renderers from the scene.</summary>
        /// <param name="renderers">The renderers to remove.</param>
        /// <returns>Returns the scene.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the thread executing this method is not the update thread.</exception>
        protected IScene<TSceneKey> RemoveRenderers(params IRenderer<TRenderState>[] renderers)
        {
            return RemoveRenderers((IEnumerable<IRenderer<TRenderState>>)renderers);
        }

        /// <summary>Removes all updaters from the scene.</summary>
        protected void ClearUpdaters()
        {
            RemoveUpdaters(_updaters);
        }

        /// <summary>Removes all renderers from the scene.</summary>
        protected void ClearRenderers()
        {
            RemoveRenderers(_renderers);
        }

        /// <summary>Removes all updaters and renderers from the scene.</summary>
        protected void ClearEntities()
        {
            RemoveUpdaters(_updaters);
            RemoveRenderers(_renderers);
        }
    }
}