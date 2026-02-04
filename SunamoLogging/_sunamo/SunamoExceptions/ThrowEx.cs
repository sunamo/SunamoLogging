namespace SunamoLogging._sunamo.SunamoExceptions;

/// <summary>
/// Exception throwing helper class for common validation and error scenarios.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws a custom exception with the specified message.
    /// </summary>
    /// <param name="message">The primary error message.</param>
    /// <param name="shouldThrow">Whether to actually throw the exception.</param>
    /// <param name="additionalMessage">Optional additional message to append.</param>
    /// <returns>True if an exception would be thrown, false otherwise.</returns>
    internal static bool Custom(string message, bool shouldThrow = true, string additionalMessage = "")
    {
        string combinedMessage = string.Join(" ", message, additionalMessage);
        string? exceptionMessage = Exceptions.Custom(FullNameOfExecutedCode(), combinedMessage);
        return ThrowIsNotNull(exceptionMessage, shouldThrow);
    }

    /// <summary>
    /// Throws a custom exception with the exception's stack trace included in the message.
    /// </summary>
    /// <param name="exception">The exception to extract information from.</param>
    /// <returns>True if an exception was thrown.</returns>
    internal static bool CustomWithStackTrace(Exception exception)
    {
        return Custom(Exceptions.TextOfExceptions(exception));
    }

    /// <summary>
    /// Validates that a string argument is not null or empty, throwing an exception if it is.
    /// </summary>
    /// <param name="argumentName">The name of the argument.</param>
    /// <param name="argumentValue">The value to validate.</param>
    /// <returns>True if validation failed and exception was thrown.</returns>
    internal static bool IsNullOrEmpty(string argumentName, string argumentValue)
    {
        return ThrowIsNotNull(Exceptions.IsNullOrWhitespace(FullNameOfExecutedCode(), argumentName, argumentValue, true));
    }

    /// <summary>
    /// Throws an exception for a not implemented case.
    /// </summary>
    /// <param name="notImplementedValue">The value or type that is not implemented.</param>
    /// <returns>True if an exception was thrown.</returns>
    internal static bool NotImplementedCase(object notImplementedValue)
    {
        return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedValue);
    }

    #region Other
    /// <summary>
    /// Gets the full name (type.method) of the currently executing code.
    /// </summary>
    /// <returns>Full name in format "Namespace.Type.Method".</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Constructs the full name of executed code from type and method name.
    /// </summary>
    /// <param name="type">The type (can be Type, MethodBase, string, or any object).</param>
    /// <param name="methodName">The method name (can be null).</param>
    /// <param name="isFromThrowEx">Whether this is called from ThrowEx (adjusts stack depth).</param>
    /// <returns>Full name in format "Namespace.Type.Method".</returns>
    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int stackDepth = 2;
            if (isFromThrowEx)
            {
                stackDepth++;
            }

            methodName = Exceptions.CallingMethod(stackDepth);
        }

        string typeFullName;
        if (type is Type asType)
        {
            typeFullName = asType.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type actualType = type.GetType();
            typeFullName = actualType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the exception message is not null.
    /// </summary>
    /// <param name="exceptionMessage">The exception message (null means no exception).</param>
    /// <param name="shouldThrow">Whether to actually throw the exception.</param>
    /// <returns>True if an exception would be thrown, false otherwise.</returns>
    internal static bool ThrowIsNotNull(string? exceptionMessage, bool shouldThrow = true)
    {
        if (exceptionMessage != null)
        {
            Debugger.Break();
            if (shouldThrow)
            {
                throw new Exception(exceptionMessage);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode
    /// <summary>
    /// Generic helper to throw exceptions with full name of executed code automatically prepended.
    /// </summary>
    /// <typeparam name="TArgument">The type of the argument to pass to the exception factory function.</typeparam>
    /// <param name="exceptionFactory">Function that creates the exception message.</param>
    /// <param name="argument">The argument to pass to the exception factory.</param>
    /// <returns>True if an exception was thrown.</returns>
    internal static bool ThrowIsNotNull<TArgument>(Func<string, TArgument, string?> exceptionFactory, TArgument argument)
    {
        string? exceptionMessage = exceptionFactory(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exceptionMessage);
    }

    #endregion
    #endregion
}