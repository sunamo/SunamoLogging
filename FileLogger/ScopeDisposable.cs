namespace SunamoLogging.FileLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ScopeDisposable : IDisposable
{
private readonly ILogger _logger;
private readonly object _state;
public ScopeDisposable(ILogger logger, object state)
{
_logger = logger;
_state = state;
}
public void Dispose()
{
GC.SuppressFinalize(this);
}
}