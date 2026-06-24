namespace SunamoLogging.Bootstrap;

// Defaults align with the global "DISKOVE LOGY" rule: wipe at startup, mirror Console to disk,
// register crash handler, single app.log/crash.log files, all severities incl. Information.
public class LoggingBootstrapOptions
{
    // If true (default), all files in the logs folder are deleted at startup.
    public bool WipeAtStartup { get; set; } = true;

    // If true (default), Console.Out and Console.Error are tee'd to AppLogFileName.
    public bool MirrorConsoleToFile { get; set; } = true;

    // If true (default), unhandled and unobserved exceptions are appended to CrashLogFileName.
    public bool RegisterCrashHandler { get; set; } = true;

    // Name of the rolling app log file (default app.log).
    public string AppLogFileName { get; set; } = "app.log";

    // Name of the crash log file (default crash.log).
    public string CrashLogFileName { get; set; } = "crash.log";

    // Levels routed to FileLogger. Default: Trace...Critical (all).
    public List<LogLevel> LevelsToLog { get; set; } =
    [
        LogLevel.Trace,
        LogLevel.Debug,
        LogLevel.Information,
        LogLevel.Warning,
        LogLevel.Error,
        LogLevel.Critical,
    ];
}
