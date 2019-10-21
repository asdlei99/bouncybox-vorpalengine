using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Autofac;
using BouncyBox.Common.NetStandard21.Logging;
using BouncyBox.VorpalEngine.Engine.Entities;
using BouncyBox.VorpalEngine.Engine.Game;
using BouncyBox.VorpalEngine.Engine.Input.Keyboard;
using BouncyBox.VorpalEngine.Engine.Input.XInput;
using BouncyBox.VorpalEngine.Engine.Logging;
using BouncyBox.VorpalEngine.Engine.Messaging;
using BouncyBox.VorpalEngine.Engine.Scenes;
using BouncyBox.VorpalEngine.Engine.Threads;
using BouncyBox.VorpalEngine.Engine.Windows;
using CommandLine;
using Serilog;

namespace BouncyBox.VorpalEngine.Engine.Bootstrap
{
    /// <summary>Creates and runs games.</summary>
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
        public static int CreateAndRun<TGame, TGameState, TSceneKey>(
            TSceneKey initialSceneKey,
            IEnumerable<string>? args = null,
            Action<ContainerBuilder>? componentRegistrationDelegate = null)
            where TGame : Game<TGameState, TSceneKey>
            where TGameState : class, new()
            where TSceneKey : struct, Enum
        {
            // Verify platform and platform version

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException("Only Microsoft Windows is supported.");
            }
            if (Environment.OSVersion.Version < WindowsVersion.MinimumVersion)
            {
                throw new PlatformNotSupportedException("Only Platform Update for Microsoft Windows 7 or newer is supported.");
            }

            // Parse command line arguments

            ProgramOptions? programOptions = null;
            ParserResult<ProgramOptions> parserResult =
                Parser.Default.ParseArguments<ProgramOptions>(args ?? Enumerable.Empty<string>()).WithParsed(a => programOptions = a);

            if (programOptions == null)
            {
                ErrorForm.CreateForInvalidCommandLineArguments(parserResult).ShowDialog();

                return ExitCodesByRunResult[RunResult.InvalidCommandLineArguments];
            }

            ISerilogLogger serilogLogger = CreateLogger(programOptions);

            serilogLogger.LogVerbose("Registering components with Autofac");

            // Register default components

            var containerBuilder = new ContainerBuilder();
            Thread mainThread = Thread.CurrentThread;

            containerBuilder.RegisterInstance(serilogLogger).As<ISerilogLogger>().SingleInstance();
            containerBuilder.RegisterType<TGame>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<EntityManager<TGameState>>().As<IEntityManager<TGameState>>().SingleInstance();
            containerBuilder.Register(a => new GameExecutionStateManager(a.Resolve<IInterfaces>())).As<IGameExecutionStateManager>().SingleInstance();
            containerBuilder
                .Register(
                    a => new Interfaces(
                        a.Resolve<ISerilogLogger>(),
                        a.Resolve<IThreadManager>(),
                        a.Resolve<ICommonGameSettings>(),
                        new ConcurrentMessageQueue<IGlobalMessage>(a.Resolve<ISerilogLogger>()),
                        new MessageQueue<IUpdateMessage>(a.Resolve<ISerilogLogger>(), "UpdateMessageQueue"),
                        a.Resolve<IKeyboard>(),
                        a.Resolve<IStatefulGamepad>()))
                .As<IInterfaces>()
                .SingleInstance();
            containerBuilder.RegisterType<Keyboard>().As<IKeyboard>().SingleInstance();
            containerBuilder.RegisterType<SceneManager<TGameState, TSceneKey>>().As<ISceneManager>().SingleInstance();
            containerBuilder.RegisterType<StatefulGamepad>().As<IStatefulGamepad>().SingleInstance();
            containerBuilder.Register(a => new ThreadManager(a.Resolve<ISerilogLogger>(), mainThread)).As<IThreadManager>().SingleInstance();
            containerBuilder.RegisterInstance(programOptions).AsSelf().SingleInstance();

            // Register custom components
            componentRegistrationDelegate?.Invoke(containerBuilder);

            using IContainer container = containerBuilder.Build();
            using ILifetimeScope lifetimeScope = container.BeginLifetimeScope();
            using var game = lifetimeScope.Resolve<TGame>();
            string gameTypeName = typeof(TGame).FullName ?? "of unknown type";
            int exitCode;

            serilogLogger = new ContextSerilogLogger(lifetimeScope.Resolve<ISerilogLogger>(), new NestedContext("Program"));

            try
            {
                long startTimestamp = Stopwatch.GetTimestamp();

                try
                {
                    serilogLogger.LogInformation("Running game {Game}", gameTypeName);

                    exitCode = ExitCodesByRunResult[game.Run(initialSceneKey)];
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
                        serilogLogger.LogDebug("Showing unhandled exception window");

                        using ErrorForm errorForm = ErrorForm.CreateForUnhandledException(exception);

                        errorForm.ShowDialog();
                    }

                    exitCode = ExitCodesByRunResult[RunResult.UnhandledException];
                }

                TimeSpan totalGameTime = TimeSpan.FromTicks(Stopwatch.GetTimestamp() - startTimestamp);

                serilogLogger.LogInformation($"Total game time: {totalGameTime.ToString("c", CultureInfo.InvariantCulture)}");
                serilogLogger.LogInformation("Exiting game {Game} with exit code {ExitCode}", gameTypeName, exitCode);
            }
            finally
            {
                serilogLogger.CloseAndFlush();
            }

            return exitCode;
        }

        /// <summary>Creates the application's Serilog logger.</summary>
        /// <param name="options">Program options.</param>
        /// <returns>Returns the Serilog logger.</returns>
        private static ISerilogLogger CreateLogger(ProgramOptions options)
        {
            const string outputTemplate = "[{Timestamp:HH:mm:ss.fffffff} {Level:u3}] {Message:lj}{NewLine}{Exception}";
            LoggerConfiguration loggerConfiguration =
                new LoggerConfiguration()
                    .MinimumLevel.Is(options.MinimumLogLevel);

            switch (options.LogDestination)
            {
                case LogDestination.Debug:
                    loggerConfiguration.WriteTo.Debug(outputTemplate: outputTemplate);
                    break;
                case LogDestination.File:
                    string applicationDirectory =
                        Path.GetDirectoryName((Assembly.GetEntryAssembly() ?? throw new InvalidOperationException()).Location) ??
                        throw new InvalidOperationException();
                    string logPath = Path.Combine(applicationDirectory, $"log-{DateTimeOffset.Now:yyyyMMdd-HHmmss}.txt");

                    loggerConfiguration.WriteTo.File(logPath, outputTemplate: outputTemplate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new SerilogLogger(loggerConfiguration, options.IsLoggingEnabled);
        }
    }
}