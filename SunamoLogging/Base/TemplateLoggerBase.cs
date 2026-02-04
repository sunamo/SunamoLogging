namespace SunamoLogging.Base;

/// <summary>
/// Base logger class for template-based logging with predefined message types.
/// </summary>
public abstract class TemplateLoggerBase(Action<TypeOfMessageLogging, string, string[]> writeLineDelegate)
{
    /// <summary>
    /// Logs a success message indicating data was saved to drive.
    /// </summary>
    /// <param name="path">The path where data was saved.</param>
    public void SavedToDrive(string path)
    {
        WriteLine(TypeOfMessageLogging.Success, Translate.FromKey(XlfKeys.SavedToDrive) + ": " + path);
    }

    /// <summary>
    /// Logs an information message prompting the user to try again after initialization completes.
    /// </summary>
    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    /// <summary>
    /// Logs a success message indicating an operation has finished.
    /// </summary>
    /// <param name="operationName">The name of the completed operation.</param>
    public void Finished(string operationName)
    {
        WriteLine(TypeOfMessageLogging.Success, operationName + " - " + Translate.FromKey(XlfKeys.Finished));
    }

    /// <summary>
    /// Logs a message indicating the application will be terminated.
    /// </summary>
    public void EndRunTime()
    {
        WriteLine(TypeOfMessageLogging.Ordinal, Messages.AppWillBeTerminated);
    }

    #region Success
    /// <summary>
    /// Logs a success message indicating the result was copied to clipboard.
    /// </summary>
    public void ResultCopiedToClipboard()
    {
        WriteLine(TypeOfMessageLogging.Success, "Result was successfully copied to clipboard.");
    }

    /// <summary>
    /// Logs a success message indicating specific content was copied to clipboard.
    /// </summary>
    /// <param name="contentDescription">Description of what was copied.</param>
    public void CopiedToClipboard(string contentDescription)
    {
        WriteLine(TypeOfMessageLogging.Success, contentDescription + " was successfully copied to clipboard.");
    }
    #endregion

    #region Error
    /// <summary>
    /// Logs an error message indicating a value could not be parsed.
    /// </summary>
    /// <param name="entityName">The name of the entity that failed to parse.</param>
    /// <param name="value">The value that could not be parsed.</param>
    public void CouldNotBeParsed(string entityName, string value)
    {
        WriteLine(TypeOfMessageLogging.Error, entityName + " with value " + value + " could not be parsed");
    }

    /// <summary>
    /// Logs an error message indicating some errors occurred and user should check the log.
    /// </summary>
    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageLogging.Error, Translate.FromKey(XlfKeys.SomeErrorsOccuredSeeLog));
    }

    /// <summary>
    /// Logs an error message indicating a folder doesn't exist.
    /// </summary>
    /// <param name="folderPath">The path to the folder that doesn't exist.</param>
    public void FolderDontExists(string folderPath)
    {
        WriteLine(TypeOfMessageLogging.Error, Translate.FromKey(XlfKeys.Folder) + " " + folderPath + " doesn't exists.");
    }

    /// <summary>
    /// Logs an error message indicating a file doesn't exist.
    /// </summary>
    /// <param name="filePath">The path to the file that doesn't exist.</param>
    public void FileDontExists(string filePath)
    {
        WriteLine(TypeOfMessageLogging.Error, Translate.FromKey(XlfKeys.File) + " " + filePath + " doesn't exists.");
    }
    #endregion

    #region Information
    /// <summary>
    /// Logs an information message indicating data was loaded from storage.
    /// </summary>
    /// <param name="description">Description of what was loaded.</param>
    public void LoadedFromStorage(string description)
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.LoadedFromStorage) + ": " + description);
    }

    /// <summary>
    /// Logs an information message instructing the user to insert indexes as zero-based.
    /// </summary>
    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.InsertAsIndexesZeroBased));
    }

    /// <summary>
    /// Logs an information message indicating the format was incorrect and user should try again.
    /// </summary>
    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }

    /// <summary>
    /// Logs an information message indicating the operation was stopped.
    /// </summary>
    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.OperationWasStopped));
    }

    /// <summary>
    /// Logs an information message indicating no data is available.
    /// </summary>
    public void NoData()
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.PleaseEnterRightInputData));
    }

    /// <summary>
    /// Logs an information message indicating successful resize operation.
    /// </summary>
    /// <param name="fileName">The file name that was resized.</param>
    public void SuccessfullyResized(string fileName)
    {
        WriteLine(TypeOfMessageLogging.Information, Translate.FromKey(XlfKeys.SuccessfullyResizedTo) + " " + fileName);
    }

    #endregion

    /// <summary>
    /// Checks if any element in the collection is null or empty.
    /// </summary>
    /// <param name="collectionName">The name of the collection being checked.</param>
    /// <param name="collection">The collection to check.</param>
    /// <returns>True if any element is null or empty, false otherwise.</returns>
    public bool AnyElementIsNullOrEmpty(string collectionName, List<string> collection)
    {
        List<int> nullOrEmptyIndexes = CAIndexesWithNull.IndexesWithNullOrEmpty(collection);
        if (nullOrEmptyIndexes.Count > 0)
        {
            string? message = Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), collectionName, nullOrEmptyIndexes);
            if (message != null)
            {
                WriteLine(TypeOfMessageLogging.Information, message);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Logs an appeal message indicating a control has an unallowed value.
    /// </summary>
    /// <param name="controlName">The name or text of the control.</param>
    public void HaveUnallowedValue(string controlName)
    {
        controlName = controlName.TrimEnd(':');
        WriteLine(TypeOfMessageLogging.Appeal, controlName + " have unallowed value");
    }

    /// <summary>
    /// Logs an appeal message indicating a control must have a value.
    /// </summary>
    /// <param name="controlName">The name or text of the control.</param>
    public void MustHaveValue(string controlName)
    {
        controlName = controlName.TrimEnd(':');
        WriteLine(TypeOfMessageLogging.Appeal, controlName + " must have value");
    }

    static Type type = typeof(TemplateLoggerBase);

    /// <summary>
    /// Checks if the collection has an even number of elements.
    /// </summary>
    /// <param name="collectionName">The name of the collection being checked.</param>
    /// <param name="collection">The collection to check.</param>
    /// <returns>True if the collection has an even number of elements, false if odd.</returns>
    public bool NotEvenNumberOfElements(string collectionName, string[] collection)
    {
        if (collection.Count() % 2 == 1)
        {
            string? message = Exceptions.NotEvenNumberOfElements(FullNameOfExecutedCode(), collectionName);
            if (message != null)
            {
                WriteLine(TypeOfMessageLogging.Error, message);
            }
            return false;
        }
        return true;
    }

    /// <summary>
    /// Gets the full name (type.method) of the currently executing code.
    /// </summary>
    /// <returns>Full name in format "Namespace.Type.Method".</returns>
    private string FullNameOfExecutedCode()
    {
        return ThrowEx.FullNameOfExecutedCode();
    }

    /// <summary>
    /// Writes a log message with the specified message type.
    /// </summary>
    /// <param name="messageType">The type of the message.</param>
    /// <param name="message">The message to write.</param>
    private void WriteLine(TypeOfMessageLogging messageType, string message)
    {
        writeLineDelegate(messageType, message, []);
    }

    /// <summary>
    /// Checks if any element in the collection is null.
    /// </summary>
    /// <param name="collectionName">The name of the collection being checked.</param>
    /// <param name="collection">The collection to check.</param>
    /// <returns>True if any element is null, false otherwise.</returns>
    public bool AnyElementIsNull(string collectionName, string[] collection)
    {
        List<int> nullIndexes = CAIndexesWithNull.IndexesWithNull(collection);
        if (nullIndexes.Count > 0)
        {
            string? message = Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), collectionName, nullIndexes);
            if (message != null)
            {
                WriteLine(TypeOfMessageLogging.Information, message);
            }
            return true;
        }
        return false;
    }
}