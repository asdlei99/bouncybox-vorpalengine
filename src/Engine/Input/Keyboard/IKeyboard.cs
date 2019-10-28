using BouncyBox.VorpalEngine.Interop;

namespace BouncyBox.VorpalEngine.Engine.Input.Keyboard
{
    /// <summary>Represents the keyboard.</summary>
    public interface IKeyboard
    {
        /// <summary>Enqueues a state change for later processing.</summary>
        /// <param name="state">The key state.</param>
        /// <param name="key">A virtual key.</param>
        void EnqueueStateChange(KeyState state, User32.VirtualKey key);

        /// <summary>Resets the state.</summary>
        void Reset();

        /// <summary>Processes enqueued state changes and snapshots the state.</summary>
        /// <returns>Returns a keyboard snapshot.</returns>
        KeyboardSnapshot ProcessQueueAndSnapshot();
    }
}