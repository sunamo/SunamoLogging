namespace SunamoLogging.Base;

/// <summary>
/// Base logger class implementing common logging operations.
/// </summary>
public abstract class LoggerBase(Action<string, string[]> writeLineDelegate) : ILoggerBase
{
    /// <summary>
    /// The delegate used to write log lines.
    /// </summary>
    protected Action<string, string[]> _writeLineDelegate = writeLineDelegate;

    /// <summary>
    /// Gets or sets whether the logger is active and should write output.
    /// </summary>
    public bool IsActive = true;

    /// <summary>
    /// Writes output to clipboard in release mode or debug output in debug mode.
    /// Only for debug purposes.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="args">Format arguments.</param>
    public void ClipboardOrDebug(string text, params string[] args)
    {
#if DEBUG

#else

#endif
    }

    /// <summary>
    /// Writes a formatted line. Legacy compatibility method for old applications.
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">Format arguments.</param>
    public void WriteLineFormat(string format, params string[] args)
    {
        WriteLine(format, args);
    }

    /// <summary>
    /// Writes the count of elements in a collection.
    /// </summary>
    /// <param name="collectionName">The name of the collection.</param>
    /// <param name="list">The collection to count.</param>
    public void WriteCount(string collectionName, IList list)
    {
        WriteLine(collectionName + " count: " + list.Count);
    }

    /// <summary>
    /// Writes a labeled list with all elements on separate lines.
    /// </summary>
    /// <param name="collectionName">The name/label of the collection.</param>
    /// <param name="list">The list to write.</param>
    public void WriteList(string collectionName, List<string> list)
    {
        WriteLine(collectionName + " elements:");
        WriteList(list);
    }

    /// <summary>
    /// Writes all list elements in a single row separated by a delimiter.
    /// Only outputs in DEBUG builds.
    /// </summary>
    /// <param name="list">The list to write.</param>
    /// <param name="separator">The separator between elements.</param>
    public void WriteListOneRow(List<string> list, string separator)
    {
#if DEBUG
        _writeLineDelegate.Invoke(string.Join(separator, list), []);
#endif
    }

    /// <summary>
    /// Writes arguments separated by semicolons.
    /// </summary>
    /// <param name="args">The arguments to write.</param>
    public void WriteArgs(params string[] args)
    {
        _writeLineDelegate.Invoke(string.Join(";", args), []);
    }

    /// <summary>
    /// Checks if the text is in the right format with the provided arguments.
    /// </summary>
    /// <param name="text">The format string to validate.</param>
    /// <param name="args">Format arguments.</param>
    /// <returns>True if the format is valid, false otherwise.</returns>
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

    /// <summary>
    /// Writes a formatted line if the logger is active.
    /// </summary>
    /// <param name="text">The format string.</param>
    /// <param name="args">Format arguments.</param>
    public void WriteLine(string text, params string[] args)
    {
        if (IsActive)
        {
            _writeLineDelegate.Invoke(text, args);
        }
    }

    /// <summary>
    /// Writes a line, converting null values to "(null)" string.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="args">Format arguments.</param>
    public void WriteLineNull(string text, params string[] args)
    {
        if (IsActive)
        {
            _writeLineDelegate.Invoke(SH.NullToStringOrDefault(text), args);
        }
    }

    /// <summary>
    /// Writes a simple line without formatting.
    /// For compatibility with CL.WriteLine.
    /// </summary>
    /// <param name="text">The text to write.</param>
    public void WriteLine(string text)
    {
        if (text != null)
        {
            _writeLineDelegate.Invoke(text, []);
        }
    }

    /// <summary>
    /// Writes a labeled line with automatic ": " separator.
    /// </summary>
    /// <param name="label">The label/prefix.</param>
    /// <param name="value">The value to write.</param>
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

    /// <summary>
    /// Writes a numbered or unnumbered list.
    /// </summary>
    /// <param name="label">The label/header for the list.</param>
    /// <param name="list">The list to write.</param>
    /// <param name="isNumbered">Whether to number the list items.</param>
    public void WriteNumberedList(string label, List<string> list, bool isNumbered)
    {
        _writeLineDelegate.Invoke(label + ":", []);
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

    /// <summary>
    /// Writes all elements of a list, each on a separate line.
    /// </summary>
    /// <param name="list">The list to write.</param>
    public void WriteList(List<string> list)
    {
        list.ForEach(element => WriteLine(element));
    }

    /// <summary>
    /// Writes a two-state (boolean) value with additional data.
    /// </summary>
    /// <param name="state">The state value.</param>
    /// <param name="additionalData">Additional data to append.</param>
    public void TwoState(bool state, params string[] additionalData)
    {
        WriteLine(state.ToString() + "," + string.Join(',', additionalData));
    }
}