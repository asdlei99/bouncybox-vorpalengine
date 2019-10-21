using BouncyBox.VorpalEngine.Engine.Game;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>A global message requesting a change to the windowed mode.</summary>
    public readonly struct WindowedModeRequestedMessage : IGlobalMessage
    {
        /// <summary>Initializes a new instance of the <see cref="WindowedModeRequestedMessage" /> type.</summary>
        /// <param name="mode">The requested windowed mode.</param>
        public WindowedModeRequestedMessage(WindowedMode mode)
        {
            Mode = mode;
        }

        /// <summary>Gets the requested windowed mode.</summary>
        public WindowedMode Mode { get; }
    }
}