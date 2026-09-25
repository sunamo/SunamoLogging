namespace SunamoLogging.LogRouter;

using Microsoft.Extensions.Logging;

/// <summary>
/// Represents a single log entry with category, level, and message.
/// </summary>
/// <typeparam name="TCategory">The enum type representing log categories.</typeparam>
/// <param name="Category">The log category.</param>
/// <param name="Level">The log level.</param>
/// <param name="Message">The log message.</param>
public record LogEntry<TCategory>(TCategory Category, LogLevel Level, string Message)
    where TCategory : struct, Enum;
