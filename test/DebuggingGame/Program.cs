using System;
using System.Collections.Immutable;
using Autofac;
using BouncyBox.VorpalEngine.DebuggingGame.Scenes;
using BouncyBox.VorpalEngine.DebuggingGame.States.Game;
using BouncyBox.VorpalEngine.DebuggingGame.States.Render;
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
            ImmutableArray<Type> ignoredMessageTypes =
                new[]
                {
                    typeof(EngineUpdateStatsMessage),
                    typeof(EngineRenderStatsMessage)
                }.ToImmutableArray();

            MessageLogFilter.ShouldLogMessageDelegate = a => !ignoredMessageTypes.Contains(a.GetType());
            MessageLogFilter.ShouldLogMessageTypeDelegate = a => !ignoredMessageTypes.Contains(a);
#endif

            return GameFactory.CreateAndRun<Game, GameState, RenderState, SceneKey>(SceneKey.Root, args, RegisterComponents);
        }

        private static void RegisterComponents(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(Game.CommonGameSettings).As<ICommonGameSettings>().SingleInstance();
            containerBuilder.RegisterType<GameStateManager<GameState>>().As<IGameStateManager<GameState>>().SingleInstance();
            containerBuilder.RegisterType<RenderStateManager<RenderState>>().As<IRenderStateManager<RenderState>>().SingleInstance();
            containerBuilder.RegisterType<SceneFactory>().As<ISceneFactory<GameState, RenderState, SceneKey>>().SingleInstance();
        }
    }
}