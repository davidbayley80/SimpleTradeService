using Confluent.Kafka;
using TradeService;
using TradeLoaderLibrary;

namespace TradeServiceConsole
{
class Program
    {
        static async Task Main(string[] args)
        {
           // var kafkaAdminClient = new KafkaCreateTopic();
           // await kafkaAdminClient.CreateTopicAsync("localhost:9092", "Trade_In_Topic");
            
           var kafkaCheckClient = new KafkaCheckTopic();
           await kafkaCheckClient.ListTopicsAsync("localhost:9092");
            
           var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
           using var producer = new ProducerBuilder<Null, string>(config).Build();

           await producer.ProduceAsync("Trade_In_Topic", new Message<Null, string> { Value = "THIS IS A TEST" });
           producer.Flush(TimeSpan.FromSeconds(10));
            
            var tradeLoader = new TradeQueueLoader("TradeDetails.csv");
            var portfolioAggregator = new PortfolioAggregator();

            var priceReader = portfolioAggregator.GetTotalPriceAsync(tradeLoader.TradeRecords);
            
            // kick the load off via async
            await Task.WhenAll(tradeLoader.LoadAsync(), priceReader);

            Console.WriteLine($"Total Valuation:{priceReader.Result}");
            Console.ReadLine();
        }
    }
}