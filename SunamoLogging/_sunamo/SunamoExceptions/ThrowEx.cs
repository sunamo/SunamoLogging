namespace SunamoLogging._sunamo.SunamoExceptions;

/// <summary>
/// Throws exceptions whose message starts with the place they were thrown from.
/// </summary>
internal static class ThrowEx
{
    /// <summary>
    /// Throws an exception with the messages of the exception and its inner exceptions.
    /// </summary>
    internal static bool CustomWithStackTrace(Exception exception)
    {
        throw new Exception(FullNameOfExecutedCode(1) + ": " + Exceptions.TextOfExceptions(exception));
    }

    /// <summary>
    /// Throws an exception about a not implemented case.
    /// </summary>
    internal static bool NotImplementedCase(object notImplementedValue)
    {
        throw new Exception(Exceptions.NotImplementedCase(FullNameOfExecutedCode(1), notImplementedValue));
    }

    /// <summary>
    /// Returns type and method name of the method that called the wrapper which called this method.
    /// </summary>
    internal static string FullNameOfExecutedCode() => FullNameOfExecutedCode(2);

    private static string FullNameOfExecutedCode(int skipFrames)
    {
        var method = new StackFrame(skipFrames + 1).GetMethod();
        return method == null ? string.Empty : method.DeclaringType?.FullName + "." + method.Name;
    }
}
