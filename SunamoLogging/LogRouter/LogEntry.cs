namespace SunamoLogging.LogRouter;

using Microsoft.Extensions.Logging;

public record LogEntry<TCategory>(TCategory Category, LogLevel Level, string Message)
    where TCategory : struct, Enum;
