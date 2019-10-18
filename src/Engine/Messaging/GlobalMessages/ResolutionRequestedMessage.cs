using System.Drawing;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>A global message requesting a change to the resolution.</summary>
    public struct ResolutionRequestedMessage : IGlobalMessage
    {
        /// <summary>Initializes a new instance of the <see cref="ResolutionRequestedMessage" /> type.</summary>
        /// <param name="resolution">The requested resolution.</param>
        public ResolutionRequestedMessage(Size resolution)
        {
            Resolution = resolution;
        }

        /// <summary>Gets the requested resolution.</summary>
        public Size Resolution { get; }
    }
}