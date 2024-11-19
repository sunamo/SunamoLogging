namespace SunamoLogging.FileLogger;
public class FileLogger(string path) : ILogger
{
    private static readonly object _lock = new();
    private readonly List<object> _scopeData = [];

    public List<LogLevel> LevelsToLog { get; set; } = [LogLevel.Warning | LogLevel.Error | LogLevel.Critical];

    public bool IsEnabled(LogLevel logLevel)
    {
        return LevelsToLog.Contains(logLevel);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
    {
        if (formatter != null)
        {
            lock (_lock)
            {
                string fullFilePath = Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt");
                var n = Environment.NewLine;
                string exc = "";
                if (exception != null)
                {
                    exc = n + exception.GetType() + ": " + exception.Message + n + exception.StackTrace + n;
                    File.AppendAllText(fullFilePath, logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + formatter(state, exception) + n + exc);
                }
                else
                {
                    CL.WriteError($"{nameof(exception)} in {nameof(FileLogger)} was null and cannot be written to file.");
                }
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