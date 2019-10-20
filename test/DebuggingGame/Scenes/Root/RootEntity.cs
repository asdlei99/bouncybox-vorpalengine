using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using TerraFX.Interop;
using User32 = BouncyBox.VorpalEngine.Engine.Interop.User32;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootEntity : Entity
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
        private readonly IGameStateManager<GameState> _gameStateManager;
        private readonly TimeSpan _maximumDelay = TimeSpan.FromSeconds(1);
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private D2D1SolidColorBrush? _brush;
        private EngineRenderStatsMessage? _latestEngineRenderStatsMessage;
        private EngineUpdateStatsMessage? _latestEngineUpdateStatsMessage;
        private KeyboardSnapshot _previousKeyboardSnapshot = new KeyboardSnapshot(ImmutableArray<User32.VirtualKey>.Empty);
        private DWriteTextFormat? _textFormat;

        public RootEntity(IInterfaces interfaces, IGameStateManager<GameState> gameStateManager, NestedContext context) : base(interfaces, 0, 0, context)
        {
            _gameStateManager = gameStateManager;

            GlobalMessagePublisherSubscriber
                .Subscribe<EngineUpdateStatsMessage>(HandleEngineUpdateStatsMessage)
                .Subscribe<EngineRenderStatsMessage>(HandleEngineRenderStatsMessage);
        }

        public RootEntity(IInterfaces interfaces, IGameStateManager<GameState> gameStateManager) : this(interfaces, gameStateManager, NestedContext.None())
        {
        }

        protected override bool UpdateWhenPaused { get; } = true;
        protected override bool UpdateWhenSuspended { get; } = true;
        protected override bool RenderWhenSuspended { get; } = true;

        protected override UpdateGameStateResult OnUpdateGameState(CancellationToken cancellationToken)
        {
            RootSceneGameState sceneGameState = _gameStateManager.GameState.SceneStates.Root!;

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
                int index = Math.Max(0, Math.Min(Resolutions.IndexOf(Game.CommonGameSettings.RequestedResolution) + 1, Resolutions.Length - 1));

                Game.CommonGameSettings.RequestedResolution = Resolutions[index];

                GlobalMessagePublisherSubscriber.Publish(new ResolutionRequestedMessage(Game.CommonGameSettings.RequestedResolution));
            }
            if (downKeys.Contains(User32.VirtualKey.VK_OEM_MINUS))
            {
                int index = Math.Max(0, Math.Min(Resolutions.IndexOf(Game.CommonGameSettings.RequestedResolution) - 1, Resolutions.Length - 1));

                Game.CommonGameSettings.RequestedResolution = Resolutions[index];

                GlobalMessagePublisherSubscriber.Publish(new ResolutionRequestedMessage(Game.CommonGameSettings.RequestedResolution));
            }
            if (downKeys.Contains(User32.VirtualKey.V))
            {
                Game.CommonGameSettings.EnableVSync = !Game.CommonGameSettings.EnableVSync;
            }
            if (downKeys.Contains(User32.VirtualKey.W))
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (Game.CommonGameSettings.WindowedMode)
                {
                    case WindowedMode.BorderedWindowed:
                        Game.CommonGameSettings.WindowedMode = WindowedMode.BorderlessWindowed;
                        GlobalMessagePublisherSubscriber.Publish(new WindowedModeRequestedMessage(WindowedMode.BorderlessWindowed));
                        break;
                    case WindowedMode.BorderlessWindowed:
                        Game.CommonGameSettings.WindowedMode = WindowedMode.BorderedWindowed;
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
            sceneGameState.XInputDownKeys.UnionWith(StatefulGamepad.DownKeys);

            // General state

            if (IsRunning)
            {
                sceneGameState.Counter++;
            }

            if (sceneGameState.UpdateDelay > TimeSpan.Zero)
            {
                cancellationToken.WaitHandle.WaitOne(sceneGameState.UpdateDelay);
            }

            return UpdateGameStateResult.Render;
        }

        protected override Action<DirectXResources, CancellationToken> OnGetRenderDelegate()
        {
            RootSceneGameState sceneGameState = _gameStateManager.GameState.SceneStates.Root!;
            ulong counter = sceneGameState.Counter;
            double? updatesPerSecond = _latestEngineUpdateStatsMessage?.UpdatesPerSecond;
            double? framesPerSecond = _latestEngineRenderStatsMessage?.FramesPerSecond;
            TimeSpan? meanFrametime = _latestEngineRenderStatsMessage?.MeanFrametime;
            TimeSpan? minimumFrametime = _latestEngineRenderStatsMessage?.MinimumFrametime;
            TimeSpan? maximumFrametime = _latestEngineRenderStatsMessage?.MaximumFrametime;
            ulong? frameCount = _latestEngineRenderStatsMessage?.FrameCount;
            var renderDelayInMilliseconds = (uint)sceneGameState.RenderDelay.TotalMilliseconds;
            var updateDelayInMilliseconds = (uint)sceneGameState.UpdateDelay.TotalMilliseconds;
            string gamePadDPadLeft = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2190" : " ";
            string gamePadDPadUp = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2191" : " ";
            string gamePadDPadRight = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2192" : " ";
            string gamePadDPadDown = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2193" : " ";
            string gamePadA = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.A) ? "A" : " ";
            string gamePadB = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.B) ? "B" : " ";
            string gamePadX = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.X) ? "X" : " ";
            string gamePadY = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Y) ? "Y" : " ";
            string gamePadBack = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Back) ? "Back " : "     ";
            string gamePadStart = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Start) ? "Start " : "      ";
            string gamePadLeftShoulder = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftShoulder) ? "LS " : "   ";
            string gamePadRightShoulder = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightShoulder) ? "RS " : "   ";
            string gamePadLeftTrigger = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftTrigger) ? "LTr " : "    ";
            string gamePadRightTrigger = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightTrigger) ? "RTr " : "    ";
            string gamePadLeftThumbPress = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftThumbPress) ? "LTh " : "    ";
            string gamePadRightThumbPress = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightThumbPress) ? "RTh" : "";

            return
                (resources, cancellationToken) =>
                {
                    DXGI_ADAPTER_DESC dxgiAdapterDesc = resources.DXGIAdapter.GetDesc();
                    string adapter;

                    unsafe
                    {
                        adapter = new string((char*)dxgiAdapterDesc.Description);
                    }

                    _stringBuilder
                        .Clear()
                        .AppendLine($"Adapter      : {adapter}")
                        .AppendLine($"Video memory : {Math.Round(dxgiAdapterDesc.DedicatedVideoMemory.ToUInt64() / 1_000_000_000.0, 2)} GB")
                        .AppendLine()
                        .AppendLine($"Client size : {resources.ClientSize.width} x {resources.ClientSize.height}")
                        .AppendLine()
                        .AppendLine($"UPS : {Math.Round(updatesPerSecond ?? 0, 2, MidpointRounding.AwayFromZero)}")
                        .AppendLine($"FPS : {Math.Round(framesPerSecond ?? 0, 2, MidpointRounding.AwayFromZero)}")
                        .AppendLine(
                            $"FT  : Mean = {meanFrametime?.TotalMilliseconds ?? 0:F3} ms; Min = {minimumFrametime?.TotalMilliseconds:F3} ms; {maximumFrametime?.TotalMilliseconds:F3} ms")
                        .AppendLine()
                        .AppendLine($"State counter      : {counter}")
                        .AppendLine($"Frame count        : {frameCount ?? 0}")
                        .AppendLine()
                        .AppendLine($"Update delay : {updateDelayInMilliseconds} ms  [\u2191] Increase delay  [\u2193] Decrease delay")
                        .AppendLine($"Render delay : {renderDelayInMilliseconds} ms  [\u2192] Increase delay  [\u2190] Decrease delay")
                        .AppendLine()
                        .AppendLine("[+] Increase resolution     [-] Decrease resolution")
                        .AppendLine("[W] Toggle bordered/borderless windowed mode")
                        .AppendLine("[V] Toggle VSync")
                        .AppendLine("[P] Toggle pause     [S] Toggle suspend")
                        .AppendLine("[U] Unload scene")
                        .AppendLine()
                        .AppendLine($"Paused    : {(IsPaused ? "Yes" : "No")}")
                        .AppendLine($"Suspended : {(IsSuspended ? "Yes" : "No")}")
                        .AppendLine()
                        .Append("Gamepad: ")
                        .Append(gamePadDPadLeft)
                        .Append(gamePadDPadUp)
                        .Append(gamePadDPadRight)
                        .Append(gamePadDPadDown)
                        .Append(" ")
                        .Append(gamePadA)
                        .Append(gamePadB)
                        .Append(gamePadX)
                        .Append(gamePadY)
                        .Append(" ")
                        .Append(gamePadBack)
                        .Append(gamePadStart)
                        .Append(" ")
                        .Append(gamePadLeftShoulder)
                        .Append(gamePadRightShoulder)
                        .Append(gamePadLeftTrigger)
                        .Append(gamePadRightTrigger)
                        .Append(gamePadLeftThumbPress)
                        .Append(gamePadRightThumbPress);

                    resources.D2D1DeviceContext.DrawText(_stringBuilder.ToString(), _textFormat!, resources.ClientRect.ToD2DRectF(), _brush!);

                    if (renderDelayInMilliseconds > 0)
                    {
                        cancellationToken.WaitHandle.WaitOne((int)renderDelayInMilliseconds);
                    }
                };
        }

        protected override unsafe void OnInitializeRenderResources(DirectXResources resources)
        {
            DXGI_RGBA brushColor = DXGIFactory.CreateRgba(1, 1, 1, 1);

            _brush = resources.D2D1DeviceContext.CreateSolidColorBrush(brushColor);
            _textFormat = resources.DWriteFactory1.CreateTextFormat("Consolas", 16);
        }

        protected override void OnReleaseRenderResources()
        {
            _brush?.Dispose();
            _textFormat?.Dispose();
        }

        private void HandleEngineUpdateStatsMessage(EngineUpdateStatsMessage message)
        {
            _latestEngineUpdateStatsMessage = message;
        }

        private void HandleEngineRenderStatsMessage(EngineRenderStatsMessage message)
        {
            _latestEngineRenderStatsMessage = message;
        }
    }
}