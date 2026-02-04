namespace SunamoLogging.FileLogger;

/// <summary>
/// Disposable scope object for logger scopes.
/// </summary>
internal class ScopeDisposable : IDisposable
{
    private readonly ILogger _logger;
    private readonly object _state;

    /// <summary>
    /// Initializes a new instance of the ScopeDisposable class.
    /// </summary>
    /// <param name="logger">The logger associated with this scope.</param>
    /// <param name="state">The state object for this scope.</param>
    public ScopeDisposable(ILogger logger, object state)
    {
        _logger = logger;
        _state = state;
    }

    /// <summary>
    /// Disposes the scope.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}