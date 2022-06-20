namespace TradeServiceConsole
{
class Program
    {
        static async Task Main(string[] args)
        {
            var tradeLoader = new TradeService.TradeQueueLoader("TradeDetails.csv");
            var portfolioAggregator = new TradeService.PortfolioAggregator();

            var priceReader = portfolioAggregator.GetTotalPriceAsync(tradeLoader.TradeRecords);

            // kick the load off via async
            await Task.WhenAll(tradeLoader.LoadAsync(), priceReader);

            Console.WriteLine($"Total Valuation:{priceReader.Result}");
            Console.ReadLine();
        }
    }
}