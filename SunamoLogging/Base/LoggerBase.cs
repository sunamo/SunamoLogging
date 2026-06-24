namespace SunamoLogging.Base;

public abstract class LoggerBase(Action<string, string[]> writeLineDelegate) : ILoggerBase
{
    protected Action<string, string[]> writeLineDelegate = writeLineDelegate;

    public bool IsActive { get; set; } = true;

    // Only for debug purposes.
    public void ClipboardOrDebug(string text, params string[] args)
    {
    }

    // Legacy compatibility method for old applications.
    public void WriteLineFormat(string format, params string[] args)
    {
        WriteLine(format, args);
    }

    public void WriteCount(string collectionName, IList list)
    {
        WriteLine(collectionName + " count: " + list.Count);
    }

    public void WriteList(string collectionName, List<string> list)
    {
        WriteLine(collectionName + " elements:");
        WriteList(list);
    }

    // Only outputs in DEBUG builds.
    public void WriteListOneRow(List<string> list, string separator)
    {
    }

    public void WriteArgs(params string[] args)
    {
        writeLineDelegate.Invoke(string.Join(";", args), []);
    }

    public bool IsInRightFormat(string text, params string[] args)
    {
        try
        {
            writeLineDelegate.Invoke(text, args);
        }
        catch (Exception ex)
        {
            ThrowEx.CustomWithStackTrace(ex);
            return false;
        }

        return true;
    }

    public void WriteLine(string text, params string[] args)
    {
        if (IsActive)
        {
            writeLineDelegate.Invoke(text, args);
        }
    }

    public void WriteLineNull(string text, params string[] args)
    {
        if (IsActive)
        {
            writeLineDelegate.Invoke(SH.NullToStringOrDefault(text), args);
        }
    }

    // For compatibility with CL.WriteLine.
    public void WriteLine(string text)
    {
        if (text != null)
        {
            writeLineDelegate.Invoke(text, []);
        }
    }

    public void WriteLine(string label, object value)
    {
        value ??= "(null)";

        string prefix = string.Empty;
        if (!string.IsNullOrEmpty(label))
        {
            prefix = label + ": ";
        }

        WriteLine(prefix + value.ToString());
    }

    public void WriteNumberedList(string label, List<string> list, bool isNumbered)
    {
        writeLineDelegate.Invoke(label + ":", []);
        for (int i = 0; i < list.Count; i++)
        {
            if (isNumbered)
            {
                WriteLine((i + 1).ToString(), list[i]);
            }
            else
            {
                WriteLine(list[i]);
            }
        }
    }

    public void WriteList(List<string> list)
    {
        list.ForEach(element => WriteLine(element));
    }

    public void TwoState(bool state, params string[] additionalData)
    {
        WriteLine(state.ToString() + "," + string.Join(',', additionalData));
    }
}
