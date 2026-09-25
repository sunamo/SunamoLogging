namespace SunamoLogging._sunamo.SunamoCollectionsIndexesWithNull;

internal class CAIndexesWithNull
{
    internal static List<int> IndexesWithNull(IList collection)
    {
        List<int> nullIndexes = [];
        int index = 0;
        foreach (var item in collection)
        {
            if (item == null)
            {
                nullIndexes.Add(index);
            }
            index++;
        }

        return nullIndexes;
    }

    internal static List<int> IndexesWithNullOrEmpty(IList collection)
    {
        List<int> nullOrEmptyIndexes = [];
        int index = 0;
        foreach (var item in collection)
        {
            if (item == null)
            {
                nullOrEmptyIndexes.Add(index);
            }
            else if (item.ToString() == string.Empty)
            {
                nullOrEmptyIndexes.Add(index);
            }
            index++;
        }

        return nullOrEmptyIndexes;
    }
}
