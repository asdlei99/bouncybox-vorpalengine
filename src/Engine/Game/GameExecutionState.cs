namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>Game execution state.</summary>
    public readonly struct GameExecutionState
    {
        /// <summary>Initializes a new instance of the <see cref="GameExecutionState" /> type.</summary>
        /// <param name="isPaused">A value indicating if the game is paused.</param>
        /// <param name="isSuspended">A value indicating if the game is suspended.</param>
        public GameExecutionState(bool isPaused = false, bool isSuspended = false)
        {
            IsPaused = isPaused;
            IsSuspended = isSuspended;
        }

        /// <summary>Gets a value indicating if the game is paused.</summary>
        public bool IsPaused { get; }

        /// <summary>Gets a value indicating if the game is suspended.</summary>
        public bool IsSuspended { get; }
    }
}