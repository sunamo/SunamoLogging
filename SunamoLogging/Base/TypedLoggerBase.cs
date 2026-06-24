namespace SunamoLogging.Base;

public abstract class TypedLoggerBase
{
    private Action<TypeOfMessageLogging, string, string[]>? typedWriteLineDelegate;

    public TypedLoggerBase(Action<TypeOfMessageLogging, string, string[]> typedWriteLineDelegate)
    {
        this.typedWriteLineDelegate = typedWriteLineDelegate;
    }


    public void WriteLineFormat(string format, params string[] args)
    {
        Ordinal(format, args);
    }

    #region Message Type Methods
    public void Success(string text, params string[] args)
    {
        typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Success, text, args);
    }

    public void Error(string text, params string[] args)
    {
        typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Error, text, args);
    }

    public void Warning(string text, params string[] args)
    {
        typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Warning, text, args);
    }

    public void Appeal(string text, params string[] args)
    {
        typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Appeal, text, args);
    }

    public void Ordinal(string text, params string[] args)
    {
        typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Ordinal, text, args);
    }

    public void WriteLine(TypeOfMessageLogging messageType, string message)
    {
        switch (messageType)
        {
            case TypeOfMessageLogging.Error:
                Error(message);
                break;
            case TypeOfMessageLogging.Warning:
                Warning(message);
                break;
            case TypeOfMessageLogging.Information:
                Information(message);
                break;
            case TypeOfMessageLogging.Ordinal:
                Ordinal(message);
                break;
            case TypeOfMessageLogging.Appeal:
                Appeal(message);
                break;
            case TypeOfMessageLogging.Success:
                Success(message);
                break;
            default:
                ThrowEx.NotImplementedCase(messageType);
                break;
        }
    }

    public void Information(string text, params string[] args)
    {
        typedWriteLineDelegate?.Invoke(TypeOfMessageLogging.Information, text, args);
    }
    #endregion
}
