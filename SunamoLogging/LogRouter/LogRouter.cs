namespace SunamoLogging.LogRouter;

using Microsoft.Extensions.Logging;

/// <summary>
/// Routes log messages through category-based filtering and raises events for logged entries.
/// </summary>
/// <typeparam name="TCategory">The enum type representing log categories.</typeparam>
/// <param name="settings">The category settings that control which categories are enabled.</param>
public class LogRouter<TCategory>(LogCategorySettingsBase<TCategory> settings)
    where TCategory : struct, Enum
{
    /// <summary>
    /// Event raised when a message is logged.
    /// </summary>
    public event Action<LogEntry<TCategory>>? Logged;

    /// <summary>
    /// Logs a message under the specified category and level.
    /// </summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The log message.</param>
    /// <param name="level">The log level (defaults to Information).</param>
    public void Log(TCategory category, string message, LogLevel level = LogLevel.Information)
    {
        if (!settings.IsEnabled(category)) return;
        Logged?.Invoke(new LogEntry<TCategory>(category, level, message));
    }

    /// <summary>
    /// Creates an ILogger adapter that routes messages through this LogRouter under the specified category.
    /// </summary>
    /// <param name="librariesCategory">The category to use for routed messages.</param>
    /// <returns>An ILogger instance that routes through this LogRouter.</returns>
    public ILogger ToILogger(TCategory librariesCategory) => new LogRouterLogger<TCategory>(this, librariesCategory);
}
