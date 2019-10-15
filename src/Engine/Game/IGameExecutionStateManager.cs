using System;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Represents an object that manages game execution state changes.
    /// </summary>
    public interface IGameExecutionStateManager : IDisposable
    {
        /// <summary>
        ///     Gets the current game execution state.
        /// </summary>
        GameExecutionState GameExecutionState { get; }

        /// <summary>
        ///     Gets a value indicating if the game is paused.
        /// </summary>
        bool IsGamePaused { get; }

        /// <summary>
        ///     Gets a value indicating if the game is suspended.
        /// </summary>
        bool IsGameSuspended { get; }

        /// <summary>
        ///     Handles dispatched messages.
        /// </summary>
        void HandleDispatchedMessages();
    }
}