namespace SunamoLogging.Bootstrap;

using Microsoft.Extensions.DependencyInjection;
using SunamoCl.SunamoCmd;
using SunamoDependencyInjection;
using SunamoPlatformUwpInterop.AppData;
using SunamoPlatformUwpInterop.Args;
using SunamoPlatformUwpInterop._public.SunamoEnums.Enums;

public static class LoggingBootstrap
{
    // Single-call console-app bootstrap: AppData folders, log wipe, Console tee, crash handler,
    // FileLoggerProvider, ServiceCollection with AddServicesEndingWithService(),
    // ServiceProvider built, ILogger resolved.
    public static ConsoleAppContext InitConsoleApp(string appName, LoggingBootstrapOptions? options = null, Action<IServiceCollection>? configureServices = null)
    {
        AppData.Instance.CreateAppFoldersIfDontExists(new CreateAppFoldersIfDontExistsArgs { AppName = appName });
        var logsFolder = AppData.Instance.GetFolder(AppFolders.Logs);
        var fileLoggerProvider = Initialize(logsFolder, options);

        var services = new ServiceCollection();
        CmdBootStrap.AddILogger(services, true, fileLoggerProvider, appName);
        services.AddServicesEndingWithService(NullLogger.Instance, [], isAddingFromReferencedSunamoAssemblies: true);
        configureServices?.Invoke(services);
        var provider = services.BuildServiceProvider();
        var logger = provider.GetService<ILogger>() ?? NullLogger.Instance;
        return new ConsoleAppContext(services, provider, logger);
    }

    // Initializes disk logging end-to-end for an app. Returns a provider ready to be registered in DI.
    // Use this if you need finer control than InitConsoleApp.
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
