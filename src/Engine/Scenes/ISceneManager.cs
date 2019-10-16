using System;
using BouncyBox.VorpalEngine.Engine.Game;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>
    ///     Represents the management of a collection of scenes.
    /// </summary>
    public interface ISceneManager<in TRenderState> : IDisposable
        where TRenderState : class, new()
    {
        /// <summary>
        ///     Instructs every loaded scene to update the game state and prepare a render state. Order is determined by each scene's
        ///     <see cref="IScene{TGameState,TRenderState,TSceneKey}.UpdateOrder" /> value, sorted in ascending order.
        /// </summary>
        void Update();

        /// <summary>
        ///     Instructs every loaded scene to render a render state. Order is determined by each scene's
        ///     <see cref="IScene{TGameState,TRenderState,TSceneKey}.UpdateOrder" /> value, sorted in ascending order.
        /// </summary>
        /// <param name="renderState">The render state to render.</param>
        /// <param name="engineStats">An <see cref="IEngineStats"/> implementation.</param>
        bool Render(TRenderState renderState, IEngineStats engineStats);

        /// <summary>
        ///     Handles dispatched messages.
        /// </summary>
        void HandleDispatchedMessages();
    }
}