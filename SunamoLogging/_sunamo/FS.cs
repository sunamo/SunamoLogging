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

        if (Directory.Exists(folderPath))
        {
            return;
        }

        List<string> foldersToCreate =
        [
            folderPath
        ];

        string? currentPath = folderPath;
        while (true)
        {
            currentPath = Path.GetDirectoryName(currentPath);

            if (currentPath == null || Directory.Exists(currentPath))
            {
                break;
            }
            foldersToCreate.Add(currentPath);
        }

        foldersToCreate.Reverse();
        foreach (string folder in foldersToCreate)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}