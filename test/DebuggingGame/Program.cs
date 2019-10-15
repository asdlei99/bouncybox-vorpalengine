using System;
using Autofac;
using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            MessageLogFilter.ShouldLogMessageDelegate = a => true;
            MessageLogFilter.ShouldLogMessageTypeDelegate = a => true;

            return GameFactory.CreateAndRun<DebuggingGame, GameState, RenderState, SceneKey>(SceneKey.Root, args, RegisterComponents);
        }

        private static void RegisterComponents(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(DebuggingGame.CommonGameSettings).As<ICommonGameSettings>().SingleInstance();
            containerBuilder.RegisterType<GameStateManager<GameState>>().As<IGameStateManager<GameState>>().SingleInstance();
            containerBuilder.RegisterType<RenderStateManager<RenderState>>().As<IRenderStateManager<RenderState>>().SingleInstance();
            containerBuilder.RegisterType<SceneFactory>().As<ISceneFactory<GameState, RenderState, SceneKey>>().SingleInstance();
        }
    }
}