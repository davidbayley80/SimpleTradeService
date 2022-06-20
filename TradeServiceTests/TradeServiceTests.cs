using TradeService;

namespace TradeServiceTests;

public class TradeService_Test
{

    public TradeQueueLoader? tradeLoader;
    public PortfolioAggregator? portfolioAggregator;

    [SetUp]
    public void Setup()
    {
        tradeLoader = new TradeQueueLoader("TradeDetails.csv");
        portfolioAggregator = new PortfolioAggregator();
    }

    [Test]
    public async Task Test_CSVImport()
    {
        // Checks time-perofrmance and asserts data
        var customer = "Barclays";
        int timeout = 10000;
        var task = tradeLoader.LoadAsync();
        if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
        {
            Assert.That(task.Status == TaskStatus.RanToCompletion);
            Assert.That(tradeLoader.TradeRecords.Count > 0);
            string firstCustomerResult = tradeLoader.TradeRecords.FirstOrDefault()._customer;
            Assert.AreEqual(customer, firstCustomerResult);
        }
        else
        {
            throw new TimeoutException("Time Limit for CSV Import has been exceeded, check input data");
        }
    }


    [Test]
    public async Task Test_PortfolioAggregator()
    { 
        var priceReader = portfolioAggregator.GetTotalPriceAsync(tradeLoader.TradeRecords);
        await Task.WhenAll(tradeLoader.LoadAsync(), priceReader);
        Assert.That(priceReader.Result, Is.EqualTo(8975857));

    }
}
