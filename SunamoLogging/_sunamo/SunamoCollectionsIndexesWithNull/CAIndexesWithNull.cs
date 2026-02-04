namespace SunamoLogging._sunamo.SunamoCollectionsIndexesWithNull;

/// <summary>
/// Collection analysis helper for finding indexes of null or empty elements.
/// </summary>
internal class CAIndexesWithNull
{
    /// <summary>
    /// Returns a list of indexes where elements are null in the specified collection.
    /// </summary>
    /// <param name="collection">The collection to analyze.</param>
    /// <returns>List of indexes where elements are null.</returns>
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

    /// <summary>
    /// Returns a list of indexes where elements are null or empty in the specified collection.
    /// </summary>
    /// <param name="collection">The collection to analyze.</param>
    /// <returns>List of indexes where elements are null or empty.</returns>
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