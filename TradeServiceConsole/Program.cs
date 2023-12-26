using Confluent.Kafka;
using KafkaSetup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using TradeLoaderLibrary;

namespace TradeServiceConsole
{
class Program
    {
        public static IConfiguration? Configuration { get; private set; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
       //  public static ICSVFileReader CsvFileReader = new CsvFileReader();

        static async Task Main(string[] args)
        {
            Logger.Info("Application started.");
            var configuration = BuildConfiguration();
            
            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            Logger.Info("Config and Dependancies have setup");

            await InitializeKafkaAsync(configuration);
            await PerformBusinessOperations(serviceProvider);
            Logger.Info("Kafka has been initialied");
            
            Logger.Info("Application has started");
            Console.ReadLine();

           // Ensure to flush and stop internal timers/threads before application exit (Avoid segmentation fault on Linux)
           LogManager.Shutdown(); 

        }
        private static async Task InitializeKafkaAsync(IConfiguration configuration)
        {
            // Broker: localhost:9092
            // Topic Name: Trade_In_Topic
            
            Logger.Info("Checking Kafka broker setup...");

            string brokerList = configuration["Kafka:BrokerList"];
            var topicList = configuration.GetSection("Kafka:Topics").GetChildren();

            var kafkaHelperClient = new KafkaHelper();
            var existingTopics = await kafkaHelperClient.ListTopicsAsync(brokerList);

            foreach (var topic in topicList)
            {
                    if (!existingTopics.Contains(topic.Value))
                    {
                        // Topic does not exist, create it
                        var kafkaAdminClient = new KafkaCreateTopic();
                        await kafkaAdminClient.CreateTopicAsync(brokerList, topic.Value);
                        Console.WriteLine($"Topic '{topic.Value}' created successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Topic '{topic.Value}' already exists.");
                    }
            }
        }

        private static async Task PerformBusinessOperations(IServiceProvider serviceProvider)
        { 
            // var csvFileLoader = new CsvFileReader(csvFilePath);
            // var tradeLoader = new TradeQueueLoader(csvFileLoader);
            // var portfolioAggregator = new PortfolioAggregator();
            //  var priceReader = portfolioAggregator.GetTotalPriceAsync(tradeLoader.TradeRecords);
            
            // kick the load off via async
            // await Task.WhenAll(tradeLoader.LoadAsync(), priceReader);

            // Console.WriteLine($"Total Valuation:{priceReader.Result}");
            
            // Run your application
            // var tradeQueueLoader = serviceProvider.GetService<ITradeQueueLoader>();
            // // var csvFileReader = serviceProvider.GetService<ICSVFileReader>();
            // var myApp = serviceProvider.GetService<MyApplication>();
            // myApp.Run();
            
            
        }
        
        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var csvFilePath = configuration["CsvFileReaderOptions:FilePath"];
            // Register CsvFileReader with the file path from configuration
            services.AddTransient<ICSVFileReader>(_ => new CsvFileReader(csvFilePath));
            
            // Register your services here. For example:
             services.AddTransient<ITradeQueueLoader,TradeQueueLoader>();
            services.AddTransient<MyApplication>();
            // ... other services and configurations
        }
    }
}
