using System;
using System.Diagnostics;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Math;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Root
{
    public class LoadingUpdater : Updater
    {
        private readonly SineWave _loadingIndicatorSineWave = default!;
        private readonly TriangleWave _loadingIndicatorTriangleWave = default!;
        private readonly Stopwatch _opacityStopwatch = new Stopwatch();

        public LoadingUpdater(IInterfaces interfaces, NestedContext context) : base(interfaces, context.CopyAndPush(nameof(LoadingUpdater)))
        {
            Initialize();
        }

        public LoadingUpdater(IInterfaces interfaces) : base(interfaces)
        {
            Initialize();
        }

        // Allows initialization code to be reused despite the fields being declared readonly
        private void Initialize()
        {
            ref readonly SineWave loadingIndicatorSineWave = ref _loadingIndicatorSineWave;
            ref readonly TriangleWave loadingIndicatorTriangleWave = ref _loadingIndicatorTriangleWave;

            UnsafeExtensions.WriteReadonly(in loadingIndicatorSineWave, new SineWave(0, 1, TimeSpan.FromSeconds(2), WaveOffset.Trough, _opacityStopwatch));
            UnsafeExtensions.WriteReadonly(
                in loadingIndicatorTriangleWave,
                new TriangleWave(0, 1, TimeSpan.FromSeconds(2), WaveOffset.Trough, _opacityStopwatch));
        }

        protected override void OnUpdateGameState(GameState gameState)
        {
            _opacityStopwatch.Start();
        }

        protected override void OnPrepareRenderState(GameState gameState, RenderState renderState)
        {
            LoadingSceneRenderState sceneRenderState = renderState.SceneStates.Loading ??= new LoadingSceneRenderState();

            sceneRenderState.SineOpacity = _loadingIndicatorSineWave.Value;
            sceneRenderState.TriangleOpacity = _loadingIndicatorTriangleWave.Value;
        }
    }
}