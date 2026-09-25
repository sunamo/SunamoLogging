namespace SunamoLogging.FileLogger;

public static class FileLoggerExtensions
{
    public static ILoggerFactory AddFile(this ILoggerFactory factory, string appName)
    {
        factory.AddProvider(FileLoggerProvider.DefaultDirectory(appName));
        return factory;
    }
}
