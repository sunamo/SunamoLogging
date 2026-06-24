namespace SunamoLogging._public.SunamoInterfaces.Interfaces;

public interface ILoggerBase
{
    void ClipboardOrDebug(string text, params string[] args);

    bool IsInRightFormat(string text, params string[] args);

    void TwoState(bool state, params string[] additionalData);

    void WriteCount(string collectionName, IList list);

    void WriteLine(string label, object value);

    void WriteLine(string text, params string[] args);

    void WriteLineFormat(string format, params string[] args);

    void WriteLineNull(string text, params string[] args);

    void WriteList(List<string> list);

    void WriteList(string collectionName, List<string> list);

    void WriteListOneRow(List<string> list, string separator);

    void WriteNumberedList(string label, List<string> list, bool isNumbered);
}
