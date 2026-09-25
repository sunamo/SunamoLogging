namespace SunamoLogging._sunamo;

/// <summary>
/// File system helper class for directory operations.
/// </summary>
internal class FS
{
    /// <summary>
    /// Creates all directories in the specified path if they don't exist.
    /// </summary>
    /// <param name="folderPath">The full path of the directory to create.</param>
    internal static void CreateFoldersPsysicallyUnlessThere(string folderPath)
    {
        ThrowEx.IsNullOrEmpty("folderPath", folderPath);
        Directory.CreateDirectory(folderPath);
    }
}