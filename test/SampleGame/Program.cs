using System;
using Autofac;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.SampleGame.Scenes;

namespace BouncyBox.VorpalEngine.SampleGame
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            return GameFactory.CreateAndRun<Game, GameState, SceneKey>(SceneKey.Title, args, RegisterComponents);
        }

        private static void RegisterComponents(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CommonGameSettings>().As<ICommonGameSettings>().SingleInstance();
            containerBuilder.RegisterType<GameStateManager<GameState>>().As<IGameStateManager<GameState>>().SingleInstance();
            containerBuilder.RegisterType<SceneFactory>().As<ISceneFactory<SceneKey>>().SingleInstance();
        }
    }
}