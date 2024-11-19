namespace SunamoLogging.FileLogger;
public class FileLogger : ILogger
{
    private string filePath;
    private static object _lock = new object();

    public List<LogLevel> LevelsToLog { get; set; } = [LogLevel.Warning | LogLevel.Error | LogLevel.Critical];

    public FileLogger(string path)
    {
        filePath = path;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return LevelsToLog.Contains(logLevel);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (formatter != null)
        {
            lock (_lock)
            {
                string fullFilePath = Path.Combine(filePath, DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt");
                var n = Environment.NewLine;
                string exc = "";
                if (exception != null) exc = n + exception.GetType() + ": " + exception.Message + n + exception.StackTrace + n;
                File.AppendAllText(fullFilePath, logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + formatter(state, exception) + n + exc);
            }
        }
    }
}