using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Some.Shared.Business.Logic;
using Some.Shared.Business.Logic.UseCases;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Some.Shared.Infra.Consumers.HostedService
{
    public class OrderEventHostedService : BackgroundService
    {
        private readonly string topic;
        private readonly IConsumer<string, string> orderConsumer;
        private readonly ISaveOrder useCase;
        public OrderEventHostedService(IConfiguration config, ISaveOrder useCase)
        {
            var consumerConfig = new ConsumerConfig();

            config.GetSection("Kafka:ConsumerSettings")
                  .Bind(consumerConfig);
            topic = config.GetValue<string>("Kafka:OrderEventTopic");

            consumerConfig.EnableAutoCommit = false;
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

            orderConsumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

            this.useCase = useCase;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => StartConsumerLoop(stoppingToken)).Start();

            return Task.CompletedTask;
        }

        private void StartConsumerLoop(CancellationToken cancellationToken)
        {
            orderConsumer.Subscribe(topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumerResult = orderConsumer.Consume(cancellationToken);
                    var message = consumerResult.Message.Value;
                    var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var order = JsonSerializer.Deserialize<OrderEvent>(message, jsonOptions);


                    Console.WriteLine(message);

                    useCase.Execute(order);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    break;
                }
            }
        }
        public override void Dispose()
        {
            orderConsumer.Close(); // Commit offsets and leave the group cleanly.
            orderConsumer.Dispose();

            base.Dispose();
        }
    }
}
