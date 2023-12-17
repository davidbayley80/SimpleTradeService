using System.Collections.Concurrent;
using TradeLoaderLibrary;

namespace TradeService
{
    public class PortfolioAggregator
    {

        public PortfolioAggregator() { }

        // Link method for 
        public double GetTotalPrice(IEnumerable<TradeAttributes> tradeItems) => tradeItems.Sum(t => t._price);

        // Async method for Trade Aggregation
        public async Task<double> GetTotalPriceAsync(BlockingCollection<TradeAttributes> tradeItems)
        {

            return await Task.Run(() =>
            {
                double totalPrice = 0;
                foreach (var tradeRecord in tradeItems.GetConsumingEnumerable())
                {
                    totalPrice += tradeRecord._price;
                }

                return Task.FromResult(totalPrice);
            });
        }
    }
}

