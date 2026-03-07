namespace SunamoLogging.LogRouter;

using System.Text.Json;

public class LogCategorySettingsBase<TCategory>
    where TCategory : struct, Enum
{
    private readonly string _settingsFilePath;
    protected readonly (TCategory Category, string Description)[] _allCategories;
    private Dictionary<string, bool> _enabled = [];

    public LogCategorySettingsBase(string settingsFilePath, (TCategory Category, string Description)[] allCategories)
    {
        _settingsFilePath = settingsFilePath;
        _allCategories = allCategories;
        Load();
    }

    private void Load()
    {
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = File.ReadAllText(_settingsFilePath, System.Text.Encoding.UTF8);
                var loaded = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);
                _enabled = loaded ?? [];
            }
            else
            {
                _enabled = [];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LogCategorySettings] Failed to load settings: {ex.Message}. Using defaults (all disabled).");
            _enabled = [];
        }
    }

    public void Save()
    {
        try
        {
            var dir = Path.GetDirectoryName(_settingsFilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(_enabled, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsFilePath, json, System.Text.Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LogCategorySettings] Failed to save settings: {ex.Message}");
        }
    }

    public bool IsEnabled(TCategory cat) =>
        _enabled.TryGetValue(cat.ToString(), out var val) && val;

    public void Toggle(TCategory cat)
    {
        var key = cat.ToString();
        _enabled[key] = !(_enabled.TryGetValue(key, out var val) && val);
    }

    public void PrintStartupBanner()
    {
        var enabledNames = _allCategories
            .Where(c => IsEnabled(c.Category))
            .Select(c => c.Category.ToString())
            .ToList();
        var disabledNames = _allCategories
            .Where(c => !IsEnabled(c.Category))
            .Select(c => c.Category.ToString())
            .ToList();

        Console.WriteLine("--- Logování kategorií ---");
        Console.WriteLine($"Zapnuté:  {(enabledNames.Count > 0 ? string.Join(", ", enabledNames) : "(žádné)")}");
        Console.WriteLine($"Vypnuté:  {(disabledNames.Count > 0 ? string.Join(", ", disabledNames) : "(žádné)")}");
        Console.WriteLine("--------------------------");
    }

    public void OpenConsoleUI()
    {
        var snapshot = _allCategories
            .ToDictionary(c => c.Category, c => IsEnabled(c.Category));

        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== NASTAVENÍ LOGOVÁNÍ ===");
            Console.WriteLine("[↑↓] Navigace   [Mezerník] Přepnout   [Enter] Uložit   [Esc] Zrušit");
            Console.WriteLine();

            for (int i = 0; i < _allCategories.Length; i++)
            {
                var (cat, desc) = _allCategories[i];
                var check = snapshot[cat] ? "x" : " ";
                var prefix = i == selectedIndex ? "> " : "  ";
                Console.WriteLine($"{prefix}[{check}] {cat,-22} - {desc}");
            }

            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.UpArrow)
            {
                if (selectedIndex > 0) selectedIndex--;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (selectedIndex < _allCategories.Length - 1) selectedIndex++;
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                var cat = _allCategories[selectedIndex].Category;
                snapshot[cat] = !snapshot[cat];
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                foreach (var (cat, _) in _allCategories)
                    _enabled[cat.ToString()] = snapshot[cat];
                Save();
                Console.Clear();
                Console.WriteLine("Nastavení logování uloženo.");
                PrintStartupBanner();
                break;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Nastavení logování zrušeno (beze změn).");
                break;
            }
        }
    }
}
