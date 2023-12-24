namespace TradeLoaderLibrary;

public interface ITradeQueueLoader
{
    public Task LoadAsync();
    public void LoadRecords();
}