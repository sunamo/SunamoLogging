namespace SunamoLogging.FileLogger;

/// <summary>
/// File-based logger implementation. Writes either to a fixed file (when <c>logFileName</c> is set)
/// or to per-day rotating files <c>yyyy-MM-dd_log.txt</c> when <c>logFileName</c> is null/empty.
/// Format: <c>[ISO timestamp] LEVEL: message</c> on its own line; exceptions append <c>ex.ToString()</c>
/// on subsequent lines.
/// </summary>
public class FileLogger(string path, List<LogLevel> levelsToLog, string? logFileName = null) : ILogger
{
    private static readonly object lockObject = new();
    private readonly List<object> scopeData = [];

    /// <summary>
    /// Gets or sets the log levels that should be logged.
    /// </summary>
    public List<LogLevel> LevelsToLog { get; set; } = levelsToLog;

    /// <summary>
    /// Determines whether the specified log level is enabled.
    /// </summary>
    public bool IsEnabled(LogLevel logLevel)
    {
        return LevelsToLog.Contains(logLevel);
    }

    /// <summary>
    /// Writes a log entry to the log file.
    /// </summary>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        if (formatter == null)
        {
            CL.WriteError($"{nameof(formatter)} in {nameof(FileLogger)} was null");
            return;
        }

        lock (lockObject)
        {
            string fileName = string.IsNullOrEmpty(logFileName)
                ? DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt"
                : logFileName!;
            string fullFilePath = Path.Combine(path, fileName);

            var sb = new StringBuilder();
            sb.Append('[').Append(DateTime.Now.ToString("O")).Append("] ");
            sb.Append(logLevel.ToString()).Append(": ");
            sb.AppendLine(formatter(state, exception ?? new Exception()));
            if (exception != null)
            {
                sb.AppendLine(exception.ToString());
            }
            File.AppendAllText(fullFilePath, sb.ToString());
        }
    }

    /// <summary>
    /// Begins a logical operation scope.
    /// </summary>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        scopeData.Add(state);
        return new ScopeDisposable(this, state);
    }
}
