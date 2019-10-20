namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     <para>Manages the game state.</para>
    ///     <para>The game state is shared by all entities in all scenes and represents the full state of the game world.</para>
    /// </summary>
    public class GameStateManager<TGameState> : IGameStateManager<TGameState>
        where TGameState : class, new()
    {
        /// <inheritdoc />
        public TGameState GameState { get; } = new TGameState();
    }
}