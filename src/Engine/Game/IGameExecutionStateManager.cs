using System;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>Represents an object that manages game execution state changes.</summary>
    public interface IGameExecutionStateManager : IDisposable
    {
        /// <summary>Gets the current game execution state.</summary>
        GameExecutionState GameExecutionState { get; }

        /// <summary>Handles dispatched messages.</summary>
        void HandleDispatchedMessages();
    }
}