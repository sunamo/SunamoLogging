namespace SunamoLogging._sunamo.SunamoShared.Helpers.Runtime;

/// <summary>
/// Runtime helper class providing empty dummy methods for use as default delegates.
/// </summary>
internal class RuntimeHelper
{
    /// <summary>
    /// Empty dummy method that accepts a message and arguments but performs no action.
    /// Used as a placeholder delegate.
    /// </summary>
    /// <param name="message">The message (unused).</param>
    /// <param name="args">The arguments (unused).</param>
    internal static void EmptyDummyMethod(string message, params Object[] args)
    {
    }

    /// <summary>
    /// Empty dummy method with no parameters that performs no action.
    /// Used as a placeholder delegate.
    /// </summary>
    internal static void EmptyDummyMethod()
    {
    }

    /// <summary>
    /// Empty dummy method for log messages that performs no action.
    /// Used as a placeholder delegate.
    /// </summary>
    /// <param name="messageType">The message type (unused).</param>
    /// <param name="message">The message (unused).</param>
    /// <param name="args">The arguments (unused).</param>
    internal static void EmptyDummyMethodLogMessageImpl(TypeOfMessageLogging messageType, string message, params Object[] args)
    {
    }

    /// <summary>
    /// Empty dummy method for log messages. Used as a placeholder delegate.
    /// </summary>
    internal static Action<TypeOfMessageLogging, string, Object[]> EmptyDummyMethodLogMessage = EmptyDummyMethodLogMessageImpl;
}