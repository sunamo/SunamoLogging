namespace SunamoLogging.FileLogger;

public class FileLogger(string path, List<LogLevel> levelsToLog, string? logFileName = null) : ILogger
{
    private static readonly object lockObject = new();
    private readonly List<object> scopeData = [];

    public List<LogLevel> LevelsToLog { get; set; } = levelsToLog;

    public bool IsEnabled(LogLevel logLevel) => LevelsToLog.Contains(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        if (formatter is null)
        {
            CL.WriteError($"{nameof(formatter)} in {nameof(FileLogger)} was null");
            return;
        }

        lock (lockObject)
        {
            var fileName = string.IsNullOrEmpty(logFileName)
                ? DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt"
                : logFileName!;
            var fullFilePath = Path.Combine(path, fileName);

            var sb = new StringBuilder();
            sb.Append('[').Append(DateTime.Now.ToString("O")).Append("] ");
            sb.Append(logLevel.ToString()).Append(": ");
            sb.AppendLine(formatter(state, exception ?? new Exception()));
            if (exception is not null)
            {
                sb.AppendLine(exception.ToString());
            }
            var line = sb.ToString();

            // Best-effort write: file may be locked by OneDrive sync, AV scanner, parallel process, etc.
            // A logging side-effect must NEVER crash the host app, so retry briefly and fall back to Console.Error.
            const int maxAttempts = 4;
            Exception? lastEx = null;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    File.AppendAllText(fullFilePath, line);
                    return;
                }
                catch (IOException ex)
                {
                    lastEx = ex;
                    if (attempt < maxAttempts) Thread.Sleep(50 * attempt);
                }
                catch (UnauthorizedAccessException ex)
                {
                    lastEx = ex;
                    if (attempt < maxAttempts) Thread.Sleep(50 * attempt);
                }
                catch (Exception ex)
                {
                    lastEx = ex;
                    break;
                }
            }
            try
            {
                Console.Error.Write($"[FileLogger fallback - write to '{fullFilePath}' failed: {lastEx?.GetType().Name}: {lastEx?.Message}] {line}");
            }
            catch (Exception)
            {
                // If even Console.Error is unusable, swallow - logging must never throw upward.
            }
        }
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        scopeData.Add(state);
        return new ScopeDisposable(this, state);
    }
}
