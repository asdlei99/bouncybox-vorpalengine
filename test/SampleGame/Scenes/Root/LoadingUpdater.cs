using System;
using System.Diagnostics;
using System.Threading;
using BouncyBox.VorpalEngine.Engine;
using BouncyBox.VorpalEngine.Engine.Math;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame.Scenes.Root
{
    public class LoadingUpdater : Updater
    {
        private readonly SineWave _loadingIndicatorSineWave;
        private readonly TriangleWave _loadingIndicatorTriangleWave;
        private readonly Stopwatch _opacityStopwatch = new Stopwatch();

        public LoadingUpdater(IInterfaces interfaces, NestedContext context) : base(interfaces, context.CopyAndPush(nameof(LoadingUpdater)))
        {
            _loadingIndicatorSineWave = new SineWave(0, 1, TimeSpan.FromSeconds(2), WaveOffset.Trough, _opacityStopwatch);
            _loadingIndicatorTriangleWave = new TriangleWave(0, 1, TimeSpan.FromSeconds(2), WaveOffset.Trough, _opacityStopwatch);
        }

        public LoadingUpdater(IInterfaces interfaces) : this(interfaces, NestedContext.None())
        {
        }

        protected override void OnUpdateGameState(CancellationToken cancellationToken)
        {
            _opacityStopwatch.Start();
        }

        protected override void OnPrepareRenderState(RenderState renderState)
        {
            LoadingSceneRenderState sceneRenderState = renderState.SceneStates.Loading ??= new LoadingSceneRenderState();

            sceneRenderState.SineOpacity = _loadingIndicatorSineWave.Value;
            sceneRenderState.TriangleOpacity = _loadingIndicatorTriangleWave.Value;
        }
    }
}