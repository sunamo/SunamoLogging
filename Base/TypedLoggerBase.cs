namespace SunamoLogging.Base;



/// <summary>
/// In difference with LoggerBase take type of message as enum
/// </summary>
public abstract class TypedLoggerBase
{
    private static Type type = typeof(TypedLoggerBase);
    private Action<TypeOfMessageLogging, string, string[]> _typedWriteLineDelegate;

    public TypedLoggerBase(Action<TypeOfMessageLogging, string, string[]> typedWriteLineDelegate)
    {
        _typedWriteLineDelegate = typedWriteLineDelegate;
    }

#if !DEBUG2
    public TypedLoggerBase()
    {

    }
#endif




    /// <summary>
    /// Only due to Old sfw apps
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="name"></param>
    /// <param name="v2"></param>
    public void WriteLineFormat(string v1, params string[] name)
    {
        Ordinal(v1, name);
    }

    #region 
    public void Success(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageLogging.Success, text, p);
    }

    public void Error(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageLogging.Error, text, p);
    }
    public void Warning(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageLogging.Warning, text, p);
    }

    public void Appeal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageLogging.Appeal, text, p);
    }

    public void Ordinal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageLogging.Ordinal, text, p);
    }

    public void WriteLine(TypeOfMessageLogging t, string m)
    {
        switch (t)
        {
            case TypeOfMessageLogging.Error:
                Error(m);
                break;
            case TypeOfMessageLogging.Warning:
                Warning(m);
                break;
            case TypeOfMessageLogging.Information:
                Information(m);
                break;
            case TypeOfMessageLogging.Ordinal:
                Ordinal(m);
                break;
            case TypeOfMessageLogging.Appeal:
                Appeal(m);
                break;
            case TypeOfMessageLogging.Success:
                Success(m);
                break;
            default:
                ThrowEx.NotImplementedCase(t);
                break;
        }
    }

    public void Information(string text, params string[] p)
    {

        _typedWriteLineDelegate.Invoke(TypeOfMessageLogging.Information, text, p);
    }
    #endregion
}
