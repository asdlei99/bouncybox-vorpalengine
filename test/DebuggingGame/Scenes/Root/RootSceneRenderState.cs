using System;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootSceneRenderState
    {
        public ulong Counter { get; set; }
        public double? UpdatesPerSecond { get; set; }
        public double? FramesPerSecond { get; set; }
        public TimeSpan? MeanFrametime { get; set; }
        public TimeSpan? MinimumFrametime { get; set; }
        public TimeSpan? MaximumFrametime { get; set; }
        public ulong? FrameCount { get; set; }
        public uint UpdateDelayInMilliseconds { get; set; }
        public uint RenderDelayInMilliseconds { get; set; }
        public string? GamePadDPadLeft { get; set; }
        public string? GamePadDPadUp { get; set; }
        public string? GamePadDPadRight { get; set; }
        public string? GamePadDPadDown { get; set; }
        public string? GamePadA { get; set; }
        public string? GamePadB { get; set; }
        public string? GamePadX { get; set; }
        public string? GamePadY { get; set; }
        public string? GamePadBack { get; set; }
        public string? GamePadStart { get; set; }
        public string? GamePadLeftShoulder { get; set; }
        public string? GamePadRightShoulder { get; set; }
        public string? GamePadLeftTrigger { get; set; }
        public string? GamePadRightTrigger { get; set; }
        public string? GamePadLeftThumbPress { get; set; }
        public string? GamePadRightThumbPress { get; set; }
    }
}