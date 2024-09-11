namespace SunamoLogging.FileLogger;

public class FileLoggerProvider : ILoggerProvider
{
    private string path;
    public FileLoggerProvider(string _path)
    {
        path = _path;
    }

    public static FileLoggerProvider CustomDirectory(string directory, string appName)
    {
        var filePath = Path.Combine(directory, appName);
        FS.CreateFoldersPsysicallyUnlessThere(filePath);

        return new FileLoggerProvider(filePath);
    }

    public static FileLoggerProvider DefaultDirectory(string appName)
    {
        return CustomDirectory(@"D:\Logs", appName);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path);
    }

    public void Dispose()
    {
    }
}