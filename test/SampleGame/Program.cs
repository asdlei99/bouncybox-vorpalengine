using System;
using Autofac;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.States.Game;
using BouncyBox.VorpalEngine.SampleGame.States.Render;

namespace BouncyBox.VorpalEngine.SampleGame
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            return GameFactory.CreateAndRun<SampleGame, GameState, RenderState, SceneKey>(SceneKey.Root, args, RegisterComponents);
        }

        private static void RegisterComponents(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CommonGameSettings>().As<ICommonGameSettings>().SingleInstance();
            containerBuilder.RegisterType<GameStateManager<GameState>>().As<IGameStateManager<GameState>>().SingleInstance();
            containerBuilder.RegisterType<RenderStateManager<RenderState>>().As<IRenderStateManager<RenderState>>().SingleInstance();
            containerBuilder.RegisterType<SceneFactory>().As<ISceneFactory<GameState, RenderState, SceneKey>>().SingleInstance();
        }
    }
}