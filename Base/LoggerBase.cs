namespace SunamoLogging.Base;
public abstract class LoggerBase(Action<string, string[]> writeLineDelegate) : ILoggerBase
{
    // TODO: Make logger public class as base and replace all occurences With Instance
    protected Action<string, string[]> _writeLineDelegate = writeLineDelegate;
    public bool IsActive = true;

    /// <summary>
    /// Only for debug purposes
    /// </summary>
    /// <param name = "v"></param>
    /// <param name = "args"></param>
    public void ClipboardOrDebug(string v, params string[] args)
    {
#if DEBUG
        //DebugLogger.DebugWriteLine(TypeOfMessage.Appeal, v, args);
#else
//sb.AppendLine(TypeOfMessage.Appeal + ": " + string.Format(v, args));
//ClipboardHelper.SetText(sb.ToString());
#endif
    }

    /// <summary>
    /// Only due to Old sfw apps
    /// </summary>
    /// <param name = "v1"></param>
    /// <param name = "name"></param>
    public void WriteLineFormat(string v1, params string[] name)
    {
        WriteLine(v1, name);
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

    public void WriteListOneRow(List<string> item, string swd)
    {


#if DEBUG
        _writeLineDelegate.Invoke(string.Join(swd, item), []);
#endif
    }

    public void WriteArgs(params string[] args)
    {
        _writeLineDelegate.Invoke(/*SHJoinPairs.JoinPairs(args)*/ string.Join(";", args), []);
    }

    public bool IsInRightFormat(string text, params string[] args)
    {
        try
        {
            _writeLineDelegate.Invoke(text, args);
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
            _writeLineDelegate.Invoke(text, args);
        }
    }

    public void WriteLineNull(string text, params string[] args)
    {
        if (IsActive)
        {
            _writeLineDelegate.Invoke(SH.NullToStringOrDefault(text), args);
        }
    }

    /// <summary>
    /// for compatibility with CL.WriteLine
    /// </summary>
    /// <param name = "what"></param>
    public void WriteLine(string what)
    {
        if (what != null)
        {
            _writeLineDelegate.Invoke(what, []);
        }
    }

    /// <summary>
    /// Will auto append ": "
    /// </summary>
    /// <param name="what"></param>
    /// <param name="text"></param>
    public void WriteLine(string what, object text)
    {
        text ??= "(null)";



        string append = string.Empty;
        if (!string.IsNullOrEmpty(what))
        {
            append = what + ": ";
        }

        WriteLine(append + text.ToString());

    }

    public void WriteNumberedList(string what, List<string> list, bool numbered)
    {
        _writeLineDelegate.Invoke(what + ":", []);
        for (int i = 0; i < list.Count; i++)
        {
            if (numbered)
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
        list.ForEach(d => WriteLine(d));
    }

    public void TwoState(bool ret, params string[] toAppend)
    {
        WriteLine(ret.ToString() + "," + string.Join(',', toAppend));
    }
}