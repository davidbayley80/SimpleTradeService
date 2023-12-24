using TradeLoaderLibrary;

namespace TradeServiceConsole;

public class MyApplication
{
    private readonly ITradeQueueLoader _tradeQueueLoader;
    
    public MyApplication(ITradeQueueLoader tradeQueueLoader)
    {
        _tradeQueueLoader = tradeQueueLoader;
    }

    public void Run()
    {
        Console.WriteLine("Dependacies are injected!");
    }
}
