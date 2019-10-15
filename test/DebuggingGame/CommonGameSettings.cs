using System.Drawing;
using BouncyBox.VorpalEngine.DebuggingGame.Resources;
using BouncyBox.VorpalEngine.Engine.Game;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    public class CommonGameSettings : ICommonGameSettings
    {
        private readonly object _lockObject = new object();
        private bool _isVSyncEnabled;
        private Size _requestedResolution = new Size(1280, 720);
        private WindowedMode _windowedMode = WindowedMode.BorderedWindowed;

        public string Title { get; } = "Vorpal Engine Debugging Game by Bouncy Box";
        public Icon Icon { get; } = Icons.app;
        public bool AllowScreensaver { get; } = false;
        public bool AllowUserResizing { get; } = true;

        public Size RequestedResolution
        {
            get
            {
                lock (_lockObject)
                {
                    return _requestedResolution;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _requestedResolution = value;
                }
            }
        }

        public WindowedMode WindowedMode
        {
            get
            {
                lock (_lockObject)
                {
                    return _windowedMode;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _windowedMode = value;
                }
            }
        }

        public bool EnableVSync
        {
            get
            {
                lock (_lockObject)
                {
                    return _isVSyncEnabled;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _isVSyncEnabled = value;
                }
            }
        }
    }
}