namespace SunamoLogging.FileLogger;
public class FileLogger(string path, List<LogLevel> levelsToLog) : ILogger
{
    private static readonly object _lock = new();
    private readonly List<object> _scopeData = [];

    public List<LogLevel> LevelsToLog { get; set; } = levelsToLog;

    public bool IsEnabled(LogLevel logLevel)
    {
        return LevelsToLog.Contains(logLevel);
    }

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
                var n = Environment.NewLine;
                string exc = "";
                if (exception != null)
                {
                    exc = string.Join(n, [exception.GetType(), exception.Message, exception.StackTrace]);

                }
                File.AppendAllText(fullFilePath, logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + formatter(state, exception ?? new Exception()) + n + exc);
            }
        }
        else
        {
            CL.WriteError($"{nameof(exception)} in {nameof(FileLogger)} was null");
        }
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        _scopeData.Add(state); // Přidání dat do scope
        return new ScopeDisposable(this, state); // V
    }
}