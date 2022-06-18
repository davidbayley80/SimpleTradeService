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
        // arrange
        var csvreader = new CSV_ImportClass();

        // act
        var csv = "TradeDetails.csv";
        var seperator = ',';

        var import = csvreader.CSV_Import_Method(csv, seperator);

        // assert
        Assert.That(import, Is.Not.EqualTo(null));
    }
}
