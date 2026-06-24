namespace SunamoLogging.FileLogger;

internal class ScopeDisposable : IDisposable
{
    private readonly ILogger logger;
    private readonly object state;

    public ScopeDisposable(ILogger logger, object state)
    {
        this.logger = logger;
        this.state = state;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
