namespace TradeLoaderLibrary;

public interface ICSVFileReader
{
    IEnumerable<TradeAttributes> Parse();

    void Run();
}