// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging._sunamo.SunamoCollectionsIndexesWithNull;

internal class CAIndexesWithNull
{
    internal static List<int> IndexesWithNull(IList times)
    {
        List<int> nulled = [];
        int i = 0;
        foreach (var item in times)
        {
            if (item == null)
            {
                nulled.Add(i);
            }
            i++;
        }

        return nulled;
    }
    internal static List<int> IndexesWithNullOrEmpty(IList times)
    {
        List<int> nulled = [];
        int i = 0;
        foreach (var item in times)
        {
            if (item == null)
            {
                nulled.Add(i);
            }
            else if (item.ToString() == string.Empty)
            {
                nulled.Add(i);
            }
            i++;
        }

        return nulled;
    }
}
