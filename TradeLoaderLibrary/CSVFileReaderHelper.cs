namespace TradeLoaderLibrary;

public static class CSVFileReaderHelper
{
    public static async IAsyncEnumerable<T> SkipAsync<T>(this IAsyncEnumerable<T> source, int count)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        for (int i = 0; i < count; i++)
        {
            if (!await enumerator.MoveNextAsync())
            {
                yield break;
            }
        }
        while (await enumerator.MoveNextAsync())
        {
            yield return enumerator.Current;
        }
    }

}