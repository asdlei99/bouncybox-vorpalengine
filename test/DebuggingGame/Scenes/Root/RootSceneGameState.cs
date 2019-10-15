using System;
using System.Collections.Generic;
using BouncyBox.VorpalEngine.Engine.Input.XInput;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootSceneGameState
    {
        public ulong Counter { get; set; }
        public TimeSpan RenderDelay { get; set; }
        public TimeSpan UpdateDelay { get; set; }
        public HashSet<XInputVirtualKey> XInputDownKeys { get; } = new HashSet<XInputVirtualKey>();
    }
}