namespace SunamoLogging._sunamo;

/// <summary>
/// String helper class for common string operations.
/// </summary>
internal class SH
{
    /// <summary>
    /// Converts an object to string, returning "(null)" if the object is null.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>String representation with "(null)" prefix if value is null, otherwise the value's string representation with space prefix.</returns>
    internal static string NullToStringOrDefault(object value)
    {
        return value == null ? " " + "(null)" : " " + value;
    }
}