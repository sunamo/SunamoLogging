namespace SunamoLogging.Bootstrap;

using Microsoft.Extensions.DependencyInjection;
using SunamoCl.SunamoCmd;
using SunamoDependencyInjection;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Args;
using SunamoPlatformUwpInterop._public.SunamoEnums.Enums;

/// <summary>
/// One-call setup for the „DISKOVÉ LOGY" pattern: wipe Logs/, tee Console to app.log,
/// register crash handler that writes to crash.log, return a configured <see cref="FileLoggerProvider"/>.
/// Intended to be called from app entry point right after the Logs folder is known.
/// </summary>
public static class LoggingBootstrap
{
    /// <summary>
    /// Single-call console-app bootstrap: AppData folders, log wipe, Console tee, crash handler,
    /// FileLoggerProvider, ServiceCollection with <c>AddServicesEndingWithService()</c>,
    /// ServiceProvider built, ILogger resolved. App calls this once and gets back everything.
    /// Use this from app static ctor: <c>var ctx = LoggingBootstrap.InitConsoleApp("MyApp");</c>
    /// </summary>
    public static ConsoleAppContext InitConsoleApp(string appName, LoggingBootstrapOptions? options = null, Action<IServiceCollection>? configureServices = null)
    {
        AppData.ci.CreateAppFoldersIfDontExists(new CreateAppFoldersIfDontExistsArgs { AppName = appName });
        var logsFolder = AppData.ci.GetFolder(AppFolders.Logs);
        var fileLoggerProvider = Initialize(logsFolder, options);

        var services = new ServiceCollection();
        CmdBootStrap.AddILogger(services, true, fileLoggerProvider, appName);
        services.AddServicesEndingWithService(NullLogger.Instance, [], isAddingFromReferencedSunamoAssemblies: true);
        configureServices?.Invoke(services);
        var provider = services.BuildServiceProvider();
        var logger = provider.GetService<ILogger>() ?? NullLogger.Instance;
        return new ConsoleAppContext(services, provider, logger);
    }

    /// <summary>
    /// Initializes disk logging end-to-end for an app. Returns a provider ready to be registered
    /// in DI (e.g. <c>CmdBootStrap.AddILogger(services, true, provider, appName)</c>).
    /// Use this if you need finer control than <see cref="InitConsoleApp"/>.
    /// </summary>
    public static FileLoggerProvider Initialize(string logsFolder, LoggingBootstrapOptions? options = null)
    {
        options ??= new LoggingBootstrapOptions();
        if (string.IsNullOrEmpty(logsFolder)) throw new ArgumentException("logsFolder is required", nameof(logsFolder));
        Directory.CreateDirectory(logsFolder);

        if (options.WipeAtStartup)
        {
            FileLoggerProvider.WipeDirectory(logsFolder);
        }

        if (options.MirrorConsoleToFile)
        {
            ConsoleTee.Install(Path.Combine(logsFolder, options.AppLogFileName));
        }

        if (options.RegisterCrashHandler)
        {
            CrashHandler.Register(Path.Combine(logsFolder, options.CrashLogFileName));
        }

        var provider = new FileLoggerProvider(logsFolder)
        {
            LevelsToLog = options.LevelsToLog,
            LogFileName = options.AppLogFileName,
        };
        return provider;
    }
}