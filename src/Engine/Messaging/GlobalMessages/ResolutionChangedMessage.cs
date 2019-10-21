using System.Drawing;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>A global message indicating that the resolution was changed.</summary>
    public readonly struct ResolutionChangedMessage : IGlobalMessage
    {
        /// <summary>Initializes a new instance of the <see cref="ResolutionChangedMessage" /> type.</summary>
        /// <param name="resolution">The new resolution.</param>
        public ResolutionChangedMessage(Size resolution)
        {
            Resolution = resolution;
        }

        /// <summary>Gets the new resolution.</summary>
        public Size Resolution { get; }
    }
}