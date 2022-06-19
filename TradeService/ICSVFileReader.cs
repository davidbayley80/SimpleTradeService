namespace TradeService
{
    public interface ICSVFileReader
    {
        IEnumerable<TradeAttributes> Parse();
    }
}