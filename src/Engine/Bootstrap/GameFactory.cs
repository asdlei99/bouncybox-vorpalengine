using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Autofac;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Forms;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.Engine.Threads;
using CommandLine;
using Serilog;

namespace BouncyBox.VorpalEngine.Engine.Bootstrap
{
    /// <summary>
    ///     Creates and runs games.
    /// </summary>
    public static class GameFactory
    {
        private static readonly IReadOnlyDictionary<RunResult, int> ExitCodesByRunResult =
            new ReadOnlyDictionary<RunResult, int>(
                new Dictionary<RunResult, int>
                {
                    { RunResult.Success, 0 },
                    { RunResult.InvalidCommandLineArguments, 1 },
                    { RunResult.UnhandledException, 2 }
                });

        /// <summary>
        ///     <para>Creates and runs a game.</para>
        ///     <para>
        ///         This method wraps common activities like parsing command line parameters, registering components with the IoC container,
        ///         and handling exit codes and unhandled exceptions.
        ///     </para>
        /// </summary>
        /// <param name="initialSceneKey">The scene to load when the game is started.</param>
        /// <param name="args">Raw command line arguments.</param>
        /// <param name="componentRegistrationDelegate">A delegate that will be called when the IoC container registers components.</param>
        /// <returns>Returns an executable exit code.</returns>
        public static int CreateAndRun<TGame, TGameState, TRenderState, TSceneKey>(
            TSceneKey initialSceneKey,
            IEnumerable<string>? args = null,
            Action<ContainerBuilder>? componentRegistrationDelegate = null)
            where TGame : Game<TRenderState, TSceneKey>
            where TGameState : class, new()
            where TRenderState : class, new()
            where TSceneKey : struct, Enum
        {
            // Parse command line arguments

            ProgramOptions? programOptions = null;
            ParserResult<ProgramOptions> parserResult =
                Parser.Default.ParseArguments<ProgramOptions>(args ?? Enumerable.Empty<string>()).WithParsed(a => programOptions = a);

            if (programOptions == null)
            {
                ErrorForm.CreateForInvalidCommandLineArguments(parserResult).ShowDialog();

                return ExitCodesByRunResult[RunResult.InvalidCommandLineArguments];
            }

            // Register default components

            var containerBuilder = new ContainerBuilder();

            RegisterLogging(programOptions, containerBuilder);
            containerBuilder.RegisterType<TGame>().AsSelf().SingleInstance();
            containerBuilder.Register(a => new GameExecutionStateManager(a.Resolve<IInterfaces>())).As<IGameExecutionStateManager>().SingleInstance();
            containerBuilder
                .Register(
                    a => new Interfaces(
                        a.Resolve<ISerilogLogger>(),
                        a.Resolve<IThreadManager>(),
                        a.Resolve<ICommonGameSettings>(),
                        new ConcurrentMessageQueue<IGlobalMessage>(a.Resolve<ISerilogLogger>()),
                        new MessageQueue<IUpdateMessage>(a.Resolve<ISerilogLogger>(), "UpdateMessageQueue"),
                        new MessageQueue<IRenderMessage>(a.Resolve<ISerilogLogger>(), "RenderMessageQueue"),
                        a.Resolve<IKeyboard>(),
                        a.Resolve<IStatefulGamepad>()))
                .As<IInterfaces>()
                .SingleInstance();
            containerBuilder.RegisterType<Keyboard>().As<IKeyboard>().SingleInstance();
            containerBuilder.RegisterType<SceneManager<TGameState, TRenderState, TSceneKey>>().As<ISceneManager<TRenderState>>().SingleInstance();
            containerBuilder.RegisterType<StatefulGamepad>().As<IStatefulGamepad>().SingleInstance();
            containerBuilder.RegisterInstance(new ThreadManager(Thread.CurrentThread)).As<IThreadManager>().SingleInstance();
            containerBuilder.RegisterInstance(programOptions).AsSelf().SingleInstance();

            // Register custom components
            componentRegistrationDelegate?.Invoke(containerBuilder);

            using IContainer container = containerBuilder.Build();
            using ILifetimeScope lifetimeScope = container.BeginLifetimeScope();
            var serilogLogger = lifetimeScope.Resolve<ISerilogLogger>();
            using var game = lifetimeScope.Resolve<TGame>();

            try
            {
                return ExitCodesByRunResult[game.Run(initialSceneKey)];
            }
            catch (Exception exception)
            {
                // Report unhandled exceptions to the user

                serilogLogger.LogError(exception, "An unhandled exception occurred");

                if (Debugger.IsAttached)
                {
                    // Do not display the exception form if a debugger is attached
                    Debugger.Break();
                }
                else
                {
                    ErrorForm.CreateForUnhandledException(exception).ShowDialog();
                }

                return ExitCodesByRunResult[RunResult.UnhandledException];
            }
            finally
            {
                serilogLogger.CloseAndFlush();
            }
        }

        /// <summary>
        ///     Creates logging-related IoC registrations.
        /// </summary>
        /// <param name="options">Program options.</param>
        /// <param name="containerBuilder">An Autofac container builder.</param>
        private static void RegisterLogging(ProgramOptions options, ContainerBuilder containerBuilder)
        {
            LoggerConfiguration loggerConfiguration =
                new LoggerConfiguration()
                    .MinimumLevel.Is(options.MinimumLogLevel)
                    .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss.fffffff} {Level:u3}] {Message:lj}{NewLine}{Exception}");
            var serilogLogger = new SerilogLogger(loggerConfiguration, options.IsLoggingEnabled);

            containerBuilder.RegisterInstance(serilogLogger).As<ISerilogLogger>().SingleInstance();
        }
    }
}