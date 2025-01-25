namespace SunamoLogging.FileLogger;

public class FileLoggerProvider(string _path) : ILoggerProvider
{
    public static FileLoggerProvider CustomDirectory(string directory, string appName)
    {
        var filePath = Path.Combine(directory, appName);
        FS.CreateFoldersPsysicallyUnlessThere(filePath);

        return new FileLoggerProvider(filePath);
    }

    /// <summary>
    /// In D:\Logs
    /// </summary>
    /// <param name="appName"></param>
    /// <returns></returns>
    public static FileLoggerProvider DefaultDirectory(string appName)
    {
        return CustomDirectory(@"D:\Logs", appName);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_path);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}