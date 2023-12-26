using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaSetup;
    
public class KafkaHelper
{   
    public async Task<List<string>> ListTopicsAsync(string brokerList)
    {
        var config = new AdminClientConfig { BootstrapServers = brokerList };
        using var adminClient = new AdminClientBuilder(config).Build();

        try
        {
            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
            var topics = new List<string>();

            foreach (var topic in metadata.Topics) topics.Add(topic.Topic);

            return topics;
        }
        catch (Exception ex)
        {
            // Handle or log the exception as appropriate
            throw new InvalidOperationException("Error in fetching Kafka topics.", ex);
        }
    }
}