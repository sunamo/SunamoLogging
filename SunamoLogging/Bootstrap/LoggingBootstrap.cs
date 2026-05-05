namespace SunamoLogging.Bootstrap;

/// <summary>
/// One-call setup for the „DISKOVÉ LOGY" pattern: wipe Logs/, tee Console to app.log,
/// register crash handler that writes to crash.log, return a configured <see cref="FileLoggerProvider"/>.
/// Intended to be called from app entry point right after the Logs folder is known.
/// </summary>
public static class LoggingBootstrap
{
    /// <summary>
    /// Initializes disk logging end-to-end for an app. Returns a provider ready to be registered
    /// in DI (e.g. <c>CmdBootStrap.AddILogger(services, true, provider, appName)</c>).
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
