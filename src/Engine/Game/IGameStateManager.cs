namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     <para>Represents an object that manages the game state.</para>
    ///     <para>The game state is shared by all updaters in all scenes and represents the full state of the game world.</para>
    /// </summary>
    public interface IGameStateManager<out TGameState>
        where TGameState : class
    {
        /// <summary>Gets the game state.</summary>
        TGameState GameState { get; }
    }
}