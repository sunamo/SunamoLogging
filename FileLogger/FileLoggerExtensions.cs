namespace SunamoLogging.FileLogger;

public static class FileLoggerExtensions
{
    public static ILoggerFactory AddFile(this ILoggerFactory factory, string appName)
    {
        //var filePath = Path.Combine(basePath, $"{DateTime.Now:yyyy-MM-dd}" + ".txt");
        factory.AddProvider(FileLoggerProvider.DefaultDirectory(appName));
        return factory;
    }
}