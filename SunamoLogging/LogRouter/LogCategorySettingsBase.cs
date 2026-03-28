namespace SunamoLogging.LogRouter;

using System.Text.Json;

/// <summary>
/// Base class for managing log category settings with persistence to a JSON file.
/// </summary>
/// <typeparam name="TCategory">The enum type representing log categories.</typeparam>
public class LogCategorySettingsBase<TCategory>
    where TCategory : struct, Enum
{
    private readonly string settingsFilePath;

    /// <summary>
    /// All available log categories with their descriptions.
    /// </summary>
    protected readonly (TCategory Category, string Description)[] allCategories;
    private Dictionary<string, bool> enabled = [];

    /// <summary>
    /// Initializes a new instance of the LogCategorySettingsBase class.
    /// </summary>
    /// <param name="settingsFilePath">The file path for persisting settings.</param>
    /// <param name="allCategories">Array of all available categories with descriptions.</param>
    public LogCategorySettingsBase(string settingsFilePath, (TCategory Category, string Description)[] allCategories)
    {
        this.settingsFilePath = settingsFilePath;
        this.allCategories = allCategories;
        Load();
    }

    private void Load()
    {
        try
        {
            if (File.Exists(settingsFilePath))
            {
                var json = File.ReadAllText(settingsFilePath, System.Text.Encoding.UTF8);
                var loaded = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
                enabled = loaded ?? [];
            }
            else
            {
                enabled = [];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LogCategorySettings] Failed to load settings: {ex.Message}. Using defaults (all disabled).");
            enabled = [];
        }
    }

    /// <summary>
    /// Saves the current category settings to the settings file.
    /// </summary>
    public void Save()
    {
        try
        {
            var directory = Path.GetDirectoryName(settingsFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(enabled, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsFilePath, json, System.Text.Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LogCategorySettings] Failed to save settings: {ex.Message}");
        }
    }

    /// <summary>
    /// Determines whether the specified category is enabled.
    /// </summary>
    /// <param name="category">The category to check.</param>
    /// <returns>True if the category is enabled, false otherwise.</returns>
    public bool IsEnabled(TCategory category) =>
        enabled.TryGetValue(category.ToString(), out var isEnabled) && isEnabled;

    /// <summary>
    /// Toggles the enabled state of the specified category.
    /// </summary>
    /// <param name="category">The category to toggle.</param>
    public void Toggle(TCategory category)
    {
        var categoryKey = category.ToString();
        enabled[categoryKey] = !(enabled.TryGetValue(categoryKey, out var isEnabled) && isEnabled);
    }

    /// <summary>
    /// Prints a startup banner showing which categories are enabled and disabled.
    /// </summary>
    public void PrintStartupBanner()
    {
        var enabledNames = allCategories
            .Where(categoryEntry => IsEnabled(categoryEntry.Category))
            .Select(categoryEntry => categoryEntry.Category.ToString())
            .ToList();
        var disabledNames = allCategories
            .Where(categoryEntry => !IsEnabled(categoryEntry.Category))
            .Select(categoryEntry => categoryEntry.Category.ToString())
            .ToList();

        Console.WriteLine("--- Log Categories ---");
        Console.WriteLine($"Enabled:  {(enabledNames.Count > 0 ? string.Join(", ", enabledNames) : "(none)")}");
        Console.WriteLine($"Disabled: {(disabledNames.Count > 0 ? string.Join(", ", disabledNames) : "(none)")}");
        Console.WriteLine("----------------------");
    }

    /// <summary>
    /// Opens an interactive console UI for toggling log categories.
    /// </summary>
    public void OpenConsoleUI()
    {
        var snapshot = allCategories
            .ToDictionary(categoryEntry => categoryEntry.Category, categoryEntry => IsEnabled(categoryEntry.Category));

        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== LOGGING SETTINGS ===");
            Console.WriteLine("[Up/Down] Navigate   [Space] Toggle   [Enter] Save   [Esc] Cancel");
            Console.WriteLine();

            for (int i = 0; i < allCategories.Length; i++)
            {
                var (category, description) = allCategories[i];
                var checkMark = snapshot[category] ? "x" : " ";
                var linePrefix = i == selectedIndex ? "> " : "  ";
                Console.WriteLine($"{linePrefix}[{checkMark}] {category,-22} - {description}");
            }

            var pressedKey = Console.ReadKey(intercept: true);

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                if (selectedIndex > 0) selectedIndex--;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                if (selectedIndex < allCategories.Length - 1) selectedIndex++;
            }
            else if (pressedKey.Key == ConsoleKey.Spacebar)
            {
                var category = allCategories[selectedIndex].Category;
                snapshot[category] = !snapshot[category];
            }
            else if (pressedKey.Key == ConsoleKey.Enter)
            {
                foreach (var (category, _) in allCategories)
                    enabled[category.ToString()] = snapshot[category];
                Save();
                Console.Clear();
                Console.WriteLine("Logging settings saved.");
                PrintStartupBanner();
                break;
            }
            else if (pressedKey.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Logging settings cancelled (no changes).");
                break;
            }
        }
    }
}
