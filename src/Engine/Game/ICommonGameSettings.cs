using System.Drawing;

namespace BouncyBox.VorpalEngine.Engine.Game
{
    /// <summary>
    ///     Represents common game settings.
    /// </summary>
    public interface ICommonGameSettings
    {
        /// <summary>
        ///     Gets the game's title, which is used for the render window caption.
        /// </summary>
        string? Title { get; }

        /// <summary>
        ///     Gets the game's icon, which is used for the render window's icon.
        /// </summary>
        Icon? Icon { get; }

        /// <summary>
        ///     Gets a value determining whether screensavers are allowed during gameplay.
        /// </summary>
        bool AllowScreensaver { get; }

        /// <summary>
        ///     Gets a value determining whether the user is allowed to resize the render window.
        /// </summary>
        bool AllowUserResizing { get; }

        /// <summary>
        ///     Gets the initial requested resolution.
        /// </summary>
        Size RequestedResolution { get; }

        /// <summary>
        ///     Gets the initial windowed mode.
        /// </summary>
        WindowedMode WindowedMode { get; }

        /// <summary>
        ///     Gets a value determining whether VSync is enabled.
        /// </summary>
        bool EnableVSync { get; }
    }
}