using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>
    ///     A global message indicating that the render window's display was changed.
    /// </summary>
    public struct DisplayChangedMessage : IGlobalMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisplayChangedMessage" /> type.
        /// </summary>
        /// <param name="monitorHandle">The new display's monitor handle.</param>
        public DisplayChangedMessage(IntPtr monitorHandle)
        {
            MonitorHandle = monitorHandle;
        }

        /// <summary>
        ///     Gets the new display's monitor handle.
        /// </summary>
        public IntPtr MonitorHandle { get; }
    }
}