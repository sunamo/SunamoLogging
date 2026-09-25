namespace SunamoLogging._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Base logger interface providing common logging operations.
/// </summary>
public interface ILoggerBase
{
    /// <summary>
    /// Writes output to clipboard or debug depending on build configuration.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="args">Format arguments.</param>
    void ClipboardOrDebug(string text, params string[] args);

    /// <summary>
    /// Checks if the text is in the right format with the provided arguments.
    /// </summary>
    /// <param name="text">The format string to validate.</param>
    /// <param name="args">Format arguments.</param>
    /// <returns>True if the format is valid.</returns>
    bool IsInRightFormat(string text, params string[] args);

    /// <summary>
    /// Writes a two-state (boolean) value with additional data.
    /// </summary>
    /// <param name="state">The state value.</param>
    /// <param name="additionalData">Additional data to append.</param>
    void TwoState(bool state, params string[] additionalData);

    /// <summary>
    /// Writes the count of elements in a collection.
    /// </summary>
    /// <param name="collectionName">The name of the collection.</param>
    /// <param name="list">The collection to count.</param>
    void WriteCount(string collectionName, IList list);

    /// <summary>
    /// Writes a labeled line with a label and value.
    /// </summary>
    /// <param name="label">The label/prefix.</param>
    /// <param name="value">The value to write.</param>
    void WriteLine(string label, object value);

    /// <summary>
    /// Writes a formatted line.
    /// </summary>
    /// <param name="text">The format string.</param>
    /// <param name="args">Format arguments.</param>
    void WriteLine(string text, params string[] args);

    /// <summary>
    /// Writes a formatted line (legacy compatibility method).
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">Format arguments.</param>
    void WriteLineFormat(string format, params string[] args);

    /// <summary>
    /// Writes a line, converting null values to "(null)" string.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="args">Format arguments.</param>
    void WriteLineNull(string text, params string[] args);

    /// <summary>
    /// Writes all elements of a list, each on a separate line.
    /// </summary>
    /// <param name="list">The list to write.</param>
    void WriteList(List<string> list);

    /// <summary>
    /// Writes a labeled list, with all elements on separate lines.
    /// </summary>
    /// <param name="collectionName">The name/label of the collection.</param>
    /// <param name="list">The list to write.</param>
    void WriteList(string collectionName, List<string> list);

    /// <summary>
    /// Writes all list elements in a single row separated by a delimiter.
    /// </summary>
    /// <param name="list">The list to write.</param>
    /// <param name="separator">The separator between elements.</param>
    void WriteListOneRow(List<string> list, string separator);

    /// <summary>
    /// Writes a numbered or unnumbered list.
    /// </summary>
    /// <param name="label">The label/header for the list.</param>
    /// <param name="list">The list to write.</param>
    /// <param name="isNumbered">Whether to number the list items.</param>
    void WriteNumberedList(string label, List<string> list, bool isNumbered);
}
