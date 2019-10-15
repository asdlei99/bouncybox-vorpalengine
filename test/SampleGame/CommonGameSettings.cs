using System.Drawing;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.SampleGame.Resources;

namespace BouncyBox.VorpalEngine.SampleGame
{
    public class CommonGameSettings : ICommonGameSettings
    {
        public Size RequestedResolution { get; } = new Size(1920, 1080);
        public string Title { get; } = "Vorpal Engine Sample Game by Bouncy Box";
        public Icon Icon { get; } = Icons.app;
        public bool AllowScreensaver { get; } = false;
        public bool AllowUserResizing { get; } = false;
        public WindowedMode WindowedMode { get; } = WindowedMode.BorderedWindowed;
        public bool EnableVSync { get; } = true;
    }
}