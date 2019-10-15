using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Threading;
using BouncyBox.Common.NetStandard21;
using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Interop;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootUpdater : Updater
    {
        private static readonly ImmutableArray<Size> Resolutions =
            new[]
            {
                new Size(1280, 720),
                new Size(1536, 864),
                new Size(1920, 1080),
                new Size(2560, 1440)
            }.ToImmutableArray();

        private readonly TimeSpan _delayIncrement = TimeSpan.FromMilliseconds(1);
        private readonly TimeSpan _maximumDelay = TimeSpan.FromSeconds(1);
        private KeyboardSnapshot _previousKeyboardSnapshot = new KeyboardSnapshot(ImmutableArray<User32.VirtualKey>.Empty);

        public RootUpdater(IInterfaces interfaces, NestedContext context) : base(interfaces, context.CopyAndPush(nameof(RootUpdater)))
        {
        }

        public RootUpdater(IInterfaces interfaces) : base(interfaces)
        {
        }

        protected override void OnUpdateGameState(GameState gameState)
        {
            System.Diagnostics.Debug.Assert(gameState.SceneStates.Root != null);

            RootSceneGameState sceneGameState = gameState.SceneStates.Root;

            // Keyboard

            KeyboardSnapshot currentKeyboardSnapshot = Keyboard.ProcessQueueAndSnapshot();

            (IReadOnlyCollection<User32.VirtualKey> downKeys, IReadOnlyCollection<User32.VirtualKey> _) =
                currentKeyboardSnapshot.GetChangedKeys(ref _previousKeyboardSnapshot);

            if (downKeys.Contains(User32.VirtualKey.VK_UP) && sceneGameState.UpdateDelay < _maximumDelay)
            {
                sceneGameState.UpdateDelay += _delayIncrement;
            }
            if (downKeys.Contains(User32.VirtualKey.VK_DOWN) && sceneGameState.UpdateDelay > TimeSpan.Zero)
            {
                sceneGameState.UpdateDelay -= _delayIncrement;
            }
            if (downKeys.Contains(User32.VirtualKey.VK_LEFT) && sceneGameState.RenderDelay > TimeSpan.Zero)
            {
                sceneGameState.RenderDelay -= _delayIncrement;
            }
            if (downKeys.Contains(User32.VirtualKey.VK_RIGHT) && sceneGameState.RenderDelay < _maximumDelay)
            {
                sceneGameState.RenderDelay += _delayIncrement;
            }
            if (downKeys.Contains(User32.VirtualKey.VK_OEM_PLUS))
            {
                int index = Math.Max(0, Math.Min(Resolutions.IndexOf(DebuggingGame.CommonGameSettings.RequestedResolution) + 1, Resolutions.Length - 1));

                DebuggingGame.CommonGameSettings.RequestedResolution = Resolutions[index];

                GlobalMessagePublisherSubscriber.Publish(new ResolutionRequestedMessage(DebuggingGame.CommonGameSettings.RequestedResolution));
            }
            if (downKeys.Contains(User32.VirtualKey.VK_OEM_MINUS))
            {
                int index = Math.Max(0, Math.Min(Resolutions.IndexOf(DebuggingGame.CommonGameSettings.RequestedResolution) - 1, Resolutions.Length - 1));

                DebuggingGame.CommonGameSettings.RequestedResolution = Resolutions[index];

                GlobalMessagePublisherSubscriber.Publish(new ResolutionRequestedMessage(DebuggingGame.CommonGameSettings.RequestedResolution));
            }
            if (downKeys.Contains(User32.VirtualKey.V))
            {
                DebuggingGame.CommonGameSettings.EnableVSync = !DebuggingGame.CommonGameSettings.EnableVSync;
            }
            if (downKeys.Contains(User32.VirtualKey.W))
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (DebuggingGame.CommonGameSettings.WindowedMode)
                {
                    case WindowedMode.BorderedWindowed:
                        DebuggingGame.CommonGameSettings.WindowedMode = WindowedMode.BorderlessWindowed;
                        GlobalMessagePublisherSubscriber.Publish(new WindowedModeRequestedMessage(WindowedMode.BorderlessWindowed));
                        break;
                    case WindowedMode.BorderlessWindowed:
                        DebuggingGame.CommonGameSettings.WindowedMode = WindowedMode.BorderedWindowed;
                        GlobalMessagePublisherSubscriber.Publish(new WindowedModeRequestedMessage(WindowedMode.BorderedWindowed));
                        break;
                }
            }
            if (downKeys.Contains(User32.VirtualKey.P))
            {
                if (IsPaused)
                {
                    GlobalMessagePublisherSubscriber.Publish<UnpauseGameMessage>();
                }
                else
                {
                    GlobalMessagePublisherSubscriber.Publish<PauseGameMessage>();
                }
            }
            if (downKeys.Contains(User32.VirtualKey.S))
            {
                if (IsSuspended)
                {
                    GlobalMessagePublisherSubscriber.Publish<ResumeGameMessage>();
                }
                else
                {
                    GlobalMessagePublisherSubscriber.Publish<SuspendGameMessage>();
                }
            }
            if (downKeys.Contains(User32.VirtualKey.U))
            {
                GlobalMessagePublisherSubscriber.Publish(new UnloadSceneMessage<SceneKey>(SceneKey.Root));
            }

            _previousKeyboardSnapshot = currentKeyboardSnapshot;

            // Gamepad

            StatefulGamepad.Update();

            sceneGameState.XInputDownKeys.Clear();
            sceneGameState.XInputDownKeys.AddRange(StatefulGamepad.DownKeys);

            // General state

            if (IsRunning)
            {
                sceneGameState.Counter++;
            }

            if (sceneGameState.UpdateDelay > TimeSpan.Zero)
            {
                Thread.Sleep(sceneGameState.UpdateDelay);
            }
        }

        protected override void OnPrepareRenderState(GameState gameState, RenderState renderState)
        {
            System.Diagnostics.Debug.Assert(gameState.SceneStates.Root != null);

            RootSceneGameState sceneGameState = gameState.SceneStates.Root;
            RootSceneRenderState sceneRenderState = renderState.SceneStates.Root ??= new RootSceneRenderState();

            sceneRenderState.Counter = sceneGameState.Counter;
            sceneRenderState.RenderDelayInMilliseconds = (ulong)sceneGameState.RenderDelay.TotalMilliseconds;
            sceneRenderState.UpdateDelayInMilliseconds = (ulong)sceneGameState.UpdateDelay.TotalMilliseconds;
            sceneRenderState.GamePadDPadLeft = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2190" : " ";
            sceneRenderState.GamePadDPadUp = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2191" : " ";
            sceneRenderState.GamePadDPadRight = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2192" : " ";
            sceneRenderState.GamePadDPadDown = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2193" : " ";
            sceneRenderState.GamePadA = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.A) ? "A" : " ";
            sceneRenderState.GamePadB = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.B) ? "B" : " ";
            sceneRenderState.GamePadX = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.X) ? "X" : " ";
            sceneRenderState.GamePadY = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Y) ? "Y" : " ";
            sceneRenderState.GamePadBack = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Back) ? "Back " : "     ";
            sceneRenderState.GamePadStart = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Start) ? "Start " : "      ";
            sceneRenderState.GamePadLeftShoulder = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftShoulder) ? "LS " : "   ";
            sceneRenderState.GamePadRightShoulder = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightShoulder) ? "RS " : "   ";
            sceneRenderState.GamePadLeftTrigger = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftTrigger) ? "LTr " : "    ";
            sceneRenderState.GamePadRightTrigger = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightTrigger) ? "RTr " : "    ";
            sceneRenderState.GamePadLeftThumbPress = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftThumbPress) ? "LTh " : "    ";
            sceneRenderState.GamePadRightThumbPress = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightThumbPress) ? "RTh" : "";
        }
    }
}