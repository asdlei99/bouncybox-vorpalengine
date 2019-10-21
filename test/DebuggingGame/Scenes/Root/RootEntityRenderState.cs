using System;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public struct RootEntityRenderState
    {
        public ulong Counter;
        public double? UpdatesPerSecond;
        public double? FramesPerSecond;
        public TimeSpan? MeanFrametime;
        public TimeSpan? MinimumFrametime;
        public TimeSpan? MaximumFrametime;
        public ulong? FrameCount;
        public uint RenderDelayInMilliseconds;
        public uint UpdateDelayInMilliseconds;
        public string GamePadDPadLeft;
        public string GamePadDPadUp;
        public string GamePadDPadRight;
        public string GamePadDPadDown;
        public string GamePadA;
        public string GamePadB;
        public string GamePadX;
        public string GamePadY;
        public string GamePadBack;
        public string GamePadStart;
        public string GamePadLeftShoulder;
        public string GamePadRightShoulder;
        public string GamePadLeftTrigger;
        public string GamePadRightTrigger;
        public string GamePadLeftThumbPress;
        public string GamePadRightThumbPress;
    }
}