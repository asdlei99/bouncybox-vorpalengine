using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using BouncyBox.VorpalEngine.Common;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Interop.D2D1;
using BouncyBox.VorpalEngine.Interop.DWrite;
using TerraFX.Interop;
using User32 = BouncyBox.VorpalEngine.Interop.User32;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootEntity : UpdatingRenderingEntity<RootEntityRenderState>
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

        protected override void OnUpdateGameState(CancellationToken cancellationToken)
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
        }

        protected override RootEntityRenderState? OnGetRenderState()
        {
            RootSceneGameState sceneGameState = _gameStateManager.GameState.SceneStates.Root!;

            return
                new RootEntityRenderState
                {
                    Counter = sceneGameState.Counter,
                    UpdatesPerSecond = _latestEngineUpdateStatsMessage?.UpdatesPerSecond,
                    FramesPerSecond = _latestEngineRenderStatsMessage?.FramesPerSecond,
                    MeanFrametime = _latestEngineRenderStatsMessage?.MeanFrametime,
                    MinimumFrametime = _latestEngineRenderStatsMessage?.MinimumFrametime,
                    MaximumFrametime = _latestEngineRenderStatsMessage?.MaximumFrametime,
                    FrameCount = _latestEngineRenderStatsMessage?.FrameCount,
                    RenderDelayInMilliseconds = (uint)sceneGameState.RenderDelay.TotalMilliseconds,
                    UpdateDelayInMilliseconds = (uint)sceneGameState.UpdateDelay.TotalMilliseconds,
                    GamePadDPadLeft = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadLeft) ? "\u2190" : " ",
                    GamePadDPadUp = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadUp) ? "\u2191" : " ",
                    GamePadDPadRight = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadRight) ? "\u2192" : " ",
                    GamePadDPadDown = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.DPadDown) ? "\u2193" : " ",
                    GamePadA = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.A) ? "A" : " ",
                    GamePadB = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.B) ? "B" : " ",
                    GamePadX = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.X) ? "X" : " ",
                    GamePadY = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Y) ? "Y" : " ",
                    GamePadBack = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Back) ? "Back " : "     ",
                    GamePadStart = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.Start) ? "Start " : "      ",
                    GamePadLeftShoulder = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftShoulder) ? "LS " : "   ",
                    GamePadRightShoulder = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightShoulder) ? "RS " : "   ",
                    GamePadLeftTrigger = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftTrigger) ? "LTr " : "    ",
                    GamePadRightTrigger = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightTrigger) ? "RTr " : "    ",
                    GamePadLeftThumbPress = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.LeftThumbPress) ? "LTh " : "    ",
                    GamePadRightThumbPress = sceneGameState.XInputDownKeys.Contains(XInputVirtualKey.RightThumbPress) ? "RTh" : ""
                };
        }

        protected override unsafe void OnInitializeRenderResources(in DirectXResources resources)
        {
            DXGI_RGBA brushColor = DXGIFactory.CreateRgba(1, 1, 1, 1);

            resources.D2D1DeviceContext.CreateSolidColorBrush(&brushColor, out _brush).ThrowIfFailed($"Failed to create {nameof(D2D1SolidColorBrush)}.");
            resources
                .DWriteFactory
                .CreateTextFormat(
                    "Consolas",
                    null,
                    DWRITE_FONT_WEIGHT.DWRITE_FONT_WEIGHT_NORMAL,
                    DWRITE_FONT_STYLE.DWRITE_FONT_STYLE_NORMAL,
                    DWRITE_FONT_STRETCH.DWRITE_FONT_STRETCH_NORMAL,
                    16,
                    CultureInfo.CurrentCulture.Name,
                    out _textFormat)
                .ThrowIfFailed($"Failed to create {nameof(DWriteTextFormat)}.");
        }

        protected override void OnReleaseRenderResources()
        {
            _brush?.Dispose();
            _textFormat?.Dispose();
        }

        protected override unsafe EntityRenderResult OnRender(
            in DirectXResources resources,
            in RootEntityRenderState renderState,
            CancellationToken cancellationToken)
        {
            resources.DXGIAdapter.GetDesc(out DXGI_ADAPTER_DESC dxgiAdapterDesc).ThrowIfFailed($"Failed to get {nameof(DXGI_ADAPTER_DESC)}.");

            var adapter = new ReadOnlySpan<char>(dxgiAdapterDesc.Description, 128).ToString();

            _stringBuilder
                .Clear()
                .AppendLine($"Adapter      : {adapter}")
                .AppendLine($"Video memory : {Math.Round(dxgiAdapterDesc.DedicatedVideoMemory.ToUInt64() / 1_000_000_000.0, 2)} GB")
                .AppendLine()
                .AppendLine($"Client size : {resources.ClientSize.width} x {resources.ClientSize.height}")
                .AppendLine()
                .AppendLine($"UPS : {Math.Round(renderState.UpdatesPerSecond ?? 0, 2, MidpointRounding.AwayFromZero)}")
                .AppendLine($"FPS : {Math.Round(renderState.FramesPerSecond ?? 0, 2, MidpointRounding.AwayFromZero)}")
                .AppendLine(
                    $"FT  : Mean = {renderState.MeanFrametime?.TotalMilliseconds ?? 0:F3} ms; Min = {renderState.MinimumFrametime?.TotalMilliseconds:F3} ms; Max = {renderState.MaximumFrametime?.TotalMilliseconds:F3} ms")
                .AppendLine()
                .AppendLine($"State counter      : {renderState.Counter}")
                .AppendLine($"Frame count        : {renderState.FrameCount ?? 0}")
                .AppendLine()
                .AppendLine($"Update delay : {renderState.UpdateDelayInMilliseconds} ms  [\u2191] Increase delay  [\u2193] Decrease delay")
                .AppendLine($"Render delay : {renderState.RenderDelayInMilliseconds} ms  [\u2192] Increase delay  [\u2190] Decrease delay")
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
                .Append(renderState.GamePadDPadLeft)
                .Append(renderState.GamePadDPadUp)
                .Append(renderState.GamePadDPadRight)
                .Append(renderState.GamePadDPadDown)
                .Append(" ")
                .Append(renderState.GamePadA)
                .Append(renderState.GamePadB)
                .Append(renderState.GamePadX)
                .Append(renderState.GamePadY)
                .Append(" ")
                .Append(renderState.GamePadBack)
                .Append(renderState.GamePadStart)
                .Append(" ")
                .Append(renderState.GamePadLeftShoulder)
                .Append(renderState.GamePadRightShoulder)
                .Append(renderState.GamePadLeftTrigger)
                .Append(renderState.GamePadRightTrigger)
                .Append(renderState.GamePadLeftThumbPress)
                .Append(renderState.GamePadRightThumbPress);

            D2D_RECT_F rect = resources.ClientRectF;

            resources
                .D2D1DeviceContext
                .DrawText(
                    _stringBuilder.ToString(),
                    _textFormat!,
                    &rect,
                    _brush!,
                    D2D1_DRAW_TEXT_OPTIONS.D2D1_DRAW_TEXT_OPTIONS_NONE,
                    DWRITE_MEASURING_MODE.DWRITE_MEASURING_MODE_NATURAL);

            if (renderState.RenderDelayInMilliseconds > 0)
            {
                cancellationToken.WaitHandle.WaitOne((int)renderState.RenderDelayInMilliseconds);
            }

            return EntityRenderResult.FrameRendered;
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