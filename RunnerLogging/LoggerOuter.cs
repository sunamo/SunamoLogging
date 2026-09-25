namespace RunnerLogging;

/// <summary>
/// Outer logger that delegates to LoggerInner for demonstration of DI composition.
/// </summary>
/// <param name="loggerInner">The inner logger to delegate to.</param>
internal class LoggerOuter(LoggerInner loggerInner)
{
    /// <summary>
    /// Logs test messages by delegating to the inner logger.
    /// </summary>
    public void Log()
    {
        loggerInner.Log();
    }
}
