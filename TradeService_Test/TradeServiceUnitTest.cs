using TradeService;

namespace TradeService_Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_CSVImport()
    { 
        var csv = "TradeDetails.csv";
        var csvreader = new CSVFileReader(csv);
        Assert.That(csvreader, Is.Not.EqualTo(null));
    }


    [Test]
    public async Task Test_PortfolioAggregator()
    {

        var tradeLoader = new TradeQueueLoader("TradeDetails.csv");
        var portfolioAggregator = new PortfolioAggregator();

        var priceReader = portfolioAggregator.GetTotalPriceAsync(tradeLoader.TradeRecords);

        await Task.WhenAll(tradeLoader.LoadAsync(), priceReader);
        Assert.That(priceReader.Result, Is.Not.EqualTo(null));
    }
}
