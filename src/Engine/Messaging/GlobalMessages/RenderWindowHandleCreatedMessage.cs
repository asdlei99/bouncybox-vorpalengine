using System;

namespace BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages
{
    /// <summary>
    ///     A global message indicating that the render window handle was created.
    /// </summary>
    public struct RenderWindowHandleCreatedMessage : IGlobalMessage
    {
        /// <summary>
        ///     Initializes a new instance <see cref="RenderWindowHandleCreatedMessage" /> type.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public RenderWindowHandleCreatedMessage(IntPtr windowHandle)
        {
            WindowHandle = windowHandle;
        }

        /// <summary>
        ///     Gets the window handle.
        /// </summary>
        public IntPtr WindowHandle { get; }
    }
}