using System;
using System.Collections.Generic;
using Autofac;
using BouncyBox.VorpalEngine.DebuggingGame.Scenes;
using BouncyBox.VorpalEngine.Engine.Bootstrap;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Messaging.GlobalMessages;
using BouncyBox.VorpalEngine.Engine.Scenes;

namespace BouncyBox.VorpalEngine.DebuggingGame
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
#if DEBUG
            var ignoredMessageTypes =
                new HashSet<Type>(
                    new[]
                    {
                        typeof(EngineUpdateStatsMessage),
                        typeof(EngineRenderStatsMessage)
                    });

            MessageLogFilter.ShouldLogMessageDelegate = a => !ignoredMessageTypes.Contains(a.GetType());
            MessageLogFilter.ShouldLogMessageTypeDelegate = a => !ignoredMessageTypes.Contains(a);
#endif

            return GameFactory.CreateAndRun<Game, GameState, SceneKey>(SceneKey.Root, args, RegisterComponents);
        }

        private static void RegisterComponents(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(Game.CommonGameSettings).As<ICommonGameSettings>().SingleInstance();
            containerBuilder.RegisterType<GameStateManager<GameState>>().As<IGameStateManager<GameState>>().SingleInstance();
            containerBuilder.RegisterType<SceneFactory>().As<ISceneFactory<SceneKey>>().SingleInstance();
        }
    }
}