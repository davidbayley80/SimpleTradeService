using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class KafkaCheckTopic
{
    public async Task ListTopicsAsync(string brokerList)
    {
        var config = new AdminClientConfig { BootstrapServers = brokerList };

        using (var adminClient = new AdminClientBuilder(config).Build())
        {
            try
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                foreach (var topic in metadata.Topics)
                {
                    Console.WriteLine(topic.Topic);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}