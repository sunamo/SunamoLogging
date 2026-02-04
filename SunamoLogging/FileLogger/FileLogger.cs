namespace SunamoLogging.FileLogger;

/// <summary>
/// File-based logger implementation that writes log messages to daily log files.
/// </summary>
public class FileLogger(string path, List<LogLevel> levelsToLog) : ILogger
{
    private static readonly object _lock = new();
    private readonly List<object> _scopeData = [];

    /// <summary>
    /// Gets or sets the log levels that should be logged.
    /// </summary>
    public List<LogLevel> LevelsToLog { get; set; } = levelsToLog;

    /// <summary>
    /// Determines whether the specified log level is enabled.
    /// </summary>
    /// <param name="logLevel">The log level to check.</param>
    /// <returns>True if the log level is enabled.</returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return LevelsToLog.Contains(logLevel);
    }

    /// <summary>
    /// Writes a log entry to the log file.
    /// </summary>
    /// <typeparam name="TState">The type of the state object.</typeparam>
    /// <param name="logLevel">The log level.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="state">The state object.</param>
    /// <param name="exception">The exception (if any).</param>
    /// <param name="formatter">Function to format the message.</param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        if (formatter != null)
        {
            lock (_lock)
            {
                string fullFilePath = Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt");
                var newLine = Environment.NewLine;
                string exceptionText = "";
                if (exception != null)
                {
                    exceptionText = string.Join(newLine, [exception.GetType(), exception.Message, exception.StackTrace]);
                }
                File.AppendAllText(fullFilePath, logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + formatter(state, exception ?? new Exception()) + newLine + exceptionText);
            }
        }
        else
        {
            CL.WriteError($"{nameof(exception)} in {nameof(FileLogger)} was null");
        }
    }

    /// <summary>
    /// Begins a logical operation scope.
    /// </summary>
    /// <typeparam name="TState">The type of the state object.</typeparam>
    /// <param name="state">The state object for the scope.</param>
    /// <returns>A disposable object that ends the logical operation scope on dispose.</returns>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        _scopeData.Add(state);
        return new ScopeDisposable(this, state);
    }
}