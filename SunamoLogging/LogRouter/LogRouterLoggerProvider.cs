namespace SunamoLogging.LogRouter;

using Microsoft.Extensions.Logging;

internal class LogRouterLogger<TCategory>(LogRouter<TCategory> logRouter, TCategory librariesCategory) : ILogger
    where TCategory : struct, Enum
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        if (exception != null)
            message += $" | {exception.GetType().Name}: {exception.Message}";
        if (!string.IsNullOrEmpty(message))
            logRouter.Log(librariesCategory, message, logLevel);
    }
}

public class LogRouterLoggerProvider<TCategory>(LogRouter<TCategory> logRouter, TCategory librariesCategory) : ILoggerProvider
    where TCategory : struct, Enum
{
    public ILogger CreateLogger(string categoryName) => new LogRouterLogger<TCategory>(logRouter, librariesCategory);

    public void Dispose() { }
}
