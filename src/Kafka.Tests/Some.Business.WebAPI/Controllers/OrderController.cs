using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Some.Shared.Business.Logic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Some.Business.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly string topic;
        private readonly KafkaDependentProducer<string, string> producer;

        public OrderController(IConfiguration config, KafkaDependentProducer<string, string> producer)
        {
            topic = config["Kafka:OrderEventTopic"];
            this.producer = producer;
        }

        [HttpPost]
        public async Task Post(OrderEvent e)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await producer.ProduceAsync(topic, new Message<string, string> { Key = "controller", Value = JsonSerializer.Serialize(e, jsonOptions) });
        }
    }
}
