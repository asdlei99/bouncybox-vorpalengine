namespace BouncyBox.VorpalEngine.Engine.Entities.Updaters
{
    /// <summary>
    ///     Represents an entity that updates the game state.
    /// </summary>
    public interface IUpdater<in TGameState, in TRenderState> : IEntity
        where TGameState : class
        where TRenderState : class
    {
        /// <summary>
        ///     Updates the game state.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        void UpdateGameState(TGameState gameState);

        /// <summary>
        ///     Prepares a render state for rendering.
        /// </summary>
        /// <param name="gameState">The game state.</param>
        /// <param name="renderState">A render state.</param>
        void PrepareRenderState(TGameState gameState, TRenderState renderState);

        /// <summary>
        ///     Allows the entity to respond to the game execution state being paused.
        /// </summary>
        void Pause();

        /// <summary>
        ///     Allows the entity to respond to the game execution state being unpaused.
        /// </summary>
        void Unpause();

        /// <summary>
        ///     Allows the entity to respond to the game execution state being suspended.
        /// </summary>
        void Suspend();

        /// <summary>
        ///     Allows the entity to respond to the game execution state being resumed.
        /// </summary>
        void Resume();
    }
}