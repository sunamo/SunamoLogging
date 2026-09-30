namespace SunamoLogging.Bootstrap;

using Microsoft.Extensions.DependencyInjection;
using SunamoLogging._sunamo.SunamoDependencyInjection;

/// <summary>
/// One-call setup for the „DISKOVÉ LOGY" pattern: wipe Logs/, tee Console to app.log,
/// register crash handler that writes to crash.log, return a configured <see cref="FileLoggerProvider"/>.
/// Intended to be called from app entry point right after the Logs folder is known.
/// </summary>
public static class LoggingBootstrap
{
    /// <summary>
    /// Single-call console-app bootstrap: logs folder under %LOCALAPPDATA%\_Sunamo, log wipe, Console tee, crash handler,
    /// FileLoggerProvider, ServiceCollection with <c>AddServicesEndingWithService()</c>,
    /// ServiceProvider built, ILogger resolved. App calls this once and gets back everything.
    /// Use this from app static ctor: <c>var ctx = LoggingBootstrap.InitConsoleApp("MyApp");</c>
    /// </summary>
    public static ConsoleAppContext InitConsoleApp(string appName, LoggingBootstrapOptions? options = null, Action<IServiceCollection>? configureServices = null)
    {
        var logsFolder = GetLogsFolder(appName);
        var fileLoggerProvider = Initialize(logsFolder, options);

        var services = new ServiceCollection();
        AddILogger(services, true, fileLoggerProvider, appName);
        services.AddServicesEndingWithService(NullLogger.Instance);
        configureServices?.Invoke(services);
        var provider = services.BuildServiceProvider();
        var logger = provider.GetService<ILogger>() ?? NullLogger.Instance;
        return new ConsoleAppContext(services, provider, logger);
    }

    /// <summary>
    /// Initializes disk logging end-to-end for an app. Returns a provider ready to be registered
    /// in DI (e.g. <c>AddILogger(services, true, provider, appName)</c>).
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

    /// <summary>
    /// Returns the non-backed-up logs folder of the app (%LOCALAPPDATA%\_Sunamo\AppName\Logs\), same location the AppData helper used.
    /// </summary>
    /// <param name="appName">Name of the application.</param>
    private static string GetLogsFolder(string appName)
    {
        var localRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "_Sunamo", appName);
        return Path.Combine(localRoot, "Logs").TrimEnd('\\') + "\\";
    }

    /// <summary>
    /// Registers logging (optional console output, the given file provider) and a singleton ILogger with the given category into the service collection.
    /// </summary>
    /// <param name="services">Service collection to register into.</param>
    /// <param name="isLoggingToConsole">Whether the console logger is added.</param>
    /// <param name="fileLoggerProvider">Optional provider added to the logger factory.</param>
    /// <param name="categoryName">Category name of the registered logger.</param>
    public static void AddILogger(IServiceCollection? services, bool isLoggingToConsole, ILoggerProvider? fileLoggerProvider, string categoryName)
    {
        if (services != null)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                if (isLoggingToConsole)
                {
                    loggingBuilder.AddConsole();
                }
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            });
            // The logger has to be created from a single factory, otherwise a new ILogger would be created for each file passed.
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            if (fileLoggerProvider != null)
            {
                loggerFactory.AddProvider(fileLoggerProvider);
            }
            if (categoryName is null)
            {
                throw new ArgumentNullException(nameof(categoryName));
            }
            var logger = loggerFactory.CreateLogger(categoryName);
            services.AddSingleton(typeof(ILogger), logger);
        }
        else if (isLoggingToConsole || fileLoggerProvider != null)
        {
            throw new Exception($"{nameof(services)} is null but {nameof(isLoggingToConsole)}/{nameof(fileLoggerProvider)} is set up");
        }
    }
}
