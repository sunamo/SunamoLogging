namespace SunamoLogging.LogRouter;

using Microsoft.Extensions.Logging;

/// <summary>
/// ILogger adapter that routes log messages through a LogRouter.
/// </summary>
/// <typeparam name="TCategory">The enum type representing log categories.</typeparam>
/// <param name="logRouter">The log router to route messages through.</param>
/// <param name="librariesCategory">The category to use for routed messages.</param>
internal class LogRouterLogger<TCategory>(LogRouter<TCategory> logRouter, TCategory librariesCategory) : ILogger
    where TCategory : struct, Enum
{
    /// <summary>
    /// Begins a logical operation scope. Returns null as scopes are not supported.
    /// </summary>
    /// <typeparam name="TState">The type of the state object.</typeparam>
    /// <param name="state">The state object for the scope.</param>
    /// <returns>Null as scopes are not supported by LogRouter.</returns>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    /// <summary>
    /// Determines whether the specified log level is enabled. Always returns true.
    /// </summary>
    /// <param name="logLevel">The log level to check.</param>
    /// <returns>Always true.</returns>
    public bool IsEnabled(LogLevel logLevel) => true;

    /// <summary>
    /// Writes a log entry by formatting the message and routing it through the LogRouter.
    /// </summary>
    /// <typeparam name="TState">The type of the state object.</typeparam>
    /// <param name="logLevel">The log level.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="state">The state object.</param>
    /// <param name="exception">The exception (if any).</param>
    /// <param name="formatter">Function to format the message.</param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        if (exception != null)
            message += $" | {exception.GetType().Name}: {exception.Message}";
        if (!string.IsNullOrEmpty(message))
            logRouter.Log(librariesCategory, message, logLevel);
    }
}

/// <summary>
/// ILoggerProvider that creates LogRouterLogger instances.
/// </summary>
/// <typeparam name="TCategory">The enum type representing log categories.</typeparam>
/// <param name="logRouter">The log router to use for created loggers.</param>
/// <param name="librariesCategory">The category to assign to created loggers.</param>
public class LogRouterLoggerProvider<TCategory>(LogRouter<TCategory> logRouter, TCategory librariesCategory) : ILoggerProvider
    where TCategory : struct, Enum
{
    /// <summary>
    /// Creates a logger instance for the specified category name.
    /// </summary>
    /// <param name="categoryName">The category name (unused, librariesCategory is used instead).</param>
    /// <returns>A new LogRouterLogger instance.</returns>
    public ILogger CreateLogger(string categoryName) => new LogRouterLogger<TCategory>(logRouter, librariesCategory);

    /// <summary>
    /// Disposes the logger provider.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
