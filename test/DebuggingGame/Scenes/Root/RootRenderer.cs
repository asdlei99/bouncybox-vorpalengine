using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using BouncyBox.VorpalEngine.DebuggingGame.Entities.Renderers;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.DirectX;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.DebuggingGame.Scenes.Root
{
    public class RootRenderer : Renderer
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private D2D1SolidColorBrush? _brush;
        private DWriteTextFormat? _textFormat;

        public RootRenderer(IInterfaces interfaces, NestedContext context) : base(interfaces, context.CopyAndPush(nameof(RootRenderer)))
        {
        }

        public RootRenderer(IInterfaces interfaces) : base(interfaces)
        {
        }

        protected override bool RenderWhenSuspended { get; set; } = true;

        protected override unsafe void OnInitializeResources(DirectXResources resources)
        {
            DXGI_RGBA brushColor = DXGIFactory.CreateRgba(1, 1, 1, 1);

            _brush = resources.D2D1DeviceContext.CreateSolidColorBrush(brushColor);
            _textFormat = resources.DWriteFactory1.CreateTextFormat("Consolas", 16);
        }

        protected override void OnReleaseResources()
        {
            _brush?.Dispose();
            _textFormat?.Dispose();
        }

        protected override unsafe void OnRender(DirectXResources resources, RenderState renderState, CancellationToken cancellationToken)
        {
            Debug.Assert(renderState.SceneStates.Root != null);

            RootSceneRenderState sceneRenderState = renderState.SceneStates.Root;
            DXGI_ADAPTER_DESC dxgiAdapterDesc = resources.DXGIAdapter.GetDesc();

            _stringBuilder
                .Clear()
                .AppendLine($"Adapter      : {new string((char*)dxgiAdapterDesc.Description)}")
                .AppendLine($"Video memory : {Math.Round(dxgiAdapterDesc.DedicatedVideoMemory.ToUInt64() / 1_000_000_000.0, 2)} GB")
                .AppendLine()
                .AppendLine($"Client size : {resources.ClientSize.width} x {resources.ClientSize.height}")
                .AppendLine()
                .AppendLine($"UPS : {Math.Round(sceneRenderState.UpdatesPerSecond ?? 0, 2, MidpointRounding.AwayFromZero)}")
                .AppendLine($"FPS : {Math.Round(sceneRenderState.FramesPerSecond ?? 0, 2, MidpointRounding.AwayFromZero)}")
                .AppendLine(
                    $"FT  : Mean = {sceneRenderState.MeanFrametime?.TotalMilliseconds ?? 0:F3} ms; Min = {sceneRenderState.MinimumFrametime?.TotalMilliseconds:F3} ms; {sceneRenderState.MaximumFrametime?.TotalMilliseconds:F3} ms")
                .AppendLine()
                .AppendLine($"State counter      : {sceneRenderState.Counter}")
                .AppendLine($"Frame count        : {sceneRenderState.FrameCount ?? 0}")
                .AppendLine()
                .AppendLine($"Update delay : {sceneRenderState.UpdateDelayInMilliseconds} ms  [\u2191] Increase delay  [\u2193] Decrease delay")
                .AppendLine($"Render delay : {sceneRenderState.RenderDelayInMilliseconds} ms  [\u2192] Increase delay  [\u2190] Decrease delay")
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
                .Append(sceneRenderState.GamePadDPadLeft)
                .Append(sceneRenderState.GamePadDPadUp)
                .Append(sceneRenderState.GamePadDPadRight)
                .Append(sceneRenderState.GamePadDPadDown)
                .Append(" ")
                .Append(sceneRenderState.GamePadA)
                .Append(sceneRenderState.GamePadB)
                .Append(sceneRenderState.GamePadX)
                .Append(sceneRenderState.GamePadY)
                .Append(" ")
                .Append(sceneRenderState.GamePadBack)
                .Append(sceneRenderState.GamePadStart)
                .Append(" ")
                .Append(sceneRenderState.GamePadLeftShoulder)
                .Append(sceneRenderState.GamePadRightShoulder)
                .Append(sceneRenderState.GamePadLeftTrigger)
                .Append(sceneRenderState.GamePadRightTrigger)
                .Append(sceneRenderState.GamePadLeftThumbPress)
                .Append(sceneRenderState.GamePadRightThumbPress);

            resources.D2D1DeviceContext.DrawText(_stringBuilder.ToString(), _textFormat!, resources.ClientRect.ToD2DRectF(), _brush!);

            if (sceneRenderState.RenderDelayInMilliseconds > 0)
            {
                cancellationToken.WaitHandle.WaitOne((int)sceneRenderState.RenderDelayInMilliseconds);
            }
        }
    }
}