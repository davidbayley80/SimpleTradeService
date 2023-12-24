using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using TradeService;
using TradeLoaderLibrary;


namespace TradeServiceConsole
{
class Program
    {
        public static IConfiguration? Configuration { get; private set; }

        static async Task Main(string[] args)
        {
            
            // /Users/davidbayley/RiderProjects/SimpleTradeService/TradeServiceConsole/bin/Debug/net7.0/TradeServiceConsole
            var currentDir = Directory.GetCurrentDirectory();
            var getJsonAppSettings = File.Exists(currentDir + "/AppSettings.json");
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            var testSetting = configuration["CsvFileReaderOptions:FilePath"];
            
            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build the ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            

            // Run your application
            var tradeQueueLoader = serviceProvider.GetService<ITradeQueueLoader>();
            var myApp = serviceProvider.GetService<MyApplication>();
            
            
            myApp.Run();
            
           // var kafkaAdminClient = new KafkaCreateTopic();
           // await kafkaAdminClient.CreateTopicAsync("localhost:9092", "Trade_In_Topic");
            
           // var kafkaCheckClient = new KafkaCheckTopic();
           // await kafkaCheckClient.ListTopicsAsync("localhost:9092");
            
           // var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
           // using var producer = new ProducerBuilder<Null, string>(config).Build();

           // await producer.ProduceAsync("Trade_In_Topic", new Message<Null, string> { Value = "THIS IS A TEST" });
           // producer.Flush(TimeSpan.FromSeconds(10));
           
           // var tradeLoader = new TradeQueueLoader();
           // var portfolioAggregator = new PortfolioAggregator();

           // var priceReader = portfolioAggregator.GetTotalPriceAsync(tradeLoader.TradeRecords);
            
            // kick the load off via async
           // await Task.WhenAll(tradeLoader.LoadAsync(), priceReader);

          //  Console.WriteLine($"Total Valuation:{priceReader.Result}");
           // Console.ReadLine();
        }
        
        private static void ConfigureServices(IServiceCollection services)
        {
            
            // Registering the configuration instance
            // services.Configure<CsvFileReaderOptions>(Configuration.GetSection("CsvFileReaderOptions"));

            // Register your services here. For example:
            services.AddTransient<ICSVFileReader, CsvFileReader>();  // Assuming CSVFileReader implements ICSVFileReader
             services.AddTransient<ITradeQueueLoader,TradeQueueLoader>();
             services.AddTransient<MyApplication>();
            // ... other services and configurations
        }
    }
}
