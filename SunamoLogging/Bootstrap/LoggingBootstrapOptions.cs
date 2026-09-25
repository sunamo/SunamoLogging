namespace SunamoLogging.Bootstrap;

/// <summary>
/// Options for <see cref="LoggingBootstrap.Initialize"/>.
/// Defaults align with the global „DISKOVÉ LOGY" rule: wipe at startup, mirror Console to disk,
/// register crash handler, single app.log/crash.log files, all severities incl. Information.
/// </summary>
public class LoggingBootstrapOptions
{
    /// <summary>If true (default), all files in the logs folder are deleted at startup.</summary>
    public bool WipeAtStartup { get; set; } = true;

    /// <summary>If true (default), Console.Out and Console.Error are tee'd to <see cref="AppLogFileName"/>.</summary>
    public bool MirrorConsoleToFile { get; set; } = true;

    /// <summary>If true (default), unhandled and unobserved exceptions are appended to <see cref="CrashLogFileName"/>.</summary>
    public bool RegisterCrashHandler { get; set; } = true;

    /// <summary>Name of the rolling app log file (default <c>app.log</c>).</summary>
    public string AppLogFileName { get; set; } = "app.log";

    /// <summary>Name of the crash log file (default <c>crash.log</c>).</summary>
    public string CrashLogFileName { get; set; } = "crash.log";

    /// <summary>Levels routed to <see cref="FileLogger"/>. Default: Trace…Critical (all).</summary>
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
