using System.Collections.Generic;

namespace BouncyBox.VorpalEngine.Engine.Input.XInput
{
    /// <summary>
    ///     Represents a stateful XInput gamepad.
    /// </summary>
    public interface IStatefulGamepad
    {
        /// <summary>
        ///     Gets the keys that are down.
        /// </summary>
        IReadOnlyCollection<XInputVirtualKey> DownKeys { get; }

        /// <summary>
        ///     Gets the up keys that are up.
        /// </summary>
        IReadOnlyCollection<XInputVirtualKey> UpKeys { get; }

        /// <summary>
        ///     Gets the keys that are repeating.
        /// </summary>
        IReadOnlyCollection<XInputVirtualKey> RepeatKeys { get; }

        /// <summary>
        ///     Updates the state.
        /// </summary>
        /// <returns>Returns the result of the update.</returns>
        UpdateResult Update();
    }
}