namespace RunnerLogging;

internal class LoggerOuter(LoggerInner loggerInner)
{
    public void Log()
    {
        loggerInner.Log();
    }
}
