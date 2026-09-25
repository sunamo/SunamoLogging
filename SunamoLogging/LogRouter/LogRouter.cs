namespace SunamoLogging.LogRouter;

using Microsoft.Extensions.Logging;

public class LogRouter<TCategory>(LogCategorySettingsBase<TCategory> settings)
    where TCategory : struct, Enum
{
    public event Action<LogEntry<TCategory>>? Logged;

    public void Log(TCategory category, string message, LogLevel level = LogLevel.Information)
    {
        if (!settings.IsEnabled(category)) return;
        Logged?.Invoke(new LogEntry<TCategory>(category, level, message));
    }

    public ILogger ToILogger(TCategory librariesCategory) => new LogRouterLogger<TCategory>(this, librariesCategory);
}
