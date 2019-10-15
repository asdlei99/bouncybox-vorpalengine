using System;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>
    ///     Represents a collection of updaters and renderers that form one logical game unit.
    /// </summary>
    public interface IScene<in TGameState, in TRenderState, out TSceneKey> : IDisposable
        where TGameState : class
        where TRenderState : class
    {
        /// <summary>
        ///     Gets the scene key.
        /// </summary>
        TSceneKey Key { get; }

        /// <summary>
        ///     Gets a value that determines the order in which to update scenes when compared to other scenes.
        /// </summary>
        uint UpdateOrder { get; }

        /// <summary>
        ///     Gets a value that determines the order in which to render scenes when compared to other scenes.
        /// </summary>
        uint RenderOrder { get; }

        /// <summary>
        ///     Gets a value that indicates whether the scene is ready to render. Scenes are ready to render when the scene is loaded and
        ///     their DirectX resources have been initialized.
        /// </summary>
        bool IsReadyForRender { get; }

        /// <summary>
        ///     Initializes the parts of the game state owned by the scene.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        void Load(TGameState gameState);

        /// <summary>
        ///     Uninitializes the parts of the game state owned by the scene.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        void Unload(TGameState gameState);

        /// <summary>
        ///     Instructs every updater in the scene to update the game state and prepare a render state. Order is determined by each
        ///     updater's <see cref="IEntity.Order" /> value, sorted in ascending order.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        /// <param name="gameExecutionState">The current game execution state.</param>
        void UpdateGameState(TGameState gameState, GameExecutionState gameExecutionState);

        /// <summary>
        ///     Instructs every renderer in the scene to render a render state. Order is determined by each renderer's
        ///     <see cref="IEntity.Order" /> value, sorted in ascending order.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        /// <param name="renderState">A render state.</param>
        void PrepareRenderState(TGameState gameState, TRenderState renderState);

        /// <summary>
        ///     Instructs every renderer in the scene to initializes DirectX resources. Order is determined by each renderer's
        ///     <see cref="IEntity.Order" /> value, sorted in ascending order.
        /// </summary>
        /// <param name="resources">Core DirectX resources that can be used to initialize other resources.</param>
        void InitializeResources(DirectXResources resources);

        /// <summary>
        ///     Instructs every renderer in the scene to resize its DirectX resources to account for the new render window client size. Order
        ///     is determined by each renderer's <see cref="IEntity.Order" /> value, sorted in ascending order.
        /// </summary>
        /// <param name="clientSize">The size of the render window's client area.</param>
        void ResizeResources(D2D_SIZE_U clientSize);

        /// <summary>
        ///     Instructs every renderer in the scene to release its DirectX resources. Order is determined by each renderer's
        ///     <see cref="IEntity.Order" /> value, sorted in ascending order.
        /// </summary>
        void ReleaseResources();

        /// <summary>
        ///     Instructs every renderer in the scene to render a render state. Order is determined by each renderer's
        ///     <see cref="IEntity.Order" /> value, sorted in ascending order.
        /// </summary>
        /// <param name="renderState">The render state to render.</param>
        /// <param name="gameExecutionState">The current game execution state.</param>
        /// <param name="engineStats">An <see cref="IEngineStats" /> implementation.</param>
        void Render(TRenderState renderState, GameExecutionState gameExecutionState, IEngineStats engineStats);
    }
}