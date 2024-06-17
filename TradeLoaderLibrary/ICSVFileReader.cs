namespace TradeLoaderLibrary;

public interface ICSVFileReader
{
    IAsyncEnumerable<TradeAttributes> ParseAsync();
    // IEnumerable<TradeAttributes> Parse();

    void Run();
}