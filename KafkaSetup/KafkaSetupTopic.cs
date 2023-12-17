using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class KafkaCreateTopic
{
    public async Task CreateTopicAsync(string brokerList, string topicName)
    {
        var config = new AdminClientConfig { BootstrapServers = brokerList };

        using (var adminClient = new AdminClientBuilder(config).Build())
        {
            try
            {
                await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                    new TopicSpecification { Name = topicName, NumPartitions = 1, ReplicationFactor = 1 } 
                });

                Console.WriteLine($"Topic {topicName} created successfully.");
            }
            catch (CreateTopicsException e) when (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
            {
                Console.WriteLine($"An error occurred creating topic {topicName}: {e.Results[0].Error.Reason}");
            }
        }
    }
}